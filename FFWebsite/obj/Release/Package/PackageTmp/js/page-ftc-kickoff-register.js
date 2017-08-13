$(document).ready(function () {
    $('#btnSubmit').click(function () {
        processUpdate();
    });
    $('#btnClear').click(function () {
     //   window.location.reload(false);
       clearform();
    });
   
});  // End of Document Ready function
 

function processUpdate() {
    var uri = "api/FrogForce/FTCKickoffRegister";
    var steamNo = document.getElementById("sTeam").value;
    if (steamNo === "") {
        msgbox(-1, "Team Number Missing!", "You must enter a team number. Try Again!!");
        document.getElementById("sTeam").focus(); 
        return false;
    }
    var steamName = document.getElementById("stname").value;
    if (steamName === "") {
        msgbox(-1, "Team Name Missing!", "You must enter a Team Name. Try Again!!");
        document.getElementById("stname").focus(); 
        return false;
    }
    var sexp = document.getElementById("sexperience").value;
    if (sexp === "0") {
        msgbox(-1, "Team Experience Missing!", "You must indicate team experience. Try Again!!");
        document.getElementById("sexperience").focus(); 
        return false;
    }
   
    var sSchool = document.getElementById("sschool").value;
    if (sSchool === "") {
        msgbox(-1, "School Affiliation Missing!", "You must enter a school affiliation. Try Again!!");
        document.getElementById("sschool").focus();
        return false;
    }
    var sContact = document.getElementById("stcname").value;
    if (sContact === "") {
        msgbox(-1, "Team Contact Name Missing!", "You must enter a team contact name. Try Again!!");
        document.getElementById("stcname").focus();
        return false;
    }

    var sContactemail = document.getElementById("p1email").value;
    if (sContactemail === "") {
        msgbox(-1, "Team Contact eMail Missing!", "You must enter a team contact eMail address. Try Again!!");
        document.getElementById("p1email").focus();
        return false;
    }

    var sContactphone = document.getElementById("sphone").value;
    if (sContactphone === "") {
        msgbox(-1, "Team Contact Phone Missing!", "You must enter a team contact phone number. Try Again!!");
        document.getElementById("sphone").focus();
        return false;
    }

    var sCoachCount = document.getElementById("scoachcount").value;
    if (sCoachCount === "") {
        msgbox(-1, "Coach/Mentor count is Missing!", "You must enter a coach/mentor count. Try Again!!");
        document.getElementById("scoachcount").focus();
        return false;
    }

    var s1 = parseInt(sCoachCount);
    if (s1 > 10) {
        msgbox(-1, "Coach/Mentor count greater than 10!", "Coach/mentor count greater than 10. Try Again!!");
        document.getElementById("scoachcount").focus();
        return false;
    }

    var sStudentCount = document.getElementById("scount").value;
    if (sStudentCount === "") {
        msgbox(-1, "Student count is Missing!", "You must enter a student count. Try Again!!");
        document.getElementById("scount").focus();
        return false;
    }

    var s1 = parseInt(sStudentCount);
    if (s1 > 15) {
        msgbox(-1, "Student count greater than 15!", "Student count greater than 15. Try Again!!");
        document.getElementById("scount").focus();
        return false;
    }
   
    var ni = {
        ID: steamNo, 
        TeamName: steamName,
        Experience: sexp, 
        School: sSchool,
        TeamContactName: sContact, 
        TeamContactEmail: sContactemail,
        TeamContactPhone: sContactphone, 
        MentorCount: sCoachCount, 
        StudentCount: sStudentCount
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

function clearform() {
    window.location.reload(false);

}