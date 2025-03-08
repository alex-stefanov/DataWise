import json
import numpy as np
import random
from collections import Counter

def load_data(filename):
    """
    Loads the dataset from a JSON file.

    Args:
        filename (str): The path to the JSON file containing the dataset.

    Returns:
        list: The list of data items from the JSON file.
    """
    with open(filename, 'r') as f:
        data = json.load(f)
    return data['data']

def tokenize(sentence):
    """
    Tokenizes a sentence by converting it to lowercase and splitting it into words.

    Args:
        sentence (str): The input sentence to be tokenized.

    Returns:
        list: A list of lowercase words from the sentence.
    """
    return sentence.lower().split()

def build_vocab(data, min_freq=1):
    """
    Builds the vocabulary from the dataset by counting word frequencies and filtering based on a minimum frequency.

    Args:
        data (list): The list of data items where each item contains an "Exercise" field.
        min_freq (int, optional): The minimum frequency for words to be included in the vocabulary. Defaults to 1.

    Returns:
        dict: A dictionary mapping words to indices.
    """
    counter = Counter()
    for item in data:
        tokens = tokenize(item["Exercise"])
        counter.update(tokens)
    vocab = {word for word, count in counter.items() if count >= min_freq}
    word2idx = {word: idx+1 for idx, word in enumerate(sorted(vocab))}
    word2idx["<PAD>"] = 0
    return word2idx

def encode_sentence(sentence, word2idx, max_len):
    """
    Encodes a sentence into a sequence of integers based on a vocabulary and ensures the sequence length is consistent.

    Args:
        sentence (str): The sentence to be encoded.
        word2idx (dict): A dictionary mapping words to indices.
        max_len (int): The maximum length of the encoded sequence.

    Returns:
        np.ndarray: A numpy array of integers representing the encoded sentence.
    """
    tokens = tokenize(sentence)
    seq = [word2idx.get(token, 0) for token in tokens]
    if len(seq) < max_len:
        seq = seq + [0]*(max_len - len(seq))
    else:
        seq = seq[:max_len]
    return np.array(seq)

def encode_labels(data, label2idx):
    """
    Encodes the labels of the dataset into integer indices based on a label-to-index mapping.

    Args:
        data (list): The list of data items containing the "Label" field.
        label2idx (dict): A dictionary mapping labels to indices.

    Returns:
        np.ndarray: A numpy array of integer-encoded labels.
    """
    labels = []
    for item in data:
        labels.append(label2idx[item["Label"]])
    return np.array(labels)

def one_hot(labels, num_classes):
    """
    Converts integer labels to one-hot encoded vectors.

    Args:
        labels (np.ndarray): An array of integer labels.
        num_classes (int): The number of classes (output dimension).

    Returns:
        np.ndarray: A numpy array of one-hot encoded labels.
    """
    onehot = np.zeros((len(labels), num_classes))
    onehot[np.arange(len(labels)), labels] = 1
    return onehot

def prepare_dataset(data, word2idx, label2idx, max_len):
    """
    Prepares the dataset by encoding the sentences and labels, and one-hot encoding the labels.

    Args:
        data (list): The list of data items containing "Exercise" and "Label".
        word2idx (dict): A dictionary mapping words to indices.
        label2idx (dict): A dictionary mapping labels to indices.
        max_len (int): The maximum length of the sentences after encoding.

    Returns:
        tuple: A tuple containing:
            - X (np.ndarray): The encoded sentences.
            - y (np.ndarray): The integer-encoded labels.
            - y_onehot (np.ndarray): The one-hot encoded labels.
    """
    X = np.array([encode_sentence(item["Exercise"], word2idx, max_len) for item in data])
    y = encode_labels(data, label2idx)
    y_onehot = one_hot(y, len(label2idx))
    return X, y, y_onehot

def train_val_split(data, val_ratio=0.2, seed=42):
    """
    Splits the dataset into training and validation sets.

    Args:
        data (list): The list of data items to be split.
        val_ratio (float, optional): The ratio of data to be used for validation. Defaults to 0.2.
        seed (int, optional): The random seed for reproducibility. Defaults to 42.

    Returns:
        tuple: A tuple containing:
            - train_data (list): The training dataset.
            - val_data (list): The validation dataset.
    """
    random.seed(seed)
    random.shuffle(data)
    split_index = int(len(data) * (1 - val_ratio))
    return data[:split_index], data[split_index:]
