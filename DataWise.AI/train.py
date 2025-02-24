import numpy as np
import json
from tqdm import trange
from data_utils import load_data, build_vocab, prepare_dataset
from model import TextCNN, save_model, load_model

train_filename = "train_data.json"
val_filename = "val_data.json"

train_data = load_data(train_filename)
val_data = load_data(val_filename)

word2idx = build_vocab(train_data)
vocab_size = len(word2idx)

labels_set = sorted({item["Label"] for item in train_data})
label2idx = {label: idx for idx, label in enumerate(labels_set)}
num_classes = len(label2idx)

with open("word2idx.json", "w") as f:
    json.dump(word2idx, f)
with open("label2idx.json", "w") as f:
    json.dump(label2idx, f)

max_len = 42

X_train, y_train, y_train_onehot = prepare_dataset(train_data, word2idx, label2idx, max_len)
X_val, y_val, y_val_onehot = prepare_dataset(val_data, word2idx, label2idx, max_len)

embedding_dim = 50
filter_sizes = [2, 3, 4]
num_filters = 64
learning_rate = 0.001
num_epochs = 1
batch_size = 32

try:
    model = load_model()
    print("Loaded saved model.")
except Exception as e:
    model = TextCNN(vocab_size=vocab_size,
                    embedding_dim=embedding_dim,
                    max_len=max_len,
                    num_filters=num_filters,
                    filter_sizes=filter_sizes,
                    num_classes=num_classes,
                    learning_rate=learning_rate)
    print("No saved model found. Training from scratch.")

num_batches = int(np.ceil(len(X_train) / batch_size))
for epoch in range(num_epochs):
    epoch_loss = 0
    indices = np.arange(len(X_train))
    np.random.shuffle(indices)
    X_train = X_train[indices]
    y_train_onehot = y_train_onehot[indices]
    
    for i in trange(num_batches, desc=f"Epoch {epoch+1}"):
        start = i * batch_size
        end = min((i+1) * batch_size, len(X_train))
        X_batch = X_train[start:end]
        y_batch_onehot = y_train_onehot[start:end]
        loss = model.train_on_batch(X_batch, y_batch_onehot)
        epoch_loss += loss
    avg_loss = epoch_loss / num_batches
    print(f"Epoch {epoch+1} average loss: {avg_loss:.4f}")

val_preds = model.forward(X_val)
val_labels = np.argmax(val_preds, axis=1)
true_labels = np.argmax(y_val_onehot, axis=1)
accuracy = np.mean(val_labels == true_labels)
print(f"Validation Accuracy: {accuracy:.4f}")

save_model(model)
print("Model saved!")