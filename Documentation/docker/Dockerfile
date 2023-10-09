FROM swaggerapi/swagger-ui:v4.12.0

COPY target/*.yaml /usr/share/nginx/html/
COPY old/*.yaml /usr/share/nginx/html/

COPY docker/versions /kit/versions
COPY docker/config/setVersion.sh /kit/setVersion.sh
COPY docker/config/setServers.sh /kit/setServers.sh
COPY docker/config/config.sh /config.sh

COPY current-version /kit/current-version

RUN apk update && \
    apk add jq && \
    wget -O /usr/bin/yq "https://github.com/mikefarah/yq/releases/download/3.3.2/yq_linux_amd64" && \
    chmod +x /usr/bin/yq && \
    chmod +x /kit/setVersion.sh && \
    chmod +x /kit/setServers.sh && \
    chmod +x /config.sh

ENTRYPOINT ["sh", "/config.sh"]