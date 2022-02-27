module.exports = {
  swcMinify: true, // use swc to minify javascript
  images: {
    domains: ['abs.twimg.com', 'pbs.twimg.com']
  },
  webpackDevMiddleware: (config) => {
    config.watchOptions = {
      poll: 1000,
      aggregateTimeout: 300
    };
    return config;
  }
};
