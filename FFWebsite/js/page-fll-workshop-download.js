var spinner;

$(document).ready(function () {
    initKickoffTable();
    loadKickoffTable();
});

function initKickoffTable() {
    if (!$.fn.dataTable.isDataTable('#kickoff-table')) {
        var table = $('#kickoff-table').DataTable({
            dom: 'T<"clear">lBfrtip',
            buttons: ['csv', 'excel'],
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "Team No", "sWidth": "70px" },
                { "sTitle": "Team Name", "sWidth": "70px" },
                { "sTitle": "Experience", "sWidth": "70px" },
                { "sTitle": "School", "sWidth": "120px" },
                { "sTitle": "Contact Name", "sWidth": "120px" },
                { "sTitle": "Contact eMail", "sWidth": "120px" },
                { "sTitle": "Contact Phone", "sWidth": "70px" },
                { "sTitle": "Mentor Count", "sClass": "FF-center", "sWidth": "50px"  },
                { "sTitle": "Student Count", "sClass": "FF-center", "sWidth": "50px"  }
            ]
        });
    }
}

function loadKickoffTable() {
    //turn on the spinner 
    spinner = displaySpinner('spin1');

    var uri = "api/frogforce/GetFLLWorkshopRegister";
        var tableData = [];
        var i = 0;

        $.getJSON(uri, function (data) {
            $.each(data, function (key, item) {
                tableData.push(formatRegisterRow(item));
            });
            //add row from array to table 
            var t = $("#kickoff-table").DataTable();
            t.clear();
            t.rows.add(tableData).draw();
            if (spinner !== null) {
                spinner.stop();
                spinner = null;
            }
        }) //End JSON call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error GetFLLWorkshopRegister()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End loadNbotTable

function formatRegisterRow(item) {
    var s = "";
    switch (item.Experience) {
        case "1":
            s = "Veteran"
            break; 
        case "2":
            s = "Rookie";
            break;
        default: 
            s = "Unknown";
            break;
    }

    return [
        item.ID,
        item.TeamName,
        s,
        item.School,
        item.TeamContactName,
        item.TeamContactEmail,
        item.TeamContactPhone,
        item.MentorCount,
        item.StudentCount 
];

}