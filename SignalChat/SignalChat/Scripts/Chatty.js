/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery-ui-1.9.0.js" />
/// <reference path="jquery.signalR-0.5.3.js" />
$(function() {
    var name;
    var chatWindows = $(".chat-window");
    var messageWindow = $("#message");
    $("#dialog").dialog(
        {
            title: "What is your chatty name?",
            buttons: {
                "What? My name is!": function() {
                    name = $("#name").val();
                    if (name) {
                        $(this).dialog("close");
                    }
                    chatWindows.append('<div class="welcome">Welcome ' + name + '!</div>');
                }
            },
            modal: true,
            beforeClose: function(event, ui) {
                if (!name) return false;
                return true;
            }
        });
    var chatty = $.connection.chatty;
    $.extend(chatty, {
        spoke: function(user, message) {
            chatWindows.append('<div><span class="user-name">' + user + ' said: <span class="words">' + message + '</span></div>');
        }
    });
    $("#speak").click(function() {
        if (messageWindow.val()) {
            chatty.speak(name, messageWindow.val());
            messageWindow.val("");
        }
    });
    $.connection.hub.start();
});