FROM nginx:1.20.1-alpine

# Copy nginx config file
COPY ./nginx.conf /etc/nginx/

# Copy web certs files
COPY ./https_certs/web-dev.fletchling.com.crt /etc/ssl/certs/
COPY ./https_certs/web-dev.fletchling.com.key /etc/ssl/private/

# Copy api certs files
COPY ./https_certs/api-dev.fletchling.com.crt /etc/ssl/certs/
COPY ./https_certs/api-dev.fletchling.com.key /etc/ssl/private/

EXPOSE 443

CMD ["nginx", "-g", "daemon off;"]