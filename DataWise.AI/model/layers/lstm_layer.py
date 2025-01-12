import numpy as np

class LSTMLayer:
    def __init__(self, input_size, hidden_size, seq_len, learning_rate=0.01):
        """
        Initialize the LSTM layer.
        
        :param input_size: Number of input features
        :param hidden_size: Number of hidden units
        :param seq_len: Sequence length
        :param learning_rate: The learning rate for parameter updates
        """
        self.input_size = input_size
        self.hidden_size = hidden_size
        self.seq_len = seq_len
        self.learning_rate = learning_rate
        
        self.h = np.zeros((seq_len, hidden_size))  # hidden state
        self.c = np.zeros((seq_len, hidden_size))  # cell state
        
        # Weight matrices for input-to-hidden, hidden-to-hidden, and biases
        self.W_f = np.random.randn(input_size + hidden_size, hidden_size) * 0.01  # forget gate
        self.b_f = np.zeros((1, hidden_size))
        
        self.W_i = np.random.randn(input_size + hidden_size, hidden_size) * 0.01  # input gate
        self.b_i = np.zeros((1, hidden_size))
        
        self.W_o = np.random.randn(input_size + hidden_size, hidden_size) * 0.01  # output gate
        self.b_o = np.zeros((1, hidden_size))
        
        self.W_g = np.random.randn(input_size + hidden_size, hidden_size) * 0.01  # candidate cell state
        self.b_g = np.zeros((1, hidden_size))

        # Gradients for backpropagation
        self.dh = np.zeros_like(self.h)
        self.dc = np.zeros_like(self.c)

        # Storing intermediate values for backpropagation
        self.f = None
        self.i = None
        self.o = None
        self.g = None

    def sigmoid(self, x):
        return 1 / (1 + np.exp(-x))
    
    def tanh(self, x):
        return np.tanh(x)

    def forward(self, input_data):
        """
        Forward pass through the LSTM layer.
        
        :param input_data: Input sequence of shape (batch_size, input_size, seq_len)
        :return: The output hidden state
        """
        batch_size, input_size, seq_len = input_data.shape
        output = np.zeros((batch_size, self.hidden_size, seq_len))
        
        # Forward pass for each timestep in the sequence
        for t in range(seq_len):
            combined_input = np.concatenate([input_data[:, :, t], self.h[t - 1] if t > 0 else np.zeros_like(self.h[t - 1])], axis=1)

            # Compute gate activations
            self.f = self.sigmoid(np.dot(combined_input, self.W_f) + self.b_f)
            self.i = self.sigmoid(np.dot(combined_input, self.W_i) + self.b_i)
            self.o = self.sigmoid(np.dot(combined_input, self.W_o) + self.b_o)
            self.g = self.tanh(np.dot(combined_input, self.W_g) + self.b_g)
            
            # Update cell state and hidden state
            self.c[t] = self.f * self.c[t - 1] + self.i * self.g
            self.h[t] = self.o * self.tanh(self.c[t])
            
            # Store the output for this timestep
            output[:, :, t] = self.h[t]
        
        return output

    def backward(self, d_output):
        """
        Backward pass through the LSTM layer.
        
        :param d_output: Gradient of the loss with respect to the output, shape (batch_size, hidden_size, seq_len)
        :return: Gradient of the loss with respect to the input, shape (batch_size, input_size, seq_len)
        """
        batch_size, hidden_size, seq_len = d_output.shape
        d_input = np.zeros((batch_size, self.input_size, seq_len))
        
        # Backward pass for each timestep in the sequence (starting from the last timestep)
        for t in reversed(range(seq_len)):
            # Compute gradients for each gate
            d_o = d_output[:, :, t] * self.tanh(self.c[t])
            d_c = d_output[:, :, t] * self.h[t] * (1 - np.tanh(self.c[t]) ** 2) + self.dc
            d_f = d_c * self.c[t - 1] * (self.f * (1 - self.f))
            d_i = d_c * self.g * (self.i * (1 - self.i))
            d_g = d_c * self.i * (1 - self.g ** 2)
            
            # Gradients for the input-to-hidden and hidden-to-hidden weight matrices
            d_combined_input = np.concatenate([d_f, d_i, d_o, d_g], axis=1)
            d_input_data = np.dot(d_combined_input, np.concatenate([self.W_f, self.W_i, self.W_o, self.W_g], axis=1).T)
            
            # Update gradients for hidden and cell states for the next timestep
            self.dc = d_c * self.f
            self.dh[:, :, t] = d_o + d_f + d_i + d_g

        return d_input

    def update_parameters(self):
        """
        Update the parameters (weights and biases) using the gradients from the backward pass.
        This is usually done using gradient descent or another optimization algorithm.
        """
        # Update weights and biases using the gradients from backpropagation
        self.W_f -= self.learning_rate * self.d_weights_f
        self.b_f -= self.learning_rate * self.d_bias_f
        
        self.W_i -= self.learning_rate * self.d_weights_i
        self.b_i -= self.learning_rate * self.d_bias_i
        
        self.W_o -= self.learning_rate * self.d_weights_o
        self.b_o -= self.learning_rate * self.d_bias_o
        
        self.W_g -= self.learning_rate * self.d_weights_g
        self.b_g -= self.learning_rate * self.d_bias_g

    def get_params(self):
        return {
            'weights_i': self.weights_i,
            'weights_f': self.weights_f,
            'weights_o': self.weights_o,
            'weights_g': self.weights_g,
            'bias_i': self.bias_i,
            'bias_f': self.bias_f,
            'bias_o': self.bias_o,
            'bias_g': self.bias_g
        }

    def set_params(self, params):
        self.weights_i = params['weights_i']
        self.weights_f = params['weights_f']
        self.weights_o = params['weights_o']
        self.weights_g = params['weights_g']
        self.bias_i = params['bias_i']
        self.bias_f = params['bias_f']
        self.bias_o = params['bias_o']
        self.bias_g = params['bias_g']