$(document).ready(function () {
    //test if URL contains passed ID - if so we want to use it 
    //var id = getURLParameters("ID");
    var id = "1";
    GetEventItem(id);
    //GetTopNewsItems();
});  // End of Document Ready function
 
function GetEventItem(id) {
    /*******************************************************************
    * GetEventItem - Go get the event data for a particular item  
    *******************************************************************/
    alert(id);
    var uri = "api/frogforce/GetEventSingleItem/";
   
    $.getJSON(uri, function (data) {
        //ignore the main new item - we dont want to duplicate it 
            var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
            var formatDate = d3.timeFormat("%B %d, %Y");
            var formatdd = d3.timeFormat("%d");
            var formattt = d3.timeFormat("%H:%M %p");
            var formatmm = d3.timeFormat("%B");
            var sd = parseDate(data.Post_Date);
            var htext = "";

            htext = htext + "<div class='upcoming_events event-col'><div class='related_post_sec single_post'>";
            htext = htext + "<span class='date-wrapper'><span class='date'><span>" + formatdd(sd) +"</span>" + formatmm(sd) + "</span></span>";
            htext = htext + "<div class='rel_right'><div class='single_post single-event'><h1>" + data.Title_text + "</h1>";
            htext = htext + "<div class='meta'><span class='place'><i class='fa fa-map-marker'></i>" + data.Location_text + "</span>";
            htext = htext + "<span class='event-time'><i class='fa fa-clock-o'></i>" + formattt(sd)+ "</span></div>";
            htext = htext + "<div class='post_desc'><p>" + data.Body_text + "</p></div><!--end post desc-->";
            htext = htext + "<div class='post_bottom'></div><!--end post bottom--></div><!--end single_post--></div></div></div>";
            $('#FFpost').html(htext);

    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getEventItem()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getEventItem

function GetTopNewsItems() {
    /*******************************************************************
    * GetTopNewsItems - Go get the top 5 news items 
    *******************************************************************/
    var uri = "api/frogforce/GetTopNewsItems/5";

    $.getJSON(uri, function (data) {
        var htext = "<ul>";
        $.each(data, function (key, item) {
            var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
            var formatDate = d3.timeFormat("%B %d, %Y");
            var sd = parseDate(item.Post_Date);
            htext = htext + "<li><span class='rel_thumb'><img src='img/FFWebsite/" + item.Image1_Name + "' alt='' width='100px' height='67px'/></span><!--end rel_thumb-->";
            htext = htext + "<div class='rel_right'><a href='page-news-item.html?ID=" + item.ID + "'><h4>" + item.Title_text + "</h4></a>";
            htext = htext + "<span class='date'>Posted: <a href='#'>" + formatDate(sd) + "</a></span></div><!--end rel right--></li>";
        });  // End each 
        $('#frogtopnews').html(htext + "</ul>");
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error gettopNewsItems()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getNewsList


