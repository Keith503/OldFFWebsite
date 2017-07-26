var newFlag = false;
var spinner;

$(document).ready(function () {

    $('#datetimepicker1').datetimepicker({ format: 'MM/DD/YYYY' });

    document.getElementById('btnNew').addEventListener('click', processNew, false);
    document.getElementById('btnSav').addEventListener('click', processUpdate, false);
    document.getElementById('btnDel').addEventListener('click', processDelete, false);
    document.getElementById('btnFTP').addEventListener('click', processFTP, false);
    $('#fNewsSelect').change(function () { refreshItem(); });

    refreshNewsItems();
    refreshCategorylist();
    refreshPrioritylist();
    refreshStatuslist();
    refreshAuthorlist();
  
});  // End of Document Ready function
 
function refreshNewsItems() {
    //
    //*******************************************
    /* Get List of News Items  
    /********************************************/
    var uri = "api/frogforce/GetNewsItemList";
    $.getJSON(uri, function (data) {
        $('#fNewsSelect').empty();
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.ID + '-' + item.Description_text + '</option>');
            $('#fNewsSelect').append(newOption);
        });
        //hiderefreshbtn("#gtbtn", "Done");
        document.getElementById("fNewsSelect").selectedIndex = 0;
        $('#fNewsSelect').trigger("change");
    }) // End Json Call 
    // Optional - fires when operation completes with error
    .error(function (jqXHR, textStatus, errorThrown) {
        msgbox(-1, "GetCategoryList Failed!", 'Error Occurred: ' + jqXHR.responseText);
        //hiderefreshbtn("#gtbtn", "Done");
    });
}

function refreshItem() {
    //
    //*******************************************
    /* Refresh screen with selected news item   
    /********************************************/
    //turn on the spinner 
    startSpinner();
    var nid = $('#fNewsSelect').val();
    var uri = "api/frogforce/GetNewsItem/" + nid;
    $.getJSON(uri, function (data) {
        var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
        var formatDate = d3.timeFormat("%m-%d-%Y %H:%M:%S");
        document.getElementById("fID").value = data.ID;
        document.getElementById("fStatus").value = data.Status_ID;
        document.getElementById("fCategory").value = data.Category_ID;
        //document.getElementById("fAppBy").value = "";
        //document.getElementById("fAppDate").value = "";
        document.getElementById("fAuthBy").value = data.Author_ID;
        var pd = parseDate(data.Post_Date);
        $('#datetimepicker1').data("DateTimePicker").date(pd);
        document.getElementById("fTitle").value = data.Title_text;
        document.getElementById("fBody").value = data.Body_text;
        document.getElementById("fImageID1").value = data.Image1_Name;
        document.getElementById("fImageID2").value = data.Image2_Name;
        stopSpinner();
    }) // End Json Call 
    // Optional - fires when operation completes with error
    .error(function (jqXHR, textStatus, errorThrown) {
        msgbox(-1, "refreshitem Failed!", 'Error Occurred: ' + jqXHR.responseText);
        stopSpinner();
    });
}


function refreshCategorylist() {
    //
    //*******************************************
    /* Get Category List 
    /********************************************/
    var uri = "api/frogforce/GetCategoryList";
    $.getJSON(uri, function (data) {
        $('#fCategory').empty();
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.ID + '-' + item.Description_text + '</option>');
            $('#fCategory').append(newOption);
        });
        //hiderefreshbtn("#gtbtn", "Done");
        $('#fCategory').trigger("change");
    }) // End Json Call 
    // Optional - fires when operation completes with error
    .error(function (jqXHR, textStatus, errorThrown) {
        msgbox(-1, "GetCategoryList Failed!", 'Error Occurred: ' + jqXHR.responseText);
        //hiderefreshbtn("#gtbtn", "Done");
    });
}

function refreshPrioritylist() {
    //
    //*******************************************
    /* Get Priority List 
    /********************************************/
    var uri = "api/frogforce/GetPriorityList";
    //showrefreshbtn("#gtbtn", "Refreshing...");

    $.getJSON(uri, function (data) {
        $('#fPriority').empty();
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.ID + '-' + item.Description_text + '</option>');
            $('#fPriority').append(newOption);
        });

        //hiderefreshbtn("#gtbtn", "Done");
        $('#fPriority').trigger("change");
    }) // End Json Call 
    // Optional - fires when operation completes with error
    .error(function (jqXHR, textStatus, errorThrown) {
        msgbox(-1, "GetPriorityList Failed!", 'Error Occurred: ' + jqXHR.responseText);
        //hiderefreshbtn("#gtbtn", "Done");
    });
}

