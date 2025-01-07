import numpy as np

class LSTMLayer:
    def __init__(self, input_size, hidden_size):
        self.input_size = input_size
        self.hidden_size = hidden_size
        
        # Initialize weights and biases for LSTM gates
        self.Wf = np.random.randn(hidden_size, input_size + hidden_size)  # Forget gate
        self.Wi = np.random.randn(hidden_size, input_size + hidden_size)  # Input gate
        self.Wo = np.random.randn(hidden_size, input_size + hidden_size)  # Output gate
        self.Wc = np.random.randn(hidden_size, input_size + hidden_size)  # Cell state
        
        self.bf = np.zeros(hidden_size)
        self.bi = np.zeros(hidden_size)
        self.bo = np.zeros(hidden_size)
        self.bc = np.zeros(hidden_size)

    def forward(self, inputs, prev_hidden, prev_cell):
        """
        Perform a forward pass through the LSTM.
        :param inputs: The input data for the current time step
        :param prev_hidden: The previous hidden state
        :param prev_cell: The previous cell state
        :return: The current hidden state and cell state
        """
        combined_input = np.concatenate((inputs, prev_hidden), axis=1)

        # Forget gate
        f = sigmoid(np.dot(self.Wf, combined_input.T) + self.bf)

        # Input gate
        i = sigmoid(np.dot(self.Wi, combined_input.T) + self.bi)
        c = np.tanh(np.dot(self.Wc, combined_input.T) + self.bc)

        # Cell state
        cell = f * prev_cell + i * c

        # Output gate
        o = sigmoid(np.dot(self.Wo, combined_input.T) + self.bo)
        hidden = o * np.tanh(cell)

        return hidden, cell

def sigmoid(x):
    return 1 / (1 + np.exp(-x))