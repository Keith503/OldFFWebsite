//============================== header =========================

jQuery(document).ready(function($) {

    // Fixa navbar ao ultrapassa-lo
    var navbar = $('.navbar-main'),
    		distance = navbar.offset().top,
        $window = $(window);

	    $window.scroll(function() {
	    	if(($window.scrollTop() >= distance) && ($(".navbar-default").hasClass("navbar-main")))
	        {
	            navbar.removeClass('navbar-fixed-top').addClass('navbar-fixed-top');
	          	$("body").css("padding-top", "70px");
	        } else {
	            navbar.removeClass('navbar-fixed-top');
	            $("body").css("padding-top", "0px");
	        }
	    });
	
});

jQuery(document).ready(function(){

    //============================== Slider =========================
	jQuery('#about_banner').flexslider({
		animation: "slide",
		controlNav: true, 
		animationSpeed: 600,
		slideshowSpeed: 5000,
		easing: "swing",     
		directionNav:true,
		prevText: "",          
		nextText: "",
		pauseOnAction: false,
		touch: true, 
		start: function(){
			jQuery('.flex-active-slide .about_caption').addClass('show')
			},            //Callback: function(slider) - Fires when the slider loads the first slide
		before: function(){
			jQuery('.flex-active-slide .about_caption').removeClass('show')
			},           //Callback: function(slider) - Fires asynchronously with each slider animation
		after: function(slider){
			jQuery('.flex-active-slide .about_caption').addClass('show')
			}
		
	});
	
	jQuery('#single_banner').flexslider({
		animation: "slide",
		controlNav: true, 
		animationSpeed: 600,
		slideshowSpeed: 5000,
		easing: "swing",     
		directionNav:false,
		prevText: "",          
		nextText: "",
		pauseOnAction: false,
		touch: true, 
	});
	
	if (document.documentElement.clientWidth < 992) {
		jQuery('.carousal_section').flexslider({
			animation: "slide",
			controlNav: true, 
			animationSpeed: 600,
			slideshowSpeed: 5000,
			easing: "swing",     
			directionNav:false,
			prevText: "",          
			nextText: "",
			pauseOnAction: false,
			touch: true, 
			minItems: 1,
			maxItems: 1,
			move: 1, 
			animationLoop: true,
			direction: "horizontal",
			reverse: false, 
		});
	}
	jQuery('.carousal_section').flexslider({
		animation: "slide",
		controlNav: true, 
		animationSpeed: 600,
		slideshowSpeed: 5000,
		easing: "swing",     
		directionNav:false,
		prevText: "",          
		nextText: "",
		pauseOnAction: false,
		touch: true, 
		itemWidth: 475,
		minItems: 1,
		maxItems: 2,
		move: 1, 
		animationLoop: true,
		direction: "horizontal",
		reverse: false, 
	});

	(function() {
 
	  // store the slider in a local variable
	  var $window = $(window),
	      flexslider = { vars:{} };
	 
	  // tiny helper function to add breakpoints
	  function getGridSize() {
	    return (window.innerWidth < 600) ? 2 :
		      (window.innerWidth < 800) ? 3 :
		      (window.innerWidth < 900) ? 4 : 5;
	  }
	 
	  $window.load(function() {
	    $('.flexslider').flexslider({
	      animation: "slide",
	      animationLoop: false,
	      itemWidth: 228,
	      itemMargin: 0,
	      prevText: "",          
		  nextText: "",
	      minItems: getGridSize(), // use function to pull in initial value
	      maxItems: getGridSize() // use function to pull in initial value
	    });
	  });
	 
	  // check grid size on resize event
	  $window.resize(function() {
	    var gridSize = getGridSize();
	 
	    flexslider.vars.minItems = gridSize;
	    flexslider.vars.maxItems = gridSize;
	  });
	}());
	
	//============================== Tooltip =========================
	 
	$('[data-toggle="tooltip"]').tooltip();
		
	$("#guiest_id1").selectbox();
	$("#guiest_id2").selectbox();
	$(".option-select").selectbox();
	
	$('.videoLeft img').click(function(){
        video = '<iframe width="550" height="368" src="'+ $(this).attr('data-video') +'"></iframe>';
        $(this).replaceWith(video);
    });
	
	$('.admission_video img').click(function(){
        video = '<iframe width="769" height="454" src="'+ $(this).attr('data-video') +'"></iframe>';
        $(this).replaceWith(video);
    });
	
	//================================ image popup ==================================
	
	$('.popup-gallery').magnificPopup({
		delegate: 'a',
		type: 'image',
		tLoading: 'Loading image #%curr%...',
		mainClass: 'mfp-img-mobile',
		closeBtnInside: false,
		gallery: {
			enabled: true,
			navigateByImgClick: true,
			preload: [0,1] // Will preload 0 - before current, and 1 after the current image
		},
		image: {
			tError: '<a href="%url%">The image #%curr%</a> could not be loaded.',
			titleSrc: function(item) {
				return item.el.attr('title') + '';
			}
		}
		
	});

	//============================== wow =========================

	new WOW().init();

	//============================== counterUp =========================

	jQuery(document).ready(function( $ ) {
		$('span.timer').counterUp({
		delay: 5, // the delay time in ms
		time: 2000 // the speed time in ms
		});
	});

	//============================== Calender =========================
	$(document).ready(function(event, jsEvent, ui, view) {
	    $('#calendar').fullCalendar({
	        header: {
				left: 'prev,next',
				center: 'title',
			},
			defaultDate: '2016-03-12',
			editable: true,
			eventLimit: true,
			events: [
				{
					title: 'All Day Event',
					start: '2016-03-01'
				},
				{
					title: 'Long Event',
					start: '2016-03-07',
					end: '2016-03-10'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2016-03-09T16:00:00'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2016-03-16T16:00:00'
				},
				{
					title: 'Conference',
					start: '2016-03-11',
					end: '2016-03-13'
				},
				{
					title: 'Meeting',
					start: '2016-03-12T10:30:00',
					end: '2016-03-12T12:30:00'
				},
				{
					title: 'Lunch',
					start: '2016-03-12T12:00:00'
				},
				{
					title: 'Meeting',
					start: '2016-03-12T14:30:00'
				},
				{
					title: 'Happy Hour',
					start: '2016-01-12T17:30:00'
				},
				{
					title: 'Dinner',
					start: '2016-03-12T20:00:00'
				},
				{
					title: 'Click for Google',
					url: 'http://google.com/',
					start: '2016-03-28'
				}
			]
	    })
	    $("a.fc-event").attr("href", "single-events.html")
	});

});
