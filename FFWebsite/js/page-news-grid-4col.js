$(document).ready(function () {
    //test if URL contains passed ID - if so we want to use it 
    var parm = getURLParameters("ID");
    var id = 0;
    if (parm === undefined) {
        id = 0;
    } else {
        id = parm;
    }
    GetNewsPage(id);
});  

 
function GetNewsPage(id) {
    /*******************************************************************
    * GetNewsPage - Go get 12 news items based on the last item number   
    *******************************************************************/
    var uri = "api/frogforce/GetNewsPage/" + id;

    $.getJSON(uri, function (data) {
        var i = 0;
        var htext = "";
        var iname = "";
        $.each(data, function (key, item) {
            htext = htext + "<div class='col-xs-6 col-sm-3'><div class='aboutImage'><a href='page-news-item.html?ID=" + item.ID + "'>"; 
            htext = htext + "<img src='img/FFWebsite/" + pickimage(item.Image1_Name, item.Image2_Name) + "' width='270' height='170' alt='' class='img-responsive' />";
            htext = htext + "<div class='overlay'><p>" + item.Body_text + "</p></div>";
            htext = htext + "<span class='captionLink'>View Details<span></span></span></a></div><!-- aboutImage -->";
            htext = htext + "<h3><a href='page-news-item.html?ID=" + item.ID + "'>" + item.Title_text + "</a></h3></div>";
        });  // End each 
        $('#frognp').html(htext + "</ul>");
    }) // End Json Call 
        .error(function (jqXHR, textStatus, errorThrown) {
            ErrorMsgBox("Error getNewsList()!", jqXHR.responseJSON, jqXHR.status);
        });
}  // End getNewsList


