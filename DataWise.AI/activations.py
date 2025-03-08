import numpy as np

def relu(x):
    """
    Applies the ReLU (Rectified Linear Unit) activation function element-wise.

    Args:
        x (np.ndarray): The input array to apply the ReLU activation.

    Returns:
        np.ndarray: The result of applying ReLU element-wise to the input array.
    """
    return np.maximum(0, x)

def d_relu(x):
    """
    Computes the derivative of the ReLU (Rectified Linear Unit) activation function element-wise.

    Args:
        x (np.ndarray): The input array to compute the derivative of ReLU.

    Returns:
        np.ndarray: The result of applying the derivative of ReLU element-wise to the input array.
    """
    return (x > 0).astype(float)

def softmax(x):
    """
    Applies the softmax function to a 2D array (e.g., for classification tasks).

    Args:
        x (np.ndarray): The input array to apply the softmax function. Typically, this is the logits.

    Returns:
        np.ndarray: The result of applying softmax along the rows of the input array.
    """
    exp = np.exp(x - np.max(x, axis=1, keepdims=True))
    return exp / np.sum(exp, axis=1, keepdims=True)

def cross_entropy_loss(preds, labels):
    """
    Computes the cross-entropy loss between predicted probabilities and true labels.

    Args:
        preds (np.ndarray): The predicted probabilities (after softmax).
        labels (np.ndarray): The one-hot encoded true labels.

    Returns:
        float: The cross-entropy loss value.
    """
    m = labels.shape[0]
    loss = -np.sum(labels * np.log(preds + 1e-8)) / m
    return loss

def leaky_relu(x, alpha=0.01):
    """
    Applies the Leaky ReLU activation function element-wise.

    Args:
        x (np.ndarray): The input array to apply the Leaky ReLU activation.
        alpha (float, optional): The negative slope of the activation for x < 0. Defaults to 0.01.

    Returns:
        np.ndarray: The result of applying Leaky ReLU element-wise to the input array.
    """
    return np.where(x > 0, x, alpha * x)

def d_leaky_relu(x, alpha=0.01):
    """
    Computes the derivative of the Leaky ReLU activation function element-wise.

    Args:
        x (np.ndarray): The input array to compute the derivative of Leaky ReLU.
        alpha (float, optional): The negative slope of the derivative for x < 0. Defaults to 0.01.

    Returns:
        np.ndarray: The result of applying the derivative of Leaky ReLU element-wise to the input array.
    """
    return np.where(x > 0, 1, alpha)
