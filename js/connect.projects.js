var mod = angular.module('projectsModule', ['ngRoute']);

mod.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Projects', {
		template: function () {
			return getTemplate('ProjectList');
		},
		controller: 'ProjectListCtrl'
	});
	$routeProvider.when('/Project/:ProjectId', {
		template: function () {
			return getTemplate('Project');
		},
		controller: 'ProjectDetailCtrl'
	});
	$routeProvider.when('/Project/Edit/:ProjectId', {
		template: function () {
			return getTemplate('EditProject');
		},
		controller: 'ProjectDetailCtrl'
	});
	$routeProvider.otherwise({ redirectTo: '/Projects' });
}]);

mod.factory('projectsFactory', [function () {

	var viewCall = function (moduleId, controller, action, view, id, success, fail, async) {
		if (async == undefined) {
			async = true;
		};
		$.ajax({
			type: "GET",
			url: $.dnnSF(moduleId).getServiceRoot('Connect/Projects') + controller + '/' + action + '/' + id,
			beforeSend: $.dnnSF(moduleId).setModuleHeaders,
			data: { view: view },
			async: async
		}).done(function (data) {
			if (success != undefined) {
				success(data);
			}
		}).fail(function (xhr, status) {
			if (fail != undefined) {
				fail(xhr.responseText);
			}
		});
	}

	var dataCall = function (moduleId, controller, action, data, success, fail) {
		$.ajax({
			type: "GET",
			url: $.dnnSF(moduleId).getServiceRoot('Connect/Projects') + controller + '/' + action,
			beforeSend: $.dnnSF(moduleId).setModuleHeaders,
			data: data
		}).done(function (retdata) {
			if (success != undefined) {
				success(retdata);
			}
		}).fail(function (xhr, status) {
			if (fail != undefined) {
				fail(xhr.responseText);
			}
		});
	}

	var apiPostCall = function (moduleId, controller, action, data, success, fail) {
		$.ajax({
			type: "POST",
			url: $.dnnSF(moduleId).getServiceRoot('Connect/Projects') + controller + '/' + action,
			beforeSend: $.dnnSF(moduleId).setModuleHeaders,
			data: data
		}).done(function (retdata) {
			if (success != undefined) {
				success(retdata);
			}
		}).fail(function (xhr, status) {
			if (fail != undefined) {
				fail(xhr.responseText);
			}
		});
	}

	return {
		view: viewCall,
		data: dataCall,
		api: apiPostCall,
		projects: function (moduleId, success, fail) {
			dataCall(moduleId, 'Projects', 'Projects', {}, success, fail);
		},
		projectTypes: function (moduleId, success, fail) {
			dataCall(moduleId, 'ProjectTypes', 'Types', {}, success, fail);
		},
		project: function (moduleId, projectId, success, fail) {
			dataCall(moduleId, 'Projects', 'Project', { id: projectId }, success, fail);
		},
		updateProject: function (moduleId, project, success, fail) {
			apiPostCall(moduleId, 'Projects', 'Put', {
				ProjectId: project.ProjectId,
				ContactUserId: project.ContactUserId
			}, success, fail);
		},
		addTask: function (moduleId, task, success, fail) {
			apiPostCall(moduleId, 'Tasks', 'Put', {
				ProjectId: task.ProjectId,
				Tariff: task.Tariff
			}, success, fail);
		}
	}
}]);

mod.controller('ProjectListCtrl', ['$scope', '$compile', 'projectsFactory', function ($scope, $compile, projectsFactory) {
	projectsFactory.Projects($scope.moduleId, function (data) {
		$scope.Projects = data;
		$scope.$apply();
		$('.iw_bgt').bootgrid({
			columnSelection: false,
			caseSensitive: false,
			formatters: {
				"projectlink": function (column, row) {
					return '<a href="#Project/' + row.ProjectId + '">' + row.ProjectName + '</a>';
				},
				"editLink": function (column, row) {
					return '<a href="#Project/Edit/' + row.ProjectId + '">' + 'edit' + '</a>';
				}
			}
		});
	});
}]);

mod.controller('ProjectDetailCtrl', ['$scope', '$routeParams', 'projectsFactory', function ($scope, $routeParams, projectsFactory) {
	$scope.accountId = $routeParams.accountId;
	projectsFactory.project($scope.moduleId, $scope.projectId, function (data) {
		$scope.project = data;
		$scope.$apply();
	});
	$scope.updateProject = function (project) {
		projectsFactory.updateProject($scope.moduleId, project, function (data) {
			window.location.href = window.location.pathname + '#/Projects';
		});
		return false;
	}
}]);

mod.directive('showErrors', function () {
	return {
		restrict: 'A',
		link: function (scope, el, attrs) {
			var inputEl = el[0].querySelector("[name]");
			var inputNgEl = angular.element(inputEl);
			inputNgEl.bind('blur', function () {
				el.toggleClass('has-error', $(inputEl).hasClass('ng-invalid'));
			});
		}
	}
});

function getTemplate(template) {
	var moduleId = angular.element(document.getElementById('projectsModule')).scope().moduleId;
	var res = '';
	angular.element(document.getElementById('projectsModule')).
	injector().get('projectsFactory').view(moduleId,
		'Module', 'Template', template, 0,
		function (data) {
			res = data;
		},
		function (xhr, status) {
		}, false);
	return res;
}
