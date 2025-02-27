FROM node:22-alpine AS build

WORKDIR /app

RUN npm install -g @angular/cli
COPY package*.json ./
RUN npm i

COPY . .

RUN ng build --configuration=production

FROM node:22-alpine

WORKDIR /app

COPY --from=build /app/dist/app ./

CMD [ "ng", "serve", "--host", "0.0.0.0" ]