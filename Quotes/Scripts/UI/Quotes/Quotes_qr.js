(function() {
	// qr's templating API
	// qr, function: params -> parameters checking, node location, DOMElement (module) injection, script tag removal
	// qr module, function: params -> DOMElement
	// params, object: keys -> _: qr module name, other: module-specific params
	// How to use qr DOM Modules: <script class="qr">qr({_: 'CenterText', text: 'One'});</script>

	window.qr = function(params) {
		qr.checkParams(params);
		var sn = qr.getNode(params);
		var r = qr[params._](params);
		sn.node.appendChild(r);
		sn.node.removeChild(sn.self);
	};

	qr.checkParams = function(params) {
		if(!params._) throw "Missing _ in call to qr";
		if(Object.keys(qr).indexOf(params._) == -1) throw "Unknown _ \"" + params._ + "\"";
	};

	qr.getNode = function(params) {
		var self = document.querySelector('.qr');
		var node = self.parentNode;
		return {self: self, node: node};
	};
})();

(function() {
	qr.CenterText = function(params) {
		if(!params.text && params.text != "") throw "Missing text in call to " + params._;

		var CenterText = document.createElement('div');
		CenterText.style.textAlign = "center";
		CenterText.appendChild(document.createTextNode(params.text));

		return CenterText;
	};

	qr.QuoteBox = function(params) {
		if(!params.backgroundColor) throw "Missing backgroundColor in call to " + params._;
		if(!params.id) throw "Missing id in call to " + params._;

		var CenterText = qr.CenterText(params);
		
		var QuoteBox = CenterText;
		var initialMargins = {
			right: Math.round(Math.random()*50 - 25),
			left: Math.round(Math.random()*100),
			top: Math.round(Math.random()*50 - 25)
		}
		//QuoteBox.style.display = 'inline-block';
		QuoteBox.style.padding = '10px';
		QuoteBox.style.marginRight = initialMargins.right + 'px';
		QuoteBox.style.marginLeft = initialMargins.left + 'px';
		QuoteBox.style.color = 'white';
		QuoteBox.style.borderRadius = '3px';
		QuoteBox.style.marginTop = initialMargins.top + 'px';
		QuoteBox.style.float = 'left';
		QuoteBox.style.backgroundColor = params.backgroundColor;
		QuoteBox.style.display = 'none';
		QuoteBox.className = 'QuoteBox' + params.id;

		setTimeout(function() {
			$('.QuoteBox' + params.id).fadeOut("slow", function() {
				$(QuoteBox).fadeIn("slow", function() {});
			});
		}, Math.floor(Math.random()*1000));

		return QuoteBox;
	};
})();


(function() {
	var inject = function(element, module) {
		var scr = document.createElement('script');
		scr.className = "qr";
		scr.innerHTML = "qr(" + JSON.stringify(module) + ");";
		element.appendChild(scr);
	};

	var displayQuotes = function(quotes) {
		for(var i = 0; i < quotes.length; i++) {
			inject(container, {
				_: 'QuoteBox',
				id: quotes[i].Quote.QuoteId,
				text: quotes[i].Quote.QuoteText,
				backgroundColor: 'rgba(0, 0, 0, 0.5)'
			});
		}
	};

	var container = document.createElement('div');
	container.style.textAlign = 'center';
	document.body.appendChild(container);

	
	setInterval(function() {
		$.getJSON("/Quote/search", function(data) {
			displayQuotes(data);
		});
	}, 1000);
	

	$.getJSON("/Quote/search", function(data) {

		displayQuotes(data);

		var pre = document.createElement('pre');
		pre.style.marginTop = '200px';
		pre.innerHTML = JSON.stringify(data, null, 4);
		document.body.appendChild(pre);
	});

})();
