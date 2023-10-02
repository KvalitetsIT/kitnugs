#!/bin/sh

IMAGE_NAME=$1

./Documentation/docker/getOldDoc.sh $IMAGE_NAME

git describe --tags --always > Documentation/current-version

mkdir -p Documentation/target

cp Documentation/api.yaml Documentation/target/$(cat Documentation/current-version).yaml

docker build -t $IMAGE_NAME:latest -f ./Documentation/docker/Dockerfile --no-cache Documentation