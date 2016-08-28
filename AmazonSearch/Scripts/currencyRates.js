var app_id = "ABC";

$.ajax({
    url: 'https://openexchangerates.org/api/latest.json?app_id='+app_id,
    dataType: 'json',
    type: 'get',
    cache: 'true',
    success: function (data) {
        
        //How to get a specific rate from json object
        console.log(data.rates["GBP"]);
        
        //Building a select element 
        var listHTMLString = "<select>";
        var i = 0;
        //Grabbing all possible currency tags
        $.each(data.rates, function (index, value) {
            listHTMLString += "<option>" + Object.keys(data.rates)[i] + "</option>";
            //console.log(Object.keys(data.rates)[i]);
            i++;
        });
        listHTMLString += "</select>";

        //Placing the created select element to #currency div
        $("#currency").append(listHTMLString);
    }
});

