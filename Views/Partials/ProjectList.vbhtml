<h3>@Html.GetLocalizedString("Projects")</h3>
<table class="dnnGrid iw_bgt">
 <thead>
  <tr class="dnnGridHeader">
   <th data-sort="string">@Html.GetLocalizedString("ProjectName")</th>
   <th data-column-id="Edit" data-formatter="editLink">@Html.GetLocalizedString("Edit")</th>
  </tr>
 </thead>
 <tbody>
  <tr data-ng-repeat="project in projects | orderBy: 'ProjectName'" class="dnnGridItem">
   <td>
    <a href="#Project/{{project.ProjectId}}">{{project.ProjectName}}</a>
   </td>
   <td>
    <a href="#Project/Edit/{{project.ProjectId}}">@Html.GetLocalizedString("Edit")</a>
   </td>
  </tr>
 </tbody>
</table>

<a href="#/Project/Edit/-1" class="dnnPrimaryAction">@Html.GetLocalizedString("Add")</a>
