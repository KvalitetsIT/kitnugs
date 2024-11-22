FROM swaggerapi/swagger-ui:v4.12.0

# Definer bruger- og gruppe-id
ARG USER_ID=1000
ARG GROUP_ID=1000

# Opret non-root gruppe og bruger med specifikke id'er
RUN addgroup -g ${GROUP_ID} appgroup && \
    adduser -u ${USER_ID} -G appgroup -S appuser
    
COPY target/*.yaml /usr/share/nginx/html/

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
    chmod +x /config.sh && \
    chown -R ${USER_ID}:${GROUP_ID} /kit /usr/share/nginx/html

# Skift til non-root bruger med specifikke id'er
USER ${USER_ID}:${GROUP_ID}

ENTRYPOINT ["sh", "/config.sh"]
