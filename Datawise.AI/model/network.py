# model.py
import numpy as np
from model.layers.conv_layer import ConvLayer
from model.layers.dense_layer import DenseLayer
from model.layers.lstm_layer import LSTMLayer
from model.layers.max_pooling_layer import MaxPoolingLayer
from model.layers.linear_layer import LinearLayer

class CLDNN:
    def __init__(self, input_channels, input_height, input_width):
        # CNN Layer 1
        self.conv1 = ConvLayer(num_filters=256, filter_size=9, input_channels=input_channels)
        self.pool1 = MaxPoolingLayer(pool_size=3)
        
        # CNN Layer 2
        self.conv2 = ConvLayer(num_filters=256, filter_size=4, input_channels=256)
        
        # Linear Layer
        self.linear = LinearLayer(input_dim=256*input_height*input_width, output_dim=256)
        
        # LSTM Layer
        self.lstm1 = LSTMLayer(input_dim=256, hidden_dim=832)
        self.lstm2 = LSTMLayer(input_dim=832, hidden_dim=832)
        
        # DNN Layer
        self.dnn = DenseLayer(input_dim=832, output_dim=1024)

    def forward(self, input_data):
        # CNN forward pass
        conv1_out = self.conv1.forward(input_data)
        pool1_out = self.pool1.forward(conv1_out)
        conv2_out = self.conv2.forward(pool1_out)
        
        # Flatten the output from CNN for the linear layer
        flattened = conv2_out.reshape(conv2_out.shape[0], -1)
        linear_out = self.linear.forward(flattened)
        
        # LSTM forward pass
        h_prev, c_prev = np.zeros((linear_out.shape[0], 832)), np.zeros((linear_out.shape[0], 832))
        lstm_out, _ = self.lstm1.forward(linear_out, h_prev, c_prev)
        lstm_out, _ = self.lstm2.forward(lstm_out, h_prev, c_prev)
        
        # DNN forward pass
        dnn_out = self.dnn.forward(lstm_out)
        
        return dnn_out