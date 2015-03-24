@Imports Connect.DNN.Modules.Projects.Models.ProjectTypes
@Imports Connect.DNN.Modules.Projects.Controllers.ProjectTypes

<div class="dnnForm dnnClear" data-ng-form name="frmEditProject">
 <fieldset>
  <div class="dnnFormItem" show-errors>
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("ProjectName")</span>
    </label>
   </div>
   <input type="text" id="txtProjectName" name="ProjectName" class="form-control" data-ng-model="project.ProjectName"
          required />
   <p class="help-block" ng-if="frmEditProject.ProjectName.$error.required">@Html.GetLocalizedString("ProjectName.Required")</p>
  </div>
  <div class="dnnFormItem" show-errors>
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("ProjectType")</span>
    </label>
   </div>
   <select id="ProjectType" name="ProjectType" class="form-control" data-ng-model="project.ProjectType" required>
    @For Each pt As ProjectType In ProjectTypesController.GetProjectTypes
     @<option value="@pt.ProjectTypeId">@pt.TypeDescription</option>
    Next
   </select>
   <p class="help-block" ng-if="frmEditProject.ProjectType.$error.required">@Html.GetLocalizedString("ProjectType.Required")</p>
  </div>
  <div class="dnnFormItem" show-errors>
   <div class="dnnLabel">
    <label>
     <span>Url 1</span>
    </label>
   </div>
   <input type="text" id="txtUrl1" name="Url1" class="form-control" data-ng-model="project.Url1"
          data-ng-pattern="/^(http|https)\:///" />
   <p class="help-block" ng-if="frmEditProject.Url1.$error.pattern">@Html.GetLocalizedString("Url.Pattern")</p>
  </div>
  <div class="dnnFormItem" show-errors>
   <div class="dnnLabel">
    <label>
     <span>Url 2</span>
    </label>
   </div>
   <input type="text" id="txtUrl2" name="Url2" class="form-control" data-ng-model="project.Url2"
          data-ng-pattern="/https?\:.*/" />
   <p class="help-block" ng-if="frmEditProject.Url2.$error.pattern">@Html.GetLocalizedString("Url.Pattern")</p>
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Status")</span>
    </label>
   </div>
   <input type="text" id="txtStatus" class="form-control" data-ng-model="project.Status" />
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Owners")</span>
    </label>
   </div>
   <input type="text" id="txtOwners" class="form-control" data-ng-model="project.Owners" />
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Aims")</span>
    </label>
   </div>
   <textarea cols="80" rows="5" data-ng-model="project.Aims"></textarea>
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Description")</span>
    </label>
   </div>
   <textarea cols="80" rows="5" data-ng-model="project.Description"></textarea>
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Dependencies")</span>
    </label>
   </div>
   <textarea cols="80" rows="5" data-ng-model="project.Dependencies"></textarea>
  </div>
 </fieldset>
</div>

<a href="#/Accounts" class="btn btn-default dnnSecondaryAction">@Html.GetLocalizedString("Return")</a>
<button type="button" class="btn btn-primary dnnPrimaryAction"
        data-ng-click="updateProject(project)"
        data-ng-disabled="frmEditProject.$invalid">
 @Html.GetLocalizedString("Submit")
</button>
