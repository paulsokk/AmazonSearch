var app_id = "ABC";
var dataObject;
var pageCurrency;

$(document).ready(function () {

    //Sets display currency on page load
    pageCurrency = getUrlVars()["currency"];
    if (pageCurrency == undefined) {
        pageCurrency = "GBP"
    }

    $('.productPrice').each(function () {
        this.innerHTML = Number(parseFloat(this.innerHTML) / 100.0).toFixed(2) + " " + pageCurrency;
    });

    
});


//Getting currency rates from API
$.ajax({
    url: 'https://openexchangerates.org/api/latest.json?app_id='+app_id,
    dataType: 'json',
    type: 'get',
    cache: 'true',
    success: function (data) {
        
        dataObject = data;
        
        //Building a select element with all available currencies 
        var listHTMLString = "<select>";
        var i = 0;
        //Grabbing all possible currency tags
        $.each(data.rates, function (index, value) {
            var selectedVal = '';
            if (Object.keys(data.rates)[i] == pageCurrency) {
                selectedVal = "selected";
            }
           
            listHTMLString += "<option "+ selectedVal +" >" + Object.keys(data.rates)[i] + "</option>";
            i++;
        });
        listHTMLString += "</select>";

        //Placing the created select element to #currency div
        $("#currency").append(listHTMLString);


        setPrices();

    }
});


var fromCurrency = "GBP";
var toCurrency;
var x;
var y;

//Listen for change
$('#currency').on('change', function () {

    setPrices();
   
});



///
//Functions
///

//Returns all variables from URL query
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}


//Converts prices from one currency to another
function setPrices() {
    //Remembers previous currency
    if (toCurrency !== undefined) {
        fromCurrency = toCurrency;
    }

    //From oldXXX to USD ratio
    x = 1 / dataObject.rates[fromCurrency];

    toCurrency = $("#currency option:selected").text();
    //console.log(fromCurrency + " to " + toCurrency);

    $('.productPrice').each(function () {

        //To USD conversion
        var XXXAmount = parseFloat(this.innerHTML);
        var USDAmount = XXXAmount * x;

        //From USD to newXXX ratio
        y = dataObject.rates[toCurrency];

        //To newXXX conversion
        var toAmount = USDAmount * y;

        //console.log(toAmount);
        this.innerHTML = Number((Math.round(toAmount * 100.0)) / 100.0).toFixed(2) + " " + toCurrency;


        //Give "Next" and "Previous" page links selected currency values
        var keyword = getUrlVars()["keyword"];
        var nextPage = parseInt(getUrlVars()["page"]) + 1;
        var prevPage = parseInt(getUrlVars()["page"]) - 1;

        //Setting up "Previous" button's href, so that keyword, page number and currency are passed to the previous page through URL query
        var prevParams = { 'keyword': keyword, 'page': prevPage, 'currency': toCurrency };
        var prevURL = "/Products/Search?" + jQuery.param(prevParams);
        $('#previous').attr("href", prevURL);

        //Setting up "Next" button's href, so that keyword, page number and currency are passed to the next page through URL query
        var nextParams = { 'keyword': keyword, 'page': nextPage, 'currency': toCurrency };
        var nextURL = "/Products/Search?" + jQuery.param(nextParams);
        $('#next').attr("href", nextURL);
    });
}