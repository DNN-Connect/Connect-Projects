@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage
@Imports Connect.DNN.Modules.Projects.Models.Projects
@Imports Connect.DNN.Modules.Projects.Controllers.Projects

<h2>@Html.GetLocalizedString("Projects")</h2>

<div data-ng-app="projectsModule" data-ng-init="moduleId = @Dnn.Module.ModuleId; security = {moderator: @Security.Moderator.ToString.ToLower, submitter: @Security.Submitter.ToString.ToLower};" id="projectsModule">
 <div data-ng-view>

 </div>
 <div class="modal fade" id="cmModal" tabindex="-1" role="dialog" aria-labelledby="cmModalLabel" aria-hidden="true"></div>
</div>

