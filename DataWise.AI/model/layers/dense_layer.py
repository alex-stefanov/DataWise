import numpy as np

class DenseLayer:
    def __init__(self, input_size, output_size, activation='relu'):
        """
        Initialize the Dense Layer.
        
        :param input_size: Number of input features
        :param output_size: Number of output units
        :param activation: Activation function to use ('relu', 'sigmoid', 'tanh', etc.)
        """
        self.input_size = input_size
        self.output_size = output_size
        self.activation = activation
        
        # Weight matrix and bias for the dense layer
        self.weights = np.random.randn(input_size, output_size) * 0.01
        self.bias = np.zeros((1, output_size))
        
        self.input_data = None
        self.output_data = None
        self.d_weights = None
        self.d_bias = None
        self.d_input = None

    def forward(self, input_data):
        """
        Forward pass through the dense layer.
        
        :param input_data: Input data of shape (batch_size, input_size)
        :return: Output of the layer after applying activation
        """
        self.input_data = input_data
        self.output_data = np.dot(input_data, self.weights) + self.bias
        
        # Apply activation function
        if self.activation == 'relu':
            self.output_data = np.maximum(0, self.output_data)
        elif self.activation == 'sigmoid':
            self.output_data = 1 / (1 + np.exp(-self.output_data))
        elif self.activation == 'tanh':
           self.output_data = np.tanh(self.output_data)
        elif self.activation == 'softmax':
        # Apply softmax activation
            exp_values = np.exp(self.output_data - np.max(self.output_data, axis=-1, keepdims=True))  # For numerical stability
            self.output_data = exp_values / np.sum(exp_values, axis=-1, keepdims=True)
        
        return self.output_data

    def backward(self, d_output):
        """
        Backward pass through the dense layer.
        
        :param d_output: Gradient of the loss with respect to the output, shape (batch_size, output_size)
        :return: Gradient of the loss with respect to the input, shape (batch_size, input_size)
        """
        # Derivative of the activation function
        if self.activation == 'relu':
            d_activation = np.where(self.output_data > 0, 1, 0)
        elif self.activation == 'sigmoid':
            d_activation = self.output_data * (1 - self.output_data)
        elif self.activation == 'tanh':
            d_activation = 1 - self.output_data ** 2
        else:
            d_activation = np.ones_like(self.output_data)
        
        # Gradient of the loss w.r.t. the input data
        d_activation *= d_output
        self.d_weights = np.dot(self.input_data.T, d_activation)
        self.d_bias = np.sum(d_activation, axis=0, keepdims=True)
        self.d_input = np.dot(d_activation, self.weights.T)
        
        return self.d_input
    
    def update_parameters(self, learning_rate):
        """
        Update the weights and biases using gradient descent.

        :param learning_rate: The learning rate for the gradient descent update.
        """
        self.weights -= learning_rate * self.d_weights
        self.bias -= learning_rate * self.d_bias

    def get_params(self):
        return {'weights': self.weights, 'biases': self.biases}

    def set_params(self, params):
        self.weights = params['weights']
        self.biases = params['biases']