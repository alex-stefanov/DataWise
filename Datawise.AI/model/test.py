import numpy as np
from network import CLDNN

# Define a simple dataset (replace with real data later)
X_train = np.random.rand(100, 40)
y_train = np.random.randint(0, 2, 100)

model = CLDNN()

# Training parameters
epochs = 1000
learning_rate = 0.01
batch_size = 10

def mean_squared_error(y_true, y_pred):
    return np.mean((y_true - y_pred) ** 2)

for epoch in range(epochs):
    epoch_loss = 0
    for i in range(0, len(X_train), batch_size):
        X_batch = X_train[i:i+batch_size]
        y_batch = y_train[i:i+batch_size]
        
        outputs = model.forward(X_batch)
        
        loss = mean_squared_error(y_batch, outputs)
        epoch_loss += loss

        model.backward(X_batch, y_batch)

        # Update weights using gradient descent
        model.update_weights(learning_rate)  # Update with your weights update method

    print(f"Epoch {epoch + 1}/{epochs}, Loss: {epoch_loss / len(X_train)}")

# Save the model
model.save('trained_model.npy')