@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage

@Code
 Dim imgPath As String = String.Format("{0}Connect/Projects/{1}/", Dnn.Portal.HomeDirectory, Dnn.Module.ModuleID)
End Code

<div data-ng-repeat="project in projects | orderBy: random" class="cp_pl">
 <div data-project-id="{{project.ProjectId}}" class="cp_pl_project" style="background-color:#{{project.ProjectColor}}" data-ng-click="gotoProject(project.ProjectId)">
  <div class="cp_pl_img">
   <img src="@imgPath{{project.ProjectId}}/{{project.FirstImage.AddImageSize('_tn')}}" width="{{settings.tnWidth}}" height="{{settings.tnHeight}}" alt="{{project.ProjectName}}" />
  </div>
  <div class="cp_pl_title">{{project.ProjectName}}</div>
  <div class="cp_pl_other">
   {{project.Owners}}<br />
   {{project.Status}}
  </div>
  <div class="cp_pl_other">
   <a href="#Project/Edit/{{project.ProjectId}}"
      data-ng-if="security.moderator == true || project.CreatedByUserID == @Dnn.User.UserID"
      >@Html.GetLocalizedString("Edit")</a>
     </div>
  </div>
</div>
<div class="cp_pl_btns">
 <a href="#/Project/Edit/-1" class="dnnPrimaryAction" data-ng-if="security.submitter == true">@Html.GetLocalizedString("Add")</a>
</div>

