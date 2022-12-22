FROM trion/ng-cli as builder
WORKDIR /app
COPY . .
RUN npm ci  --debug 
RUN ng build --prod

FROM nginx:alpine
COPY --from=builder  /app/dist/ynventory.frontend /usr/share/nginx/html 
EXPOSE 80