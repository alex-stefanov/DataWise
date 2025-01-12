import numpy as np

class ConvLayer:
    def __init__(self, input_size, filter_size, num_filters, stride=1, padding=0):
        """
        Initialize the convolutional layer.
        
        :param input_size: Tuple (in_channels, input_length) for the input shape.
        :param filter_size: Size of the filter (filter_length).
        :param num_filters: Number of filters.
        :param stride: The stride of the convolution.
        :param padding: Padding for the input.
        """
        self.in_channels, self.input_length = input_size
        self.filter_length = filter_size
        self.num_filters = num_filters
        self.stride = stride
        self.padding = padding
        
        # Initialize filters and bias
        self.filters = np.random.randn(num_filters, self.in_channels, self.filter_length) * 0.01
        self.bias = np.zeros(num_filters)
        
        # Output dimensions after convolution
        self.output_length = (self.input_length - self.filter_length + 2 * self.padding) // self.stride + 1

        # For storing intermediate results
        self.input = None
        self.output = None
        self.d_filters = None
        self.d_bias = None

    def forward(self, input_data):
        """
        Perform the forward pass through the convolutional layer.
        
        :param input_data: Input data of shape (batch_size, in_channels, input_length)
        :return: Convolved output
        """
        self.input = input_data
        batch_size = input_data.shape[0]
        
        # Apply padding
        if self.padding > 0:
            input_data = np.pad(input_data, ((0, 0), (0, 0), (self.padding, self.padding)), mode='constant')
        
        # Output of shape (batch_size, num_filters, output_length)
        output = np.zeros((batch_size, self.num_filters, self.output_length))
        
        for i in range(batch_size):
            for j in range(self.num_filters):
                for k in range(self.output_length):
                    start = k * self.stride
                    end = start + self.filter_length
                    output[i, j, k] = np.sum(input_data[i, :, start:end] * self.filters[j]) + self.bias[j]
        
        self.output = output
        return self.output

    def backward(self, d_output):
        """
        Perform the backward pass through the convolutional layer to update weights and compute gradients.
        
        :param d_output: Gradient of the loss with respect to the output, shape (batch_size, num_filters, output_length)
        :return: Gradient of the loss with respect to the input, shape (batch_size, in_channels, input_length)
        """
        batch_size = d_output.shape[0]
        d_input = np.zeros_like(self.input)
        self.d_filters = np.zeros_like(self.filters)
        self.d_bias = np.zeros_like(self.bias)
        
        # Loop over each example in the batch
        for i in range(batch_size):
            for j in range(self.num_filters):
                for k in range(self.output_length):
                    start = k * self.stride
                    end = start + self.filter_length
                    d_input[i, :, start:end] += d_output[i, j, k] * self.filters[j]
                    self.d_filters[j] += d_output[i, j, k] * self.input[i, :, start:end]
                    self.d_bias[j] += d_output[i, j, k]
        
        return d_input
    
    def update_parameters(self, learning_rate):
        """
        Update the weights (filters) and biases using gradient descent.

        :param learning_rate: The learning rate for the gradient descent update.
        """
        self.filters -= learning_rate * self.d_filters
        self.bias -= learning_rate * self.d_bias

    def get_params(self):
        return {'weights': self.weights, 'biases': self.biases}
    
    def set_params(self, params):
        self.weights = params['weights']
        self.biases = params['biases']