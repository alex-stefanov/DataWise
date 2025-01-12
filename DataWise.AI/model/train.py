import json
import numpy as np
import matplotlib.pyplot as plt
from gensim.models import Word2Vec
from network import CLDNN
import os

# Define custom stopwords (you can add more if needed)
stop_words = set([
    'i', 'me', 'my', 'myself', 'we', 'our', 'ours', 'ourselves', 'you', 'your', 'yours', 'yourself', 'yourselves', 
    'he', 'him', 'his', 'himself', 'she', 'her', 'hers', 'herself', 'it', 'its', 'itself', 'they', 'them', 'their', 
    'theirs', 'themselves', 'what', 'which', 'who', 'whom', 'this', 'that', 'these', 'those', 'am', 'is', 'are', 'was', 
    'were', 'be', 'been', 'being', 'have', 'has', 'had', 'having', 'do', 'does', 'did', 'doing', 'a', 'an', 'the', 'and', 
    'but', 'if', 'or', 'because', 'as', 'until', 'while', 'of', 'at', 'by', 'for', 'with', 'about', 'against', 'between', 
    'into', 'through', 'during', 'before', 'after', 'above', 'below', 'to', 'from', 'up', 'down', 'in', 'out', 'on', 'off', 
    'over', 'under', 'again', 'further', 'then', 'once', 'here', 'there', 'when', 'where', 'why', 'how', 'all', 'any', 
    'both', 'each', 'few', 'more', 'most', 'other', 'some', 'such', 'no', 'nor', 'not', 'only', 'own', 'same', 'so', 'than', 
    'too', 'very', 's', 't', 'can', 'will', 'just', 'don', 'should', 'now', 'd', 'll', 'm', 'o', 're', 've', 'y', 'ain', 'aren', 
    'couldn', 'didn', 'doesn', 'hadn', 'hasn', 'haven', 'isn', 'ma', 'mightn', 'mustn', 'needn', 'shan', 'shouldn', 'wasn', 
    'weren', 'won', 'wouldn'
])

# Tokenization and Preprocessing (using Gensim)
def preprocess_text(text):
    """
    Preprocess the input text by tokenizing, lowercasing, removing stopwords, and lemmatizing (simple).
    Args:
        text (str): The input text.
    Returns:
        list: A list of processed tokens (words).
    """
    # Tokenize by splitting by whitespace and converting to lowercase
    tokens = text.lower().split()
    
    # Remove stopwords (simple check)
    tokens = [word for word in tokens if word not in stop_words and word.isalpha()]
    
    # Simple "lemmatization" approach: removing plural 's' (can be improved)
    tokens = [word[:-1] if word.endswith('s') else word for word in tokens]
    
    return tokens

# Load Data from JSON Files
def load_data_from_json(labeled_file, unlabeled_file):
    """
    Load labeled and unlabeled data from JSON files.
    Args:
        labeled_file (str): Path to the labeled data JSON file.
        unlabeled_file (str): Path to the unlabeled data JSON file.
    Returns:
        Tuple: (features, labels, unique_labels) for labeled data, features for unlabeled data.
    """
    # Load labeled data
    with open(labeled_file, 'r') as lf:
        labeled_content = json.load(lf)
    labeled_data = labeled_content['data']
    labeled_exercises = [entry['Exercise'] for entry in labeled_data]
    labeled_labels = [entry['Label'] for entry in labeled_data]

    # Encode labels into integers
    unique_labels = sorted(set(labeled_labels))
    label_to_index = {label: idx for idx, label in enumerate(unique_labels)}
    encoded_labels = np.array([label_to_index[label] for label in labeled_labels])

    # Load unlabeled data
    with open(unlabeled_file, 'r') as uf:
        unlabeled_content = json.load(uf)
    unlabeled_data = [entry['Exercise'] for entry in unlabeled_content['data']]

    return (labeled_exercises, encoded_labels, unique_labels), unlabeled_data

# Train Word2Vec Model on Exercises
def train_word2vec_model(exercises):
    """
    Train a Word2Vec model using the preprocessed exercises.
    Args:
        exercises (list): List of preprocessed exercise texts.
    Returns:
        Word2Vec: The trained Word2Vec model.
    """
    # Train Word2Vec on the tokenized exercises
    model = Word2Vec(sentences=exercises, vector_size=100, window=5, min_count=1, sg=1)
    model.save("word2vec.model")
    return model

