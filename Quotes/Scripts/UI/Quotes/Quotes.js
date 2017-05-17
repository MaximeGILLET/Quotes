

function TagQuote(quoteId, tagLabel) {

    //Optimistic Behavior, update UI first
    $("#badge-" + tagLabel + "-" + quoteId).val($("#badge-" + tagLabel + "-" + quoteId).val()+1)

    //ajax to send to controller to record in database
    var controllerUrl = "" + '/Quote/TagQuote';
    var posting = $.post(controllerUrl, { quoteId: quoteId, tag: tagLabel });

    posting.done(function (response) {

        if (response.success) {            
            //update badge when done

        } else {
            $("#errorMessage").text("There was an error tagging the quote:" + response.message);
            $("#errorBox").show();
        }
    });

   


}

