@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage
<h3>@Html.GetLocalizedString("Projects")</h3>
<table class="dnnGrid iw_bgt">
 <thead>
  <tr class="dnnGridHeader">
   <th data-sort="string">@Html.GetLocalizedString("ProjectName")</th>
   <th data-sort="string">@Html.GetLocalizedString("ProjectType")</th>
   <th data-sort="string">@Html.GetLocalizedString("Owners")</th>
   <th data-column-id="Edit" data-formatter="editLink">@Html.GetLocalizedString("Edit")</th>
  </tr>
 </thead>
 <tbody>
  <tr data-ng-repeat="project in projects | orderBy: 'ProjectName'" class="dnnGridItem">
   <td>
    <a href="#Project/{{project.ProjectId}}">{{project.ProjectName}}</a>
   </td>
   <td>{{project.TypeDescription}}</td>
   <td>{{project.Owners}}</td>
   <td>
    <a href="#Project/Edit/{{project.ProjectId}}" data-ng-if="security.moderator == true || project.CreatedByUserID == @Dnn.User.UserID">@Html.GetLocalizedString("Edit")</a>
   </td>
  </tr>
 </tbody>
</table>

<a href="#/Project/Edit/-1" class="dnnPrimaryAction" data-ng-if="security.submitter == true">@Html.GetLocalizedString("Add")</a>
