//var eventid = 30;    //indiana St Joseph 
//var eventid = 15;    //Kettering 1 

$(document).ready(function () {
    //loadEventDropdown();
    initNbotTable();
    //$('#eventdropdown').change(function () {
        loadNbotTable();
    //});
});

function loadEventDropdown() {
    var uri = "api/frogforce/GetScoutingEventList";

    $.getJSON(uri, function (data) {
        $('#eventdropdown').empty();
        $.each(data, function (index, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.Title_text + '</option>');    //TODO: what is the members of the data coming back?
            $('#eventdropdown').append(newOption);
        });
          // This code selects an event to make it easier on everyone 
        var e = document.getElementById('eventdropdown');
        e.value = 47;   // st louis - carson
        $('#eventdropdown').trigger('change');
        //end preselect 
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadEventDropdown()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End loadEventDropdown

function initNbotTable() {
    if (!$.fn.dataTable.isDataTable('#nbot-table')) {
        var table = $('#nbot-table').DataTable({
            dom: 'T<"clear">lBfrtip',
            buttons: ['csv', 'excel'],
            "lengthMenu": [[15, 25, 50, -1], [15, 25, 50, "All"]],
            "aoColumns": [
                { "sTitle": "ID", "sWidth": "70px" },
                { "sTitle": "FIRST Program", "sWidth": "70px" },
                { "sTitle": "Student First Name", "sWidth": "120px" },
                { "sTitle": "Student Last Name", "sWidth": "120px" },
                { "sTitle": "Student Phone", "sWidth": "70px" },
                { "sTitle": "Student eMail", "sWidth": "70px" },
                { "sTitle": "School", "sWidth": "70px" },
                { "sTitle": "Grade", "sWidth": "70px" },
                { "sTitle": "Gender", "sWidth": "70px" },
                { "sTitle": "Parent 1 Name", "sWidth": "120px" },
                { "sTitle": "Parent 1 eMail", "sWidth": "70px" },
                { "sTitle": "Parent 1 Phone", "sWidth": "70px" },
                { "sTitle": "Parent 2 Name", "sWidth": "120px" },
                { "sTitle": "Parent 2 eMail", "sWidth": "70px" },
                { "sTitle": "Question 1", "sWidth": "70px" },
                { "sTitle": "Question 2", "sWidth": "70px" },
                { "sTitle": "Question 3", "sWidth": "70px" },
                { "sTitle": "Prior Experience", "sWidth": "70px" } 
            ]
        });
    }
}

function loadNbotTable() {
    var uri = "api/frogforce/GetNbotInterest";
        var tableData = [];
        var i = 0;

        $.getJSON(uri, function (data) {
            $.each(data, function (key, item) {
                tableData.push(formatNbotRow(item));
            });
            //add row from array to table 
            var t = $("#nbot-table").DataTable();
            t.clear();
            t.rows.add(tableData).draw();
        }) //End JSON call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error loadNbotTable()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End loadNbotTable

function formatNbotRow(item) {
    var s = "";
    var g = ""; 
    var p = ""; 
    switch (item.SchoolID) {
        case 1:
            s = "Deerfield"; 
            break; 
        case 2:
            s = "Novi Woods";
            break;
        case 3: 
            s = "Orchard Hills";
            break;
        case 4:
            s = "Parkview";
            break;
        case 5:
            s = "Village Oaks"; 
            break; 
        case 6: 
            s = "Novi Meadows";
            break;
        case 7:
            s = "Novi Middle School"; 
            break; 
        case 8:
            s = "Novi High School";
    }

    switch (item.Grade) {
        case 2:
            g = "Kindergarten";
            break;
        case 3:
            g = "1st";
            break;
        case 4:
            g = "2nd";
            break;
        case 5:
            g = "3rd";
            break;
        case 6:
            g = "4th"; 
            break;
        case 7:
            g = "5th";
            break;
        case 8:
            g = "6th";
            break;
        case 9:
            g = "7th";
            break;
        case 10:
            g = "8th";
            break;
        case 11:
            g = "9th";
            break;
        case 12:
            g = "10th";
            break;
        case 13:
            g = "11th";
            break;
        case 14:
            g = "12th";
            break;
    }

    switch (item.FirstProgram) {
        case 1:
            p = "Jr FLL";
            break;
        case 2:
            p = "FLL";
            break;
        case 3:
            p = "FTC";
            break;
        case 4:
            p = "FRC";
            break;
    }

    return [
        item.ID,
        p,
        item.StudentFirstName, 
        item.StudentLastName, 
        item.StudentPhone, 
        item.StudenteMail,
        s, 
        g, 
        item.Gender,
        item.Parent1Name, 
        item.Parent1eMail, 
        item.Parent1Phone, 
        item.Parent2Name, 
        item.Parent2eMail,  
        item.Question1,
        item.Question2, 
        item.Question3, 
        item.PriorExperience
];

}