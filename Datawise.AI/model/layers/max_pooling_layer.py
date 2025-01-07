import numpy as np

class MaxPoolingLayer:
    def __init__(self, pool_size):
        self.pool_size = pool_size

    def forward(self, input_data):
        batch_size, num_filters, height, width = input_data.shape
        output_height = height // self.pool_size
        output_width = width // self.pool_size
        output = np.zeros((batch_size, num_filters, output_height, output_width))

        for i in range(batch_size):
            for j in range(num_filters):
                for h in range(output_height):
                    for w in range(output_width):
                        output[i, j, h, w] = np.max(
                            input_data[i, j, h*self.pool_size:(h+1)*self.pool_size,
                                       w*self.pool_size:(w+1)*self.pool_size])
        return output