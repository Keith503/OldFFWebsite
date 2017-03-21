//var eventid = 30;    //indiana St Joseph 
//var eventid = 15;    //Kettering 1 

$(document).ready(function () {
    loadEventDropdown();
    initScoutTable();
    $('#eventdropdown').change(function () {
        loadScoutTable();
    });
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

function initScoutTable() {
    if (!$.fn.dataTable.isDataTable('#scout-table')) {
        var table = $('#scout-table').DataTable({
            dom: 'T<"clear">lBfrtip',
            buttons: ['csv', 'excel'],
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "ID", "sWidth": "70px" },
                { "sTitle": "ScoutName", "sWidth": "70px" },
                { "sTitle": "TeamNumber", "sWidth": "70px" },
                { "sTitle": "MatchNumber", "sWidth": "70px" },
                { "sTitle": "Alliance", "sWidth": "70px" },
                { "sTitle": "BreachLineA", "sWidth": "70px" },
                { "sTitle": "ScoreGearA", "sWidth": "70px" },
                { "sTitle": "GearLocation", "sWidth": "70px" },
                { "sTitle": "ScoreHighFuelA", "sWidth": "70px" },
                { "sTitle": "ScoreAtLeast50A", "sWidth": "70px" },
                { "sTitle": "ScoreLowFuelA", "sWidth": "70px" },
                { "sTitle": "ScoreGearT", "sWidth": "70px" },
                { "sTitle": "ScoreHighFuelT", "sWidth": "70px" },
                { "sTitle": "ScoreHighFuelScore", "sWidth": "70px" },
                { "sTitle": "ScoreLowFuelT", "sWidth": "70px" },
                { "sTitle": "Climb", "sWidth": "70px" },
                { "sTitle": "Climblocation", "sWidth": "70px" },
                { "sTitle": "DropGears", "sWidth": "70px" },
                { "sTitle": "TechDiff", "sWidth": "70px" }
            ]
        });
    }
}


function loadScoutTable() {
    var nid = $('#eventdropdown').val();
    if (nid !== null) {
        var uri = "api/frogforce/GetScoutingDump/" + nid;
        var tableData = [];
        var i = 0;

        $.getJSON(uri, function (data) {
            $.each(data, function (key, item) {
                tableData.push(formatScoutRow(item));
            });
            //add row from array to table 
            var t = $("#scout-table").DataTable();
            t.clear();
            t.rows.add(tableData).draw();
        }) //End JSON call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadScoutTable()!", jqXHR.responseJSON, jqXHR.status);
        });
    }  // end if

}  // End loadScoutTable

function formatScoutRow(item) {
    return [item.ID,item.ScoutName,
    item.TeamNumber,
    item.MatchNumber,
    item.Alliance,
    item.BreachLineA,
    item.ScoreGearA,
    item.GearLocation,
    item.ScoreHighFuelA,
    item.ScoreatLeast50A,
    item.ScoreLowFuelA,
    item.ScoreGearT,
    item.ScoreHighFuelT,
    item.TotalHighFuelScore,
    item.ScoreLowFuelT,
    item.Climb,
    item.ClimbLocation,
    item.DropGears,
    item.TechDiff];



}