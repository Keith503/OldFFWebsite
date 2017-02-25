function NumberFormat(number) {
	var decimalplaces = 2, decimalcharacter = ".", thousandseparater = ",";
	number = parseFloat(number);
	var sign = 0 > number ? "-" : "", formatted = String(number.toFixed(decimalplaces));
	decimalcharacter.length && "." !== decimalcharacter && (formatted = formatted.replace(/\./, decimalcharacter));
	var integer = "", fraction = "", strnumber = String(formatted), dotpos = decimalcharacter.length ? strnumber.indexOf(decimalcharacter) : -1;
	for (dotpos > -1 ? (dotpos && (integer = strnumber.substr(0, dotpos)), fraction = strnumber.substr(dotpos + 1)) : integer = strnumber, 
	integer && (integer = String(Math.abs(integer))); fraction.length < decimalplaces; ) fraction += "0";
	for (var temparray = []; integer.length > 3; ) temparray.unshift(integer.substr(-3)), 
	integer = integer.substr(0, integer.length - 3);
	return temparray.unshift(integer), integer = temparray.join(thousandseparater), 
	sign + integer + decimalcharacter + fraction;
}

function CurrencyFormat(number) {
	var decimalplaces = 2, decimalcharacter = ".", thousandseparater = ",";
	number = parseFloat(number);
	var sign = 0 > number ? "$-" : "$", formatted = String(number.toFixed(decimalplaces));
	decimalcharacter.length && "." !== decimalcharacter && (formatted = formatted.replace(/\./, decimalcharacter));
	var integer = "", fraction = "", strnumber = String(formatted), dotpos = decimalcharacter.length ? strnumber.indexOf(decimalcharacter) : -1;
	for (dotpos > -1 ? (dotpos && (integer = strnumber.substr(0, dotpos)), fraction = strnumber.substr(dotpos + 1)) : integer = strnumber, 
	integer && (integer = String(Math.abs(integer))); fraction.length < decimalplaces; ) fraction += "0";
	for (var temparray = []; integer.length > 3; ) temparray.unshift(integer.substr(-3)), 
	integer = integer.substr(0, integer.length - 3);
	return temparray.unshift(integer), integer = temparray.join(thousandseparater), 
	sign + integer + decimalcharacter + fraction;
}

function CurrencyNoDFormat(number) {
	var decimalplaces = 0, decimalcharacter = "", thousandseparater = ",";
	number = parseFloat(number);
	var sign = 0 > number ? "$-" : "$", formatted = String(number.toFixed(decimalplaces));
	decimalcharacter.length && "." !== decimalcharacter && (formatted = formatted.replace(/\./, decimalcharacter));
	var integer = "", fraction = "", strnumber = String(formatted), dotpos = decimalcharacter.length ? strnumber.indexOf(decimalcharacter) : -1;
	for (dotpos > -1 ? (dotpos && (integer = strnumber.substr(0, dotpos)), fraction = strnumber.substr(dotpos + 1)) : integer = strnumber,
	integer && (integer = String(Math.abs(integer))); fraction.length < decimalplaces;) fraction += "0";
	for (var temparray = []; integer.length > 3;) temparray.unshift(integer.substr(-3)),
	integer = integer.substr(0, integer.length - 3);
	return temparray.unshift(integer), integer = temparray.join(thousandseparater),
	sign + integer + decimalcharacter + fraction;
}

function OneDecFormat(number) {
	var decimalplaces = 1, decimalcharacter = ".", thousandseparater = ",";
	number = parseFloat(number);
	var sign = 0 > number ? "-" : "", formatted = String(number.toFixed(decimalplaces));
	decimalcharacter.length && "." !== decimalcharacter && (formatted = formatted.replace(/\./, decimalcharacter));
	var integer = "", fraction = "", strnumber = String(formatted), dotpos = decimalcharacter.length ? strnumber.indexOf(decimalcharacter) : -1;
	for (dotpos > -1 ? (dotpos && (integer = strnumber.substr(0, dotpos)), fraction = strnumber.substr(dotpos + 1)) : integer = strnumber,
	integer && (integer = String(Math.abs(integer))); fraction.length < decimalplaces;) fraction += "0";
	for (var temparray = []; integer.length > 3;) temparray.unshift(integer.substr(-3)),
	integer = integer.substr(0, integer.length - 3);
	return temparray.unshift(integer), integer = temparray.join(thousandseparater),
	sign + integer + decimalcharacter + fraction;
}

