// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
console.log('connected!');

var net = require('follow-redirects').https;
var fs = require('fs');
var auth_key = Buffer.from('access_key:secret_key').toString('base64');

var options = {
  method: 'GET',
  hostname: 'api.roadgoat.com',
  port: 80,
  path: '/api/v2/destinations/new-york-ny-usa',
  headers: {
    Authorization: `Basic ${auth_key}`,
  },
  maxRedirects: 20,
};

var req = net.request(options, function (res) {
  var chunks = [];

  res.on('data', function (chunk) {
    chunks.push(chunk);
  });

  res.on('end', function (chunk) {
    var body = Buffer.concat(chunks);
    console.log(body.toString());
  });

  res.on('error', function (error) {
    console.error(error);
  });
});

req.end();
