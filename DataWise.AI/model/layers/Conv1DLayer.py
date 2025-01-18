import numpy as np
from model.layers.BaseLayer import BaseLayer

class Conv1D(BaseLayer):
    def __init__(self, input_dim, kernel_size, num_filters):
        self.input_dim = input_dim
        self.kernel_size = kernel_size
        self.num_filters = num_filters

        self.W = np.random.randn(kernel_size, input_dim, num_filters) * 0.01
        self.b = np.zeros(num_filters)

    def forward(self, inputs):
        self.inputs = inputs
        batch_size, seq_len, _ = inputs.shape
        output_len = seq_len - self.kernel_size + 1
        self.output = np.zeros((batch_size, output_len, self.num_filters))

        for i in range(output_len):
            input_slice = inputs[:, i:i + self.kernel_size, :]
            self.output[:, i, :] = np.tensordot(input_slice, self.W, axes=([1, 2], [0, 1])) + self.b
        return self.output

    def backward(self, d_out):
        print(f"Backward pass through {self.__class__.__name__}, d_out type: {type(d_out)}, shape: {getattr(d_out, 'shape', None)}")
        batch_size, seq_len, _ = self.inputs.shape
        d_inputs = np.zeros_like(self.inputs)
        d_W = np.zeros_like(self.W)
        d_b = np.sum(d_out, axis=(0, 1))

        for i in range(seq_len - self.kernel_size + 1):
            input_slice = self.inputs[:, i:i + self.kernel_size, :]
            d_W += np.tensordot(input_slice, d_out[:, i, :], axes=([0], [0]))
            d_inputs[:, i:i + self.kernel_size, :] += np.tensordot(d_out[:, i, :], self.W, axes=([1], [2]))
        return d_inputs, d_W, d_b