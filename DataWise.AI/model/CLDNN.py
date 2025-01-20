from model.layers.Conv1DLayer import Conv1D
from model.layers.ReLULayer import ReLU
from model.layers.MaxPooling1DLayer import MaxPooling1D
from model.layers.DenseLayer import Dense
from model.layers.FlattenLayer import Flatten
from model.SoftmaxCrossEntropy import SoftmaxCrossEntropy
import pickle

class CLDNN:
    def __init__(self, input_shape, num_classes):
        final_height = ((input_shape[0] - 2) // 2 - 2) // 2
        assert final_height > 0, "Final height after convolutions and pooling is non-positive. Check input shape and layer configurations."
        self.layers = [
            Conv1D(input_dim=input_shape[-1], kernel_size=3, num_filters=128),
            ReLU(),
            MaxPooling1D(pool_size=2),
            Conv1D(input_dim=128, kernel_size=3, num_filters=256),
            ReLU(),
            MaxPooling1D(pool_size=2),
            Flatten(),
            Dense(input_dim=256 * final_height, output_dim=128),
            ReLU(),
            Dense(input_dim=128, output_dim=num_classes)
        ]
        self.loss_layer = SoftmaxCrossEntropy()

    def __getstate__(self):
        state = self.__dict__.copy()
        return state

    def __setstate__(self, state):
        self.__dict__.update(state)

    def forward(self, X, y=None):
        for layer in self.layers:
            print(f"Layer: {layer.__class__.__name__}, Input shape: {X.shape}")
            X = layer.forward(X)
            print(f"Layer: {layer.__class__.__name__}, Output shape: {X.shape}")
        loss = self.loss_layer.forward(X, y)
        return loss

    def backward(self):
        d_out = self.loss_layer.backward()
        for layer in reversed(self.layers):
            if isinstance(layer, Conv1D):
                d_out, d_W, d_b = layer.backward(d_out)
                print(f"Layer: {layer.__class__.__name__}, Gradient shape (d_out): {d_out.shape}")
            else:
                d_out = layer.backward(d_out)
                print(f"Layer: {layer.__class__.__name__}, Gradient shape: {d_out.shape}")

    def update_weights(self, learning_rate):
        for layer in self.layers:
            if hasattr(layer, 'W') and hasattr(layer, 'd_W'):
                layer.W -= learning_rate * layer.d_W
            if hasattr(layer, 'b') and hasattr(layer, 'd_b'):
                layer.b -= learning_rate * layer.d_b

    def train(self, X_train, y_train, learning_rate):
        loss = self.forward(X_train, y_train)
        self.backward()
        self.update_weights(learning_rate)
        print(f"Loss: {loss}")

    def save(self, file_path):
        """Save the model to a file using pickle."""
        try:
            with open(file_path, "wb") as f:
                pickle.dump(self, f)
            print(f"Model saved successfully to {file_path}")
        except Exception as e:
            print(f"Error saving model: {e}")

    def load(self, file_path):
        """Load the model from a file using pickle."""
        try:
            with open(file_path, "rb") as f:
                model = pickle.load(f)
                print(model)
            print(f"Model loaded successfully from {file_path}")
            self = model
        except Exception as e:
            print(f"Error loading model: {e}")