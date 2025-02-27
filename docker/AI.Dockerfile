FROM python:3.10-alpine

WORKDIR /app

COPY . .

RUN chmod +x ./install.sh
RUN ./install.sh

CMD [ "python3", "-m" , "flask", "run", "--host=0.0.0.0", "--port=5000" ]
