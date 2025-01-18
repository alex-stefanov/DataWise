import json
import numpy as np
from gensim.models import Word2Vec
from sklearn.model_selection import train_test_split
from model.CLDNN import CLDNN

def load_data(labeled_file, unlabeled_file):
    with open(labeled_file, 'r') as f:
        labeled_data = json.load(f)['data']
    
    with open(unlabeled_file, 'r') as f:
        unlabeled_data = json.load(f)['data']

    return labeled_data, unlabeled_data

def preprocess_text(text_data, word2vec_model, seq_length=20):
    def text_to_sequence(text, model, seq_length):
        words = text.split()
        embeddings = [model.wv[word] for word in words if word in model.wv]
        if len(embeddings) < seq_length:
            embeddings += [np.zeros(model.vector_size)] * (seq_length - len(embeddings))
        return np.array(embeddings[:seq_length])

    sequences = []
    for entry in text_data:
        exercise_text = entry['Exercise']
        sequence = text_to_sequence(exercise_text, word2vec_model, seq_length)
        sequences.append(sequence)

    return np.array(sequences)

def encode_labels(labels, num_classes):
    encoded = np.zeros((len(labels), num_classes))
    for i, label in enumerate(labels):
        encoded[i, label] = 1
    return encoded

def train_model(X_train, y_train, model, epochs=10, learning_rate=0.001, save_path="model.pkl"):
    for epoch in range(epochs):
        print(f"Epoch {epoch + 1}/{epochs}")
        model.train(X_train, y_train, learning_rate=learning_rate)
        model.save(save_path)
        print(f"Model saved after epoch {epoch + 1}")


def main():
    labeled_data, unlabeled_data = load_data('labeled_data.json', 'non_labeled_data.json')

    sentences = [entry['Exercise'].split() for entry in labeled_data + unlabeled_data]
    word2vec_model = Word2Vec(sentences, vector_size=50, min_count=1)

    X_labeled = preprocess_text(labeled_data, word2vec_model)
    y_labeled = [entry['Label'] for entry in labeled_data]

    label_map = {
        "BFS": 0,
        "DFS": 1,
        "Two pointers": 2,
        "Dynamic Programming(DP)": 3,
        "Greedy Algorithm": 4,
        "Backtracking": 5,
        "Binary Search": 6,
        "Disjoint Set": 7,
        "Game Theory": 8,
        "N/A": 9,
    }

    y_labeled = [label_map[label] for label in y_labeled]
    y_labeled = encode_labels(y_labeled, num_classes=10)

    X_train, X_val, y_train, y_val = train_test_split(X_labeled, y_labeled, test_size=0.2, random_state=42)

    input_shape = X_train.shape[1:]
    num_classes = len(label_map)
    model = CLDNN(input_shape, num_classes)

    model_path = "model.pkl"
    try:
        model.load(model_path)
    except FileNotFoundError:
        print("No saved model found. Starting fresh.")

    train_model(X_train, y_train, model, epochs=10, learning_rate=0.001)

    print("Evaluating on validation set...")
    evaluate(model, X_val, y_val)

def evaluate(model, X, y):
    predictions = []
    for sample in X:
        output = sample[np.newaxis, ...]
        for layer in model.layers:
            output = layer.forward(output)
        predictions.append(np.argmax(output, axis=-1))
    predictions = np.concatenate(predictions)
    accuracy = np.mean(predictions == np.argmax(y, axis=1))
    print(f"Accuracy: {accuracy * 100:.2f}%")

if __name__ == '__main__':
    main()