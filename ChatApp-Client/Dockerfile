FROM node:dubnium-alpine3.11 as build-step

RUN mkdir -p /app

WORKDIR /app

COPY package.json /app

RUN npm install

COPY . /app

RUN  npm run build -- --prod

FROM nginx:1.17.1-alpine
EXPOSE 80
COPY --from=build-step /app/dist/ChatApp-Client /usr/share/nginx/html