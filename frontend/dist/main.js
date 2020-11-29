const playerName = new URLSearchParams(window.location.search).get('playerName') || 'Player1';

console.log("Starting with " + playerName);

const connection = new signalR.HubConnectionBuilder()
    .withUrl(`http://0.0.0.0:5000/game?user=${playerName}&game=2`)
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

connection.onclose(start);

connection.on("Send", function () {
    console.log(...arguments);
});

start();