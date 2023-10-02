#!/bin/sh

echo "${GITHUB_REPOSITORY}"
echo "${DOCKER_SERVICE}"
if [ "${GITHUB_REPOSITORY}" != "KvalitetsIT/kitnugs" ] && [ "${DOCKER_SERVICE}" = "kvalitetsit/kitnugs" ]; then
  echo "Please run setup.sh REPOSITORY_NAME"
  exit 1
fi
