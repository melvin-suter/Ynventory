#!/bin/bash

#################
#   Versions
#################

FRONTEND_VERSION=0.1.0
BACKEND_VERSION=0.1.0




#################
#    Backend
#################

# cd ynventory.backend

# dotnet publish --os linux --arch x64 -c Release -p:PublishProfile=DefaultContainer

# docker tag mtg_inventory_backend:$BACKEND_VERSION suterdev/ynventory.backend:$BACKEND_VERSION
# docker tag mtg_inventory_backend:$BACKEND_VERSION suterdev/ynventory.backend:latest
# docker push suterdev/ynventory.backend:$BACKEND_VERSION
# docker push suterdev/ynventory.backend:latest


#################
#    Frontend
#################

cd ../ynventory.frontend

npm i
ng build

docker build -t suterdev/ynventory.frontend:$FRONTEND_VERSION .
docker tag suterdev/ynventory.frontend:$FRONTEND_VERSION suterdev/ynventory.frontend:latest
docker push suterdev/ynventory.frontend:$FRONTEND_VERSION
docker push suterdev/ynventory.frontend:latest

