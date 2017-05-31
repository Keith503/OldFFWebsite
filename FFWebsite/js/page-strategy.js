//var eventid = 30;    //indiana St Joseph 
//var eventid = 15;    //Kettering 1 
var spinner;
var spinner2;

$(document).ready(function () {
    loadEventDropdown();
    initTeamTable();
    initScoreTable();
    initTeleopTable();
    initGearTable();
    initClimbTable();

    $('#eventdropdown').change(function () {
        loadMatchDropdown();
        loadTeamDropdown();
        loadGearTable();
        loadClimbTable();
    });
    $('#matchdropdown').change(function () { loadTeamTable(); });
    $('#teamdropdown').change(function () { TeamSelected(); });
});

function loadEventDropdown() {
    var uri = "api/frogforce/GetScoutingEventList";
    //turn on the spinner 
    spinner = displaySpinner('spin1');


    $.getJSON(uri, function (data) {
        $('#eventdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.Title_text + '</option>');    //TODO: what is the members of the data coming back?
            $('#eventdropdown').append(newOption);
        }
        );
        if (spinner !== null) {
            spinner.stop();
            spinner = null;
        }
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

function loadTeamDropdown() {
    var nid = $('#eventdropdown').val();
    var uri = "api/frogforce/GetTeamsAtEvent/" + nid;

    $.getJSON(uri, function (data) {
        $('#teamdropdown').empty();
        $('#kateamdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.ID + '-' +item.Description_text + '</option>');    //TODO: what is the members of the data coming back?
            $('#teamdropdown').append(newOption);
            $('#kateamdropdown').append(newOption);
            
        });

        loadTeamTable();

    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadTeamDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End loadTeamDropdown


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
                { "sTitle": "Total Fuel", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Rotor Points", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Mobility Points", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Total Auton Points", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Scout High Fuel", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Scout Gears", "sClass": "FF-center", "sWidth": "70px" },
                { "sTitle": "Scout Gear Location", "sClass": "FF-center", "sWidth": "100px" },
                { "sTitle": "Scout Score High", "sClass": "FF-center", "sWidth": "90px" }
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
                { "sTitle": "Teleop Fuel", "sClass": "FF-center", "sWidth": "40px" },
                { "sTitle": "Teleop Rotor", "sClass": "FF-center", "sWidth": "40px" },
                { "sTitle": "Total Takeoff", "sClass": "FF-center", "sWidth": "40px" },
                { "sTitle": "Total Auton", "sClass": "FF-center", "sWidth": "40px" },
                { "sTitle": "Total Penalty", "sClass": "FF-center", "sWidth": "50px" },
                { "sTitle": "Final Score", "sClass": "FF-center", "sWidth": "60px" },
                { "sTitle": "Scout High Fuel", "sClass": "FF-center", "sWidth": "60px" },
                { "sTitle": "Scout Gears", "sClass": "FF-center", "sWidth": "50px" },
                { "sTitle": "Scout Drop Gears", "sClass": "FF-center", "sWidth": "80px" },
                { "sTitle": "Scout Climb", "sClass": "FF-center", "sWidth": "50px" },
                { "sTitle": "Scout Tech Diff", "sClass": "FF-center", "sWidth": "180px" }
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
    if (item !== null) {
        var uri = "api/frogforce/GetScoutingScoresforTeam/" + item[0];
        var tableData = [];
        var tableData2 = [];

        document.getElementById("p1name").innerHTML = "<h5>Team: " + item[0] + " - " + item[1] + "</h5>";
        document.getElementById("p2name").innerHTML = "<h5>Team: " + item[0] + " - " + item[1] + "</h5>";

        var options = document.getElementById('teamdropdown').options;
        //change team drop down box to match 
        for (var i = 0, n = options.length; i < n; i++) {
            if (options[i].value == item[0]) {
                document.getElementById('teamdropdown').selectedIndex = i;
                break;
            }
        }

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
            t2.rows.add(tableData2).draw();

        }) //End JSON call 
      .error(function (jqXHR, textStatus, errorThrown) {
          ErrorMsgBox("Error TableitemSelected()!", jqXHR.responseJSON, jqXHR.status);
      });
    } // end if 
}

function TeamSelected() {
    var nid = $('#teamdropdown').val();
    var uri = "api/frogforce/GetScoutingScoresforTeam/" + nid;
    var tableData = [];
    var tableData2 = [];
    var i = 0;
    //turn on the spinner 
    spinner2 = displaySpinner('spin2');

    document.getElementById("p1name").innerHTML = "<h5>Team: " + nid + " - " + " " + "</h5>";
    document.getElementById("p2name").innerHTML = "<h5>Team: " + nid + " - " + " " + "</h5>";

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
        t2.rows.add(tableData2).draw();

        if (spinner2 !== null) {
            spinner2.stop();
            spinner2 = null;
        }
    }) //End JSON call 
  .error(function (jqXHR, textStatus, errorThrown) {
      ErrorMsgBox("Error TeamSelected()!", jqXHR.responseJSON, jqXHR.status);
  });
}




function formatscorerow(item) {
    return [item.MatchNumber, item.AllianceTeams, item.AutoFuelLow + item.AutoFuelHigh, item.AutoRotorPoints, item.AutoMobilityPoints, item.AutoPoints, item.ScoutAutonHighFuelScore, item.ScoutScoreGearA, item.ScoutGearLocationA, item.ScoutScoreHighA];
}

function formatteleoprow(item) {
    return [item.MatchNumber, item.AllianceTeams, item.TeleopFuelPoints, item.TotalRotorPoints, item.TeleopTakeOffPoints, item.AutoPoints, item.FoulPoints, item.TotalPoints, item.ScoutTotalHighFuel, item.ScoutGearT, item.ScoutDropGears, item.ScoutClimb, item.ScoutTechDiff];
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
                { "sTitle": "Team", "sClass": "FF-left", "sWidth": "300px" },
                { "sTitle": "Total Climbs", "sClass": "FF-center", "sWidth": "35px" }
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