function IntegerFormat(number) {
	var decimalplaces = 0, decimalcharacter = "", thousandseparater = ",";
	number = parseFloat(number);
	var sign = 0 > number ? "-" : "", formatted = String(number.toFixed(decimalplaces));
	decimalcharacter.length && "." !== decimalcharacter && (formatted = formatted.replace(/\./, decimalcharacter));
	var integer = "", fraction = "", strnumber = String(formatted), dotpos = decimalcharacter.length ? strnumber.indexOf(decimalcharacter) : -1;
	for (dotpos > -1 ? (dotpos && (integer = strnumber.substr(0, dotpos)), fraction = strnumber.substr(dotpos + 1)) : integer = strnumber,
	integer && (integer = String(Math.abs(integer))); fraction.length < decimalplaces;) fraction += "0";
	for (var temparray = []; integer.length > 3;) temparray.unshift(integer.substr(-3)),
	integer = integer.substr(0, integer.length - 3);
	return temparray.unshift(integer), integer = temparray.join(thousandseparater),
	sign + integer + decimalcharacter + fraction;
}

function numWCommas(x) {
	var parts = x.toString().split(".");
	return parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ","), parts.join(".");
}

function getFilename() {
	var url = window.location.pathname, filename = url.substring(url.lastIndexOf("/") + 1);
	return filename;
}

function showrefreshbtn(btn, t) {
	$(btn).find("i").show();
	$(btn).find("i").addClass("fa-spin");
	$(btn).find("span").text(t);
}

function hiderefreshbtn(btn, t) {
	$(btn).find("i").removeClass("fa-spin");
	$(btn).find("i").hide();
	$(btn).find("span").text(t);
}

function msgbox(type, title, msg) {
	//type 0-Success, -1-Failed, 1-Warning, 2-Info 
	//examples of bootstrap dialog: http://nakupanda.github.io/bootstrap3-dialog/
	if (type === 0) {
		BootstrapDialog.show({
			type: BootstrapDialog.TYPE_SUCCESS,
			title: title,
			message: msg
		});
	} else if (type === -1) {
		//type must be a -1 - failed 
		BootstrapDialog.show({
			type: BootstrapDialog.TYPE_DANGER,
			title: title,
			message: msg
		});
	} else if (type === 1) {
		//type must be a 1 - Warning 
		BootstrapDialog.show({
			type: BootstrapDialog.TYPE_WARNING,
			title: title,
			message: msg
		});
	} else {
		//type must be a 2 - informational 
		BootstrapDialog.show({
			type: BootstrapDialog.TYPE_INFO,
			title: title,
			message: msg
		});
	} // end else 

} // End msgbox function 

//This will strip off the universal time indicator and remove and fractions of a second 
function fixutcdate(d) {
	var str = d.replace('Z', '');
	var str2 = str.split(".");
	return str2[0];
}

function getURLParameters(paramName) {
	var sURL = window.document.URL.toString();
	if (sURL.indexOf("?") > 0) {
		var arrParams = sURL.split("?");
		var arrURLParams = arrParams[1].split("&");
		var arrParamNames = new Array(arrURLParams.length);
		var arrParamValues = new Array(arrURLParams.length);

		var i = 0;
		for (i = 0; i < arrURLParams.length; i++) {
			var sParam = arrURLParams[i].split("=");
			arrParamNames[i] = sParam[0];
			if (sParam[1] !== "")
				arrParamValues[i] = unescape(sParam[1]);
			else
				arrParamValues[i] = "No Value";
		}

		for (i = 0; i < arrURLParams.length; i++) {
			if (arrParamNames[i] === paramName) {
				//alert("Parameter:" + arrParamValues[i]);
				return arrParamValues[i];
			}
		}
		return "No Parameters Found";
	}
}


