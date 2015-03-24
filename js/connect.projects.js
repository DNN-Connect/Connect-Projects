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
				"lasttask": function (column, row) {
					return row.LastTaskDisplay;
				},
				"editLink": function (column, row) {
					return '<a href="#Project/Edit/' + row.ProjectId + '">' + 'edit' + '</a>';
				},
				"addLink": function (column, row) {
					return '<a href="#" data-action="addTask" data-Project-id="' + row.ProjectId + '">' + 'add' + '</a>';
				}
			}
		})
		.on("loaded.rs.jquery.bootgrid", function (e) {
			$('a[data-action="addTask"]').click(function () {
				projectsFactory.view($scope.moduleId, 'Tasks', 'Add', 'AddTask', $(this).attr('data-Project-id'), function (data) {
					$scope.newTask = {};
					$('#cmModal').html(data);
					$compile($('#cmModal'))($scope);
					preparePopup();
					$('#cmModal').modal();
				});
				return false;
			});
		});
		$scope.addTask = function () {
			projectsFactory.addTask($scope.moduleId, $scope.newTask, function (data) {
			});
		};
	});
}]);

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
