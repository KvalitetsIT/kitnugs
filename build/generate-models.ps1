Remove-Item -Recurse Documentation/Generated/*

docker run --rm -v $PWD/Documentation:/local openapitools/openapi-generator-cli:v7.3.0 generate -i /local/api.yaml -o /local/Generated -g aspnetcore -p buildTarget=library,returnICollection=true,aspnetCoreVersion=6.0,isLibrary=true,operationIsAsync=true,operationResultTask=true,nullableReferenceTypes=true,useNewtonsoft=true,useDateTimeOffset=true