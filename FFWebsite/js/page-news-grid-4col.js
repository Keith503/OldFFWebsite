$(document).ready(function () {
     //test if URL contains passed ID - if so we want to use it 
    id = getparms();

    //setup an event handler for the user selecting a different event
    $('#catsel').change(function () {
        GetNewsPage();
    });
   
    refreshcategory(id);
});  

function getparms() {
 var parm = getURLParameters("ID");
    var id = 0;
    if (parm === undefined) {
        id = 0;
    } else {
        id = parm;
    }
    return id;
}

function refreshcategory(id) {
    //
    //*******************************************
    /* Get Category List  
    /********************************************/
    var uri = "api/frogforce/GetCategoryList";
    showrefreshbtn("#gtbtn", "Refreshing...");

    $.getJSON(uri, function (data) {
        $('#catsel').empty();
        var newOption = $('<option value="0">All</option>');
        $('#catsel').append(newOption);
        $.each(data, function (key, item) {
            var newOption = $('<option value="' + item.ID + '">' + item.Description_text + '</option>');
            $('#catsel').append(newOption);
        });

        //test if URL contains passed ID - if so we want to use it 
        if(id > 0) {
            var select = document.getElementById("catsel");
            for (var i = 0; i < select.options.length; i++) {
                if (select.options[i].value === id) {
                    select.options[i].selected = true;
                }
            }
        }
        hiderefreshbtn("#gtbtn", "Done");
        $('#catsel').trigger("change");
    }) // End Json Call 
    // Optional - fires when operation completes with error
    .error(function (jqXHR, textStatus, errorThrown) {
        msgbox(-1, "RefreshteamList Failed!", 'Error Occurred: ' + jqXHR.responseText);
        hiderefreshbtn("#gtbtn", "Done");
    });
}
 
function GetNewsPage() {
    /*******************************************************************
    * GetNewsPage - Go get news pag for the category requested    
    *******************************************************************/
    var saveqid = $('#catsel').val();
    var uri = "api/frogforce/GetNewsPage/" + saveqid;

    $.getJSON(uri, function (data) {
        var i = 0;
        var htext = "";
        var iname = "";
        //was 270 170 item.Body_text
        $.each(data, function (key, item) {
            htext = htext + "<div class='col-xs-6 col-sm-3'><div class='aboutImage'><a href='page-news-item.html?ID=" + item.ID + "'>"; 
            htext = htext + "<img src='img/FFWebsite/" + pickimage(item.Image1_Name, item.Image2_Name) + "' width='270' height='170' alt='' class='img-responsive' />";
            htext = htext + "<div class='overlay'><p>" + item.Body_text + "</p></div>";
            htext = htext + "<span class='captionLink'>View Details<span></span></span></a></div>";
            htext = htext + "<h3><a href='page-news-item.html?ID=" + item.ID + "'>" + shrinktext(item.Title_text) + "</a></h3></div>";

            //htext = htext + "<div class='col-xs-6 col-sm-3'><div class='aboutImage'><a href='single-course-right-sidebar.html'><img src='img/FFWebsite/" + pickimage(item.Image1_Name, item.Image2_Name) + "' width='270' height='170' alt='' class='img-responsive' /> ";
            //htext = htext + "<div class='overlay'><p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Aenean consectetur ante volutpat sem aliquam lobortis.</p></div><span class='captionLink'>View Details<span></span></span>";
            //htext = htext + "</a></div><!-- aboutImage --><h3><a href='single-course-right-sidebar.html'>" + shrinktext(item.Title_text) +"</a></h3></div>";
          
        });  // End each 
        $('#frognp').html(htext);
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getNewsList()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getNewsList

function shrinktext(intext) {
    // function to reduce a line of text to a maximum of 25 characters 
    var st = "";
    var sx = "";
    var l = 0;
    var n;
    //if length of text is less than limit than return input string to caller
    if (intext.length < 25) {
        return intext;
    }
    //get the first 150 characters of string 
    sx = intext.substring(0, 25);
    //find the last blank in the string 
    n = sx.lastIndexOf(" ");
    //only move part of string from start to first blank less than 25 
    st = intext.substring(0, n);
    st = st + "...";
    return st;
}
