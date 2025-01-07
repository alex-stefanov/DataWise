import numpy as np

def relu_forward(inputs):
    """
    ReLU activation forward pass.
    :param inputs: Input data (typically from a previous layer).
    :return: Output after applying ReLU (non-negative values).
    """
    return np.maximum(0, inputs)

def relu_backward(grad_output, inputs):
    """
    ReLU activation backward pass (computing gradients for backpropagation).
    :param grad_output: Gradients from the output layer.
    :param inputs: Original input data to the ReLU function.
    :return: Gradients of the input data to the ReLU function.
    """
    return grad_output * (inputs > 0)