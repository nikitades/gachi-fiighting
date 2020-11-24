const express = require('express');
const app = express();
const port = 3000;

app.use(express.static('dist'));
app.use('/signalr', express.static('node_modules/@microsoft/signalr/dist/browser'));

app.listen(port, () => {
    console.log(`Gachi fighting game started at ${port}`);
})