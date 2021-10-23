# Install npm dependencies
FROM node:alpine AS base
WORKDIR /app
COPY package.json .
COPY package-lock.json .
RUN npm install

# Dev image
FROM base AS dev
CMD ["npm", "run", "dev-docker"]

# Rebuild the source code only when needed
FROM node:alpine AS build
WORKDIR /app
COPY . .
COPY --from=base /app/node_modules ./node_modules
RUN npm run build

# Production image
FROM node:alpine AS prod
ENV NEXT_TELEMETRY_DISABLED 1

# Create non-root user
RUN addgroup -g 1001 -S web-group && \
    adduser -u 1001 -S web-user
USER web-user

WORKDIR /app
# Copy files
COPY --from=build /app/next.config.js .
COPY --from=build /app/public ./public
COPY --from=build --chown=web-user:web-group /app/.next ./.next
COPY --from=build /app/node_modules ./node_modules
COPY --from=build /app/package.json ./package.json

# Run NEXT
CMD ["npm", "run", "start-docker"]