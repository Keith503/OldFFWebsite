$(document).ready(function () {
    GetTop3NewsItems();
    GetRelatedItems();
});  // End of Document Ready function
 

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
            htext = htext + "<img src='img/FFWebsite/" + pickimage(item.Image1_Name,item.Image2_Name) + "' alt=''></a></span><!--end rel_thumb-->";
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

function GetRelatedItems() {
    /*******************************************************************
    * GetRelatedItems - Go get items related to this news item  
    *******************************************************************/
    var id = "16";
    var uri = "api/frogforce/GetRelatedbyCategoryItems/" + id;

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
    sx = intext.substring(0, 100);
    //find the last blank in the string 
    n = sx.lastIndexOf(" ");
    //only move part of string from start to first blank less than 150 
    st = intext.substring(0, n);
    st = st + "...";
    return st;
}

