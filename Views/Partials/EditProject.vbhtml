<div class="dnnForm dnnClear">
 <fieldset>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("ProjectName")</span>
    </label>
   </div>
   <input type="text" id="txtProjectName" class="form-control" data-ng-model="project.ProjectName" />
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("ProjectType")</span>
    </label>
   </div>

  </div>
 </fieldset>
</div>

<a href="#/Accounts" class="btn btn-default dnnSecondaryAction">@Html.GetLocalizedString("Return")</a>
<button type="button" class="btn btn-primary dnnPrimaryAction" data-ng-click="updateProject(project)">@Html.GetLocalizedString("Submit")</button>
