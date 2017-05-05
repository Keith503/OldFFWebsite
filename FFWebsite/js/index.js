$(document).ready(function () {
    GetCarouselList();
    GetTop3NewsItems();
    GetEventItems();

});  // End of Document Ready function
 

function GetCarouselList() {
    /*******************************************************************
    * GetNewsCarouselList - Go get the news items we want to show in the 
    *    jumbotron carousel 
    *******************************************************************/
    var uri = "api/frogforce/GetNewsCarouselList";
   
    $.getJSON(uri, function (data) {
        var i = 0;
        var htext = "";
        $.each(data, function (key, item) {
            i++;
            htext += buildCarouselItem(i, item.Image1_Name, item.Title_text, item.Body_text, item.ID);
        });  // End each
        $('#FFcarousel').append(htext + "</div>");
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getNewsList()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getNewsList

function getEventList() {
    /*******************************************************************
    * getEventList - Go get a list of upcoming events 
    *******************************************************************/
    var uri = "api/FF/GetNewsList";

    $.getJSON(uri, function (data) {
       $.each(data, function (key, item) {
        });  // End each 
       
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getEventList()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getEventList

function buildCarouselItem(n, img, title, bodytext, id) {
    /*******************************************************************
   * buildCarouselItem - build a single slide for the Jumbotron Carousel
   *******************************************************************/
    var s= "<div class='item'>";
    //add the image 
    s+= "<img src='img/FFWebsite/" + img + "' width='1600' height='575' />";
    s+= "<div class='banner_caption'><div class='container'><div class='row'><div class='col-xs-12'><div class='caption_inner animated fadeInUp'>";
    s+= "<h1>" + title + "</h1>"; 
    //s+= "<p>" + bodytext + "</p>";
    s+= "<a href='page-news-item.html?ID=" + id + "'>Learn More</a>"; 
    s += "</div></div></div></div></div></div>";
    return s; 
}

function GetTop3NewsItems() {
    /*******************************************************************
    * GetTop3NewsItems - Go get the top 3 news items 
    *******************************************************************/
    var uri = "api/frogforce/GetTopNewsItems/3";

    $.getJSON(uri, function (data) {
        var i = 0;
        var htext = "<ul>";
      
        $.each(data, function (key, item) {
            var parseDate = d3.timeParse("%Y-%m-%dT%H:%M:%S");
            var formatDate = d3.timeFormat("%B %d, %Y");
            var sd = parseDate(item.Post_Date);
            htext = htext + "<li><span class='rel_thumb'><a href='page-news-item.html?ID="+item.ID + "'>";
            htext = htext + "<img src='img/FFWebsite/" + pickimage(item.Image1_Name, item.Image2_Name) + "' alt=''></a></span><!--end rel_thumb-->";
            htext = htext + "<div class='rel_right'><h4><a href='page-news-item.html?ID=" + item.ID + "'>";
            htext = htext + item.Title_text + "</a></h4><div class='meta'>"; 
            htext = htext + "<span class='author'>Posted in: <a href='#'>" + item.Category_Name + "</a></span>";
            htext = htext + "<span class='date'>on: <a href='#'>" + formatDate(sd) + "</a></span></div>";
            htext = htext + "<p>" +  shrinktext(item.Body_text) + "</p></div><!--end rel right--></li>";
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
            htext = htext + "<span class='date'><span>" + formatdd(sd) + "</span>" + formatmm(sd) +"</span></span>";
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
}  // End getEventItems

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
    sx = intext.substring(0, 100);
    //find the last blank in the string 
    n = sx.lastIndexOf(" ");
    //only move part of string from start to first blank less than 150 
    st = intext.substring(0, n);
    st = st + "...";
    return st;
}
