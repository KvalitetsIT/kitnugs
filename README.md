
![Build Status](https://github.com/KvalitetsIT/kithugs/workflows/CICD/badge.svg)
# kitnugs![Build Status](https://github.com/KvalitetsIT/kithugs/workflows/CICD/badge.svg)
# kitnugs

Template repository showing how to be a good .NET application in a k8s cluster.

## A good citizen

Below is a set of recommendations for being a good service. The recommendations are not tied to a specific language or 
framework.

1. Configuration through environment variables.
2. Expose readiness endpoint
3. Expose endpoint that Prometheus can scrape
4. Be stateless
5. Support multiple instances
6. Always be in a releasable state
7. Automate build and deployment.

Some of above recommendations are heavily inspired by [https://12factor.net/](https://12factor.net/). It is recommended 
read [https://12factor.net/](https://12factor.net/) for more inspiration and further details. Some points go 
further than just being a good service and also touches areas like operations.

## Requirements

- `dotnet tool install --global Microsoft.dotnet-openapi`



## Getting started

Run `./setup.sh GIT_REPOSITORY_NAME`.

Above does a search/replace in relevant files. 

## Endpoints

### Service

The service is listening for connections on port 8080.

Health endpoint is listening on port 8081. This is used for Prometheus scraping and health endpoints. 

Prometheus scrape endpoint: `http://localhost:8081/metrics`  
Health URL that can be used for readiness probe etc: `http://localhost:8081/healthz`

### Documentation

Documentation of the API is build as a separate Docker image. Documentation is build using Swagger. The documentation 
image is post-fixed with `-documentation`. The file `documentation/docker/compose/docker-compose.yml` contains a  setup 
that starts both the service and documentation image. The documentation can be accessed at `http://localhost/test` 
and the service can be called through the Swagger UI. 

In the docker-compose setup is also an example on how to set custom endpoints for the Swagger documentation service.

## Dependency updates

Out of the box we use GitHub Actions as our CI/CD platform and that can also handle dependency updates. We utilize 
GitHubs [Dependabot](https://docs.github.com/en/code-security/dependabot/dependabot-version-updates/configuring-dependabot-version-updates) 
to create PR's with dependency updates. Further we have a job that automatically approves and merges dependencies. By 
default, it is only enabled in the template repository. You can enable this by removing ` && github.repository == 'KvalitetsIT/kithugs'`
from [dependabot-auto-merge.yml](.github/workflows/dependabot-auto-merge.yml). Before enabling it please consider below. 

- If no branch protection rule is configured dependency udpates that fails the automatic build and test will get merged. 
- You will not have a chance to review the changes in the dependency updates before it gets merged.  
- Enable auto-merge must be enabled in the repository.

## Configuration

| Environment variable | Description                                                                                          | Required |
|----------------------|------------------------------------------------------------------------------------------------------|----------|
| JDBC_URL             | JDBC connection URL                                                                                  | Yes      |
| JDBC_USER            | JDBC user                                                                                            | Yes      |
| JDBC_PASS            | JDBC password                                                                                        | Yes      |
| LOG_LEVEL            | Log Level for applikation  log. Defaults to INFO.                                                    | No       |
| LOG_LEVEL_FRAMEWORK  | Log level for framework. Defaults to INFO.                                                           | No       |
| CORRELATION_ID       | HTTP header to take correlation id from. Used to correlate log messages. Defaults to "x-request-id". | No       |
