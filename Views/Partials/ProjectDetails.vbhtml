@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage
<h2>{{project.ProjectName}}</h2>

<ul>
 <li data-ng-repeat="image in album.Images | orderBy:'Order'">{{image.File}} {{image.Order}} {{image.Remarks}}</li>
</ul>


<div nv-file-drop="" uploader="uploader" data-ng-if="security.moderator == true || project.CreatedByUserID == @Dnn.User.UserID">
 <div nv-file-over="" uploader="uploader" over-class="cp_dropzone_hover" class="cp_dropzone">
  <ul class="cp_sortable">
   <li data-ng-repeat="image in album.Images | orderBy:'Order'" data-img-id="{{image.File}}">
    <div class="delbutton">
     <a class="yag-close" href="#">&times;</a>
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
  <a href="#/Project/{{projectId}}" class="btn btn-default dnnPrimaryAction" data-ng-click="saveAlbum(album)">@Html.GetLocalizedString("SaveChanges")</a>
 </div>
</div>

<a href="#/Projects" class="btn btn-default dnnSecondaryAction">@Html.GetLocalizedString("Return")</a>
