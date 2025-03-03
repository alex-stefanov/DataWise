from flask import Flask, request, jsonify
from flask_cors import CORS
import numpy as np
import json
import pickle

from model import softmax
from data_utils import tokenize, encode_sentence;

with open("models/cnn_model_fixed.pkl", "rb") as f:
    model = pickle.load(f)

with open('word2idx.json', 'r') as f:
    word2idx = json.load(f)
with open('label2idx.json', 'r') as f:
    label2idx = json.load(f)

idx2label = {v: k for k, v in label2idx.items()}

max_len = 42

app = Flask(__name__)
CORS(app)

@app.route('/predict', methods=['POST'])
def predict():
    if not request.is_json:
        return jsonify({'error': 'Request must be JSON.'}), 400
    
    data = request.get_json()
    if 'exercise' not in data:
        return jsonify({'error': 'Missing "exercise" field in JSON data.'}), 400
    
    exercise_text = data['exercise']
    
    encoded = encode_sentence(exercise_text, word2idx, max_len)
    X = np.expand_dims(encoded, axis=0)
    
    preds = model.forward(X)
    predicted_idx = np.argmax(preds, axis=1)[0]
    predicted_label = idx2label.get(predicted_idx, "Unknown")
    
    return jsonify({
        'exercise': exercise_text,
        'prediction': predicted_label,
        'probabilities': preds.tolist()
    })

if __name__ == '__main__':
    app.run(debug=True)