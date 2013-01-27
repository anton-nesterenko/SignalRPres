$(function() {
    var chattyHub = $.connection.chatty;
    // spoke, secret, connected
    var chatWindow = $("#chat-window");

    $.extend(chattyHub.client,
        {
            spoke: function(data) {
                chatWindow.append("<span>" + data + "</span>");
            },
            secret: function(data) {
                chatWindow.append("<span class='secret'>" + data + "</span>");
            },
            connected: function(data) {
                chatWindow.append("<span>" + data + "</span>");
            }
        });

    var message = $("#message");
    var button = $("#speak");
    button.click(function() {
        var text = message.val();
        chattyHub.server.speak(text);
        message.val("");
        chatWindow.append("<span>I said: " + text + "</span>");
    });
    $.connection.hub.start();
});