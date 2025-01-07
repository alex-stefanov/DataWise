import numpy as np

class MaxPoolingLayer:
    def __init__(self, pool_size, stride):
        self.pool_size = pool_size
        self.stride = stride

    def forward(self, input_data):
        self.input_data = input_data
        self.input_height, self.input_width = input_data.shape

        self.output_height = (self.input_height - self.pool_size) // self.stride + 1
        self.output_width = (self.input_width - self.pool_size) // self.stride + 1

        self.output = np.zeros((self.output_height, self.output_width))

        for i in range(self.output_height):
            for j in range(self.output_width):
                region = self.input_data[i * self.stride:i * self.stride + self.pool_size, 
                                         j * self.stride:j * self.stride + self.pool_size]
                self.output[i, j] = np.max(region)

        return self.output

    def backward(self, output_error, learning_rate):
        dinput = np.zeros_like(self.input_data)

        for i in range(self.output_height):
            for j in range(self.output_width):
                region = self.input_data[i * self.stride:i * self.stride + self.pool_size,
                                         j * self.stride:j * self.stride + self.pool_size]
                max_value = np.max(region)
                max_pos = np.unravel_index(np.argmax(region), region.shape)
                dinput[i * self.stride + max_pos[0], j * self.stride + max_pos[1]] = output_error[i, j]

        return dinput
