<h3>@Html.GetLocalizedString("Projects")</h3>
<table class="table table-condensed table-hover table-striped iw_bgt">
 <thead>
  <tr>
   <th data-column-id="ProjectId" data-type="numeric" data-identifier="true" data-visible="false">ID</th>
   <th data-column-id="ProjectName" data-formatter="accountlink">@Html.GetLocalizedString("ProjectName")</th>
   <th data-column-id="Edit" data-formatter="editLink">@Html.GetLocalizedString("Edit")</th>
  </tr>
 </thead>
 <tbody>
  <tr data-ng-repeat="project in projects | orderBy: 'ProjectName'">
   <td>{{project.ProjectId}}</td>
   <td>{{project.ProjectName}}</td>
   <td></td>
  </tr>
 </tbody>
</table>

<a href="#/Project/Edit/-1" class="dnnPrimaryAction">@Html.GetLocalizedString("Add")</a>
