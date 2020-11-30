class GameSession {
    blockId;
    gameId = 0;
    playerName;
    connection;
    eventsLog = [];
    lastReceivedAt = new Date();
    delays = [];
    frames = {};

    constructor(blockId, playerName) {
        this.blockId = blockId;
        this.playerName = playerName;

        this.connection = this.buildConnection(this.gameId, this.playerName);
        this.connectEvents();
    }

    async start() {
        try {
            await this.connection.start();
            this.drawLogs(`Connected player ${this.playerName} to a game ${this.gameId}`);
        } catch (err) {
            this.drawLogs(err);
            setTimeout(this.start.bind(this), 5000);
        }
    }

    connectEvents() {
        this.connection.onclose(this.start.bind(this));
        this.connection.on("Send", this.onSend.bind(this));
    }

    onSend() {
        if (this.eventsLog.length > 15) {
            this.eventsLog.shift();
        }
        if (this.delays.length > 15) {
            this.delays.shift();
        }
        const curDate = new Date();
        this.delays.push(curDate - this.lastReceivedAt);
        this.eventsLog.push(arguments);
        this.lastReceivedAt = curDate;
        this.drawLogs();
        this._handeFrames();
        this.drawFrames();
        this.drawFrames5sec();
    }

    drawLogs() {
        document.getElementById('box' + this.blockId).innerText = JSON.stringify(this.eventsLog, null, 4);
    }

    drawFrames() {
        const framesToDraw = Object
            .values(this.frames)
            .slice(1, -1);

        const framesToDrawSummarized = framesToDraw.reduce((value, storage) => storage += value, 0);
        document.getElementById('delay' + this.blockId).innerText = Math.round(framesToDrawSummarized / framesToDraw.length);
    }

    drawFrames5sec() {
        const framesToDraw = Object
            .values(this.frames)
            .slice(1, -1)
            .slice(0, 5);

        const framesToDrawSummarized = framesToDraw.reduce((value, storage) => storage += value, 0);
        document.getElementById('delay5' + this.blockId).innerText = framesToDrawSummarized / framesToDraw.length;
    }

    _handeFrames() {
        const curSecond = new Date().getTime().toString().slice(0, -3);
        if (!this.frames[curSecond]) {
            this.frames[curSecond] = 0;
        }
        if (Object.values(this.frames).length > 120) {
            delete this.frames[Object.keys(this.frames)[0]];
        }
        this.frames[curSecond]++;
    }

    buildConnection(gameId, playerName) {
        return new signalR.HubConnectionBuilder()
            .withUrl(`http://0.0.0.0:5000/game?gameId=${gameId}&playerName=${playerName}`)
            .configureLogging(signalR.LogLevel.Information)
            .build();
    }
}