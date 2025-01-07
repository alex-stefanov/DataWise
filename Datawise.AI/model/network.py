# model.py
import numpy as np
from model.layers.conv_layer import ConvLayer
from model.layers.dense_layer import DenseLayer
from model.layers.lstm_layer import LSTMLayer

class CLDNNModel:
    def __init__(self):
        # Define the layers
        self.conv1 = ConvLayer(input_channels=3, output_channels=256, kernel_size=(9, 9))
        self.conv2 = ConvLayer(input_channels=256, output_channels=256, kernel_size=(4, 3))
        self.lstm = LSTMLayer(input_size=256, hidden_size=832, num_layers=2)
        self.dense1 = DenseLayer(input_size=832, output_size=1024)
        self.dense2 = DenseLayer(input_size=1024, output_size=10)  # Assuming 10 classes

    def forward(self, x):
        x = self.conv1.forward(x)
        x = self.conv2.forward(x)
        x = self.lstm.forward(x)
        x = self.dense1.forward(x)
        x = self.dense2.forward(x)
        return x

    def backward(self, grad_output, learning_rate):
        grad = self.dense2.backward(grad_output, learning_rate)
        grad = self.dense1.backward(grad, learning_rate)
        grad = self.lstm.backward(grad, learning_rate)
        grad = self.conv2.backward(grad, learning_rate)
        grad = self.conv1.backward(grad, learning_rate)
        return grad