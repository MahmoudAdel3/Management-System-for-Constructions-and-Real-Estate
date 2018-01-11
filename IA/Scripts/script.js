$(function() {
	$("html").niceScroll({
		cursorcolor: "#444",
		background: "#ddd",
		cursorwidth: "6px",
		cursorborderradius: "6px",
		railpadding: {
			top: 0,
			right: 1,
			left: 0,
			bottom: 0
		},
		smoothscroll: true
	});
});
$(function() {
	$(window).scroll(function() {
		var scroll = $(this).scrollTop();
		if (scroll >130) {
			$('#scroll_nav').slideDown();
		} else {
			$('#scroll_nav').slideUp();
		}
	});
	$('header nav ul.nav li a').click(function(){
	$('html , body').animate({
	scrollTop : $('#' + $(this).data('value')).offset().top},1500);
	
	
});
	});


$(document).ready(function(){
	$('.row-image').mixItUp();
});


    
  