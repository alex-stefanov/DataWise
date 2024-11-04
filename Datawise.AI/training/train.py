import numpy as np
from model.cnn_lstm_model import CNN_LSTM_Model
from model.utils import compute_loss, compute_loss_grad

model = CNN_LSTM_Model()

epochs = 10
learning_rate = 0.001

def train(model, data, lables, epochs, learning_rate):
    for epoch in range(epochs):
        total_loss = 0
        for x, y in zip(data, lables):
            output = model.forward(x)

            loss = compute_loss(output, y)
            total_loss += loss

            grid_output = compute_loss_grad(output, y)
            model.backward(grid_output)
        print(f"Epoch {epoch + 1}/{epochs}, Loss: {total_loss/len(data)}")

data = [np.random.randn(10, 1) for _ in range(100)]
labels = [np.array([1, 0]) for _ in range(100)]

train(model, data, labels, epochs, learning_rate)