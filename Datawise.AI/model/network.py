import numpy as np
from model.layers.conv_layer import ConvLayer
from model.layers.dense_layer import DNNLayer
from model.layers.lstm_layer import LSTM
from model.layers.max_pooling_layer import MaxPoolingLayer
from model.layers.linear_layer import LinearLayer

class CLDNN:
    def __init__(self):
        self.conv_layer1 = ConvLayer(num_filters=256, filter_size=9)
        self.max_pooling_layer1 = MaxPoolingLayer(pool_size=3, stride=3)  # MaxPooling after the first Conv layer
        self.conv_layer2 = ConvLayer(num_filters=256, filter_size=4)
        self.linear_layer = LinearLayer(input_size=256, output_size=256)  # Linear layer to reduce dimensionality
        self.lstm_layer1 = LSTM(input_size=256, hidden_size=832)
        self.lstm_layer2 = LSTM(input_size=832, hidden_size=832)
        self.dnn_layer = DNNLayer(input_size=832, output_size=1024)

    def forward(self, input_data):
        # Convolutional layers
        conv_out1 = self.conv_layer1.forward(input_data)
        pooled_out = self.max_pooling_layer1.forward(conv_out1)  # Apply MaxPooling after Conv1
        conv_out2 = self.conv_layer2.forward(pooled_out)  # Apply Conv2

        # Linear layer to reduce dimensionality before LSTM
        linear_out = self.linear_layer.forward(conv_out2.flatten())

        # LSTM layers
        h, c = self.lstm_layer1.forward(linear_out, np.zeros((832, 1)), np.zeros((832, 1)))
        h, c = self.lstm_layer2.forward(h, np.zeros((832, 1)), np.zeros((832, 1)))

        # DNN layer
        dnn_out = self.dnn_layer.forward(h)
        return dnn_out

    def backward(self, output_error, learning_rate):
        # Backpropagation for DNN layer
        dnn_error = self.dnn_layer.backward(output_error, learning_rate)

        # Backpropagation for LSTM layers
        lstm_error, _ = self.lstm_layer2.backward(dnn_error, dc=0, learning_rate=learning_rate)
        lstm_error, _ = self.lstm_layer1.backward(lstm_error, dc=0, learning_rate=learning_rate)

        # Backpropagation for Linear layer
        linear_error = self.linear_layer.backward(lstm_error, learning_rate)

        # Backpropagation for Convolutional layers
        conv_error = self.conv_layer2.backward(linear_error, learning_rate)
        conv_error = self.max_pooling_layer1.backward(conv_error, learning_rate)
        self.conv_layer1.backward(conv_error, learning_rate)