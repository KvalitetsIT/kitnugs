#!/bin/sh

FROM_TAG=$1
TO_TAG=$2

if [ -z "$FROM_TAG" ]; then
  echo "FROM_TAG variable not set."
  exit 1
fi

if [ -z "$TO_TAG" ]; then
  echo "TO_TAG variable not set."
  exit 1
fi

docker tag $FROM_TAG $TO_TAG
