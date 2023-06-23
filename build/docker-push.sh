#!/bin/sh

TAG_NAME=$1

if [ -z $TAG_NAME ]; then
  echo "TAG_NAME variable not set."
  exit 1
fi

docker push $TAG_NAME
