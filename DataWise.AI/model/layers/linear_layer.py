import numpy as np

class LinearLayer:
    def __init__(self, input_size, output_size):
        self.weights = np.random.randn(output_size, input_size) * 0.1
        self.bias = np.zeros((output_size, 1))

    def forward(self, input_data):
        self.input_data = input_data
        self.output = np.dot(self.weights, self.input_data) + self.bias
        return self.output

    def backward(self, output_error, learning_rate):
        dweights = np.dot(output_error, self.input_data.T)
        dbias = np.sum(output_error, axis=1, keepdims=True)
        dinput = np.dot(self.weights.T, output_error)

        self.weights -= learning_rate * dweights
        self.bias -= learning_rate * dbias

        return dinput