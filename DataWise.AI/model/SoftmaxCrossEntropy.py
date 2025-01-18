import numpy as np

class SoftmaxCrossEntropy:
    def forward(self, logits, labels):
        self.labels = labels
        exps = np.exp(logits - np.max(logits, axis=1, keepdims=True))
        self.probs = exps / np.sum(exps, axis=1, keepdims=True)
        loss = -np.sum(labels * np.log(self.probs + 1e-8)) / logits.shape[0]
        return loss

    def backward(self):
        return (self.probs - self.labels) / self.labels.shape[0]