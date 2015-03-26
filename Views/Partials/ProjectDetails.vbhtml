﻿@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage
<h2>{{project.ProjectName}}</h2>

<div class="cp_container">
 <div class="cp_column">
  <dl class="cp_dl_horizontal">
   <dt>@Html.GetLocalizedString("ProjectType")</dt>
   <dd>{{project.TypeDescription}}&nbsp;</dd><div style="clear:both"></div>
   <dt>@Html.GetLocalizedString("Owners")</dt>
   <dd>{{project.Owners}}&nbsp;</dd><div style="clear:both"></div>
   <dt>@Html.GetLocalizedString("Status")</dt>
   <dd>{{project.Status}}&nbsp;</dd>
   <dt data-ng-if="project.Url1 != ''">Url</dt>
   <dd data-ng-if ="project.Url1 != ''"><a href="{{project.Url1}}">{{project.Url1}}</a></dd>
   <dt data-ng-if="project.Url2 != ''">Url</dt>
   <dd data-ng-if="project.Url2 != ''"><a href="{{project.Url2}}">{{project.Url2}}</a></dd>
  </dl>
  <h3>@Html.GetLocalizedString("Aims")</h3>
  <div class="cp_para">
   {{project.Aims | newlines}}
  </div>
  <h3>@Html.GetLocalizedString("Description")</h3>
  <div class="cp_para">
   {{project.Description | newlines}}
  </div>
  <h3>@Html.GetLocalizedString("Dependencies")</h3>
  <div class="cp_para">
   {{project.Dependencies | newlines}}
  </div>
 </div>
 <div class="cp_column">
  <div data-ng-repeat="image in album.Images | orderBy:'Order'" class="cp_img">
   <img src="{{album.ImagePath}}{{image.File}}_med{{image.Extension}}" alt="{{image.Title}}" />
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
