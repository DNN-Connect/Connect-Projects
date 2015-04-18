﻿@Imports Connect.DNN.Modules.Projects.Models.ProjectTypes
@Imports Connect.DNN.Modules.Projects.Controllers.ProjectTypes
@Imports Connect.DNN.Modules.Projects.Models.LicenseTypes
@Imports Connect.DNN.Modules.Projects.Controllers.LicenseTypes

<div class="dnnForm dnnClear" data-ng-form name="frmEditProject">
 <fieldset>
  <div class="dnnFormItem" show-errors>
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("ProjectName")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("ProjectName.Help")</span>
     </div>
    </div>
   </div>
   <input type="text" id="txtProjectName" name="ProjectName" class="form-control" data-ng-model="project.ProjectName"
          required />
   <p class="help-block" ng-if="frmEditProject.ProjectName.$error.required">@Html.GetLocalizedString("ProjectName.Required")</p>
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("ProjectTypes")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("ProjectTypes.Help")</span>
     </div>
    </div>
   </div>
   <div class="dnnFormItemValue">
    <ul class="cp_checkboxList">
     <li data-ng-repeat="pt in project.ProjectTypes">
      <input type="checkbox"
             name="selectedTypes[]"
             value="{{pt.TypeId}}"
             ng-model="pt.IsSelected"> {{pt.TypeDescription}}
     </li>
    </ul>
   </div>
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Tags")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("Tags.Help")</span>
     </div>
    </div>
   </div>
   <tags-input data-ng-model="project.ProjectTags" display-property="Name" key-property="TermId">
    <auto-complete source="loadTags($query)" display-property="Name" min-length="2"></auto-complete>
   </tags-input>
</div>
  <div class="dnnFormItem" show-errors>
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("LicenseType")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("LicenseType.Help")</span>
     </div>
    </div>
   </div>
   <select id="LicenseType" name="LicenseType" class="form-control" data-ng-model="project.LicenseTypeId" required>
    @For Each lt As LicenseType In LicenseTypesController.GetLicenseTypes
     @<option value="@lt.LicenseTypeId">@lt.TypeDescription</option>
    Next
   </select>
   <p class="help-block" ng-if="frmEditProject.ProjectType.$error.required">@Html.GetLocalizedString("ProjectType.Required")</p>
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Status")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("Status.Help")</span>
     </div>
    </div>
   </div>
   <input type="text" id="txtStatus" class="form-control" data-ng-model="project.Status" />
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Owners")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("Owners.Help")</span>
     </div>
    </div>
   </div>
   <input type="text" id="txtOwners" class="form-control" data-ng-model="project.Owners" />
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("People")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("People.Help")</span>
     </div>
    </div>
   </div>
   <input type="text" id="txtPeople" class="form-control" data-ng-model="project.People" />
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Aims")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("Aims.Help")</span>
     </div>
    </div>
   </div>
   <textarea cols="80" rows="5" data-ng-model="project.Aims"></textarea>
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Description")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("Description.Help")</span>
     </div>
    </div>
   </div>
   <textarea cols="80" rows="5" data-ng-model="project.Description"></textarea>
  </div>
  <div class="dnnFormItem">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Dependencies")</span>
    </label>
    <a href="#" class="dnnFormHelp" tabindex="-1"></a>
    <div class="dnnTooltip" style="position: absolute; right: -29%; top: -102px;">
     <div class="dnnFormHelpContent dnnClear" style="visibility: hidden;">
      <span class="dnnHelpText">@Html.GetLocalizedString("Dependencies.Help")</span>
     </div>
    </div>
   </div>
   <textarea cols="80" rows="5" data-ng-model="project.Dependencies"></textarea>
  </div>
  <div class="dnnFormItem" data-ng-if="security.moderator == true">
   <div class="dnnLabel">
    <label>
     <span>@Html.GetLocalizedString("Visible")</span>
    </label>
   </div>
   <input type="checkbox" id="chkVisible" class="form-control" data-ng-model="project.Visible" />
  </div>
 </fieldset>
 <h3>Urls</h3>
 <div class="cp_editurl">
  <div>Url</div>
  <div>@Html.GetLocalizedString("Description")</div>
 </div>
 <div show-errors data-ng-repeat="u in project.Urls" class="cp_editurl">
  <input type="text" class="form-control" name="url" data-ng-model="u.Url"
         data-ng-pattern="/^(http|https)\:///" />
  <input type="text" class="form-control" data-ng-model="u.Description" />
  <button type="button" data-ng-click="deleteUrl(u)" class="dnnSecondaryAction">@Html.GetLocalizedString("Delete")</button>
  <p class="help-block" ng-if="frmEditProject.url.$error.pattern">@Html.GetLocalizedString("Url.Pattern")</p>
 </div>
 <button type="button" data-ng-click="addUrl()" class="dnnSecondaryAction">@Html.GetLocalizedString("Add")</button>
</div>

<a href="#/Projects" class="btn btn-default dnnSecondaryAction">@Html.GetLocalizedString("Return")</a>
<button type="button" class="btn btn-primary dnnPrimaryAction"
        data-ng-click="updateProject(project)"
        data-ng-disabled="frmEditProject.$invalid">
 @Html.GetLocalizedString("Submit")
</button>
<button type="button" class="btn btn-primary dnnSecondaryAction"
        data-ng-click="deleteProject(projectId, '@Html.GetLocalizedString("DeleteProject.Confirm")')"
        data-ng-if="projectId != -1">
 @Html.GetLocalizedString("Delete")
</button>
