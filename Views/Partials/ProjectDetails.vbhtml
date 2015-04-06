@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage
<h2>{{project.ProjectName}}</h2>

<div class="cp_container">
 <div class="cp_column">
  <div data-ng-if="project.NrLiveLinks == 0" class="cp_warning">@Html.GetLocalizedString("NoActiveLinks")</div>
  <dl class="cp_dl_horizontal">
   <dt>@Html.GetLocalizedString("ProjectTypes")</dt>
   <dd>
    <span data-ng-repeat="pt in project.ProjectTypes | filter:{IsSelected: true}" class="cp_tag">{{pt.TypeDescription}}</span>
   </dd>
   <div style="clear:both"></div>
   <dt>@Html.GetLocalizedString("LicenseType")</dt>
   <dd>{{project.LicenseType}}&nbsp;</dd>
   <div style="clear:both"></div>
   <dt>@Html.GetLocalizedString("Owners")</dt>
   <dd>{{project.Owners}}&nbsp;</dd>
   <div style="clear:both"></div>
   <dt>@Html.GetLocalizedString("People")</dt>
   <dd>{{project.People}}&nbsp;</dd>
   <div style="clear:both"></div>
   <dt>@Html.GetLocalizedString("Status")</dt>
   <dd>{{project.Status}}&nbsp;</dd>
   <div style="clear:both"></div>
   <span data-ng-repeat="u in project.Urls">
    <dt>Url</dt>
    <dd>
     <a href="{{u.Url}}">{{u.Description.isNull(u.Url)}}</a>
     <span class="cp_note"><br />
      {{'@Html.GetLocalizedString("LastRetrieved")' | stringFormat:[dateFormat(u.LastChecked, 'short')]}}
     </span>
     <span class="cp_note" data-ng-if="u.LastChange">
      <br />
      {{'@Html.GetLocalizedString("LastModified")' | stringFormat:[dateFormat(u.LastChange, 'short')]}}
     </span>
    </dd>
    <div style="clear:both"></div>
   </span>
  </dl>
  <h3>@Html.GetLocalizedString("Aims")</h3>
  <div class="cp_para" data-ng-bind-html="ProjectAims">
  </div>
  <h3>@Html.GetLocalizedString("Description")</h3>
  <div class="cp_para" data-ng-bind-html="ProjectDescription">
  </div>
  <h3>@Html.GetLocalizedString("Dependencies")</h3>
  <div class="cp_para" data-ng-bind-html="ProjectDependencies">
  </div>
 </div>
 <div class="cp_column">
  <div data-ng-repeat="image in album.Images | orderBy:'Order'" class="cp_img">
   <a href="{{album.ImagePath}}{{image.File}}_zoom{{image.Extension}}"><img src="{{album.ImagePath}}{{image.File}}_med{{image.Extension}}" alt="{{image.Title}}" /></a>
   <span>{{image.Remarks}}</span>
  </div>
 </div>
 <div style="clear:both"></div>
</div>

<div nv-file-drop="" uploader="uploader" data-ng-if="security.moderator == true || project.CreatedByUserID == @Dnn.User.UserID">
 <div nv-file-over="" uploader="uploader" over-class="cp_dropzone_hover" class="cp_dropzone">
  <ul class="cp_sortable">
   <li data-ng-repeat="image in album.Images | orderBy:'Order'" data-img-id="{{image.File}}">
    <div class="delbutton">
     <a class="yag-close" href="#/Project/{{projectId}}" data-ng-click="deleteImage(image)">&times;</a>
    </div>
    <div class="imagetn">
     <img src="{{album.ImagePath}}{{image.File}}_tn{{image.Extension}}" width="{{settings.tnWidth}}" height="{{settings.tnHeight}}" alt="{{image.Title}}" />
    </div>
    <textarea rows="3" data-ng-model="image.Remarks" />
   </li>
  </ul>
  <div class="cp_album_droptext">
   @Html.GetLocalizedString("DropHere")
  </div>
 </div>
 <div class="btnright">
  <a href="#/Project/{{projectId}}" class="btn btn-default dnnPrimaryAction" data-ng-click="saveAlbum(album, true)">@Html.GetLocalizedString("SaveChanges")</a>
 </div>
</div>

<a href="#/Projects" class="btn btn-default dnnSecondaryAction">@Html.GetLocalizedString("Return")</a>
<a href="#/Project/Edit/{{projectId}}" class="dnnSecondaryAction" data-ng-if="security.moderator == true || project.CreatedByUserID == @Dnn.User.UserID">@Html.GetLocalizedString("Edit")</a>
<a href="@(GetModuleUrl("API/Projects/Pdf"))/{{projectId}}?moduleId=@Dnn.Module.ModuleId&tabId=@Dnn.Tab.TabId" class="dnnSecondaryAction">PDF</a>

<div class="cp_audit">
 {{'@Html.GetLocalizedString("Audit")' | stringFormat:[project. CreatedByUser, dateFormat(project.CreatedOnDate, 'short'), project.LastModifiedByUser, dateFormat(project.LastModifiedOnDate, 'short')]}}
</div>
