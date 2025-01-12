import numpy as np

class MaxPoolLayer:
    def __init__(self, pool_size, stride):
        """
        Initialize the max pooling layer.
        
        :param pool_size: Size of the pooling window (length).
        :param stride: Stride of the pooling window.
        """
        self.pool_size = pool_size
        self.stride = stride
        self.input = None
        self.output = None
        self.indices = None

    def forward(self, input_data):
        """
        Perform the forward pass of the max pooling layer.
        
        :param input_data: Input data of shape (batch_size, num_filters, input_length)
        :return: Pooled output
        """
        self.input = input_data
        batch_size, num_filters, input_length = input_data.shape
        output_length = (input_length - self.pool_size) // self.stride + 1
        output = np.zeros((batch_size, num_filters, output_length))
        self.indices = np.zeros_like(output, dtype=int)

        for i in range(batch_size):
            for j in range(num_filters):
                for k in range(output_length):
                    start = k * self.stride
                    end = start + self.pool_size
                    pooled_segment = input_data[i, j, start:end]
                    output[i, j, k] = np.max(pooled_segment)
                    self.indices[i, j, k] = np.argmax(pooled_segment)
        
        self.output = output
        return self.output

    def backward(self, d_output):
        """
        Perform the backward pass of the max pooling layer.
        
        :param d_output: Gradient of the loss with respect to the output, shape (batch_size, num_filters, output_length)
        :return: Gradient of the loss with respect to the input, shape (batch_size, num_filters, input_length)
        """
        batch_size, num_filters, output_length = d_output.shape
        d_input = np.zeros_like(self.input)

        for i in range(batch_size):
            for j in range(num_filters):
                for k in range(output_length):
                    start = k * self.stride
                    end = start + self.pool_size
                    idx = self.indices[i, j, k]
                    d_input[i, j, start:end][idx] += d_output[i, j, k]
        
        return d_input
    
    def get_params(self):
        # Return a dictionary containing the pooling configuration
        return {'pool_size': self.pool_size, 'stride': self.stride}

    def set_params(self, params):
        # Set the pooling configuration
        self.pool_size = params['pool_size']
        self.stride = params['stride']