@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage
@Imports Connect.DNN.Modules.Projects.Models.Projects
@Imports Connect.DNN.Modules.Projects.Controllers.Projects

<div data-ng-app="projectsModule"
     data-ng-init="moduleId = @Dnn.Module.ModuleId; tabId = @Dnn.Tab.TabID; security = {moderator: @Security.Moderator.ToString.ToLower, submitter: @Security.Submitter.ToString.ToLower}; settings = {tnWidth: @Settings.TnWidth, tnHeight: @Settings.TnHeight, medWidth: @Settings.MedWidth, medHeight: @Settings.MedHeight, zoomWidth: @Settings.ZoomWidth, zoomHeight: @Settings.ZoomHeight}"
     id="projectsModule">
 <div data-ng-view>

 </div>
 <div class="modal fade" id="cmModal" tabindex="-1" role="dialog" aria-labelledby="cmModalLabel" aria-hidden="true"></div>
</div>

