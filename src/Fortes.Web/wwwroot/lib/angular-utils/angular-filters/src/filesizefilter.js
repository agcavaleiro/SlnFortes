//string number, finite/infinite, prefix,precision,automatic prefix and precision,zero
//TODO: add conversions to parse string with prefixes and change to appropriate sizes
//Supported prefixes: k,K,kb,KB (replace k by m,g,p,t,e, or remove k for just b),both capital and small letters supported
//only 1st letter of supplied prefix is used to format
//cases to care for: if user doesn't give prefix then 800bytes are shown as 0.8KB; similarly 800 KB is shown as 0.8MB
//without prefix auto generated prefix are always capital and have a space between them and the number
//currently it is assumed that size is given in bytes.
(function (angular) {
	angular.module('filesize-filter', [])
		.filter('filesize', function ($filter) {
			function checkNum(number) {
				//number as well as number in string reprsentation are supported
				//FIX: comma formatted number strings are returned false
				var num = 0;
				var anum = /(^\d+$)|(^\d+\.\d+$)/;
				if ((number || number === 0) && typeof number != 'object' && typeof number != 'boolean') {
					if(typeof number=='string')
					{
						number=number.trim();
					}

					if (anum.test(number)) {
						num = parseFloat(number);
						return num;
					} else
					{
						return false;
					}
				} else
				{
					return false;
				}
			}
			var defaultPrefixes = [' B', ' KB', ' MB', ' GB', ' TB', ' PB', ' EB', ' ZB', ' YB'];
			var dataPrefixes=['b','k','m','g','t','p','e','z','y'];
			return function (number, pref, prec) //number,prefix,precision
				{

					var num = 0;
					var precison = checkNum(prec);
					if(!(precison||precison===0))
					{
						precison=2;
					}
					var prefix = false;
					if (typeof pref == 'string' && pref) {
						prefix = pref.trim().charAt(0).toLowerCase();
						//TODO: Error message needed here for wrong/no prefixes.
					}
					var divnum=dataPrefixes.indexOf(prefix);
					num = checkNum(number);
					if (num || num === 0) {
						if(divnum!==-1)
						{
							num = num / Math.pow(1024, divnum);
							num = $filter("number")(num, precison);
							return num + pref;
						}
						else
						{
							var length = ~~ (Math.log(num) / Math.LN10 + 1);
							var divnum2= parseInt(length/3);
							num = num / Math.pow(1024, divnum2);
							num = $filter("number")(num, precison);
							return num + defaultPrefixes[divnum2];
						}
					}
					else {
						return number;
					}
				};
		});
})(angular);
