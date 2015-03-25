var mod = angular.module('projectsModule', ['ngRoute', 'angularFileUpload']);

mod.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Projects', {
		template: function () {
			return getTemplate('ProjectList');
		},
		controller: 'ProjectListCtrl'
	});
	$routeProvider.when('/Project/:ProjectId', {
		template: function () {
			return getTemplate('ProjectDetails');
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
			if (projectId == undefined) {
				projectId = -1;
			}
			dataCall(moduleId, 'Projects', 'Project', { id: projectId }, success, fail);
		},
		updateProject: function (moduleId, project, success, fail) {
			apiPostCall(moduleId, 'Projects', 'Put', {
				ProjectId: project.ProjectId,
				Visible: project.Visible,
				ProjectName: project.ProjectName,
				ProjectType: project.ProjectType,
				Url1: project.Url1,
				Url2: project.Url2,
				Status: project.Status,
				Owners: project.Owners,
				Aims: project.Aims,
				Description: project.Description,
				Dependencies: project.Dependencies
			}, success, fail);
		},
		approveProject: function (moduleId, projectId, approved, success, fail) {
			dataCall(moduleId, 'Projects', 'Approve', { id: projectId, approved: approved }, success, fail);
		},
		deleteProject: function (moduleId, projectId, success, fail) {
			dataCall(moduleId, 'Projects', 'Delete', { id: projectId }, success, fail);
		}
	}
}]);

mod.controller('ProjectListCtrl', ['$scope', '$compile', 'projectsFactory', function ($scope, $compile, projectsFactory) {
	projectsFactory.projects($scope.moduleId, function (data) {
		$scope.projects = data;
		$scope.$apply();
		$('.iw_bgt').stupidtable();
	});
	$scope.approveProject = function (projectId, approved, success, fail) {
		projectsFactory.approveProject($scope.moduleId, projectId, approved, success, fail);
		return false;
	}
	$scope.deleteProject = function (projectId, confirmMsg, success, fail) {
		if (confirm(confirmMsg)) {
			projectsFactory.deleteProject($scope.moduleId, projectId, success, fail);
		}
		return false;
	}
}]);

mod.controller('ProjectDetailCtrl', ['$scope', '$routeParams', 'projectsFactory', 'FileUploader', function ($scope, $routeParams, projectsFactory, FileUploader) {
	$scope.projectId = $routeParams.ProjectId;
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
	$scope.approveProject = function (projectId, approved) {
		projectsFactory.approveProject($scope.moduleId, projectId, approved, function (data) {
		});
		return false;
	}
	$scope.deleteProject = function (projectId, confirmMsg) {
		if (confirm(confirmMsg)) {
			projectsFactory.deleteProject($scope.moduleId, projectId, function (data) {
				window.location.href = window.location.pathname + '#/Projects';
			});
		}
		return false;
	}
	var uploader = $scope.uploader = new FileUploader({
		url: $.dnnSF($scope.moduleId).getServiceRoot('Connect/Projects') + 'Module/UploadFile/' + $scope.projectId,
		autoUpload: true,
		headers: {
			moduleId: $scope.moduleId,
			tabId: $scope.tabId,
			RequestVerificationToken: $('[name="__RequestVerificationToken"]').val()
		}
	});
	uploader.filters.push({
		name: 'imageFilter',
		fn: function (item /*{File|FileLikeObject}*/, options) {
			var type = '|' + item.type.slice(item.type.lastIndexOf('/') + 1) + '|';
			return '|jpg|png|jpeg|bmp|gif|'.indexOf(type) !== -1;
		}
	});
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