# Encode exercises into Word2Vec vectors
def encode_exercises_with_word2vec(exercises, word2vec_model):
    """
    Convert exercises into Word2Vec vectors by averaging the word vectors for each exercise.
    Args:
        exercises (list): List of tokenized exercises.
        word2vec_model (Word2Vec): The trained Word2Vec model.
    Returns:
        np.ndarray: Array of exercise embeddings.
    """
    exercise_embeddings = []
    for exercise in exercises:
        # Get word vectors for each token in the exercise
        word_vectors = [word2vec_model.wv[word] for word in exercise if word in word2vec_model.wv]
        # Average the word vectors
        if word_vectors:
            exercise_embeddings.append(np.mean(word_vectors, axis=0))
        else:
            # If no word vectors (e.g., exercise is empty after stopword removal), use zero vector
            exercise_embeddings.append(np.zeros(word2vec_model.vector_size))
    return np.array(exercise_embeddings)

# Train Model
def train_model(model, train_data, train_labels, epochs, learning_rate):
    """
    Train the CLDNN model.
    Args:
        model (CLDNN): The initialized CLDNN model.
        train_data (np.ndarray): Input data (2D array).
        train_labels (np.ndarray): One-hot encoded labels.
        epochs (int): Number of training iterations.
        learning_rate (float): Learning rate for gradient updates.
    Returns:
        list: List of average losses per epoch.
    """
    losses = []

    for epoch in range(epochs):
        total_loss = 0
        for i in range(len(train_data)):
            # Reshape the input to ensure it's 2D
            input_data = train_data[i].reshape(1, -1)  # Reshaping for a single input
            
            # Forward pass
            output = model.forward(input_data)

            # Compute loss
            loss = np.mean((output - train_labels[i]) ** 2)
            total_loss += loss

            # Backward pass
            grad_output = 2 * (output - train_labels[i]) / output.shape[0]
            model.backward(grad_output, learning_rate)
        # Average loss per epoch
        avg_loss = total_loss / len(train_data)
        losses.append(avg_loss)
        print(f"Epoch {epoch + 1}/{epochs}, Loss: {avg_loss:.4f}")

    # Plot Loss Curve
    plt.figure(figsize=(8, 6))
    plt.plot(range(1, epochs + 1), losses, label='Loss', color='blue')
    plt.xlabel('Epochs')
    plt.ylabel('Average Loss')
    plt.title('Training Loss Over Time')
    plt.legend()
    plt.grid(True)
    plt.show()

    return losses

def main():
    # File paths
    labeled_file = '../labeled_data.json'    # Path to labeled data file
    unlabeled_file = '../non_labeled_data.json'  # Path to unlabeled data file
    model_save_path = '../trained_model.npy'  # Path to save/load the trained model

    # Load data
    (train_exercises, train_labels, unique_labels), unlabeled_data = load_data_from_json(
        labeled_file, unlabeled_file
    )

    # Preprocess text data (tokenize, lowercase, stopword removal, simple lemmatization)
    preprocessed_exercises = [preprocess_text(exercise) for exercise in train_exercises]

    # Train Word2Vec model on the preprocessed exercises
    word2vec_model = train_word2vec_model(preprocessed_exercises)

    # Encode exercises into vectors using Word2Vec
    train_data = encode_exercises_with_word2vec(preprocessed_exercises, word2vec_model)

    # Ensure labels are one-hot encoded
    num_classes = len(unique_labels)
    train_labels = np.eye(num_classes)[train_labels]

    # Initialize the CLDNN model
    input_size = train_data.shape[1]
    model = CLDNN(input_size=input_size)

    # Load the trained model if it exists
    if os.path.exists(model_save_path):
        print("Loading the previously trained model...")
        model.load(model_save_path)
    else:
        print("No model found. Training from scratch...")

    # Training parameters
    epochs = 10
    learning_rate = 0.001

    # Train the CLDNN model
    train_model(model, train_data, train_labels, epochs, learning_rate)

    # Save the trained model
    print("Saving the trained model...")
    model.save(model_save_path)

if __name__ == "__main__":
    main()