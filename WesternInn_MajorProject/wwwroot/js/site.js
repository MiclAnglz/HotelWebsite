// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$("#gridRadios2").click(function () {
	$('#fName').css('display', 'none');
	$('#sName').css('display', 'none');
	$('#bID').css('display', 'block');
	$("#fname").val('');
	$("#sname").val('');
});

$("#gridRadios1").click(function () {
	$('#bID').css('display', 'none');
	$('#fName').css('display', 'block');
	$('#sName').css('display', 'block');
	$("#bid").val(0);
});

