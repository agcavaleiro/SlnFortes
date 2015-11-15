(function(angular){
	angular.module('filesize-ctrl', ['filesize-filter'])
	.controller('test-ctrl', ['$scope',
							  function ($scope) {
									$scope.sizes = [0,80,999, 800, 8000, 80000, 800000, 8000000, 80000000, 800000000, 8000000000, 800000000000, 8000000000000, 80000000000000, 800000000000000];
									$scope.prefix='';
								  	$scope.precision=2;
							  }]);
})(angular);
