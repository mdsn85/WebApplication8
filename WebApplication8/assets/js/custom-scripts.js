/*------------------------------------------------------
    Author : www.webthemez.com
    License: Commons Attribution 3.0
    http://creativecommons.org/licenses/by/3.0/
---------------------------------------------------------  */

(function ($) {
    "use strict";
    var mainApp = {

        initFunction: function () {
            /*MENU 
            ------------------------------------*/
            $('#main-menu').metisMenu();
			
            $(window).bind("load resize", function () {
                if ($(this).width() < 768) {
                    $('div.sidebar-collapse').addClass('collapse')
                } else {
                    $('div.sidebar-collapse').removeClass('collapse')
                }
            });

            /* MORRIS BAR CHART
			-----------------------------------------*/



            /* MORRIS DONUT CHART
			----------------------------------------*/


            /* MORRIS AREA CHART


            /* MORRIS LINE CHART
			----------------------------------------*/


       
	 
        },

        initialization: function () {
            mainApp.initFunction();

        }

    }
    // Initializing ///

    $(document).ready(function () {
        $(".dropdown-button").dropdown();

        var t = false;

		$("#sideNav").click(function(){
			if($(this).hasClass('closed')){
				$('.navbar-side').animate({left: '0px'});
				$(this).removeClass('closed');
				$('#page-wrapper').animate({'margin-left' : '260px'});
				
			}
			else{
			    $(this).addClass('closed');
				$('.navbar-side').animate({left: '-260px'});
				$('#page-wrapper').animate({'margin-left' : '0px'}); 
            }
            t = true;
        });



		
        mainApp.initFunction(); 
    });

	$(".dropdown-button").dropdown();
	
}(jQuery));
