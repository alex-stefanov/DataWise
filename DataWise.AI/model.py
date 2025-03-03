import numpy as np
from activations import relu, d_relu, softmax, cross_entropy_loss

class TextCNN:
    def __init__(self, vocab_size, embedding_dim, max_len, num_filters, filter_sizes, num_classes, learning_rate=0.01):
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
        preds = self.forward(X)
        loss = cross_entropy_loss(preds, y_onehot)
        self.backward(X, y_onehot)
        return loss

def save_model(model, filename="models/cnn_model.npy"):
    np.save(filename, model)

def load_model(filename="models/cnn_model.npy"):
    return np.load(filename, allow_pickle=True).item()
