#! /bin/sh

export DOC_FILES=/usr/share/nginx/html/*.yaml

echo "Running set version"
/kit/setVersion.sh
echo "Running set servers"
/kit/setServers.sh

echo "Sets env URLS to list of versions"
export URLS=$(cat /kit/env)

echo "Restarting nginx"
/docker-entrypoint.sh nginx -g 'daemon off;'