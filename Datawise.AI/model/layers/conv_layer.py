import numpy as np

class ConvLayer:
    def __init__(self, input_channels, output_channels, filter_size):
        self.input_channels = input_channels
        self.output_channels = output_channels
        self.filter_size = filter_size
        
        # Initialize filters with small random values
        self.filters = np.random.randn(output_channels, input_channels, filter_size, filter_size)
        self.biases = np.zeros(output_channels)
    
    def forward(self, inputs):
        """
        Perform a forward pass through the convolutional layer.
        :param inputs: The input data with shape (batch_size, input_channels, height, width)
        :return: The output after applying the convolution filters
        """
        batch_size, _, height, width = inputs.shape
        output_height = height - self.filter_size + 1
        output_width = width - self.filter_size + 1
        
        output = np.zeros((batch_size, self.output_channels, output_height, output_width))
        
        # Apply convolution for each filter
        for i in range(self.output_channels):
            for b in range(batch_size):
                for h in range(output_height):
                    for w in range(output_width):
                        region = inputs[b, :, h:h+self.filter_size, w:w+self.filter_size]
                        output[b, i, h, w] = np.sum(region * self.filters[i]) + self.biases[i]
        
        return output

    def backward(self, grad_output, learning_rate):
        """
        Perform a backward pass through the convolutional layer.
        :param grad_output: The gradient of the loss with respect to the output
        :param learning_rate: The learning rate for updating the filters and biases
        :return: The gradient of the loss with respect to the input
        """
        batch_size, _, output_height, output_width = grad_output.shape
        
        grad_input = np.zeros_like(self.filters)
        grad_filters = np.zeros_like(self.filters)
        grad_biases = np.zeros_like(self.biases)
        
        for i in range(self.output_channels):
            for b in range(batch_size):
                for h in range(output_height):
                    for w in range(output_width):
                        region = grad_output[b, i, h, w]
                        grad_filters[i] += region
                        grad_biases[i] += grad_output[b, i, h, w]
                        
        # Update filters and biases using gradient descent
        self.filters -= learning_rate * grad_filters
        self.biases -= learning_rate * grad_biases
        
        return grad_input