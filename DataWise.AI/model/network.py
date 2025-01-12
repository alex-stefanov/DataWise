from layers.conv_layer import ConvLayer
from layers.max_pooling_layer import MaxPoolLayer
from layers.dense_layer import DenseLayer
from layers.lstm_layer import LSTMLayer
import numpy as np

class CLDNN:
    def __init__(self, input_size):
        # Initialize layers as before
        self.conv1 = ConvLayer(input_size=input_size, filter_size=9, num_filters=9, padding='valid')
        self.maxpool1 = MaxPoolLayer(pool_size=3)
        self.conv2 = ConvLayer(input_size=256, output_size=256, filter_size=(4, 3), padding='valid')
        self.dense1 = DenseLayer(input_size=256, output_size=256)
        self.lstm1 = LSTMLayer(input_size=256, hidden_size=832)
        self.lstm2 = LSTMLayer(input_size=832, hidden_size=832)
        self.dense2 = DenseLayer(input_size=832, output_size=1024)
        self.output_layer = DenseLayer(input_size=1024, output_size=10, activation='softmax')

    def forward(self, x):
        # Convolutional layers and pooling
        x = self.conv1.forward(x)
        x = self.maxpool1.forward(x)
        x = self.conv2.forward(x)
        
        # Flatten and reshape for LSTM
        time_steps = x.shape[1]  # Get time_steps from the feature dimensions
        x = x.reshape(x.shape[0], time_steps, -1)  # Reshape to (batch_size, time_steps, features)
        
        # Dimensionality reduction (Linear Layer)
        x = self.dense1.forward(x)
        
        # Pass through LSTM layers
        x = self.lstm1.forward(x)
        x = self.lstm2.forward(x)
        
        # Fully connected dense layers
        x = self.dense2.forward(x)
        output = self.output_layer.forward(x)
        
        return output

    def backward(self, d_output):
        # Backpropagation through layers (same as before)
        d_dense2 = self.output_layer.backward(d_output)
        d_dense1 = self.dense2.backward(d_dense2)
        d_lstm2 = self.lstm2.backward(d_dense1)
        d_lstm1 = self.lstm1.backward(d_lstm2)
        d_dense1 = self.dense1.backward(d_lstm1)
        d_conv2 = self.conv2.backward(d_dense1)
        d_maxpool1 = self.maxpool1.backward(d_conv2)
        d_conv1 = self.conv1.backward(d_maxpool1)
        
        return d_conv1

    def update_parameters(self, learning_rate):
        # Update parameters of each layer
        self.conv1.update_parameters(learning_rate)
        self.conv2.update_parameters(learning_rate)
        self.dense1.update_parameters(learning_rate)
        self.lstm1.update_parameters(learning_rate)
        self.lstm2.update_parameters(learning_rate)
        self.dense2.update_parameters(learning_rate)
        self.output_layer.update_parameters(learning_rate)

    def save(self, filename):
        """
        Save the model's parameters to a file.
        
        :param filename: The file path to save the model.
        """
        model_params = {
            'conv1': self.conv1.get_params(),
            'pool1': self.maxpool1.get_params(),
            'conv2': self.conv2.get_params(),
            'dense1': self.dense1.get_params(),
            'lstm1': self.lstm1.get_params(),
            'lstm2': self.lstm2.get_params(),
            'dense2': self.dense2.get_params(),
            'output_layer': self.output_layer.get_params(),
        }
        
        # Save the parameters as a dictionary in a .npy file
        np.save(filename, model_params)
        print(f"Model saved to {filename}")

    def load(self, filename):
        """
        Load the model's parameters from a file.
        
        :param filename: The file path from which to load the model.
        """
        model_params = np.load(filename, allow_pickle=True).item()

        # Load the parameters into the layers
        self.conv1.set_params(model_params['conv1'])
        self.pool1.set_params(model_params['pool1'])
        self.conv2.set_params(model_params['conv2'])
        self.dense1.set_params(model_params['dense1'])
        self.lstm1.set_params(model_params['lstm1'])
        self.lstm2.set_params(model_params['lstm2'])
        self.dense2.set_params(model_params['dense2'])
        self.output_layer.set_params(model_params['output_layer'])
        print(f"Model loaded from {filename}")