function initalizeMenus(activePage) {
	activatePage(activePage);

	/************************
	/*	MAIN NAVIGATION
	/************************/

	$('.main-menu .js-sub-menu-toggle').on('click', function (e) {

		e.preventDefault();

		$li = $(this).parent('li');
		if (!$li.hasClass('active')) {
			$li.find(' > a .toggle-icon').removeClass('fa-angle-left').addClass('fa-angle-down');
			$li.addClass('active');
			$li.find('ul.sub-menu')
				.slideDown(300);
		}
		else {
			$li.find(' > a .toggle-icon').removeClass('fa-angle-down').addClass('fa-angle-left');
			$li.removeClass('active');
			$li.find('ul.sub-menu')
				.slideUp(300);
		}
	});

	// checking for minified left sidebar
	checkMinified();

	$('.js-toggle-minified').on('click', function () {
		if (!$('.left-sidebar').hasClass('minified')) {
			$('.left-sidebar').addClass('minified');
			$('.content-wrapper').addClass('expanded');

		} else {
			$('.left-sidebar').removeClass('minified');
			$('.content-wrapper').removeClass('expanded');
		}

		checkMinified();
	});

	function checkMinified() {
		if (!$('.left-sidebar').hasClass('minified')) {
			setTimeout(function () {

				$('.left-sidebar .sub-menu.open')
				.css('display', 'block')
				.css('overflow', 'visible')
				.siblings('.js-sub-menu-toggle').find('.toggle-icon').removeClass('fa-angle-left').addClass('fa-angle-down');
			}, 200);

			$('.main-menu > li > a > .text').animate({
				opacity: 1
			}, 1000);

		} else {
			$('.left-sidebar .sub-menu.open')
			.css('display', 'none')
			.css('overflow', 'hidden');

			$('.main-menu > li > a > .text').animate({
				opacity: 0
			}, 200);
		}
	}

	$('.toggle-sidebar-collapse').on('click', function () {
		if ($(window).width() < 992) {
			// use float sidebar
			if (!$('.left-sidebar').hasClass('sidebar-float-active')) {
				$('.left-sidebar').addClass('sidebar-float-active');
			} else {
				$('.left-sidebar').removeClass('sidebar-float-active');
			}
		} else {
			// use collapsed sidebar
			if (!$('.left-sidebar').hasClass('sidebar-hide-left')) {
				$('.left-sidebar').addClass('sidebar-hide-left');
				$('.content-wrapper').addClass('expanded-full');
			} else {
				$('.left-sidebar').removeClass('sidebar-hide-left');
				$('.content-wrapper').removeClass('expanded-full');
			}
		}
	});

	$(window).bind("load resize", determineSidebar);

	function determineSidebar() {

		if ($(window).width() < 992) {
			$('body').addClass('sidebar-float');

		} else {
			$('body').removeClass('sidebar-float');
		}
	}

	// main responsive nav toggle
	$('.main-nav-toggle').clickToggle(
		function () {
			$('.left-sidebar').slideDown(300);
		},
		function () {
			$('.left-sidebar').slideUp(300);
		}
	);

	// slimscroll left navigation
	if ($('body.sidebar-fixed').length > 0) {
		$('body.sidebar-fixed .sidebar-scroll').slimScroll({
			height: '100%',
			wheelStep: 5
		});
	}
}

function activatePage(activePage) {
	var menuLink = $('a[href$="' + activePage + '"]');
	null === menuLink && isNaN(menuLink) || ($li = menuLink.parents("li"), $li.hasClass("active") ? ($li.find(".toggle-icon").removeClass("fa-angle-down").addClass("fa-angle-left"), 
	$li.removeClass("active")) : ($li.find(".toggle-icon").removeClass("fa-angle-left").addClass("fa-angle-down"), 
	$li.addClass("active")), $li.find(".sub-menu").toggle());
}

//function initTopbar() {
//	//
//	//*******************************************
//	/*	Get User ID 
//	/********************************************/
//	//
//	var uri = "api/utility/GetUserID";
//	$.getJSON(uri, function(data) {
//		$("#username").text(data);
//	}).error(function() {
//		alert("Problem getting GetUserID data");
//	});
//	//*******************************************
//	/*	Get Version Number  
//	/********************************************/
//	uri = "api/utility/GetVersion";
//	$.getJSON(uri, function(data) {
//		$("#verbtn").text("Version: " + data);
//	}).error(function() {
//		alert("Problem getting GetVersion data");
//	});


//	// check global volume setting for each loaded page
//	var btnGlobalvol = $('.btn-global-volume');
//	var theIcon = btnGlobalvol.find('i');

//	checkGlobalVolume(theIcon, localStorage.getItem('global-volume'));

//	btnGlobalvol.click(function () {
//		var currentVolSetting = localStorage.getItem('global-volume');
//		// default volume: 1 (on)
//		if (currentVolSetting === null || currentVolSetting === "1") {
//			localStorage.setItem('global-volume', 0);
//		} else {
//			localStorage.setItem('global-volume', 1);
//		}

//		checkGlobalVolume(theIcon, localStorage.getItem('global-volume'));
//	}
//	);

//	function checkGlobalVolume(iconElement, vSetting) {
//		if (vSetting === null || vSetting === "1") {
//			iconElement.removeClass('fa-volume-off').addClass('fa-volume-up');
//		} else {
//			iconElement.removeClass('fa-volume-up').addClass('fa-volume-off');
//		}
//	}
//}

function initTopbar() {
}

function initBottombar() {

}




// End of  function 
function playIt(el) {
	//document.getElementById("embed").innerHTML = "<embed src='a.mp3' autostart=true loop=false volume=100 hidden=true>";
	//return el.mp3 ? el.mp3.paused ? el.mp3.play() : el.mp3.pause() : (el.mp3 = new Audio("a.mp3"), 
	//el.mp3.play()), !0;


	var globalVolume = localStorage.getItem('global-volume');

	if (globalVolume === null || globalVolume === '1') {
		if (el.mp3) {
			if (el.mp3.paused) el.mp3.play();
			else el.mp3.pause();
		} else {
			el.mp3 = new Audio(filename);
			el.mp3.play();
		}
	}
	return true;
}

