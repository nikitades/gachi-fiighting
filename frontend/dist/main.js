const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://0.0.0.0:5000/chat")
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
    console.log(arguments);
});

start();