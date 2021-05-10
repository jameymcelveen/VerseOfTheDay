// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var VerseOfTheDay = VerseOfTheDay || {};

$(function () {
    var $startDate = $("#startdate");
    var $pageSize = $("#pagesize");
    
    _init();
    
    function _addFavorite(date) {
        $.get( '/home/mark?date=' + date + '&how=on', function( data ) {
            $( ".result" ).html( data );
        });
    }
    
    function _removeFavorite(id) {
        alert(id);
    }
    
    function _submit() {
        window.location.href = '/Home/Index?startDate=' + $startDate.val() + '&pageSize='  + $pageSize.val();
    }
    
    function _init() {
        var today = new Date();
        var dd = ('0' + (today.getDate())).slice(-2);
        var mm = ('0' + (today.getMonth() +　1)).slice(-2);
        var yyyy = today.getFullYear();
        today = yyyy + '-' + mm + '-' + dd ;
        $startDate.attr('value', today);
        $pageSize.attr('value', 10);
    }
    
    VerseOfTheDay.addFavorite = _addFavorite;
    VerseOfTheDay.removeFavorite = _removeFavorite;
    VerseOfTheDay.submit = _submit;
});