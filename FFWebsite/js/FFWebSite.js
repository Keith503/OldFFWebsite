$(document).ready(function () {
   GetNewsList();
  //  getEventList();

});  // End of Document Ready function
 

function GetNewsList() {
    /*******************************************************************
    * getJumbotronNewList - Go get the news items we want to show in the 
    *    jumbotron carousel 
    *******************************************************************/
    var uri = "api/frogforce/GetNewsList";
   
    $.getJSON(uri, function (data) {
        var i = 0;
        var htext = "";
        $.each(data, function (key, item) {
            i++;
            htext += buildCarouselItem(i, item.Image1_Name, item.Title_text, item.Body_text);
        });  // End each 
        $('#FFcarousel').html(htext + "</div>"); 
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



function buildCarouselItem(n, img, title, bodytext) {
    /*******************************************************************
   * buildCarouselItem - build a single slide for the Jumbotron Carousel
   *******************************************************************/
    var s="";
    //first time though make sure to mark it active 
    if(n===1) {
        s = "<div class='item active'>";
    } else {
        s = "<div class='item'>";
    } 
    //add the image 
    s+= "<img src='img/" + img + "'/>";
    s+= "<div class='banner_caption'><div class='container'><div class='row'><div class='col-xs-12'><div class='caption_inner animated fadeInUp'>";
    s+= "<h1>" + title + "</h1>"; 
    s+= "<p>" + bodytext + "</p>";
    s+= "<a href='about.html'>Learn More</a>"; 
    s += "</div></div></div></div></div></div>";
    return s; 
}

