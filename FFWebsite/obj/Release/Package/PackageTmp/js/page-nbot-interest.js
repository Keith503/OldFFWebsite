$(document).ready(function () {
    $('#grade').change(function () {
        loadParentQuest();
    });
    $('#btnSubmit').click(function () {
        processUpdate();
    });
    $('#btnClear').click(function () {
     //   window.location.reload(false);
       clearform();
    });
   
});  // End of Document Ready function
 

function loadParentQuest() {
    //get selected grade from dropdown 
    var gid = $('#grade').val();
    var htext = "";

    //junior FLL k-3
    if (gid > 0 && gid < 6) {
        activaTab('jrfll');
        document.getElementById("fprogram").value = "Jr FLL";
    }

    // fll 4-6
    if (gid > 5 && gid < 9) {
        activaTab('fll');
        document.getElementById("fprogram").value = "FLL";
    }

    // ftc 7-8
    if (gid > 8 && gid < 11) {     
        activaTab('ftc');
        document.getElementById("fprogram").value = "FTC";
    }

    //FRC 9-12 
    if (gid > 10 && gid < 15) {
        activaTab('frc');
        document.getElementById("fprogram").value = "FRC";
    } 
}


function activaTab(tab) {
    $('.nav-tabs a[href="#' + tab + '"]').tab('show');
};

function processUpdate() {
    //production webserver 
    //   var uri = "api/FrogForce/UpdateNbotInterest";
    var uri = "http://locathost:3000/FrogForce/UpdateNbotInterest";
    var sfn = document.getElementById("sfname").value;
    if (sfn === "") {
        msgbox(-1, "Student First Name Missing!", "You must enter a student first name. Try Again!!");
        document.getElementById("sfname").focus(); 
        return false;
    }
    var sln = document.getElementById("slname").value;
    if (sln === "") {
        msgbox(-1, "Student Last Name Missing!", "You must enter a student last name. Try Again!!");
        document.getElementById("slname").focus(); 
        return false;
    }
    var sch = document.getElementById("school").value;
    if (sch === "0") {
        msgbox(-1, "School Missing!", "You select a school. Try Again!!");
        document.getElementById("school").focus(); 
        return false;
    }
    var g = document.getElementById("grade").value;
    if (g === "0") {
        msgbox(-1, "Grade Missing!", "You select a grade. Try Again!!");
        document.getElementById("school").focus();
        return false;
    }

    x = document.getElementById("gender");
    var gen = x.options[x.selectedIndex].text;
    if (x.value === "0") {
        msgbox(-1, "Gender Missing!", "You select a gender (male or female). Try Again!!");
        document.getElementById("school").focus();
        return false;
    }

    var fpgmname = document.getElementById("fprogram").value;
    var q1 = "";
    var q2 = "";
    var q3 = "";
    var stuemail = "";
    var stuphn = 0;
    var parphn = 0;
    var px = ""; 
    var x;

    var p1name = document.getElementById("pn1name").value;
    if (p1name === "") {
        msgbox(-1, "Parent 1 Name Missing!", "You must enter a parent 1 name. Try Again!!");
        document.getElementById("pn1name").focus();
        return false;
    }
    var p1e = document.getElementById("p1email").value;
    if (p1e === "") {
        msgbox(-1, "Parent 1 eMail Address Missing!", "You must enter a parent 1 eMail address. Try Again!!");
        document.getElementById("p1email").focus();
        return false;
    }
    var p2name = document.getElementById("pn2name").value;
    var p2e = document.getElementById("p2email").value;

    //test to ensure that school and grade match 
    var s1 = parseInt(sch);
    var g1 = parseInt(g); 
    if (s1 < 6) {
        //must be elementary school
        if (g1 > 6) {
            msgbox(-1, "Grade/school mismatch!", "You selected an elementary school but grade in greater than 4th. Try Again!!");
            return false;
        }
    }
    if (s1 === 6) {
        // must be novi meadows 
        if (g1 < 7 || g > 8) {
            msgbox(-1, "Grade/school mismatch!", "You selected Novi Meadows but grade is not 5th or 6th. Try Again!!");
            return false;
        }
    } 
    if (s1 === 7) {
        // must be middle school
        if (g1 < 9 || g > 10) {
            msgbox(-1, "Grade/school mismatch!", "You selected middle school but grade is not 7th or 8th. Try Again!!");
            return false;
        }
    } 
    if (s1 === 8) {
        // must be high school 
        if (g1 < 11) {
            msgbox(-1, "Grade/school mismatch!", "You selected high school but grade is not 9, 10, 11 or 12. Try Again!!");
            return false;
        }
    }

    switch (fpgmname) {
        case "Jr FLL":
            fpgm = 1;
            x = document.getElementById("jrfllq1");
            q1 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 1 Missing!", "Please select an anwser to question 1. Try Again!!");
                document.getElementById("jrfllq1").focus();
                return false;
            }
            x = document.getElementById("jrfllq2");
            q2 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 2 Missing!", "Please select an anwser to question 2. Try Again!!");
                document.getElementById("jrfllq2").focus();
                return false;
            }
            x = document.getElementById("jrfllq3");
            q3 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 3 Missing!", "Please select an anwser to question 3. Try Again!!");
                document.getElementById("jrfllq3").focus();
                return false;
            }
            break;
        case "FLL":
            fpgm = 2;
            x = document.getElementById("fllq1");
            q1 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 1 Missing!", "Please select an anwser to question 1. Try Again!!");
                document.getElementById("fllq1").focus();
                return false;
            }
            x = document.getElementById("fllq2");
            q2 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 2 Missing!", "Please select an anwser to question 2. Try Again!!");
                document.getElementById("fllq2").focus();
                return false;
            }
            x = document.getElementById("fllq3");
            q3 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 3 Missing!", "Please select an anwser to question 3. Try Again!!");
                document.getElementById("fllq3").focus();
                return false;
            }
            break;
        case "FTC":
            fpgm = 3;
            x = document.getElementById("ftcq1");
            q1 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 1 Missing!", "Please select an anwser to question 1. Try Again!!");
                document.getElementById("ftcq1").focus();
                return false;
            }
            x = document.getElementById("ftcq2");
            q2 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 2 Missing!", "Please select an anwser to question 2. Try Again!!");
                document.getElementById("ftcq2").focus();
                return false;
            }
            x = document.getElementById("ftcq3");
            q3 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 3 Missing!", "Please select an anwser to question 3. Try Again!!");
                document.getElementById("ftcq3").focus();
                return false;
            }
            px = document.getElementById("ftcpexp").value;
            break;
        case "FRC":
            fpgm = 4;
            stuemail = document.getElementById("frcstuemail").value;
            if (stuemail === "") {
                msgbox(-1, "Student eMail Missing!", "Please enter student email address. Try Again!!");
                document.getElementById("frcstuemail").focus();
                return false;
            }
            stuphn = document.getElementById("frcstuphone").value;
            if (stuphn === "") {
                msgbox(-1, "Student Phone Missing!", "Please enter student phone number. Try Again!!");
                document.getElementById("frcstuphone").focus();
                return false;
            }
            parphn = document.getElementById("frcparphone").value;
            if (parphn.value === "0") {
                msgbox(-1, "Parent Phone Number Missing!", "Please enter parent phone number. Try Again!!");
                document.getElementById("frcparphone").focus();
                return false;
            }

            x = document.getElementById("frcq1");
            q1 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 1 Missing!", "Please select an anwser to question 1. Try Again!!");
                document.getElementById("frcq1").focus();
                return false;
            }
            x = document.getElementById("frcq2");
            q2 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 2 Missing!", "Please select an anwser to question 2. Try Again!!");
                document.getElementById("frcq2").focus();
                return false;
            }
            x = document.getElementById("frcq3");
            q3 = x.options[x.selectedIndex].text;
            if (x.value === "0") {
                msgbox(-1, "Question 3 Missing!", "Please select an anwser to question 3. Try Again!!");
                document.getElementById("frcq3").focus();
                return false;
            }
            px = document.getElementById("frcpexp").value;
            break;
    }
  

    var ni = {
        StudentFirstName: sfn, 
        StudentLastName: sln,
        StudentPhone: stuphn, 
        StudenteMail: stuemail,
        SchoolID: sch, 
        Grade: g,
        Gender: gen, 
        Parent1Name: p1name, 
        Parent1eMail: p1e, 
        Parent1Phone: parphn, 
        Parent2Name: p2name,
        Parent2eMail: p2e,
        FirstProgram: fpgm,
        Question1: q1,
        Question2: q2,
        Question3: q3,
        PriorExperience: px
    };
