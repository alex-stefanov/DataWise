import numpy as np
from model.cnn_lstm_model import CNN_LSTM_Model
from model.utils import compute_loss

model = CNN_LSTM_Model()

def evaluate(model, data, lables):
    total_loss = 0
    correct_predictions = 0

    for x, y in zip(data, lables):
        output = model.forward(x)

        loss = compute_loss(output, y)
        total_loss += loss

        predicted_class = np.argmax(output)
        true_class = np.argmax(y)
        if predicted_class == true_class:
            correct_predictions += 1
    
    accuracy = correct_predictions / len(data)
    print(f"Test Loss: {total_loss / len(data)}, Accuracy: {accuracy * 100}%")

test_data = [np.random.randn(10, 1) for _ in range(20)]
test_labels = [np.array([1, 0]) for _ in range(20)]

evaluate(model, test_data, test_labels)