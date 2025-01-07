# test.py
import numpy as np
from model import CLDNNModel
from data import generate_dummy_data

def test_model(model, test_data):
    correct = 0
    total = len(test_data)
    for batch in test_data:
        output = model.forward(batch)
        prediction = np.argmax(output)  # Get the index of the highest score
        # Assuming the ground truth is 0 or 1 for simplicity
        if prediction == 1:  # Modify this based on your test labels
            correct += 1
    
    accuracy = (correct / total) * 100
    print(f"Test Accuracy: {accuracy}%")

def main():
    model = CLDNNModel()
    
    # Generate dummy data for testing
    test_data = generate_dummy_data(batch_size=5, input_channels=3, height=32, width=32)
    
    # Test the model
    test_model(model, test_data)

if __name__ == "__main__":
    main()