function refreshStatuslist() {
    //
    //*******************************************
    /* Get Status List 
    /********************************************/
    var uri = "api/frogforce/GetStatusList";
    $.getJSON(uri, function (data) {
        $('#fStatus').empty();
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.ID + '-' + item.Description_text + '</option>');
            $('#fStatus').append(newOption);
        });
        //hiderefreshbtn("#gtbtn", "Done");
        $('#fStatus').trigger("change");
    }) // End Json Call 
    // Optional - fires when operation completes with error
    .error(function (jqXHR, textStatus, errorThrown) {
        msgbox(-1, "GetStatusList Failed!", 'Error Occurred: ' + jqXHR.responseText);
        //hiderefreshbtn("#gtbtn", "Done");
    });
}


function refreshAuthorlist() {
    //
    //*******************************************
    /* Get Author List 
    /********************************************/
    var uri = "api/frogforce/GetAuthorList";
    $.getJSON(uri, function (data) {
        $('#fAuthBy').empty();
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.ID + '-' + item.Description_text + '</option>');
            $('#fAuthBy').append(newOption);
        });
        //hiderefreshbtn("#gtbtn", "Done");
        $('#fAuthBy').trigger("change");
    }) // End Json Call 
    // Optional - fires when operation completes with error
    .error(function (jqXHR, textStatus, errorThrown) {
        msgbox(-1, "GetAuthorList Failed!", 'Error Occurred: ' + jqXHR.responseText);
        //hiderefreshbtn("#gtbtn", "Done");
    });
}

function processNew() {
    document.getElementById("fID").value = "0";
    document.getElementById("fStatus").selectedIndex = 0;
    document.getElementById("fCategory").selectedIndex = 0;
    document.getElementById("fAppBy").value = "";
    document.getElementById("fAppDate").value = "";
    document.getElementById("fAuthBy").selectedIndex = 0;

    var formatDate = d3.timeFormat("%m-%d-%Y %H:%M:%S");
    $('#datetimepicker1').data("DateTimePicker").date(formatDate(new Date()));
    document.getElementById("fTitle").value = "";
    document.getElementById("fBody").value = "";
    document.getElementById("fImageID1").value = "";
    document.getElementById("fImageID2").value = "";
    newFlag = true; 
}

function processUpdate() {
    var uri = "api/frogforce/UpdateNewsItem";
    var id = document.getElementById("fID").value;
    var cat = document.getElementById("fCategory").value;
    var status = $('#fStatus').val();
    var authby = $('#fAuthBy').val();
    var postdt = $('#datetimepicker1').data("DateTimePicker").date();
    var title = document.getElementById("fTitle").value;
    var body = document.getElementById("fBody").value;
    var image1 = document.getElementById("fImageID1").value;
    var image2 = document.getElementById("fImageID2").value;
    var ni = {
        ID: id,
        Status_ID: status,
        Author_ID: authby,
        Post_Date: postdt,
        Image1_Name: image1,
        Image2_Name: image2,
        Category_ID: cat,
        Title_text: title,
        Body_text: body
    };

    var testpost = $.post(uri, { "": JSON.stringify(ni) })
        .success(function (data) {
            msgbox(0, "News Updated Successfully!", "News Item was successfully updated!");
            //if this was a new add, then refresh the selection box to include it 
            if (newFlag) {
                refreshNewsItems();
            }
            newFlag = false; 
           
        })
        .error(function (data) {
            msgbox(-1, "News Updated Failed!", "News Item Update failed! " + data);
        });
        

}

function processDelete() {
    var uri = "api/frogforce/RemoveNewsItem";
    BootstrapDialog.confirm('Are you sure you want to delete this news item?', function (result) {
        if (result) {
            var id = document.getElementById("fID").value;
            var ni = {
                ID: id
            };
            var testpost = $.post(uri, { "": JSON.stringify(ni) })
                .success(function (data) {
                    msgbox(0, "News Deleted Successfully!", "News Item was successfully deleted!");
                    refreshNewsItems();
                })
                .error(function (data) {
                    msgbox(-1, "News Delete Failed!", "News Item delete failed! " + data);
            });
        } //end if 
      } // End bootstrap function 
    )// End Boot strap Dialog
}


