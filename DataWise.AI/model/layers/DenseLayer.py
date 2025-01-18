import numpy as np
from model.layers.BaseLayer import BaseLayer

class Dense(BaseLayer):
    def __init__(self, input_dim, output_dim):
        self.W = np.random.randn(input_dim, output_dim) * 0.01
        self.b = np.zeros(output_dim)

    def forward(self, inputs):
        assert inputs.shape[1] == self.W.shape[0], \
            f"Input features {inputs.shape[1]} do not match expected input_dim {self.W.shape[0]}"
        self.inputs = inputs
        self.output = np.dot(inputs, self.W) + self.b
        return self.output


    def backward(self, d_out):
        print(f"Backward pass through {self.__class__.__name__}, d_out type: {type(d_out)}, shape: {getattr(d_out, 'shape', None)}")
        self.d_W = np.dot(self.inputs.T, d_out)
        self.d_b = np.sum(d_out, axis=0)
        
        d_inputs = np.dot(d_out, self.W.T)
        return d_inputs