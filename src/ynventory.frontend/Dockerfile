FROM trion/ng-cli as builder
WORKDIR /app
COPY package.json package.json
COPY package-lock.json package-lock.json
RUN npm ci  --debug 
COPY . .
RUN ng build

FROM nginx:alpine
COPY --from=builder  /app/dist/ynventory.frontend /usr/share/nginx/html 
EXPOSE 80