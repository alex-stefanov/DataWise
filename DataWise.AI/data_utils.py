import json
import numpy as np
import random
from collections import Counter

def load_data(filename):
    with open(filename, 'r') as f:
        data = json.load(f)
    return data['data']

def tokenize(sentence):
    return sentence.lower().split()

def build_vocab(data, min_freq=1):
    counter = Counter()
    for item in data:
        tokens = tokenize(item["Exercise"])
        counter.update(tokens)
    vocab = {word for word, count in counter.items() if count >= min_freq}
    word2idx = {word: idx+1 for idx, word in enumerate(sorted(vocab))}
    word2idx["<PAD>"] = 0
    return word2idx

def encode_sentence(sentence, word2idx, max_len):
    tokens = tokenize(sentence)
    seq = [word2idx.get(token, 0) for token in tokens]
    if len(seq) < max_len:
        seq = seq + [0]*(max_len - len(seq))
    else:
        seq = seq[:max_len]
    return np.array(seq)

def encode_labels(data, label2idx):
    labels = []
    for item in data:
        labels.append(label2idx[item["Label"]])
    return np.array(labels)

def one_hot(labels, num_classes):
    onehot = np.zeros((len(labels), num_classes))
    onehot[np.arange(len(labels)), labels] = 1
    return onehot

def prepare_dataset(data, word2idx, label2idx, max_len):
    X = np.array([encode_sentence(item["Exercise"], word2idx, max_len) for item in data])
    y = encode_labels(data, label2idx)
    y_onehot = one_hot(y, len(label2idx))
    return X, y, y_onehot

def train_val_split(data, val_ratio=0.2, seed=42):
    random.seed(seed)
    random.shuffle(data)
    split_index = int(len(data) * (1 - val_ratio))
    return data[:split_index], data[split_index:]
