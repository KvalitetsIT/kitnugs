#!/bin/bash

rm -rf Documentation/Generated/*

docker run --rm --user $(id -u) -v $(pwd)/Documentation:/local openapitools/openapi-generator-cli generate -i /local/api.yaml -o /local/Generated -g aspnetcore -p buildTarget=library,returnICollection=true,aspnetCoreVersion=6.0,isLibrary=true,operationIsAsync=true,operationResultTask=true,nullableReferenceTypes=true,useNewtonsoft=true,useDateTimeOffset=true