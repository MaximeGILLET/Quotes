

function TagQuote(quoteId, tagLabel) {
    
    //ajax to send to controller to record in database
    var controllerUrl = "" + '/Quote/TagQuote';
    var posting = $.post(controllerUrl, { quoteId: quoteId, tag: tagLabel });

    posting.done(function (response) {

        if (response.success) {
            alert("Success!");
            //update badge when done
        } else {
            $("#errorMessage").text("There was an error performing the action:" + response.message);
            $("#errorBox").show();
        }
    });

   


}

