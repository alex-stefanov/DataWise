import numpy as np

def relu(x):
    return np.maximum(0, x)

def d_relu(x):
    return (x > 0).astype(float)

def softmax(x):
    exp = np.exp(x - np.max(x, axis=1, keepdims=True))
    return exp / np.sum(exp, axis=1, keepdims=True)

def cross_entropy_loss(preds, labels):
    m = labels.shape[0]
    loss = -np.sum(labels * np.log(preds + 1e-8)) / m
    return loss

def leaky_relu(x, alpha=0.01):
    return np.where(x > 0, x, alpha * x)

def d_leaky_relu(x, alpha=0.01):
    return np.where(x > 0, 1, alpha)
