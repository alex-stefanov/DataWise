import numpy as np
from model.layers.BaseLayer import BaseLayer

class MaxPooling1D(BaseLayer):
    def __init__(self, pool_size):
        self.pool_size = pool_size

    def forward(self, inputs):
        self.inputs = inputs
        batch_size, seq_len, num_filters = inputs.shape
        output_len = seq_len // self.pool_size
        self.output = np.zeros((batch_size, output_len, num_filters))

        self.max_indices = np.zeros_like(inputs, dtype=bool)

        for i in range(output_len):
            pool_slice = inputs[:, i * self.pool_size:(i + 1) * self.pool_size, :]
            self.output[:, i, :] = np.max(pool_slice, axis=1)
            self.max_indices[:, i * self.pool_size:(i + 1) * self.pool_size, :] = \
                pool_slice == self.output[:, i, :][:, None, :]
        return self.output
    
    def backward(self, d_out):
        # Ensure d_out is a numpy array
        if not isinstance(d_out, np.ndarray):
            print("Expected d_out to be a numpy array, but got:", type(d_out))
            return None  # or raise an error, depending on your needs
        
        batch_size, output_len, num_filters = d_out.shape  # Confirm d_out is now an array
        d_inputs = np.zeros_like(self.inputs)

        # Debugging outputs for gradient shape at each layer
        print(f"Backward pass through {self.__class__.__name__}, d_out shape: {d_out.shape}")
        
        for i in range(output_len):
            start_idx = i * self.pool_size
            end_idx = start_idx + self.pool_size
            pool_region = self.inputs[:, start_idx:end_idx, :]
            max_mask = (pool_region == np.max(pool_region, axis=1, keepdims=True))
            d_inputs[:, start_idx:end_idx, :] += max_mask * d_out[:, i, :][:, np.newaxis, :]

        return d_inputs