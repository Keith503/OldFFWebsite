var spinner;
var spinner2;

$(document).ready(function () {
    document.getElementById('btnAddPart').addEventListener('click', processInventoryUpdate, false);
    document.getElementById('btnAddKit').addEventListener('click', processKitUpdate, false);
    document.getElementById('btnAllocPart').addEventListener('click', processPartAlloc, false);
    document.getElementById('btnAllocKit').addEventListener('click', processKitAlloc, false);
        
    initPartTable();
    loadKitDropdown();
    loadKitDropdown2();
    loadInvTable();
    loadPartDropdown();
    loadTeamDropdown();
    loadTeamDropdown2();
});

function initPartTable() {
    if (!$.fn.dataTable.isDataTable('#part-table')) {
        var table = $('#part-table').DataTable({
            dom: 'T<"clear">lfrtip',
            bFilter: false,
            "paging": true,
            "info": false,
            "order": [[2,"asc"]],
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Part ID", "sWidth": "50px" },
                { "sTitle": "Description", "sWidth": "250px" },
                { "sTitle": "Quantity", "sWidth": "70px" },
                { "sTitle": "Distributed", "sWidth": "70px" }
            ]
        });
        //setup callback function if row is clicked 
        $('#part-table tbody').on('click', 'tr', function (eventObject) {
            var aData = table.row(this).data();
            if ($(this).hasClass('wireSelected')) {
                //not going to allow for toggling or unselecting of a selected row.
                table.$('tr.wireSelected').removeClass('wireSelected');
                TableitemSelected(aData);
            }
            else {
                table.$('tr.wireSelected').removeClass('wireSelected');
                $(this).addClass('wireSelected');
                TableitemSelected(aData);
            }
        });
    }
}

function loadInvTable() {
    //turn on the spinner 
    spinner = displaySpinner('spin1');

    var uri = "api/FTCKOP/GetInventoryList/";
    var tableData = [];
    var i = 0;

    $.getJSON(uri, function (data) {
        $.each(data, function (key, item) {
            tableData.push(formatinvrow(item));
        });
        //add row from array to table 
        var t = $("#part-table").DataTable();
        t.clear();
        t.rows.add(tableData).draw();
        if (spinner !== null) {
            spinner.stop();
            spinner = null;
        }
    }) //End JSON call 
    .error(function (jqXHR, textStatus, errorThrown) {
        ErrorMsgBox("Error loadInvTable()!", jqXHR.responseJSON, jqXHR.status);
    });
}  // End loadInvTable

function formatinvrow(item) {
    return [item.PartID, item.Description, item.Quantity, item.Quantity_Distributed];
}

function TableitemSelected(item) {
    if (item !== null) {
        document.getElementById("apPartid").value = item[0]; 
        document.getElementById("apPartname").value = item[1];
        document.getElementById("apQty").value = 0;
        $('#partAllocModal').modal('show');
    } // end if 
}


function loadPartDropdown() {
    var uri = "api/FTCKOP/GetPartList/";
 
    $.getJSON(uri, function (data) {
        $('#partdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.PartID + '">' + item.PartID + '-' + item.Description + '</option>');    //TODO: what is the members of the data coming back?
            $('#partdropdown').append(newOption);
        }
        );
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadPartDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });

}  // End loadPartDropdown

function processInventoryUpdate() {
    var uri = "api/FTCKOP/UpdateInventory";
    var pid = $('#partdropdown').val();              //get part id 
    var q = document.getElementById("fQty").value;
    var d = 0;

    var ni = {
        PartID: pid,
        Quantity: q,
        Quantity_Distributed: d
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "Inventory Updated Successfully!", "Part was successfully added to Inventory!");
            myModal.hide;
            //if this was a new add, then refresh the selection box to include it
            if (newFlag) {
                loadInvTable();
            }
            newFlag = false;
        })
        .error(function (data) {
            msgbox(-1, "Inventory Updated Failed!", "Part was not added to Inventory! " + data);
        });
}

function loadKitDropdown() {
    var uri = "api/FTCKOP/GetKitList/";

    $.getJSON(uri, function (data) {
        $('#kitdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.KitID + '">' + item.KitID + '-' + item.Description + '</option>');    //TODO: what is the members of the data coming back?
            $('#kitdropdown').append(newOption); 
        }
        );
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadKitDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });

}  // End loadKitDropdown

function loadKitDropdown2() {
    var uri = "api/FTCKOP/GetKitList/";

    $.getJSON(uri, function (data) {
        $('#kakitdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.KitID + '">' + item.KitID + '-' + item.Description + '</option>');  
            $('#kakitdropdown').append(newOption);
        }
        );
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadKitDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });

}  // End loadKitDropdown

function processKitUpdate() {
    var uri = "api/FTCKOP/UpdateKitInventory/";
    var kid = $('#kitdropdown').val();              //get kit id 
    var q = document.getElementById("kQty").value;

    var ni = {
        KitID: kid,
        Quantity: q 
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "Kit Inventory Updated Successfully!", "Kit was successfully added to Inventory!");
            //if this was a new add, then refresh the selection box to include it 
            if (newFlag) {
                loadInvTable();
            }
            newFlag = false;
        })
        .error(function (data) {
            msgbox(-1, "Kit Inventory Updated Failed!", "Kit was not added to Inventory! " + data);
        });
}

function loadTeamDropdown() {
    var uri = "api/FTCKOP/GetTeamList/";

    $.getJSON(uri, function (data) {
        $('#t1dropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.TeamID + '">' + item.TeamID + '-' + item.TeamName + '</option>');    //TODO: what is the members of the data coming back?
            $('#t1dropdown').append(newOption);
        }
        );
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadTeamDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });

}  // End loadTeamDropdown

function loadTeamDropdown2() {
    var uri = "api/FTCKOP/GetTeamList/";

    $.getJSON(uri, function (data) {
        $('#kateamdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.TeamID + '">' + item.TeamID + '-' + item.TeamName + '</option>');
            $('#kateamdropdown').append(newOption);
        }
        );
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadTeamDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });

}  // End loadTeamDropdown

function processPartAlloc() {
    var uri = "api/FTCKOP/ProcessPartAllocation/";
    var pid = document.getElementById("apPartid").value;
    var tid = $('#t1dropdown').val();              //get kit id 
    var q = document.getElementById("apQty").value;

    var ni = {
        PartID: pid,
        TeamID: tid, 
        Quantity: q
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "Part Allocated to Team Successfully!", "Part was successfully added to Team Inventory!");
            //if this was a new add, then refresh the selection box to include it 
            if (newFlag) {
                loadInvTable();
            }
            newFlag = false;
        })
        .error(function (data) {
            msgbox(-1, "Part Allocation Failed!", "Part was not added to Team Inventory! " + data.responseText);
        });
}

function showKitAlloc() {
    $('#"kitAllocModal').modal('show');
}


function processKitAlloc() {
    var uri = "api/FTCKOP/ProcessKitAllocation/";
    var tid = $('#kateamdropdown').val();              //get Team id 
    var kid = $('#kakitdropdown').val();              //get Team id 
    var q = document.getElementById("kaQty").value;

    var ni = {
        PartID: kid,       //borrow the part id field and store the kit id in it 
        TeamID: tid,
        Quantity: q
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "Kit Allocated to Team Successfully!", "Kit was successfully added to Team Inventory!");
            //if this was a new add, then refresh the selection box to include it 
            if (newFlag) {
                loadInvTable();
            }
            newFlag = false;
        })
        .error(function (data) {
            msgbox(-1, "Kit Allocation Failed!", "Kit was not added to Team Inventory! " + data.responseText);
        });
}
