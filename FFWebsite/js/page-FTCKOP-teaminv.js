var spinner;
var spinner2;

$(document).ready(function () {
    loadTeamDropdown();
    initPartTable();
    initCompareTable();
    loadKitDropdown();
    document.getElementById('btnPrint').addEventListener('click', printreport, false);
    $('#teamdropdown').change(function () { loadTeamTable(); });
    $('#tikitdropdown').change(function () { compareKit(); });
    document.getElementById('btnComparePrint').addEventListener('click', printKitCompareReport, false);
    document.getElementById('btnCommit').addEventListener('click', processcommit, false);
   
});

function loadTeamDropdown() {
    var uri = "api/FTCKOP/GetTeamList/";
    //turn on the spinner 
    spinner = displaySpinner('spin1');

    $.getJSON(uri, function (data) {
        $('#teamdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.TeamID + '">' + item.TeamID + '-' + item.TeamName + '</option>');    //TODO: what is the members of the data coming back?
            $('#teamdropdown').append(newOption);
        }
        );
        // This code selects an event to make it easier on everyone 
        //var e = document.getElementById('eventdropdown');
        //e.value = 47;   // st louis - carson
        $('#teamdropdown').trigger('change');
        //end preselect 

        if (spinner !== null) {
            spinner.stop();
            spinner = null;
        }
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadTeamDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });

}  // End loadTeamDropdown


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
                { "sTitle": "Returned", "sWidth": "70px" }
            ]
        });
        //setup callback function if row is clicked 
        //$('#part-table tbody').on('click', 'tr', function (eventObject) {
        //    var aData = table.row(this).data();
        //    if ($(this).hasClass('wireSelected')) {
        //        //not going to allow for toggling or unselecting of a selected row.
        //        table.$('tr.wireSelected').removeClass('wireSelected');
        //        TableitemSelected(aData);
        //    }
        //    else {
        //        table.$('tr.wireSelected').removeClass('wireSelected');
        //        $(this).addClass('wireSelected');
        //        TableitemSelected(aData);
        //   }
        //});
    }
}

function loadTeamTable() {
    var nid = $('#teamdropdown').val();
    var uri = "api/FTCKOP/GetTeamInventoryList/" + nid;
    var tableData = [];
    var i = 0;

    $.getJSON(uri, function (data) {
        $.each(data, function (key, item) {
            tableData.push(formatPartrow(item));
        });
        //add row from array to table 
        var t = $("#part-table").DataTable();
        t.clear();
        t.rows.add(tableData).draw();
    }) //End JSON call 
    .error(function (jqXHR, textStatus, errorThrown) {
        ErrorMsgBox("Error loadTeamTable()!", jqXHR.responseJSON, jqXHR.status);
    });
}  // End loadteamTable

function formatPartrow(item) {
    return [item.PartID, item.Description, item.Quantity, item.Returned];
}

function TableitemSelected(item) {
 //   if (item !== null) {
 //       var nid = $('#eventdropdown').val();
 //       var uri = "api/frogforce/GetScoutingScoresforTeam/" + item[0] + "?eventid=" + nid;
 //       var tableData = [];
 //       var tableData2 = [];
//
 //       document.getElementById("p1name").innerHTML = "<h5>Team: " + item[0] + " - " + item[1] + "</h5>";
 //       document.getElementById("p2name").innerHTML = "<h5>Team: " + item[0] + " - " + item[1] + "</h5>";
//
//        var options = document.getElementById('teamdropdown').options;
//        //change team drop down box to match 
//        for (var i = 0, n = options.length; i < n; i++) {
 //           if (options[i].value === item[0]) {
//                document.getElementById('teamdropdown').selectedIndex = i;
//                break;
//            }
//        }
//
//        $.getJSON(uri, function (data) {
//            $.each(data, function (key, item) {
//                tableData.push(formatscorerow(item));
//                tableData2.push(formatteleoprow(item));
//            });
//            //add row from array to table 
//            var t = $("#score-table").DataTable();
//            t.clear();
//            t.rows.add(tableData).draw();
//
//            var t2 = $("#teleop-table").DataTable();
//            t2.clear();
//            t2.rows.add(tableData2).draw();
//
//        }) //End JSON call 
//      .error(function (jqXHR, textStatus, errorThrown) {
//          ErrorMsgBox("Error TableitemSelected()!", jqXHR.responseJSON, jqXHR.status);
//      });
//    } // end if 
}

function TeamSelected() {
//    var nid = $('#teamdropdown').val();  
//    var eid = $('#eventdropdown').val();
//    var uri = "api/frogforce/GetScoutingScoresforTeam/" + nid + "?eventid=" + eid;
//
//    var tableData = [];
//    var tableData2 = [];
//    var i = 0;
//    //turn on the spinner 
//    spinner2 = displaySpinner('spin2');
//
//   document.getElementById("p1name").innerHTML = "<h5>Team: " + nid + " - " + " " + "</h5>";
//    document.getElementById("p2name").innerHTML = "<h5>Team: " + nid + " - " + " " + "</h5>";
//
//    $.getJSON(uri, function (data) {
//        $.each(data, function (key, item) {
//            tableData.push(formatscorerow(item));
//            tableData2.push(formatteleoprow(item));
//        });
//        //add row from array to table 
//        var t = $("#score-table").DataTable();
//        t.clear();
//        t.rows.add(tableData).draw();
//
//        var t2 = $("#teleop-table").DataTable();
//        t2.clear();
//        t2.rows.add(tableData2).draw();
//
//        if (spinner2 !== null) {
//            spinner2.stop();
//            spinner2 = null;
//        }
//    }) //End JSON call 
//  .error(function (jqXHR, textStatus, errorThrown) {
//      ErrorMsgBox("Error TeamSelected()!", jqXHR.responseJSON, jqXHR.status);
//  });
}

