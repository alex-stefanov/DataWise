from layers import Convo_Layer, MaxPool_Layer, Flat_Layer, LSTM_Layer, Dense_Layer

class CNN_LSTM_Model:
    def __init__(self, num_conv_layers, conv_filters, filter_size, stride, padding, num_lstm_units, num_dense_units):
        self.conv_layers = []
        for i in range(num_conv_layers):
            self.conv_layers.append(Convo_Layer(num_filters=conv_filters[i], filter_size=filter_size, stride=stride, padding=padding))
        
        self.pool = MaxPool_Layer(pool_size=2, stride=2)
        self.flatten = Flat_Layer()
        self.lstm = LSTM_Layer(input_size=conv_filters[-1], hidden_size=num_lstm_units)
        self.dense = Dense_Layer(input_size=num_lstm_units, output_size=num_dense_units)

    
    def forward(self, x):
        for i in range(self.conv_layers.count):
            self.conv_layers[i].forward(x)
        x = self.pool.forward(x)
        x = self.flatten.forward(x)
        x = self.lstm.forward(x)
        output = self.dense.forward(x)
        return output
    
    def backward(self, grad_output):
        grad_output = self.dense.backward(grad_output)
        grad_output = self.lstm.backward(grad_output)
        grad_output = self.flatten.backward(grad_output)
        grad_output = self.pool.backward(grad_output)
        grad_output = self.conv.backward(grad_output)