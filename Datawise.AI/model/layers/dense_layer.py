import numpy as np

class DenseLayer:
    def __init__(self, input_size, output_size):
        self.input_size = input_size
        self.output_size = output_size
        
        # Initialize weights and biases
        self.weights = np.random.randn(output_size, input_size)
        self.biases = np.zeros(output_size)

    def forward(self, inputs):
        """
        Perform a forward pass through the dense layer.
        :param inputs: The input data
        :return: The output after applying the weights and biases
        """
        return np.dot(inputs, self.weights.T) + self.biases

    def backward(self, grad_output, learning_rate):
        """
        Perform a backward pass through the dense layer.
        :param grad_output: The gradient of the loss with respect to the output
        :param learning_rate: The learning rate for updating weights and biases
        :return: The gradient with respect to the input
        """
        grad_input = np.dot(grad_output, self.weights)
        
        # Update weights and biases
        self.weights -= learning_rate * np.dot(grad_output.T, grad_input)
        self.biases -= learning_rate * np.sum(grad_output, axis=0)
        
        return grad_input