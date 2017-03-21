//var eventid = 30;    //indiana St Joseph 
//var eventid = 15;    //Kettering 1 

$(document).ready(function () {
    loadEventDropdown();
    initTeamTable();
    initScoreTable();
    initTeleopTable();
    initGearTable();
    initClimbTable();
    $('#eventdropdown').change(function () {
        loadMatchDropdown();
        loadGearTable();
        loadClimbTable();
    });
    $('#matchdropdown').change(function () { loadTeamTable(); });
});

function loadEventDropdown() {
    var uri = "api/frogforce/GetScoutingEventList";

    $.getJSON(uri, function (data) {
        $('#eventdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.Title_text + '</option>');    //TODO: what is the members of the data coming back?
            $('#eventdropdown').append(newOption);
        });
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadEventDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End loadEventDropdown

function loadMatchDropdown() {
    var nid = $('#eventdropdown').val();
    var uri = "api/frogforce/GetScoutingMatchList/" + nid;   

    $.getJSON(uri, function (data) {
        $('#matchdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.Description + '</option>');    //TODO: what is the members of the data coming back?
            $('#matchdropdown').append(newOption);
        });

        loadTeamTable();
       
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadMatchDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End loadMatchDropdown

function initTeamTable() {
    if (!$.fn.dataTable.isDataTable('#team-table')) {
        var table = $('#team-table').DataTable({
            dom: 'T<"clear">lfrtip',
            bFilter: false,
            "paging": false,
            "info": false,
            "order": [[2,"asc"]],
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Team Number", "sWidth": "70px" },
                { "sTitle": "Name", "sWidth": "10px" },
                { "sTitle": "Station", "sWidth": "70px" }
            ]
        });
        //setup callback function if row is clicked 
        $('#team-table tbody').on('click', 'tr', function (eventObject) {
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
        })
    }
}

function initScoreTable() {
    if (!$.fn.dataTable.isDataTable('#score-table')) {
        var table = $('#score-table').DataTable({
            dom: 'T<"clear">lfrtip',
            bFilter: false,
            "paging": false,
            "info": false,
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Match ID","sClass": "FF-center", "sWidth": "50px" },
                { "sTitle": "Alliance Teams", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Fuel Low", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Fuel High", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Rotor Point", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Total Points", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Scout Gears", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Scout Gear Location", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Scout Shooting High", "sClass": "FF-center", "sWidth": "90px" }
            ]
        });
        //setup callback function if row is clicked 
        //$('#score-table tbody').on('click', 'tr', function () {
        //    var aData = table.row(this).data();
        //    ScoreTableitemSelected(aData);
        //})
    }
}



function loadTeamTable() {
    var nid = $('#matchdropdown').val();
    if (nid !== null) {
        var uri = "api/frogforce/GetTeamsInMatch/" + nid;
        var tableData = [];
        var i = 0;

        $.getJSON(uri, function (data) {
            $.each(data, function (key, item) {
                tableData.push(formatteamrow(item));
            });
            //add row from array to table 
            var t = $("#team-table").DataTable();
            t.clear();
            t.rows.add(tableData).draw();

            var aData = t.rows(0).data();
            if (aData !== null) {
                TableitemSelected(aData[0]);
            }
        }) //End JSON call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadTeamTable()!", jqXHR.responseJSON, jqXHR.status);
        });

    } else {
        var t = $("#team-table").DataTable();
        t.clear();
        t.draw();
        var s = $("#score-table").DataTable();
        s.clear();
        s.draw();
        document.getElementById("p1name").innerHTML = "<h5>&nbsp;</h5>";
    }  // end if

}  // End loadteamTable

function formatteamrow(item) {
    return [item.TeamNumber, item.TeamName, item.Station];
}

function TableitemSelected(item) {
    var uri = "api/frogforce/GetScoutingScoresforTeam/" + item[0];
    var tableData = [];
    var tableData2 = [];
    var i = 0;
    document.getElementById("p1name").innerHTML = "<h5>Team: " + item[0] + " - " + item[1] + "</h5>";
    document.getElementById("p2name").innerHTML = "<h5>Team: " + item[0] + " - " + item[1] + "</h5>";

    $.getJSON(uri, function (data) {
        $.each(data, function (key, item) {
            tableData.push(formatscorerow(item));
            tableData2.push(formatteleoprow(item));
        });
        //add row from array to table 
        var t = $("#score-table").DataTable();
        t.clear();
        t.rows.add(tableData).draw();

        var t2 = $("#teleop-table").DataTable();
        t2.clear();
        t2.rows.add(tableData).draw();

    }) //End JSON call 
  .error(function (jqXHR, textStatus, errorThrown) {
      ErrorMsgBox("Error TableitemSelected()!", jqXHR.responseJSON, jqXHR.status);
  });
}

function formatscorerow(item) {
    return [item.MatchNumber, item.AllianceTeams, item.AutoFuelLow, item.AutoFuelHigh, item.AutoRotor, item.AutoPoints, item.ScoutScoreGearA, item.ScoutGearLocationA, item.ScoutScoreHighA];
}

function formatteleoprow(item) {
    return [item.MatchNumber, item.AllianceTeams, item.TeleopFuelPoints, item.TeleopPoints, item.TeleopTakeOffPoints, item.ScoutGearT,item.TotalHighFuelScore,item.ScoutDropGears,item.ScoutTechDiff];
}


function initGearTable() {
    if (!$.fn.dataTable.isDataTable('#topgear-table')) {
        var table = $('#topgear-table').DataTable({
            dom: 'T<"clear">lfrtip',
            bFilter: false,
            "paging": true,
            "order": [[3, "desc"]],
            "info": false,
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Team", "sClass": "FF-left", "sWidth": "160px" },
                { "sTitle": "Auton Gears", "sClass": "FF-center", "sWidth": "40px" },
                { "sTitle": "Teleop Gears", "sClass": "FF-center", "sWidth": "40px" },
                { "sTitle": "Total Gears", "sClass": "FF-center", "sWidth": "40px" }
            ]
        });
        //setup callback function if row is clicked 
        //$('#score-table tbody').on('click', 'tr', function () {
        //    var aData = table.row(this).data();
        //    ScoreTableitemSelected(aData);
        //})
    }
}

