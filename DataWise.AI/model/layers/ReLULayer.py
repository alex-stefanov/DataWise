import numpy as np
from model.layers.BaseLayer import BaseLayer

class ReLU(BaseLayer):
    def forward(self, inputs):
        self.inputs = inputs
        self.output = np.maximum(0, inputs)
        return self.output

    def backward(self, d_out):
        print(f"Backward pass through {self.__class__.__name__}, d_out type: {type(d_out)}, shape: {getattr(d_out, 'shape', None)}")
        d_inputs = d_out * (self.inputs > 0)
        return d_inputs