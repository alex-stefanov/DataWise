class BaseLayer:
    def forward(self, inputs):
        raise NotImplementedError

    def backward(self, inputs):
        raise NotImplementedError 