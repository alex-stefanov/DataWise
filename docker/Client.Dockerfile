FROM node:22-alpine AS build

WORKDIR /app

RUN npm install -g @angular/cli
COPY package*.json ./
RUN npm i

COPY . .

RUN ng build --configuration=production

CMD ng serve -c production --host 0.0.0.0