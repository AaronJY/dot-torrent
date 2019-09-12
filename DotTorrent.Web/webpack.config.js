const path = require("path");

module.exports = {
    entry: [
        "babel-polyfill",
        "./Scripts/main"
    ],
    output: {
        publicPath: "/js/",
        path: path.join(__dirname, "/wwwroot/js/"),
        filename: "main.build.js"
    },
    module: {
        loaders: [{
            exclude: /node_modules/,
            loader: "babel-loader",
            query: {
                presets: ["es2015", "stage-1"]
            }
        }]
    },
};