//production 
//    var testpost = $.post(uri, { "": JSON.stringify(ni) })
    var testpost = $.post(uri, ni)
        .success(function (data) {
            if (confirm('Nbot Interest Form has been successfully saved! - Thank you!!')) {
                clearform();
            } else {
                clearform();
            }
        })
        .error(function (data) {
            msgbox(-1, "Form Save Failed!", "Nbot INterest Form Save failed! " + data);
        });
}

function clearform() {
    window.location.reload(false);

    //var x;
    //document.getElementById("sfname").value = "";
    //document.getElementById("slname").value = "";
    //$('#school select').val('0');
    //$('#school select').data('combobox').refresh();
    //document.querySelector('school [value="0"]').selected = true;
    //document.getElementById("grade").selectedIndex = 0;
    //document.getElementById("gender").selectedIndex = 0;
    //document.getElementById("fprogram").value = "";
    //document.getElementById("pn1name").value = "";
    //document.getElementById("p1email").value = "";
    //document.getElementById("pn2name").value = "";
    //document.getElementById("p2email").value = "";
    //document.getElementById("jrfllq1").selectedIndex = 0;
    //document.getElementById("jrfllq2").selectedIndex = 0;
    //document.getElementById("jrfllq3").selectedIndex = 0;
    //document.getElementById("fllq1").selectedIndex = 0;
    //document.getElementById("fllq2").selectedIndex = 0;
    //document.getElementById("fllq3").selectedIndex = 0;
    //document.getElementById("ftcq1").selectedIndex = 0;
    //document.getElementById("ftcq2").selectedIndex = 0;
    //document.getElementById("ftcq3").selectedIndex = 0;
    //document.getElementById("ftcpexp").value = "";
    //document.getElementById("frcstuemail").value = "";
    //document.getElementById("frcstuphone").value = "";
    //document.getElementById("frcparphone").value = "";
    //document.getElementById("frcq1").selectedIndex = 0;
    //document.getElementById("frcq2").selectedIndex = 0;
    //document.getElementById("frcq3").selectedIndex = 0;
    //document.getElementById("frcpexp").value = "";
}