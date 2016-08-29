var app_id = "ABC";
var dataObject;

$(document).ready(function () {
    $('.productPrice').each(function () {
        this.innerHTML = (parseFloat(this.innerHTML)/100.0)+" GBP";
    });
});


///
//Getting currency rates
///
$.ajax({
    url: 'https://openexchangerates.org/api/latest.json?app_id='+app_id,
    dataType: 'json',
    type: 'get',
    cache: 'true',
    success: function (data) {
        
        dataObject = data;

        //How to get a specific rate from json object
        //console.log(data.rates["GBP"]);
        
        //Building a select element 
        var listHTMLString = "<select>";
        var i = 0;
        //Grabbing all possible currency tags
        $.each(data.rates, function (index, value) {
            var selectedVal = '';
            if (Object.keys(data.rates)[i] == 'GBP') {
                selectedVal = "selected";
            }
           
            listHTMLString += "<option "+ selectedVal +" >" + Object.keys(data.rates)[i] + "</option>";
            //console.log(Object.keys(data.rates)[i]);
            i++;
        });
        listHTMLString += "</select>";

        //Placing the created select element to #currency div
        $("#currency").append(listHTMLString);

    }
});




///
//Currency conversion
///

var fromCurrency = "GBP";
var toCurrency;
var x;
var y;

//Listen for change
$('#currency').on('change', function () {

    //Remembers previous currency
    if (toCurrency !== undefined) {
        fromCurrency = toCurrency;
    }

    //From oldXXX to USD ratio
    x = 1 / dataObject.rates[fromCurrency];

    toCurrency = $("#currency option:selected").text(); // or $(this).val()
    console.log(fromCurrency +" to "+ toCurrency);

    $('.productPrice').each(function () {

        //To USD conversion
        var XXXAmount = parseFloat(this.innerHTML);
        var USDAmount = XXXAmount * x;

        //From USD to newXXX ratio
        y = dataObject.rates[toCurrency];

        //To newXXX conversion
        var toAmount = USDAmount * y;

        //console.log(toAmount);
        this.innerHTML = (Math.round(toAmount*100)) / 100.0 + " " + toCurrency;
    });
});

