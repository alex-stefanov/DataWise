import numpy as np

def sigmoid(x):
    return 1 / (1 + np.exp(-x))

def sigmoid_derivative(x):
    return sigmoid(x) * (1 - sigmoid(x))

class LSTM:
    def __init__(self, input_size, hidden_size):
        self.input_size = input_size
        self.hidden_size = hidden_size
        self.Wf = np.random.randn(hidden_size, input_size + hidden_size) * 0.1
        self.bf = np.zeros((hidden_size, 1))
        self.Wi = np.random.randn(hidden_size, input_size + hidden_size) * 0.1
        self.bi = np.zeros((hidden_size, 1))
        self.Wo = np.random.randn(hidden_size, input_size + hidden_size) * 0.1
        self.bo = np.zeros((hidden_size, 1))
        self.Wc = np.random.randn(hidden_size, input_size + hidden_size) * 0.1
        self.bc = np.zeros((hidden_size, 1))

    def forward(self, x, h_prev, c_prev):
        self.x = x
        self.h_prev = h_prev
        self.c_prev = c_prev

        self.z = np.vstack((h_prev, x))

        self.f = sigmoid(np.dot(self.Wf, self.z) + self.bf)
        self.i = sigmoid(np.dot(self.Wi, self.z) + self.bi)
        self.o = sigmoid(np.dot(self.Wo, self.z) + self.bo)
        self.c_hat = np.tanh(np.dot(self.Wc, self.z) + self.bc)

        self.c = self.f * c_prev + self.i * self.c_hat
        self.h = self.o * np.tanh(self.c)

        return self.h, self.c

    def backward(self, dh, dc, learning_rate):
        do = dh * np.tanh(self.c) * sigmoid_derivative(self.o)
        self.Wo -= learning_rate * np.dot(do, self.z.T)
        self.bo -= learning_rate * np.sum(do, axis=1, keepdims=True)

        dc_output = dh * self.o * sigmoid_derivative(self.o)
        dc += dc_output

        dc_cell = dc * self.f * np.tanh(self.c)
        dWc = np.dot(dc_cell, self.z.T)
        dbc = np.sum(dc_cell, axis=1, keepdims=True)

        di = dc_cell * self.c_hat * sigmoid_derivative(self.i)
        df = dc_cell * self.c_prev * sigmoid_derivative(self.f)

        self.Wc -= learning_rate * dWc
        self.bc -= learning_rate * dbc
        self.Wi -= learning_rate * np.dot(di, self.z.T)
        self.bi -= learning_rate * np.sum(di, axis=1, keepdims=True)
        self.Wf -= learning_rate * np.dot(df, self.z.T)
        self.bf -= learning_rate * np.sum(df, axis=1, keepdims=True)

        dh_prev = np.dot(self.Wf.T, df) + np.dot(self.Wi.T, di) + np.dot(self.Wo.T, do) + np.dot(self.Wc.T, dc_cell)
        dc_prev = dc * self.f

        return dh_prev, dc_prev