function initClimbTable() {
    if (!$.fn.dataTable.isDataTable('#topclimb-table')) {
        var table = $('#topclimb-table').DataTable({
            dom: 'T<"clear">lfrtip',
            bFilter: false,
            "paging": true,
            "order": [[1, "desc"]],
            "info": false,
            "lengthMenu": [[10, 25, 50, -1], [10, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Team", "sClass": "FF-leftr", "sWidth": "100px" },
                { "sTitle": "Total Climbs", "sClass": "FF-center", "sWidth": "60px" }
            ]
        });
        //setup callback function if row is clicked 
        //$('#score-table tbody').on('click', 'tr', function () {
        //    var aData = table.row(this).data();
        //    ScoreTableitemSelected(aData);
        //})
    }
}

function initShootTable() {
    if (!$.fn.dataTable.isDataTable('#topshoot-table')) {
        var table = $('#topshoot-table').DataTable({
            dom: 'T<"clear">lfrtip',
            bFilter: false,
            "paging": false,
            "info": false,
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Team", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Auton Fuel", "sClass": "FF-center", "sWidth": "60px" },
                { "sTitle": "Teleop Fuel", "sClass": "FF-center", "sWidth": "60px" },
                { "sTitle": "Total Fuel", "sClass": "FF-center", "sWidth": "60px" }
            ]
        });
        //setup callback function if row is clicked 
        //$('#score-table tbody').on('click', 'tr', function () {
        //    var aData = table.row(this).data();
        //    ScoreTableitemSelected(aData);
        //})
    }
}

function initTeleopTable() {
    if (!$.fn.dataTable.isDataTable('#teleop-table')) {
        var table = $('#teleop-table').DataTable({
            dom: 'T<"clear">lfrtip',
            bFilter: false,
            "paging": false,
            "info": false,
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Match ID", "sClass": "FF-center", "sWidth": "50px" },
                { "sTitle": "Alliance Teams", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Total Fuel Points", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Total Teleop Points", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Takeoff Points", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Scout Total Gears", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Scout total High Fuel", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Scout gears Dropped", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Scout Commentss", "sClass": "FF-center", "sWidth": "100px" }
            ]
        });
        //setup callback function if row is clicked 
        //$('#score-table tbody').on('click', 'tr', function () {
        //    var aData = table.row(this).data();
        //    ScoreTableitemSelected(aData);
        //})
    }
}

function loadGearTable() {
    var nid = $('#eventdropdown').val();

    if (nid !== null) {
        var uri = "api/frogforce/GetGearRanking/" + nid;
        var tableData = [];
        var i = 0;

        $.getJSON(uri, function (data) {
            $.each(data, function (key, item) {
                tableData.push(formatgearrow(item));
            });
            //add row from array to table 
            var t = $("#topgear-table").DataTable();
            t.clear();
            t.rows.add(tableData).draw();
        }) //End JSON call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadGearTable()!", jqXHR.responseJSON, jqXHR.status);
        });

    } else {
        var t = $("#topgear-table").DataTable();
        t.clear();
        t.draw();
    }  // end if

}  // End loadgearTable

function formatgearrow(item) {
    return [item.Team, item.AutonGears, item.TeleOpGears, item.TotalGears];
}

function loadClimbTable() {
    var nid = $('#eventdropdown').val();

    if (nid !== null) {
        var uri = "api/frogforce/GetClimbRanking/" + nid;
        var tableData = [];
        var i = 0;

        $.getJSON(uri, function (data) {
            $.each(data, function (key, item) {
                tableData.push(formatclimbrow(item));
            });
            //add row from array to table 
            var t = $("#topclimb-table").DataTable();
            t.clear();
            t.rows.add(tableData).draw();
        }) //End JSON call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadClimbTable()!", jqXHR.responseJSON, jqXHR.status);
        });

    } else {
        var t = $("#topclimb-table").DataTable();
        t.clear();
        t.draw();
    }  // end if

}  // End loadclimbTable

function formatclimbrow(item) {
    return [item.Team, item.TotalClimb];
}