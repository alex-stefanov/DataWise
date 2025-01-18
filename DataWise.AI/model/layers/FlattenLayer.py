from model.layers.BaseLayer import BaseLayer

class Flatten:
    def forward(self, inputs):
        self.input_shape = inputs.shape
        batch_size = inputs.shape[0]
        return inputs.reshape(batch_size, -1)

    def backward(self, d_out):
        return d_out.reshape(self.input_shape)