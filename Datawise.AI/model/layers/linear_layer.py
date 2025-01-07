import numpy as np

class LinearLayer:
    def __init__(self, input_dim, output_dim):
        self.weights = np.random.randn(input_dim, output_dim) * 0.1
        self.bias = np.zeros(output_dim)

    def forward(self, input_data):
        return np.dot(input_data, self.weights) + self.bias