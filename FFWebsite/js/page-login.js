$(document).ready(function () { 
    $('#btnlogin').click(function () {
        processLogin();
    });
});  // End of Document Ready function


function processLogin() {
    var uri = "api/Auth/Login";
    var uid = document.getElementById("username").value;
    var pwd = document.getElementById("password").value;

    var ni = {
        UserID: uid,
        Password: pwd
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            if (confirm('Registration Form has been successfully saved! - Thank you!!')) {
                // clearform();
            } else {
                // clearform();
            }
        })
        .error(function (data) {
            msgbox(-1, "Form Save Failed!", "Registration Forms was not saved! " + data);
        });
}
