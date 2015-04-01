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

	var apiPostCall = function (moduleId, controller, action, id, data, success, fail) {
		var postUrl = $.dnnSF(moduleId).getServiceRoot('Connect/Projects') + controller + '/' + action;
		if (id != null) {
			postUrl += '/' + id;
		}
		$.ajax({
			type: "POST",
			url: postUrl,
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
		projectTypes: function (moduleId, projectId, success, fail) {
			dataCall(moduleId, 'ProjectTypes', 'Project', { id: projectId }, success, fail);
		},
		project: function (moduleId, projectId, success, fail) {
			if (projectId == undefined) {
				projectId = -1;
			}
			dataCall(moduleId, 'Projects', 'Project', { id: projectId }, success, fail);
		},
		updateProject: function (moduleId, project, success, fail) {
			apiPostCall(moduleId, 'Projects', 'Put', null, {
				project: JSON.stringify(project)
			}, success, fail);
		},
		deleteProject: function (moduleId, projectId, success, fail) {
			dataCall(moduleId, 'Projects', 'Delete', { id: projectId }, success, fail);
		},
		getAlbum: function (moduleId, projectId, success, fail) {
			dataCall(moduleId, 'Album', 'Get', { id: projectId }, success, fail);
		},
		deleteImage: function (moduleId, projectId, imageName, success, fail) {
			dataCall(moduleId, 'Album', 'DeleteImage', { id: projectId, image: imageName }, success, fail);
		},
		updateAlbum: function (moduleId, projectId, album, success, fail) {
			apiPostCall(moduleId, 'Album', 'Put', projectId, {
				album: JSON.stringify(album)
			}, success, fail);
		},
		commitFile: function (moduleId, projectId, fileName, success, fail) {
			dataCall(moduleId, 'Album', 'CommitFile', { id: projectId, fileName: fileName }, success, fail);
		}
	}
}]);

mod.controller('ProjectListCtrl', ['$scope', '$compile', 'projectsFactory', function ($scope, $compile, projectsFactory) {
	projectsFactory.projects($scope.moduleId, function (data) {
		$scope.projects = data;
		$scope.$apply();
	});
	$scope.deleteProject = function (projectId, confirmMsg, success, fail) {
		if (confirm(confirmMsg)) {
			projectsFactory.deleteProject($scope.moduleId, projectId, success, fail);
		}
		return false;
	}
	$scope.random = function () {
		return 0.5 - Math.random();
	}
	$scope.gotoProject = function (projectId) {
		window.location.href = window.location.pathname + '#/Project/' + projectId;
	}
}]);

mod.controller('ProjectDetailCtrl', ['$scope', '$routeParams', 'projectsFactory', 'FileUploader', '$sce', function ($scope, $routeParams, projectsFactory, FileUploader, $sce) {
	$scope.projectId = $routeParams.ProjectId;
	projectsFactory.project($scope.moduleId, $scope.projectId, function (data) {
		$scope.project = data;
		$scope.ProjectAims = $sce.trustAsHtml($scope.project.Aims.replace(/\n/g, '<br/>'));
		$scope.ProjectDescription = $sce.trustAsHtml($scope.project.Description.replace(/\n/g, '<br/>'));
		$scope.ProjectDependencies = $sce.trustAsHtml($scope.project.Dependencies.replace(/\n/g, '<br/>'));
		projectsFactory.projectTypes($scope.moduleId, $scope.projectId, function (data) {
			if (data == '') {
				$scope.project.ProjectTypes = [];
			} else {
				$scope.project.ProjectTypes = data;
			}
			$scope.$apply();
		});
	});
	projectsFactory.getAlbum($scope.moduleId, $scope.projectId, function (data) {
		$scope.album = data;
		$scope.$apply();
		$('.cp_sortable').sortable({
			update: function (e, ui) {
				var imgOrder = [];
				$('.cp_sortable li').each(function (i, el) {
					imgOrder.push($(el).attr('data-img-id'));
				});
				$scope.album.Images.forEach(function (el) {
					el.Order = imgOrder.indexOf(el.File);
				});
				$scope.$apply();
			}
		});
		$('a[href$=".gif"], a[href$=".jpg"], a[href$=".png"], a[href$=".bmp"]').colorbox();
	});
	$scope.updateProject = function (project) {
		project.SelectedProjectTypes = $scope.selection;
		projectsFactory.updateProject($scope.moduleId, project, function (data) {
			window.location.href = window.location.pathname + '#/Project/' + data;
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
		url: $.dnnSF($scope.moduleId).getServiceRoot('Connect/Projects') + 'Album/UploadFile/' + $scope.projectId,
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
	uploader.onSuccessItem = function (fileItem, response, status, headers) {
		projectsFactory.commitFile($scope.moduleId, $scope.projectId, response[0].name + response[0].ext, function (data) {
			$scope.album = data;
			$scope.$apply();
		});
	};
	uploader.onAfterAddingFile = function (fileItem) {
		$scope.saveAlbum($scope.album, false);
	};
	$scope.saveAlbum = function (album, confirmSuccess) {
		projectsFactory.updateAlbum($scope.moduleId, $scope.projectId, album, function () {
			if (confirmSuccess) {
				alert('Saved');
			}
		});
		return false;
	};
	$scope.deleteImage = function (image) {
		if (confirm('Delete this image?')) {
			projectsFactory.deleteImage($scope.moduleId, $scope.projectId, image.File, function (data) {
				$scope.album = data;
				$scope.$apply();
			});
		};
	};
	$scope.selection = [];
	$scope.$watch('project.ProjectTypes|filter:{IsSelected:true}', function (nv) {
		$scope.selection = nv.map(function (pt) {
			return pt.TypeId;
		});
	}, true);
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

mod.filter('newlines', function () {
	return function (text) {
		return text.replace(/\n/g, '<br/>');
	};
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

String.prototype.AddImageSize = function (size) {
	return this.substring(0, 15) + size + this.substring(15);
};
