

var SignalrConnection;
var ChatProxy;

var appSignalR = {
    init: function () {

        //This will hold the connection to the signalr hub   
        SignalrConnection = $.hubConnection(ChatServerUrl, {
            useDefaultPath: false
        });
        ChatProxy = SignalrConnection.createHubProxy('NotifyHub');

        //receive data
        ChatProxy.on("sendUnitStatus", function (obj) {
            //$.growl.notice({
            //    title: obj.UnitCode + " : สถานะ " + obj.UnitStatusName,
            //    message: obj.ProjectName,
            //    duration: 7000, location: 'tr'
            //});
            app-appSignalR.changeUnitStatusColor(obj)            
        });

        //connecting the client to the signalr hub   
        SignalrConnection.start().done(function () {
            console.log("Connected to Signalr Server");
        })
            .fail(function () {                
                appSignalR.reconnect();
            })
    },
    reconnect: function () {
        SignalrConnection.start().done(function () {
            console.log("Connected to Signalr Server");
        }).fail(function () {
            app.notify("error", "failed in connecting to the signalr server");
            setTimeout(() => {                                
                appSignalR.reconnect();
            }, 5000);
        })
    },
    sendUnitStatusHubServer: function (obj) {
        try {
            ChatProxy.invoke('sendUnitStatus', obj);
        } catch (e) {
            appSignalR.reconnect();            
        }        
    },
    changeUnitStatusColor: (obj) => {        
        if (obj.UnitStatusName === 'Available') {
            $("#matrix-" + obj.UnitID).css('color', 'black');
            obj.UnitStatusColor = "white";
        }
        else {
            $("#matrix-" + obj.UnitID).css('color', 'transparent');
        }
        $("#matrix-" + obj.UnitID).css('background-color', obj.UnitStatusColor);
    }
}