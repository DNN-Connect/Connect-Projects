var mod = angular.module('projectsModule', ['ngRoute', 'angularFileUpload', 'stringFormatterModule', 'ngTagsInput']);

mod.config(['$routeProvider', 'tagsInputConfigProvider', function ($routeProvider, tagsInputConfigProvider) {
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
	tagsInputConfigProvider.setTextAutosizeThreshold(30);
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

	var dataCall = function (moduleId, controller, action, data, success, fail, async) {
		if (async == undefined) {
			async = true;
		};
		$.ajax({
			type: "GET",
			url: $.dnnSF(moduleId).getServiceRoot('Connect/Projects') + controller + '/' + action,
			beforeSend: $.dnnSF(moduleId).setModuleHeaders,
			data: data,
			async: async
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
        approveProject: function (moduleId, projectId, success, fail) {
            apiPostCall(moduleId, 'Projects', 'ApproveProject', projectId, { }, success, fail);
        },
		updateProject: function (moduleId, project, success, fail) {
			apiPostCall(moduleId, 'Projects', 'Put', null, {
				project: JSON.stringify(project)
			}, success, fail);
		},
		deleteProject: function (moduleId, projectId, success, fail) {
			apiPostCall(moduleId, 'Projects', 'Delete', projectId, {}, success, fail);
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
		},
		loadTags: function (moduleId, query) {
			var res = [];
			dataCall(moduleId, 'Terms', 'Search', { query: query }, function (data) {
				res = data;
			}, null, false);
			return res;
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

mod.controller('ProjectDetailCtrl', ['$scope', '$filter', '$routeParams', 'projectsFactory', 'FileUploader', '$sce', function ($scope, $filter, $routeParams, projectsFactory, FileUploader, $sce) {
	$scope.projectId = $routeParams.ProjectId;
	projectsFactory.project($scope.moduleId, $scope.projectId, function (data) {
		$scope.project = data;
		$scope.ProjectAims = $sce.trustAsHtml($scope.project.Aims.replace(/\n/g, '<br/>'));
		$scope.ProjectDescription = $sce.trustAsHtml($scope.project.Description.replace(/\n/g, '<br/>'));
		$scope.ProjectDependencies = $sce.trustAsHtml($scope.project.Dependencies.replace(/\n/g, '<br/>'));
		$scope.$apply();
		// alert($filter('date')($scope.project.Urls[0].LastChecked, 'short'));
	});
	$scope.dateFormat = function (date, format) {
		return $filter('date')(date, format);
	}
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
    $scope.approveProject = function (projectId) {
        projectsFactory.approveProject($scope.moduleId, $scope.projectId, function (data) {
            window.location.reload();
        });
    };
	$scope.selection = [];
	$scope.$watch('project.ProjectTypes|filter:{IsSelected:true}', function (nv) {
		$scope.selection = nv.map(function (pt) {
			return pt.TypeId;
		});
	}, true);
	$scope.addUrl = function () {
		var newUrl = { "Description": "", "IsDead": false, "LastChange": "", "LastChecked": "", "ProjectId": $scope.project.ProjectId, "Retries": 5, "Url": "", "UrlId": -1, "UrlType": 0 };
		$scope.project.Urls.push(newUrl);
		$scope.$apply();
	}
	$scope.deleteUrl = function (url) {
		if (confirm('Remove this url?')) {
			$scope.project.Urls = $scope.project.Urls.filter(function (el) { return el !== url });
			$scope.$apply();
		}
	}
	$scope.loadTags = function (query) {
		var res = projectsFactory.loadTags($scope.moduleId, query);
		return res;
	}
	$scope.newTagId = -1;
	$scope.addTag = function(tag) {
		tag.TermId = $scope.newTagId;
		$scope.newTagId -= 1;
		return true;
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

String.prototype.isNull = function (replacement) {
	if (this == '') {
		return replacement;
	} else {
		return String(this);
	}
};

function isNullOrEmpty(input, replacement) {
	if (input == null || input == '') {
		return replacement;
	} else {
		return input;
	}
};

(function () {
	//Port of String.Format from C# to Angular written by David Votrubec - st-software.com
	//http://davidjs.com
	//Based on String.js from Ajax Control Toolkit - http://ajaxcontroltoolkit.codeplex.com/SourceControl/latest#Client/MicrosoftAjax/Extensions/String.js

	// Placehoders processing
	angular.module('stringFormatterModule', []).filter('stringFormat', function () {

		// function _toFormattedString is based on String.js from http://ajaxcontroltoolkit.codeplex.com/SourceControl/latest#Client/MicrosoftAjax/Extensions/String.js
		// as seen in http://stackoverflow.com/questions/2534803/string-format-in-javascript
		function toFormattedString(useLocale, format, values) {
			var result = '';

			for (var i = 0; ;) {
				// Find the next opening or closing brace
				var open = format.indexOf('{', i);
				var close = format.indexOf('}', i);
				if ((open < 0) && (close < 0)) {
					// Not found: copy the end of the string and break
					result += format.slice(i);
					break;
				}
				if ((close > 0) && ((close < open) || (open < 0))) {

					if (format.charAt(close + 1) !== '}') {
						throw new Error('format stringFormatBraceMismatch');
					}

					result += format.slice(i, close + 1);
					i = close + 2;
					continue;
				}

				// Copy the string before the brace
				result += format.slice(i, open);
				i = open + 1;

				// Check for double braces (which display as one and are not arguments)
				if (format.charAt(i) === '{') {
					result += '{';
					i++;
					continue;
				}

				if (close < 0) throw new Error('format stringFormatBraceMismatch');

				// Find the closing brace

				// Get the string between the braces, and split it around the ':' (if any)
				var brace = format.substring(i, close);
				var colonIndex = brace.indexOf(':');
				var argNumber = parseInt((colonIndex < 0) ? brace : brace.substring(0, colonIndex), 10);

				if (isNaN(argNumber)) throw new Error('format stringFormatInvalid');

				var argFormat = (colonIndex < 0) ? '' : brace.substring(colonIndex + 1);

				var arg = values[argNumber];
				if (typeof (arg) === "undefined" || arg === null) {
					arg = '';
				}

				// If it has a toFormattedString method, call it.  Otherwise, call toString()
				if (arg.toFormattedString) {
					result += arg.toFormattedString(argFormat);
				} else if (useLocale && arg.localeFormat) {
					result += arg.localeFormat(argFormat);
				} else if (arg.format) {
					result += arg.format(argFormat);
				} else
					result += arg.toString();

				i = close + 1;
			}

			return result;
		};

		return function (/*string*/template, /*array*/values) {
			if (!values || !values.length || !template) {
				return template;
			}
			return toFormattedString(false, template, values);
		};
	});

})(this);