(function ($) {
	$.QueryString = (function (a) {
		if (a === "") return {};
		var b = {};
		for (var i = 0; i < a.length; ++i) {
			var p = a[i].split('=');
			if (p.length !== 2) continue;
			b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
		}
		return b;
	})(window.location.search.substr(1).split('&'));
})(jQuery);

function ErrorMsgBox(title, msg, errno) {
	//errno = 0 OK - Success  
	//errno = 1 Continue - Info
	//errno = 417 No Content   - Warning  
	//errno = 500 Internal Server Error - Failed/Danger

	var type;
	switch (errno) {
		case 0:
			type = BootstrapDialog.TYPE_SUCCESS;
			break;
		case 1:
			type = BootstrapDialog.TYPE_INFORMATIONAL;
			break;
		case 417:
			type = BootstrapDialog.TYPE_WARNING;
			break;
		case 500:
			type = BootstrapDialog.TYPE_DANGER;
			break;
		default:
			type = BootstrapDialog.TYPE_DANGER;
	}


	BootstrapDialog.show({
		message: msg,
		title: title,
		type: type
	});
}

//pick which image to use 
function pickimage(i1, i2) {
    var ret;
    if (i2 === "") {
        ret = i1;
    } else {
        ret = i2;
    }
    return ret;
}




// toggle function
$.fn.clickToggle = function (f1, f2) {
	return this.each(function () {
		var clicked = false;
		$(this).bind('click', function () {
			if (clicked) {
				clicked = false;
				return f2.apply(this, arguments);
			}

			clicked = true;
			return f1.apply(this, arguments);
		});
	});

};
/*
 * Date Format 1.2.3
 * (c) 2007-2009 Steven Levithan <stevenlevithan.com>
 * MIT license
 *
 * Includes enhancements by Scott Trenda <scott.trenda.net>
 * and Kris Kowal <cixar.com/~kris.kowal/>
 *
 * Accepts a date, a mask, or a date and a mask.
 * Returns a formatted version of the given date.
 * The date defaults to the current date/time.
 * http://blog.stevenlevithan.com/archives/date-time-format
 * The mask defaults to dateFormat.masks.default.
 */



var dateFormat = function () {
	var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
			val = String(val);
			len = len || 2;
			while (val.length < len) val = "0" + val;
			return val;
		};

	// Regexes and supporting functions are cached through closure
	return function (date, mask, utc) {
		var dF = dateFormat;

		// You can't provide utc if you skip other args (use the "UTC:" mask prefix)
		if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
			mask = date;
			date = undefined;
		}

		// Passing date through Date applies Date.parse, if necessary
		date = date ? new Date(date) : new Date;
		if (isNaN(date)) throw SyntaxError("invalid date");

		mask = String(dF.masks[mask] || mask || dF.masks["default"]);

		// Allow setting the utc argument via the mask
		if (mask.slice(0, 4) == "UTC:") {
			mask = mask.slice(4);
			utc = true;
		}

		var _ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
				d: d,
				dd: pad(d),
				ddd: dF.i18n.dayNames[D],
				dddd: dF.i18n.dayNames[D + 7],
				m: m + 1,
				mm: pad(m + 1),
				mmm: dF.i18n.monthNames[m],
				mmmm: dF.i18n.monthNames[m + 12],
				yy: String(y).slice(2),
				yyyy: y,
				h: H % 12 || 12,
				hh: pad(H % 12 || 12),
				H: H,
				HH: pad(H),
				M: M,
				MM: pad(M),
				s: s,
				ss: pad(s),
				l: pad(L, 3),
				L: pad(L > 99 ? Math.round(L / 10) : L),
				t: H < 12 ? "a" : "p",
				tt: H < 12 ? "am" : "pm",
				T: H < 12 ? "A" : "P",
				TT: H < 12 ? "AM" : "PM",
				Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
				o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
				S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

		return mask.replace(token, function ($0) {
			return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
		});
	};
}();

// Some common format strings
dateFormat.masks = {
	"default": "ddd mmm dd yyyy HH:MM:ss",
	shortDate: "m/d/yy",
	mediumDate: "mmm d, yyyy",
	longDate: "mmmm d, yyyy",
	fullDate: "dddd, mmmm d, yyyy",
	shortTime: "h:MM TT",
	mediumTime: "h:MM:ss TT",
	longTime: "h:MM:ss TT Z",
	isoDate: "yyyy-mm-dd",
	isoTime: "HH:MM:ss",
	isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
	isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
	dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
	],
	monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
	]
};

