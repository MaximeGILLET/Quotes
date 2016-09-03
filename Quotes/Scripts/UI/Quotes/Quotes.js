

function TagQuote(quoteId, tagLabel) {
    
    //ajax to send to controller to record in database
    var controllerUrl = "" + '/Quote/TagQuote';
    var posting = $.post(controllerUrl, { quoteId: quoteId, tag: tagLabel });

    posting.done(function (response) {

        if (response.success) {
            alert("Success!");
            //update badge when done
        } else {
            alert("Error processing your request.");
            //display error message (already tagged,like...), if we are here it means user tried to force the button action by altering html on the page.
        }
    });

   


}

