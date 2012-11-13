/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery.signalR-1.0.0-alpha1.js" />
$(function() {
    var button = $("#echoMessage");
    var message = $("#message");

    var echoHub = $.connection.echo;
    $.extend(echoHub.client,
        {
            echo: function(cid, message) {
                if ($.connection.hub.id !== cid)
                    alert(message);
            }
        });
    button.click(function() {
        echoHub.server.echo(message.val());
    });
    $.connection.hub.start();
});