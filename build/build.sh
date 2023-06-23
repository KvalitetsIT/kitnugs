#!/bin/sh

IMAGE_NAME=$1
docker build -t $IMAGE_NAME:latest -f ./KitNugs/Dockerfile --no-cache .