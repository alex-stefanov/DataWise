import json
import numpy as np
import matplotlib.pyplot as plt
from gensim.models import Word2Vec
from network import CLDNN

# Load Data from JSON Files
def load_data_from_json(labeled_file, unlabeled_file):
    """
    Load labeled and unlabeled data from JSON files in the given format.
    Args:
    - labeled_file: Path to the labeled data JSON file.
    - unlabeled_file: Path to the unlabeled data JSON file.
    Returns:
    - labeled_data: Tuple (features, labels) for labeled data.
    - unlabeled_data: Features (exercises) for unlabeled data.
    """
    with open(labeled_file, 'r') as lf:
        labeled_content = json.load(lf)
    labeled_data = labeled_content['data']
    labeled_exercises = [entry['Exercise'] for entry in labeled_data]
    labeled_labels = [entry['Label'] for entry in labeled_data]

    # Encode labels as integers
    unique_labels = list(set(labeled_labels))
    label_to_index = {label: idx for idx, label in enumerate(unique_labels)}
    encoded_labels = np.array([label_to_index[label] for label in labeled_labels])

    with open(unlabeled_file, 'r') as uf:
        unlabeled_content = json.load(uf)
    unlabeled_data = [entry['Exercise'] for entry in unlabeled_content['data']]

    return (labeled_exercises, encoded_labels, unique_labels), unlabeled_data

# Word2Vec Training
def train_word2vec(sentences, vector_size=100, window=5, min_count=1, epochs=10):
    """
    Train a Word2Vec model on the given sentences.
    Args:
    - sentences: List of tokenized sentences (e.g., [["word1", "word2"], ["word3"]])
    - vector_size: Dimensionality of word embeddings.
    - window: Context window size.
    - min_count: Minimum frequency for a word to be included in the vocabulary.
    - epochs: Number of training iterations.
    Returns:
    - model: Trained Word2Vec model.
    """
    word2vec_model = Word2Vec(
        sentences=sentences,
        vector_size=vector_size,
        window=window,
        min_count=min_count,
        sg=1,  # Skip-gram method
    )
    word2vec_model.train(sentences, total_examples=len(sentences), epochs=epochs)
    return word2vec_model

# Text Encoding
def encode_exercises_with_word2vec(exercises, word2vec_model):
    """
    Encode exercises into numerical vectors using a trained Word2Vec model.
    Args:
    - exercises: List of exercises (raw text).
    - word2vec_model: Trained Word2Vec model.
    Returns:
    - Encoded numpy array for exercises.
    """
    encoded = []
    for exercise in exercises:
        tokens = exercise.split()  # Tokenize exercise into words
        vectors = [word2vec_model.wv[word] for word in tokens if word in word2vec_model.wv]
        if vectors:
            encoded.append(np.mean(vectors, axis=0))  # Average the word vectors
        else:
            encoded.append(np.zeros(word2vec_model.vector_size))  # Handle empty vectors
    return np.array(encoded)

# Training Function
def train_model(model, train_data, train_labels, epochs, learning_rate):
    losses = []

    for epoch in range(epochs):
        total_loss = 0
        for i in range(len(train_data)):
            output = model.forward(train_data[i])

            # Compute loss
            loss = np.mean((output - train_labels[i]) ** 2)
            total_loss += loss

            # Backward pass
            grad_output = 2 * (output - train_labels[i]) / output.shape[0]
            model.backward(grad_output, learning_rate)

        avg_loss = total_loss / len(train_data)
        losses.append(avg_loss)
        print(f"Epoch {epoch + 1}/{epochs}, Loss: {avg_loss:.4f}")

    # Plot loss
    plt.figure(figsize=(8, 6))
    plt.plot(range(1, epochs + 1), losses, label='Loss', color='blue')
    plt.xlabel('Epochs')
    plt.ylabel('Average Loss')
    plt.title('Training Loss Over Time')
    plt.legend()
    plt.grid(True)
    plt.show()

    return losses

# Main Function
def main():
    # File paths
    labeled_file = '../labeled_data.json'    # Path to labeled data file
    unlabeled_file = '../non_labeled_data.json'  # Path to unlabeled data file

    # Load data
    (train_exercises, train_labels, unique_labels), unlabeled_data = load_data_from_json(labeled_file, unlabeled_file)

    # Tokenize exercises for Word2Vec
    tokenized_sentences = [exercise.split() for exercise in train_exercises]

    # Train Word2Vec model
    word2vec_model = train_word2vec(tokenized_sentences)

    # Encode exercises into vectors
    train_data = encode_exercises_with_word2vec(train_exercises, word2vec_model)
    
    # Ensure labels are one-hot encoded
    num_classes = len(unique_labels)
    train_labels = np.eye(num_classes)[train_labels]  # Convert to one-hot

    # Initialize model
    model = CLDNN()

    # Training parameters
    epochs = 10
    learning_rate = 0.001

    # Train model
    train_model(model, train_data, train_labels, epochs, learning_rate)

if __name__ == "__main__":
    main()