function initCompareTable() {
    if (!$.fn.dataTable.isDataTable('#compare-table')) {
        var table = $('#compare-table').DataTable({
            dom: 'T<"clear">lfrtip',
            bFilter: false,
            "paging": true,
            "info": false,
            "order": [[2, "asc"]],
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Kit Name", "sWidth": "100px" },
                { "sTitle": "Part ID", "sWidth": "60px" },
                { "sTitle": "Description" },
                { "sTitle": "Kit Quantity", "sWidth": "60px" },
                { "sTitle": "Team Quantity", "sWidth": "60px" },
                { "sTitle": "Diff", "sWidth": "60px" }
            ]
        });
        //setup callback function if row is clicked 
        $('#compare-table tbody').on('click', 'tr', function (eventObject) {
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

function loadKitDropdown() {
    var uri = "api/FTCKOP/GetKitList/";

    $.getJSON(uri, function (data) {
        $('#tikitdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.KitID + '">' + item.KitID + '-' + item.Description + '</option>');    //TODO: what is the members of the data coming back?
            $('#tikitdropdown').append(newOption);
        }
        );
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadKitDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });

}  // End loadKitDropdown

function compareKit() {
    var tid = $('#teamdropdown').val();
    var kid = $('#tikitdropdown').val();     //kit id
    var uri = "api/FTCKOP/GetKitCompare/" + kid + "?team=" + tid;

    var tableData = [];
    var i = 0;
    //turn on the spinner 
    //spinner2 = displaySpinner('spin2');

    $.getJSON(uri, function (data) {
        $.each(data, function (key, item) {
            tableData.push(formatcomparerow(item));
        });
        //add row from array to table 
        var t = $("#compare-table").DataTable();
        t.clear();
        t.rows.add(tableData).draw();
        
        //if (spinner2 !== null) {
        //    spinner2.stop();
        //    spinner2 = null;
        //}
    }) //End JSON call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error TeamSelected()!", jqXHR.responseJSON, jqXHR.status);
        });
}
function formatcomparerow(item) {
    return [item.KitName, item.PartID, item.PartName, item.KitQuantity, item.TeamQuantity, item.Difference];
}

function processcommit() {
    var uri = "api/FTCKOP/ProcessReturnCommit";
    var tid = $('#teamdropdown').val();

    var ni = {
        TeamID: tid
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "Commit Returned Updated Successfully!", "Team Kit was successfully returned to Inventory!");
            loadTeamTable();
        })
        .error(function (data) {
            msgbox(-1, "Commit Returned Kit Failed!", "Team Kit was not returned to inventory! " + data);
        });
}


function printreport() {
    var tid = $('#teamdropdown').val();
    var uri = "api/FTCKOP/GetTeamInvReport/" + tid + "?year=2018";   // add department id and budget year to query 

    //using the XMLHttpRequest functionality as JQuery has a problem with 'blob' data types.
    var xhr = new XMLHttpRequest();
    xhr.open('GET', uri, true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status === 200) {
            //we recieved a valid stream of data from server
            var blob = new Blob([this.response], { type: 'application/pdf' })
            //'fake' a download of this stream to the users machine by creating
            //a temporary link and 'clicking' it.
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);   //this 'wraps' the stream in a DOM url object
            link.download = "teamInvReport_" + new Date() + ".pdf";
            link.click();

            window.URL.revokeObjectURL(link.download);  //clean up afterwards so we don't have a memory leak

            //FUTURE: this is somethign that could be explored to possibly display the PDF:
            //_iFrame = document.createElement('iframe');
            // _iFrame.setAttribute('src', url);
            // _iFrame.setAttribute('style', 'visibility:hidden;');
            // $('#someDiv').append(_iFrame)
        } else {
            //TODO: enhance the error handling with more user friendly messages
            alert("error? " + this.status)
        }
    };

    xhr.send();
}

function printKitCompareReport() {
    var tid = $('#teamdropdown').val();
    var kid = $('#tikitdropdown').val();     //kit id
    var uri = "api/FTCKOP/GetKitCompareReport/" + kid + "?team=" + tid;
    
    //using the XMLHttpRequest functionality as JQuery has a problem with 'blob' data types.
    var xhr = new XMLHttpRequest();
    xhr.open('GET', uri, true);
    xhr.responseType = 'blob';
    xhr.onload = function (e) {
        if (this.status === 200) {
            //we recieved a valid stream of data from server
            var blob = new Blob([this.response], { type: 'application/pdf' })
            //'fake' a download of this stream to the users machine by creating
            //a temporary link and 'clicking' it.
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(blob);   //this 'wraps' the stream in a DOM url object
            link.download = "kitCompareReport_" + new Date() + ".pdf";
            link.click();

            window.URL.revokeObjectURL(link.download);  //clean up afterwards so we don't have a memory leak

            //FUTURE: this is somethign that could be explored to possibly display the PDF:
            //_iFrame = document.createElement('iframe');
            // _iFrame.setAttribute('src', url);
            // _iFrame.setAttribute('style', 'visibility:hidden;');
            // $('#someDiv').append(_iFrame)
        } else {
            //TODO: enhance the error handling with more user friendly messages
            alert("error? " + this.status)
        }
    };

    xhr.send();
}