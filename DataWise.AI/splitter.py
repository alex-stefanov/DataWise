import json
import numpy as np
import random
from collections import Counter

def load_data(filename):
    """
    Loads data from a JSON file.

    Args:
        filename (str): The path to the JSON file containing the data.

    Returns:
        list: A list of data objects (usually containing "Exercise" and "Label" fields).
    """
    with open(filename, 'r') as f:
        data = json.load(f)
    return data['data']

def tokenize(sentence):
    """
    Tokenizes a sentence into lowercase words.

    Args:
        sentence (str): The sentence to tokenize.

    Returns:
        list: A list of lowercase words.
    """
    return sentence.lower().split()

def analyze_data(data):
    """
    Analyzes the data and computes basic statistics, such as sentence length, vocabulary size, 
    and label distribution.

    Args:
        data (list): A list of data items, where each item contains an "Exercise" and a "Label".

    Returns:
        dict: A dictionary containing the following statistics:
            - "max_sentence_length": The maximum sentence length.
            - "avg_sentence_length": The average sentence length.
            - "min_sentence_length": The minimum sentence length.
            - "vocab_size": The number of unique words in the dataset.
            - "label_distribution": A Counter object showing the distribution of labels.
    """
    all_tokens = []
    sentence_lengths = []
    labels = []
    for item in data:
        tokens = tokenize(item["Exercise"])
        sentence_lengths.append(len(tokens))
        all_tokens.extend(tokens)
        labels.append(item["Label"])
    
    vocab = set(all_tokens)
    label_counter = Counter(labels)
    
    return {
        "max_sentence_length": max(sentence_lengths),
        "avg_sentence_length": np.mean(sentence_lengths),
        "min_sentence_length": min(sentence_lengths),
        "vocab_size": len(vocab),
        "label_distribution": label_counter
    }

def train_val_split(data, val_ratio=0.2, seed=42):
    """
    Splits the data into training and validation sets.

    Args:
        data (list): The dataset to split.
        val_ratio (float, optional): The proportion of data to use for validation. Defaults to 0.2.
        seed (int, optional): The random seed to ensure reproducibility. Defaults to 42.

    Returns:
        tuple: A tuple containing two lists: (train_data, val_data).
    """
    random.seed(seed)
    random.shuffle(data)
    split_index = int(len(data) * (1 - val_ratio))
    return data[:split_index], data[split_index:]

def save_data(filename, data):
    """
    Saves the data to a JSON file.

    Args:
        filename (str): The path to the file where data will be saved.
        data (list): The data to save.

    Returns:
        None
    """
    with open(filename, 'w') as f:
        json.dump({"data": data}, f, indent=4)


if __name__ == "__main__":
    filename = "exercises.json"
    data = load_data(filename)
    stats = analyze_data(data)
    print("Dataset Statistics:")
    print(f"  Max sentence length: {stats['max_sentence_length']}")
    print(f"  Average sentence length: {stats['avg_sentence_length']:.2f}")
    print(f"  Min sentence length: {stats['min_sentence_length']}")
    print(f"  Vocabulary size: {stats['vocab_size']}")
    print("  Label distribution:")
    for label, count in stats['label_distribution'].items():
        print(f"    {label}: {count}")
    
    train_data, val_data = train_val_split(data, val_ratio=0.2)
    print("\nSplit Information:")
    print(f"  Number of training examples: {len(train_data)}")
    print(f"  Number of validation examples: {len(val_data)}")

    train_data, val_data = train_val_split(data, val_ratio=0.2)

    save_data("train_data.json", train_data)
    save_data("val_data.json", val_data)

    print("Train and validation data saved to 'train_data.json' and 'val_data.json'.")