#!/bin/sh

./Documentation/docker/getOldDoc.sh

git describe --tags --always > Documentation/current-version

cp Documentation/api.yaml Documentation/target/$(cat Documentation/current-version).yaml

IMAGE_NAME=$1
docker build -t $IMAGE_NAME:latest -f ./Documentation/docker/Dockerfile --no-cache Documentation