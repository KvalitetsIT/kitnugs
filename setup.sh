#!/usr/bin/env bash

if ! command -v tree &> /dev/null; then
  echo "Command 'tree' not found. Please install tree."
  exit 1
fi

if [ -z $1 ]; then
  echo "Run setup.sh REPOSITORY_NAME"
  exit 1
fi

ignore_files=".git|setup.sh|dependabot-auto-merge.yml"

files=(
  ".github/build.yaml"
  ".github/pr_build.yaml"
  "IntegrationTest/AbstractIntegrationTest.cs"
)

#for input_file in `tree -I "${ignore_files}" -Ffai --noreport`
for input_file in files
do
  if [ ! -d "${input_file}" ]; then
    echo "Processing file: ${input_file}"
    sed -i -e "s/kitnugs/$1/g" "${input_file}"
  fi
done

# Clean up / implode
rm setup.sh