import numpy as np

class Convo_Layer:
    def __init__():
        NotImplemented
    
    def forward(self, input):
        NotImplemented
    
    def backward(self, d_output):
        NotImplemented

class MaxPool_Layer:
    def __init__():
        NotImplemented

    def forward(self, input):
        NotImplemented
    
    def backward(self, d_output):
        NotImplemented

class Flat_Layer:
    def forward(self, input):
        self.input_shape = input.shape
        return input.flatten()
    
    def backward(self, d_output):
        return d_output.reshape(self.input_shape)

class LSTM_Layer:
    def __init__():
        NotImplemented

    def forward(self, input_sequence):
        NotImplemented
    
    def backward(self, d_output):
        NotImplemented

class Dense_Layer:
    def __init__(self, input_size, output_size):
        self.weights = np.random.randn(output_size, input_size) * 0.1
        self.bias = np.zeros((output_size, 1))

    def forward(self, input):
        self.input = input
        return np.dot(self.weights, input) + self.bias
    
    def backward(self, d_output, learning_rate = 0.001):
        d_weights = np.dot(d_output, self.input.T)
        d_bias = d_output
        self.weights -= learning_rate * d_weights
        self.bias -= learning_rate * d_bias
        return np.dot(self.weights.T, d_output)