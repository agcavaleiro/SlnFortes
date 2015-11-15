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

					}
					num = checkNum(number);
					if (num || num === 0) {
						switch (prefix) {
						case 'b':
							return num + pref;
						case 'k':
							num = num / 1024;
							num = $filter("number")(num, precison);
							return num + pref;
						case 'm':
							num = num / Math.pow(1024, 2);
							num = $filter("number")(num, precison);
							return num + pref;
						case 'g':
							num = num / Math.pow(1024, 3);
							num = $filter("number")(num, precison);
							return num + pref;
						case 't':
							num = num / Math.pow(1024, 4);
							num = $filter("number")(num, precison);
							return num + pref;
						case 'p':
							num = num / Math.pow(1024, 5);
							num = $filter("number")(num, precison);
							return num + pref;
						case 'e':
							num = num / Math.pow(1024, 6);
							num = $filter("number")(num, precison);
							return num + pref;
						case 'z':
							num = num / Math.pow(1024, 7);
							num = $filter("number")(num, precison);
							return num + pref;
						case 'y':
							num = num / Math.pow(1024, 8);
							num = $filter("number")(num, precison);
							return num + pref;
						default:
							var length = ~~ (Math.log(num) / Math.LN10 + 1);
							if (length < 3) {
								return num + defaultPrefixes[0];
							} else if (length < 6) //length<6&&length>3
							{
								num = num / Math.pow(1024, 1);
								num = $filter("number")(num, precison);
								return num + defaultPrefixes[1];
							} else if (length < 9) {
								num = num / Math.pow(1024, 2);
								num = $filter("number")(num, precison);
								return num + defaultPrefixes[2];
							} else if (length < 12) {
								num = num / Math.pow(1024, 3);
								num = $filter("number")(num, precison);
								return num + defaultPrefixes[3];
							} else if (length < 15) {
								num = num / Math.pow(1024, 4);
								num = $filter("number")(num, precison);
								return num + defaultPrefixes[4];
							} else if (length < 18) {
								num = num / Math.pow(1024, 5);
								num = $filter("number")(num, precison);
								return num + defaultPrefixes[5];
							} else if (length < 21) {
								num = num / Math.pow(1024, 6);
								num = $filter("number")(num, precison);
								return num + defaultPrefixes[6];
							} else if (length < 24) {
								num = num / Math.pow(1024, 7);
								num = $filter("number")(num, precison);
								return num + defaultPrefixes[7];
							} else if (length < 27) {
								num = num / Math.pow(1024, 8);
								num = $filter("number")(num, precison);
								return num + defaultPrefixes[8];
							}
							break;

						}
					} else {
						return number;
					}
				};
		});
})(angular);
