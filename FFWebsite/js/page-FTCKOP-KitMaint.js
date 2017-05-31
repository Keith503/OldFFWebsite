var newFlag = false;
var spinner;

$(document).ready(function () {
    document.getElementById('btnNew').addEventListener('click', processNew, false);
    document.getElementById('btnSav').addEventListener('click', processUpdate, false);
    document.getElementById('btnDel').addEventListener('click', processDelete, false);
    $('#fKitSelect').change(function () { refreshItem(); });

    refreshKits();

});  // End of Document Ready function

function refreshKits() {
    //
    //*******************************************
    /* Get List of Kits  
    /********************************************/
    var uri = "api/FTCKOP/GetKitList/";
    $.getJSON(uri, function (data) {
        $('#fKitSelect').empty();
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.KitID + '">' + item.KitID + '-' + item.Description + '</option>');
            $('#fKitSelect').append(newOption);
        });
        //hiderefreshbtn("#gtbtn", "Done");
        document.getElementById("fKitSelect").selectedIndex = 0;
        $('#fKitSelect').trigger("change");
    }) // End Json Call 
        // Optional - fires when operation completes with error
        .error(function (jqXHR, textStatus, errorThrown) {
            msgbox(-1, "GetKitList Failed!", 'Error Occurred: ' + jqXHR.responseText);
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
    var nid = $('#fKitSelect').val();
    var uri = "api/FTCKOP/GetKit/" + nid;
    $.getJSON(uri, function (data) {
        document.getElementById("fID").value = data.KitID;
        document.getElementById("fDesc").value = data.Description;
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
    var uri = "api/FTCKOP/UpdateKit";
    var id = document.getElementById("fID").value;
    var d = document.getElementById("fDesc").value;

    var ni = {
        KitID: id,
        Description: d
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "Kit Updated Successfully!", "Kit was successfully updated!");
            //if this was a new add, then refresh the selection box to include it 
            if (newFlag) {
                refreshKits();
            }
            newFlag = false;
        })
        .error(function (data) {
            msgbox(-1, "Kit Updated Failed!", "Kit Update failed! " + data);
        });
}

function processDelete() {
    var uri = "api/FTCKOP/RemoveKit";
    BootstrapDialog.confirm('Are you sure you want to delete this Kit?', function (result) {
        if (result) {
            var id = document.getElementById("fID").value;
            var ni = {
                KitID: id
            };
            var testpost = $.post(uri, { "": JSON.stringify(ni) })
                .success(function (data) {
                    msgbox(0, "Kit Deleted Successfully!", "Kit was successfully deleted!");
                    refreshKits();
                })
                .error(function (data) {
                    msgbox(-1, "Kit Delete Failed!", "Kit delete failed! " + data);
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
