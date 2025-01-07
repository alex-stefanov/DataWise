from flask import Flask, render_template
import numpy as np
import matplotlib
import matplotlib.pyplot as plt
import io
import base64

matplotlib.use('Agg')

app = Flask(__name__)

@app.route('/')
def index():
    epochs = 10
    losses = np.random.rand(epochs)

    plt.plot(range(epochs), losses, label='Loss')
    plt.xlabel('Epochs')
    plt.ylabel('Loss')
    plt.title('Training Loss Over Time')
    plt.legend()

    img = io.BytesIO()
    plt.savefig(img, format='png')
    img.seek(0)

    plot_url = base64.b64encode(img.getvalue()).decode('utf8')
    return render_template('index.html', plot_url=plot_url)

if __name__ == '__main__':
    app.run(debug=True)