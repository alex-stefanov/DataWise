import sys
import pickle
from model import TextCNN, load_model

sys.modules['__main__'].TextCNN = TextCNN

model = load_model("cnn_model.npy")

with open("models/cnn_model_fixed.pkl", "wb") as f:
    pickle.dump(model, f)

print("Model re‑saved to cnn_model_fixed.pkl")
