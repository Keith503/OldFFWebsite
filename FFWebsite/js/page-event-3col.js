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
        $.each(data, function (key, item) {
            var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
            var formatDate = d3.timeFormat("%B %d, %Y");
            var formatdd = d3.timeFormat("%d");
            var formattt = d3.timeFormat("%H:%M %p");
            var formatmm = d3.timeFormat("%B");
            var sd = parseDate(item.Start_date);
            htext = htext + "<div class='col-xs-6 col-sm-4'><div class='related_post_sec single_post'><span class='date-wrapper'>";
            htext = htext + "<span class='date'><span>" + formatdd(sd) + "</span>" + formatmm(sd) + "</span></span>";
            htext = htext + "<div class='rel_right'><h4><a href='single-events.html'>" + item.Title_text + "</a></h4>";
            htext = htext + "<div class='meta'><span class='place'><i class='fa fa-map-marker'></i>" + item.Location_text + "</span>";
            htext = htext + "<span class='event-time'><i class='fa fa-clock-o'></i>" + formattt(sd) + "</span></div>";
            htext = htext + "<p>" + item.Body_text + "...</p>";
            htext = htext + "<a href='single-events.html' class='btn btn-default commonBtn'>view Detals</a></div></div></div>";
        });  // End each 
        $('#ffthumb').html(htext);
    }) // End Json Call 
     .error(function (jqXHR, textStatus, errorThrown) {
         ErrorMsgBox("Error GetAllEventItems()!", jqXHR.responseJSON, jqXHR.status);
     });
}  // End getEventItem

