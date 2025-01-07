import numpy as np
import matplotlib.pyplot as plt
from network import CLDNN
from data import generate_dummy_data, generate_labels

def train_model(model, train_data, train_labels, epochs, learning_rate):
    losses = []  # List to store losses for visualization

    for epoch in range(epochs):
        total_loss = 0
        for batch in range(len(train_data)):
            # Forward pass
            output = model.forward(train_data[batch])
            
            # Dummy loss function (mean squared error)
            loss = np.mean((output - train_labels[batch]) ** 2)
            total_loss += loss
            
            # Backpropagation
            grad_output = 2 * (output - train_labels[batch]) / output.shape[0]
            model.backward(grad_output, learning_rate)
        
        avg_loss = total_loss / len(train_data)
        losses.append(avg_loss)  # Store the average loss
        print(f"Epoch {epoch+1}/{epochs}, Loss: {avg_loss}")

    # After training, visualize the training loss
    plt.plot(range(epochs), losses, label='Loss')
    plt.xlabel('Epochs')
    plt.ylabel('Loss')
    plt.title('Training Loss Over Time')
    plt.legend()
    plt.show()

def main():
    model = CLDNN()
    
    # Generate dummy data for training
    train_data = generate_dummy_data(batch_size=5, input_channels=3, height=32, width=32)
    train_labels = generate_labels(batch_size=5, num_classes=10)
    
    # Train the model
    epochs = 10
    learning_rate = 0.001
    train_model(model, train_data, train_labels, epochs, learning_rate)

if __name__ == "__main__":
    main()
