#!/bin/sh

function updateFile {
    local f="$1"
    cmp -s "$f" "$f.tmp"
    if [ $? = 0 ] ; then
      /bin/rm $f.tmp
    else
      /bin/mv "$f.tmp" "$f"
    fi
}

echo "Add Dev version to list of versions"
GIT_BRANCH=$(cat /kit/current-version)
echo "[]" > /kit/env

if (echo "$GIT_BRANCH" | grep -Eq ^v[0-9]*\\.[0-9]*\\.[0-9]*$); then
  echo "Release version"
else
  echo "Is dev version"

  url="${BASE_URL}/${GIT_BRANCH}.yaml"

  echo $url

  cat /kit/env | jq --arg u $url '. += [{"name": "Dev", "url": $u }] | sort_by(.name)' > /kit/env.tmp
  updateFile /kit/env
fi

echo "Creating file with version and path"
(
   IFS=$'\n'
   for version in $(cat kit/versions)
   do
     url="${BASE_URL}/${version}.yaml"

     cat /kit/env | jq --arg n $version --arg u $url '. += [{"name": $n, "url": $u}]' > /kit/env.tmp
     updateFile /kit/env
   done
)



echo "Updates version in doc file"
for file in $DOC_FILES
do
   file_name=${file##*/}
   yq w -i $file 'info.version' $(echo ${file_name%.*} | cut -d "v" -f 2)
done