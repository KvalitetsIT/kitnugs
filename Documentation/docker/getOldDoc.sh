#!/bin/bash

IMAGE=$1

if docker pull $IMAGE:latest; then
    echo "Copy from old documentation image."
    docker cp $(docker create $IMAGE:latest):/usr/share/nginx/html Documentation/target
fi
