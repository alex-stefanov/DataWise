import numpy as np

class LSTM:
    def __init__(self, input_size, hidden_size):
        self.input_size = input_size
        self.hidden_size = hidden_size
        self.Wf = np.random.randn(hidden_size, input_size + hidden_size) * 0.1  # Forget gate weights
        self.bf = np.zeros((hidden_size, 1))  # Forget gate bias
        self.Wi = np.random.randn(hidden_size, input_size + hidden_size) * 0.1  # Input gate weights
        self.bi = np.zeros((hidden_size, 1))  # Input gate bias
        self.Wo = np.random.randn(hidden_size, input_size + hidden_size) * 0.1  # Output gate weights
        self.bo = np.zeros((hidden_size, 1))  # Output gate bias
        self.Wc = np.random.randn(hidden_size, input_size + hidden_size) * 0.1  # Cell state weights
        self.bc = np.zeros((hidden_size, 1))  # Cell state bias

    def forward(self, x, h_prev, c_prev):
        self.x = x
        self.h_prev = h_prev
        self.c_prev = c_prev

        # Combine previous hidden state and input
        self.z = np.vstack((h_prev, x))  # Concatenate h_prev and x
        
        # Gates
        self.f = sigmoid(np.dot(self.Wf, self.z) + self.bf)  # Forget gate
        self.i = sigmoid(np.dot(self.Wi, self.z) + self.bi)  # Input gate
        self.o = sigmoid(np.dot(self.Wo, self.z) + self.bo)  # Output gate
        self.c_hat = np.tanh(np.dot(self.Wc, self.z) + self.bc)  # Candidate cell state

        # Cell state
        self.c = self.f * c_prev + self.i * self.c_hat  # Current cell state
        self.h = self.o * np.tanh(self.c)  # Hidden state (output)

        return self.h, self.c

    def backward(self, dh, dc, learning_rate):
        # Backpropagate through the output gate
        do = dh * np.tanh(self.c) * sigmoid_derivative(self.o)
        self.Wo -= learning_rate * np.dot(do, self.z.T)
        self.bo -= learning_rate * np.sum(do, axis=1, keepdims=True)

        # Backpropagate through the cell state
        dc_output = dh * self.o * sigmoid_derivative(self.o)  # Derivative of output with respect to cell
        dc += dc_output  # Add previous cell gradient

        dc_cell = dc * self.f * np.tanh(self.c)  # Derivative of cell state
        dWc = np.dot(dc_cell, self.z.T)
        dbc = np.sum(dc_cell, axis=1, keepdims=True)

        # Gradient through the gates
        di = dc_cell * self.c_hat * sigmoid_derivative(self.i)
        df = dc_cell * self.c_prev * sigmoid_derivative(self.f)

        # Update parameters
        self.Wc -= learning_rate * dWc
        self.bc -= learning_rate * dbc
        self.Wi -= learning_rate * np.dot(di, self.z.T)
        self.bi -= learning_rate * np.sum(di, axis=1, keepdims=True)
        self.Wf -= learning_rate * np.dot(df, self.z.T)
        self.bf -= learning_rate * np.sum(df, axis=1, keepdims=True)

        # Gradient with respect to previous hidden state and cell state
        dh_prev = np.dot(self.Wf.T, df) + np.dot(self.Wi.T, di) + np.dot(self.Wo.T, do) + np.dot(self.Wc.T, dc_cell)
        dc_prev = dc * self.f

        return dh_prev, dc_prev