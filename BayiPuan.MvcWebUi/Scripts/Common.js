
//<![CDATA[
$(window).on("load", function () { // makes sure the whole site is loaded
    $('#status').fadeOut(); // will first fade out the loading animation
    $('#preloader').delay(0).fadeOut('slow'); // will fade out the white DIV that covers the website.
    $('body').delay(0).css({ 'overflow': 'visible' });
})
//]]>

// Get the Sidebar
var mySidebar = document.getElementById("mySidebar");
// Get the DIV with overlay effect
var overlayBg = document.getElementById("myOverlay");

// Toggle between showing and hiding the sidebar, and add overlay effect
function w3_open() {
    if (mySidebar.style.display === 'block') {
        mySidebar.style.display = 'none';
        overlayBg.style.display = "none";
    } else {
        mySidebar.style.display = 'block';
        overlayBg.style.display = "block";
    }


    // Close the sidebar with the close button
    function w3_close() {
        mySidebar.style.display = "none";
        overlayBg.style.display = "none";
    }
}

function myFunction(id) {
    var x = document.getElementById(id);
    var menu = [
        "Main.ProductActions", "Main.AccountActions", "Main.Definitions", "Main.Reports", "Main.SaleActions", "Main.Gifts", "Main.Companies"
    ];
    if (x.className.indexOf("w3-show") === -1) {
        x.className += " w3-show";
        x.previousElementSibling.className += " w3-red";
    for (var i = 0; i < menu.length; i++) {
        if (menu[i] !== x.id) {
            document.getElementById(menu[i]).className = document.getElementById(menu[i]).className.replace(" w3-show", "");
            document.getElementById(menu[i]).previousElementSibling.className = document.getElementById(menu[i]).previousElementSibling.className.replace(" w3-red", "");
        }
    }
    } else {
        x.className = x.className.replace(" w3-show", "");
        x.previousElementSibling.className =
            x.previousElementSibling.className.replace(" w3-red", "");
    }
}