function processFTP() {
    //var uri = "api/frogforce/RemoveNewsItem";
    //BootstrapDialog.confirm('Are you sure you want to delete this news item?', function (result) {
    //    if (result) {
    //        var id = document.getElementById("fID").value;
    //        var ni = {
    //            ID: id
    //        };
    //        var testpost = $.post(uri, { "": JSON.stringify(ni) })
    //            .success(function (data) {
    //                msgbox(0, "News Deleted Successfully!", "News Item was successfully deleted!");
    //                refreshNewsItems();
    //            })
    //            .error(function (data) {
    //                msgbox(-1, "News Delete Failed!", "News Item delete failed! " + data);
    //           });
    //  } //end if 
    //} // End bootstrap function 
    //)// End Boot strap Dialog
 
    //var file_path = app.activeDocument.fullName
    //var file = new File("/d/project/test_file.psd");
    //var file = new File("/c/Projects/FFWebsite/FFWebsite/img/apply/visa.png");

    //var ftp = new FtpConnection("ftp://192.168.1.150/DATA/");
    //var ftp = new FtpConnection("ftp://50.62.160.32/frogforce503.com/img/");
    //ftp.login("username", "password");
    //ftp.login("TOAmaster", "17Frog01");

    //ftp.cd("frogforce503.com/img")
    //ftp.put(file, "visa.png");

    //ftp.close();
    //file.close();   
    //alert("Tranferring file");
    $('#uploadFileModal').modal('show');
} // end ProcessFTP 

//handle the file upload submit
$('#uploadFileForm').on('submit', function (e) {
    e.preventDefault();

    var form = document.getElementById('uploadFileForm');
    var fileSelect = document.getElementById('file-select');
    var uploadButton = document.getElementById('upload-response-button');
    // Update button text.
    uploadButton.innerHTML = 'Uploading...';

    // Get the selected files from the input.
    var files = fileSelect.files;
    if (files.length > 0) {
        if (window.FormData !== undefined) {
            var data = new FormData();
            //only recieving the first file controller even though we could send multiple
            for (var x = 0; x < files.length; x++) {
                data.append("file" + x, files[x]);
                //getAsText(files[x]);
            }

            $.ajax({
                xhr: function () {
                    var xhr = new window.XMLHttpRequest();
                    //Upload progress
                    xhr.upload.addEventListener("progress", function (evt) {
                        if (evt.lengthComputable) {
                            var percentComplete = (evt.loaded / evt.total) * 100;
                            //Do something with upload progress
                            $('.progress-bar').css('width', percentComplete + '%').attr('aria-valuenow', percentComplete);
                        }
                    }, false);
                    //Download progress
                    //xhr.addEventListener("progress", function (evt) {
                    //    if (evt.lengthComputable) {
                    //        var percentComplete = (evt.loaded / evt.total) * 100;
                    //        //Do something with download progress
                    //        console.log(percentComplete);
                    //    }
                    //}, false);
                    return xhr;
                },
                type: "POST",
                url: 'api/frogforce/UploadFile',
                contentType: false,
                processData: false,
                data: data,
                success: function (result) {
                    BootstrapDialog.alert("File Uploaded Successfully.");
                    var uploadButton = document.getElementById('upload-response-button');
                    uploadButton.innerHTML = 'Upload';
                    $('.progress-bar').css('width', 0 + '%').attr('aria-valuenow', 0);
                    $('#upload-button').prop("disabled", false);
                    $('#uploadFileModal').modal('hide');
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    if (xhr.responseText && xhr.responseText[0] === "{")
                        err = JSON.parse(xhr.responseText).Message;
                    console.log(err);
                    BootstrapDialog.alert("issue with file upload: " + err);
                    var uploadButton = document.getElementById('upload-response-button');
                    uploadButton.innerHTML = 'Upload';
                    $('.progress-bar').css('width', 0 + '%').attr('aria-valuenow', 0);
                    $('#upload-button').prop("disabled", false);
                    $('uploadFileModal').modal('hide');
                }
            });
        } else {
            BootstrapDialog.alert("This browser doesn't support HTML5 file uploads!");
            console.log("This browser doesn't support HTML5 file uploads!");
        }
    }

    $('#upload-button').prop("disabled", true);
});


function startSpinner() {
    spinner = displaySpinner('spin1');
}

function stopSpinner() {
    if (spinner !== null) {
        spinner.stop();
        spinner = null;
    }
}


