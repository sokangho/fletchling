FROM nginx:1.20.1-alpine

# Copy nginx config file
COPY ./nginx.conf /etc/nginx/

# Copy cert files
COPY ./https_certs/dev.fletchling.com.crt /etc/ssl/certs/
COPY ./https_certs/dev.fletchling.com.key /etc/ssl/private/

EXPOSE 443

CMD ["nginx", "-g", "daemon off;"]