@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage
<h2>{{project.ProjectName}}</h2>

<div nv-file-drop="" uploader="uploader">
 <div nv-file-over="" uploader="uploader" over-class="cp_dropzone_hover" class="cp_dropzone">
  <ul class="cp_sortable">
   <li data-ng-repeat="image in album.Images" data-img-id="{{image.File}}">
    <div class="delbutton">
     <a class="yag-close" href="#">&times;</a>
    </div>
    <div class="imagetn">
     <img src="{{album.ImagePath}}{{image.File}}_tn{{image.Extension}}" width="{{settings.tnWidth}}" height="{{settings.tnHeight}}" alt="{{image.Title}}" />
    </div>
    <textarea rows="3" data-ng-model="image.remarks" data-img-id="{{image.File}}" />
   </li>
  </ul>
  <div class="cp_album_droptext">
   @Html.GetLocalizedString("DropHere")
  </div>
 </div>
</div>

<a href="#/Projects" class="btn btn-default dnnSecondaryAction">@Html.GetLocalizedString("Return")</a>
