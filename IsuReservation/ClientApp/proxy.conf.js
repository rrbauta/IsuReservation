const {env} = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:45371';

const PROXY_CONFIG = [
  {
    context: [
      "/api/reservations",
      "/api/contacts",
      "/api/destinations",
    ],
    target: target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
