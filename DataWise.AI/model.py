import numpy as np
from activations import relu, d_relu, softmax, cross_entropy_loss

class TextCNN:
    """
    A Convolutional Neural Network (CNN) model for text classification.
    
    This model utilizes multiple convolutional filters of varying sizes to process
    text sequences, followed by max-pooling, and then a fully connected layer for
    classification.

    Attributes:
        vocab_size (int): Size of the vocabulary.
        embedding_dim (int): The dimension of the word embeddings.
        max_len (int): The maximum length of input sequences.
        num_filters (int): Number of filters per convolutional layer.
        filter_sizes (list): List of filter sizes for the convolutional layers.
        num_classes (int): Number of output classes.
        lr (float): Learning rate for the optimizer.
        embeddings (np.ndarray): Word embeddings matrix.
        conv_filters (dict): Convolutional filters for each filter size.
        fc_weights (np.ndarray): Weights for the fully connected layer.
        fc_bias (np.ndarray): Bias for the fully connected layer.
        fc_input_dim (int): Input dimension of the fully connected layer.
    """
    def __init__(self, vocab_size, embedding_dim, max_len, num_filters, filter_sizes, num_classes, learning_rate=0.01):
        """
        Initializes the TextCNN model with the provided hyperparameters.

        Args:
            vocab_size (int): The size of the vocabulary.
            embedding_dim (int): The dimension of the word embeddings.
            max_len (int): The maximum length of input sequences.
            num_filters (int): The number of filters per convolutional layer.
            filter_sizes (list): List of filter sizes for convolutional layers.
            num_classes (int): Number of output classes.
            learning_rate (float, optional): The learning rate for the optimizer. Default is 0.01.
        """
        self.vocab_size = vocab_size
        self.embedding_dim = embedding_dim
        self.max_len = max_len
        self.num_filters = num_filters
        self.filter_sizes = filter_sizes
        self.num_classes = num_classes
        self.lr = learning_rate

        self.embeddings = np.random.randn(vocab_size, embedding_dim) * 0.01

        self.conv_filters = {}
        for fs in filter_sizes:
            self.conv_filters[fs] = np.random.randn(fs, embedding_dim, num_filters) * np.sqrt(2.0 / (fs * embedding_dim))

        self.fc_input_dim = num_filters * len(filter_sizes)
        self.fc_weights = np.random.randn(self.fc_input_dim, num_classes) * np.sqrt(1.0 / self.fc_input_dim)
        self.fc_bias = np.zeros((1, num_classes))

    def forward(self, X):
        """
        Performs the forward pass through the network.

        Args:
            X (np.ndarray): The input batch of text sequences (shape: [batch_size, max_len]).

        Returns:
            np.ndarray: The probabilities for each class (shape: [batch_size, num_classes]).
        """
        self.batch_size = X.shape[0]
        self.embedded = self.embeddings[X]
        self.conv_outputs = {} 
        self.pooled_outputs = []

        for fs in self.filter_sizes:
            filter_w = self.conv_filters[fs]
            conv_out = []

            for i in range(self.max_len - fs + 1):
                window = self.embedded[:, i:i+fs, :]  
                conv_result = np.tensordot(window, filter_w, axes=([1,2],[0,1]))
                conv_out.append(conv_result)
            conv_out = np.stack(conv_out, axis=1) 
            conv_out = relu(conv_out)
            self.conv_outputs[fs] = conv_out

            pooled = np.max(conv_out, axis=1)  
            self.pooled_outputs.append(pooled)


        self.fc_input = np.concatenate(self.pooled_outputs, axis=1)  

        logits = np.dot(self.fc_input, self.fc_weights) + self.fc_bias  
        self.probs = softmax(logits)
        return self.probs

    def backward(self, X, y_onehot):
        """
        Performs the backward pass to compute gradients and update model parameters.

        Args:
            X (np.ndarray): The input batch of text sequences (shape: [batch_size, max_len]).
            y_onehot (np.ndarray): One-hot encoded labels (shape: [batch_size, num_classes]).
        """
        d_logits = (self.probs - y_onehot) / self.batch_size 
        dW_fc = np.dot(self.fc_input.T, d_logits)  
        db_fc = np.sum(d_logits, axis=0, keepdims=True)
        d_fc_input = np.dot(d_logits, self.fc_weights.T)  

        d_pooled_splits = np.split(d_fc_input, len(self.filter_sizes), axis=1)

        d_embeddings = np.zeros_like(self.embedded)
        d_filter_sum = {} 

        for idx, fs in enumerate(self.filter_sizes):
            conv_out = self.conv_outputs[fs] 
            d_pool = d_pooled_splits[idx] 
            d_conv = np.zeros_like(conv_out)

            for b in range(self.batch_size):
                for f in range(self.num_filters):
                    max_index = np.argmax(conv_out[b, :, f])
                    d_conv[b, max_index, f] = d_pool[b, f]

            d_conv *= d_relu(conv_out)

            for i in range(self.max_len - fs + 1):
                window = self.embedded[:, i:i+fs, :]
                d_conv_slice = d_conv[:, i, :] 

                d_filter = np.tensordot(window, d_conv_slice, axes=([0],[0]))
                d_filter_sum.setdefault(fs, 0)
                d_filter_sum[fs] += d_filter

                for b in range(self.batch_size):
                    d_embeddings[b, i:i+fs, :] += np.tensordot(d_conv_slice[b, :], self.conv_filters[fs], axes=([0], [2]))
        
        for fs in self.filter_sizes:
            self.conv_filters[fs] -= self.lr * d_filter_sum[fs]

        for b in range(self.batch_size):
            for i in range(self.max_len):
                word_idx = X[b, i]
                self.embeddings[word_idx] -= self.lr * d_embeddings[b, i, :]

        self.fc_weights -= self.lr * dW_fc
        self.fc_bias -= self.lr * db_fc

    def train_on_batch(self, X, y_onehot):
        """
        Trains the model on a single batch of data.

        Args:
            X (np.ndarray): The input batch of text sequences (shape: [batch_size, max_len]).
            y_onehot (np.ndarray): One-hot encoded labels (shape: [batch_size, num_classes]).

        Returns:
            float: The loss computed for the batch.
        """
        preds = self.forward(X)
        loss = cross_entropy_loss(preds, y_onehot)
        self.backward(X, y_onehot)
        return loss

def save_model(model, filename="models/cnn_model.npy"):
    """
    Saves the trained model to a file.

    Args:
        model (TextCNN): The trained model to save.
        filename (str, optional): The filename to save the model. Default is "models/cnn_model.npy".
    """
    np.save(filename, model)

def load_model(filename="models/cnn_model.npy"):
    """
    Loads a trained model from a file.

    Args:
        filename (str, optional): The filename from which to load the model. Default is "models/cnn_model.npy".

    Returns:
        TextCNN: The loaded model.
    """
    return np.load(filename, allow_pickle=True).item()
