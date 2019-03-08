function displayNotification(mesaj, messagetype) {

    var alert;
    var icerik;

    if (messagetype === 'success') {
        alert = $("#dialog-notifications-success");
        icerik = $("#dialog-notifications-success .mesaj");
    } else {
        alert = $("#dialog-notifications-danger");
        icerik = $("#dialog-notifications-danger .mesaj");
    }

    alert.finish();

    icerik.html(mesaj);
    alert.fadeIn(500).delay(7000).fadeOut(500);
};

function displayPopupNotification(message, messagetype) {
    //types: success, error
    var container;
    var icerik;
    

    if (messagetype === 'error') {
        //error
        container = $('#dialog-notifications-danger');
        icerik = $('#dialog-notifications-danger .mesaj');
    }
    else {
        //other
        container = $('#dialog-notifications-success');
        icerik = $('#dialog-notifications-success .mesaj');
    }

    //we do not encode displayed message
    var htmlcode = '';
    if ((typeof message) === 'string') {
        htmlcode = message;
    } else {
        for (var i = 0; i < message.length; i++) {
            htmlcode = htmlcode + '<p>' + message[i] + '</p>';
        }
    }

    container.finish();
    icerik.html(htmlcode);
    container.show().animate({ "top": "10%", "opacity": "1" }).delay(5000).animate({ "top": "0", "opacity": "0" }, function() {

    });
}

function mesajGoster(message, messagetype) {
    //types: success, error
    var container;
    var icerik;


    if (messagetype === 'error') {
        //error
        container = $('#dialog-notifications-danger');
        icerik = $('#dialog-notifications-danger .mesaj');
    }
    else {
        //other
        container = $('#myModalSuccess');
        icerik = $('#myModalSuccess .modal-body');
    }

    //we do not encode displayed message
    var htmlcode = '';
    if ((typeof message) === 'string') {
        htmlcode = message;
    } else {
        for (var i = 0; i < message.length; i++) {
            htmlcode = htmlcode + '<p>' + message[i] + '</p>';
        }
    }

    //container.finish();
    icerik.html(htmlcode);
    container.modal({
        backdrop: 'static',
        show: true
    });
}


