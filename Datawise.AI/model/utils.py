import numpy as np

def compute_loss(predictions, targets):
    return np.sum((predictions - targets) ** 2) / 2

def compute_loss_grad(predictions, targets):
    return predictions - targets