$(document).ready(function () {
    //test if URL contains passed ID - if so we want to use it 
    var id = getURLParameters("ID");
    GetNewsItem(id);
    GetTopNewsItems(id);
    GetEventItems();
    GetRelatedItems(id);

});  // End of Document Ready function
 
var nid = $('#fNewsSelect').val();
var uri = "api/frogforce/GetNewsItem/" + nid;

function GetNewsItem(nid) {
    /*******************************************************************
    * GetNewsItem - Go get the news data for a particular item  
    *******************************************************************/
    //var nid = $('#fNewsSelect').val();
    var uri = "api/frogforce/GetNewsItem/" + nid;
   
    $.getJSON(uri, function (data) {
        //ignore the main new item - we dont want to duplicate it 
            var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
            var formatDate = d3.timeFormat("%B %d, %Y");
            var sd = parseDate(data.Post_Date);
            var htext = "";        
            htext = htext + "<div class='post single_post'><div class='post_thumb'><img src='img/FFWebsite/" + data.Image1_Name + "' alt='' /></div><!--end post thumb-->";
            htext = htext + "<div class='meta'><span class='author'>By: <a href='#'>" + data.Author_Name + "</a></span>";
            htext = htext + "<span class='category'><a href='#'>" + data.Category_Name + "</a></span><span class='date'>Posted: <a href='#'>" + formatDate(sd) + "</a></span>";
            htext = htext + "</div><!--end meta--><h1>" + data.Title_text + "</h1><div class='post_desc'>";
            htext = htext + "<p>" + data.Body_text + "</p>";
            htext = htext + "</div><!--end post desc--></div><!--end post-->";
            $('#FFpost').html(htext);

    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getNewsItem()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getNewsItem

function GetTopNewsItems(id) {
    /*******************************************************************
    * GetTopNewsItems - Go get the top 5 news items 
    *******************************************************************/
    var uri = "api/frogforce/GetTopNewsItemsNoDups/" + id;

    $.getJSON(uri, function (data) {
        var i = 0;
        var htext = "<ul>";
        $.each(data, function (key, item) {
            var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
            var formatDate = d3.timeFormat("%B %d, %Y");
            var sd = parseDate(item.Post_Date);
            htext = htext + "<li><span class='rel_thumb'><img src='img/FFWebsite/" + pickimage(item.Image1_Name, item.Image2_Name) + "' alt='' width='100px' height='67px'/></span><!--end rel_thumb-->";
            htext = htext + "<div class='rel_right'><a href='page-news-item.html?ID=" + item.ID + "'><h4>" + item.Title_text + "</h4></a>";
            htext = htext + "<span class='date'>Posted: <a href='#'>" + formatDate(sd) + "</a></span></div><!--end rel right--></li>";
        });  // End each 
        $('#frogtopnews').html(htext + "</ul>");
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getNewsList()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getNewsList

function GetEventItems() {
    /*******************************************************************
    * GetEventItems - Go get the most recent events  
    *******************************************************************/
    var uri = "api/frogforce/GetEventItems";

    $.getJSON(uri, function (data) {
        var i = 0;
        var htext = "<ul>";

        $.each(data, function (key, item) {
            var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
            var formatDate = d3.timeFormat("%B %d, %Y");
            var formatdd = d3.timeFormat("%d");
            var formattt = d3.timeFormat("%H:%M %p");
            var formatmm = d3.timeFormat("%B");
            var sd = parseDate(item.Start_Date);

            htext = htext + "<li class='related_post_sec single_post'><span class='date-wrapper'>";
            htext = htext + "<span class='date'><span>" + formatdd(sd) + "</span>" + formatmm(sd) + "</span></span>";
            htext = htext + "<div class='rel_right'><h4><a href='page-single-event.html?ID=" + item.ID + "'>" + item.Title_text + "</a></h4>";
            htext = htext + "<div class='meta'><span class='place'><i class='fa fa-map-marker'></i>";
            htext = htext + item.Location_text + "</span><span class='event-time'><i class='fa fa-clock-o'></i>";
            htext = htext + formattt(sd) + "</span></div></div></li>";

        });  // End each 
        $('#ffevents').html(htext + "</ul>");
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getNewsList()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getNewsList

function GetRelatedItems(id) {
    /*******************************************************************
    * GetRelatedItems - Go get items related to this news item  
    *******************************************************************/
    var uri = "api/frogforce/GetRelatedItems/" + id;

    $.getJSON(uri, function (data) {
        var i = 0;
        var htext = "<ul>";
        $.each(data, function (key, item) {
            var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
            var formatDate = d3.timeFormat("%B %d, %Y");
            var sd = parseDate(item.Post_Date);
            htext = htext + "<li><span class='rel_thumb'<a href='page-news-item.html?ID=" + item.ID + "'><img src='img/FFWebsite/" + pickimage(item.Image1_Name, item.Image2_Name) + "' alt='' width='244px' height='170px'/></a></span><!--end rel_thumb-->";
            htext = htext + "<div class='rel_right'><h4><a href='page-news-item.html?ID=" + item.ID + "'>" + item.Title_text + "</a></h4>";
            htext = htext + "<div class='meta'>";
            htext = htext + "<span class='author'>By: <a href='#'>" + item.Author_Name + "</a></span>";         
            htext = htext + "<span class='category'><a href='#'>" + item.Category_Name + "</a></span>";
            htext = htext + "<span class='date'>Posted: <a href='#'>" + formatDate(sd) + "</a></span></div>";
            htext = htext + "<p>" + shrinktext(item.Body_text) + "</p>";
            htext = htext + "</div><!--end rel right--></li>";
        });  // End each 
        $('#ffrelated').html(htext + "</ul>");
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getNewsList()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getrelated 

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
