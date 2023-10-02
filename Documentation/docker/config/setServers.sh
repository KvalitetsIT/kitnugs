#! /bin/sh

if [[ -z "${SERVER_URLS}" ]]; then
  echo "SERVER_URLS NOT set"
else
  for file in $DOC_FILES
  do
    yq d -i $file 'servers[*]'

    for i in $(echo $SERVER_URLS | jq -r '. | keys | .[]'); do

       yq w -i $file 'servers[+].url' $(echo $SERVER_URLS | jq -r ".[$i].url")
       yq w -i $file "servers[$i].description" $(echo $SERVER_URLS | jq -r ".[$i].name")

    done

  done
fi


