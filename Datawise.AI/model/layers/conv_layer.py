import numpy as np

class ConvLayer:
    def __init__(self, num_filters, filter_size, stride=1, padding=0):
        self.num_filters = num_filters
        self.filter_size = filter_size
        self.stride = stride
        self.padding = padding
        self.filters = np.random.randn(num_filters, filter_size, filter_size) * 0.1
        self.bias = np.zeros((num_filters, 1))

    def forward(self, input_data):
        # Store input data for use in the backward pass
        self.input_data = input_data
        self.input_height, self.input_width = input_data.shape
        self.output_height = (self.input_height - self.filter_size) // self.stride + 1
        self.output_width = (self.input_width - self.filter_size) // self.stride + 1

        # Initialize the output
        self.output = np.zeros((self.num_filters, self.output_height, self.output_width))

        for f in range(self.num_filters):
            for i in range(self.output_height):
                for j in range(self.output_width):
                    region = self.input_data[i*self.stride:i*self.stride+self.filter_size, 
                                            j*self.stride:j*self.stride+self.filter_size]
                    self.output[f, i, j] = np.sum(region * self.filters[f]) + self.bias[f]
        return self.output

    def backward(self, output_error, learning_rate):
        # Initialize gradients
        dfilters = np.zeros_like(self.filters)
        dbias = np.zeros_like(self.bias)
        dinput = np.zeros_like(self.input_data)

        for f in range(self.num_filters):
            for i in range(self.output_height):
                for j in range(self.output_width):
                    region = self.input_data[i*self.stride:i*self.stride+self.filter_size,
                                            j*self.stride:j*self.stride+self.filter_size]
                    dfilters[f] += output_error[f, i, j] * region
                    dbias[f] += output_error[f, i, j]

                    # Gradient of input (for backpropagation to earlier layers)
                    dinput[i*self.stride:i*self.stride+self.filter_size,
                           j*self.stride:j*self.stride+self.filter_size] += output_error[f, i, j] * self.filters[f]

        # Update weights and biases
        self.filters -= learning_rate * dfilters
        self.bias -= learning_rate * dbias

        return dinput