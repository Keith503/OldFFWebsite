var newFlag = false;
var spinner;

$(document).ready(function () {
    document.getElementById('btnNew').addEventListener('click', processNew, false);
    document.getElementById('btnSav').addEventListener('click', processUpdate, false);
    document.getElementById('btnDel').addEventListener('click', processDelete, false);
    $('#fTeamSelect').change(function () { refreshItem(); });

    refreshTeams();

});  // End of Document Ready function

function refreshTeams() {
    //
    //*******************************************
    /* Get List of Teams  
    /********************************************/
    var uri = "api/FTCKOP/GetTeamList/";
    $.getJSON(uri, function (data) {
        $('#fTeamSelect').empty();
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.TeamID + '">' + item.TeamID + '-' + item.TeamName + '</option>');
            $('#fTeamSelect').append(newOption);
        });
        //hiderefreshbtn("#gtbtn", "Done");
        document.getElementById("fTeamSelect").selectedIndex = 0;
        $('#fTeamSelect').trigger("change");
    }) // End Json Call 
        // Optional - fires when operation completes with error
        .error(function (jqXHR, textStatus, errorThrown) {
            msgbox(-1, "GetTeamList Failed!", 'Error Occurred: ' + jqXHR.responseText);
            //hiderefreshbtn("#gtbtn", "Done");
        });
}

function refreshItem() {
    //
    //*******************************************
    /* Refresh screen with selected part   
    /********************************************/
    //turn on the spinner 
    startSpinner();
    var nid = $('#fTeamSelect').val();
    var uri = "api/FTCKOP/GetTeam/" + nid;
    $.getJSON(uri, function (data) {
        document.getElementById("fID").value = data.TeamID;
        document.getElementById("fDesc").value = data.TeamName;
        stopSpinner();
    }) // End Json Call 
        // Optional - fires when operation completes with error
        .error(function (jqXHR, textStatus, errorThrown) {
            msgbox(-1, "refreshItem Failed!", 'Error Occurred: ' + jqXHR.responseText);
            stopSpinner();
        });
}


function processNew() {
    document.getElementById("fID").value = "";
    document.getElementById("fDesc").value = "";
    newFlag = true;
}

function processUpdate() {
    var uri = "api/FTCKOP/UpdateTeam";
    var id = document.getElementById("fID").value;
    var d = document.getElementById("fDesc").value;

    var ni = {
        TeamID: id,
        TeamName: d
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "Team Updated Successfully!", "Team was successfully updated!");
            //if this was a new add, then refresh the selection box to include it 
            if (newFlag) {
                refreshTeams();
            }
            newFlag = false;
        })
        .error(function (data) {
            msgbox(-1, "Team Updated Failed!", "Team Update failed! " + data);
        });
}

function processDelete() {
    var uri = "api/FTCKOP/RemoveTeam";
    BootstrapDialog.confirm('Are you sure you want to delete this team?', function (result) {
        if (result) {
            var id = document.getElementById("fID").value;
            var ni = {
                TeamID: id
            };
            var testpost = $.post(uri, { "": JSON.stringify(ni) })
                .success(function (data) {
                    msgbox(0, "Team Deleted Successfully!", "Team was successfully deleted!");
                    refreshTeams();
                })
                .error(function (data) {
                    msgbox(-1, "Team Delete Failed!", "Team delete failed! " + data);
                });
        } //end if 
    } // End bootstrap function 
    )// End Boot strap Dialog
}

function startSpinner() {
    spinner = displaySpinner('spin1');
}

function stopSpinner() {
    if (spinner !== null) {
        spinner.stop();
        spinner = null;
    }
}
