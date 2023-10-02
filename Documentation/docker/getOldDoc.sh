#!/bin/bash

if docker pull kvalitetsit/kitnugs-documentation:latest; then
    echo "Copy from old documentation image."
    docker cp $(docker create kvalitetsit/kitnugs-documentation:latest):/usr/share/nginx/html Documentation/old
fi
