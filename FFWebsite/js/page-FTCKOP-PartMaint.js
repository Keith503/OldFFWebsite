var newFlag = false;
var spinner;

$(document).ready(function () {
    document.getElementById('btnNew').addEventListener('click', processNew, false);
    document.getElementById('btnSav').addEventListener('click', processUpdate, false);
    document.getElementById('btnDel').addEventListener('click', processDelete, false);
    $('#fPartSelect').change(function () { refreshItem(); });

    refreshPartItems();

});  // End of Document Ready function

function refreshPartItems() {
    //
    //*******************************************
    /* Get List of Part Items  
    /********************************************/
    var uri = "api/FTCKOP/GetPartList/";
    $.getJSON(uri, function (data) {
        $('#fPartSelect').empty();
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.PartID + '">' + item.PartID + '-' + item.Description + '</option>');
            $('#fPartSelect').append(newOption);
        });
        //hiderefreshbtn("#gtbtn", "Done");
        document.getElementById("fPartSelect").selectedIndex = 0;
        $('#fPartSelect').trigger("change");
    }) // End Json Call 
        // Optional - fires when operation completes with error
        .error(function (jqXHR, textStatus, errorThrown) {
            msgbox(-1, "GetPartList Failed!", 'Error Occurred: ' + jqXHR.responseText);
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
    var nid = $('#fPartSelect').val();
    var uri = "api/FTCKOP/GetPart/" + nid;
    $.getJSON(uri, function (data) {
        document.getElementById("fID").value = data.PartID;
        document.getElementById("fDesc").value = data.Description;
        document.getElementById("fUnit").value = data.UnitCost;
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
    document.getElementById("fUnit").value = 0;
    newFlag = true;
}

function processUpdate() {
    var uri = "api/FTCKOP/UpdatePart";
    var id = document.getElementById("fID").value;
    var d = document.getElementById("fDesc").value;
    var c = document.getElementById("fUnit").value;

    var ni = {
        PartID: id,
        Description: d,
        UnitCost: c
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "Part Updated Successfully!", "Part was successfully updated!");
            //if this was a new add, then refresh the selection box to include it 
            if (newFlag) {
                refreshPartItems();
            }
            newFlag = false;
        })
        .error(function (data) {
            msgbox(-1, "Part Updated Failed!", "Part Update failed! " + data);
        });
}

function processDelete() {
    var uri = "api/FTCKOP/RemovePart";
    BootstrapDialog.confirm('Are you sure you want to delete this part?', function (result) {
        if (result) {
            var id = document.getElementById("fID").value;
            var ni = {
                PartID: id
            };
            var testpost = $.post(uri, { "": JSON.stringify(ni) })
                .success(function (data) {
                    msgbox(0, "Part Deleted Successfully!", "Part was successfully deleted!");
                    refreshPartItems();
                })
                .error(function (data) {
                    msgbox(-1, "Part Delete Failed!", "Part delete failed! " + data);
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
