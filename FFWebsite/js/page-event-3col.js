var canvas = document.createElement('canvas');

$(document).ready(function () {
    //test if URL contains passed ID - if so we want to use it 
    //var id = getURLParameters("ID");
    var id = "1";
    GetAllEventItems(id);
});  // End of Document Ready function
 
function GetAllEventItems(id) {
    /*******************************************************************
    * GetAllEventItem - Go get the event data for a particular item  
    *******************************************************************/
    var uri = "api/frogforce/GetAllEventItems/";
   
    $.getJSON(uri, function (data) {
        var htext = "";
        var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
        var formatDate = d3.timeFormat("%B %d, %Y");
        var formatdd = d3.timeFormat("%d");
        var formattt = d3.timeFormat("%H:%M %p");
        var formatmm = d3.timeFormat("%B");
        $.each(data, function (key, item) {
          
            var sd = parseDate(item.Start_Date);
            htext = htext + "<div  style='height: 300px;' class='col-xs-6 col-sm-4'><div class='related_post_sec single_post'><span class='date-wrapper'>";
            htext = htext + "<span class='date'><span>" + formatdd(sd) + "</span>" + formatmm(sd) + "</span></span>";
            htext = htext + "<div class='rel_right'><h4><a href='page-single-event.html?ID=" + item.ID + "'>" + item.Title_text + "</a></h4>";
            htext = htext + "<div class='meta'><span class='place'><i class='fa fa-map-marker'></i>" + item.Location_text + "</span>";
            htext = htext + "<span class='event-time'><i class='fa fa-clock-o'></i>" + formattt(sd) + "</span></div>";
            htext = htext + "<p>" + shrinktext(item.Body_text) + "</p>";
            htext = htext + "<a href='page-single-event.html?ID=" + item.ID + "' class='btn btn-default commonBtn'>view Detals</a></div></div></div>";
        });  // End each 
        $('#ffthumb').html(htext);
    }) // End Json Call 
     .error(function (jqXHR, textStatus, errorThrown) {
         ErrorMsgBox("Error GetAllEventItems()!", jqXHR.responseJSON, jqXHR.status);
     });
}  // End getEventItem

function shrinktext(intext) {
    // function to reduce a line of text to a maximum of 150 characters 
    var st = "";
    var sx = ""; 
    var l = 0;
    var n; 
    //if length of text is less than limit than return input string to caller
    if (intext.length < 150) {
       return intext;   
    }
    //get the first 150 characters of string 
    sx = intext.substring(0, 150);
    //find the last blank in the string 
    n = sx.lastIndexOf(" ");
    //only move part of string from start to first blank less than 150 
    st = intext.substring(0, n);
    st = st + "...";    
    return st; 
}
