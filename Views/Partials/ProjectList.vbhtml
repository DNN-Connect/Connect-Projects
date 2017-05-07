@Inherits Connect.DNN.Modules.Projects.Common.ModuleWebPage

@Code
 Dim imgPath As String = String.Format("{0}Connect/Projects/{1}/", Dnn.Portal.HomeDirectory, Dnn.Module.ModuleID)
End Code

<div data-ng-repeat="project in projects | orderBy: random" class="cp_pl">
 <div class="col-md-4 col-sm-6" data-project-id="{{project.ProjectId}}" data-ng-click="gotoProject(project.ProjectId)">
  <div class="card-container">
   <div class="card">
    <div class="front">
     <div class="cover">
      <img src="@imgPath{{project.ProjectId}}/{{project.FirstImage.AddImageSize('_med')}}" />
     </div>
     <div class="user">
      <img class="img-circle" src="@DotNetNuke.Common.Globals.ApplicationPath/DnnImageHandler.ashx?mode=profilepic&w=128&h=128&userId={{project.CreatedByUserID}}" />
     </div>
     <div class="content">
      <div class="main">
       <h3 class="name">{{project.ProjectName}}</h3>
       <p class="profession">{{project.Owners}}</p>
       <p class="text-center">"{{project.Aims.substr(0,150)}} ..."</p>
      </div>
      <div class="footer" style="background-color:#{{project.ProjectColor}}">
       <i class="fa fa-mail-forward"></i> More
      </div>
     </div>
    </div> <!-- end front panel -->
    <div class="back">
     <div class="header">
      <h5 class="motto">"{{project.ProjectName}}"</h5>
     </div>
     <div class="content">
      <div class="main">
       <h4 class="text-center">People</h4>
       <p class="text-center">{{project.People}}</p>
       <div class="stats-container">
        <div class="stats">
         <h4>Status</h4>
         <p>
          {{project.Status}}
         </p>
        </div>
        <div class="stats">
         <h4>Owners</h4>
         <p>
          {{project.Owners}}
         </p>
        </div>
        <div class="stats">
         <h4>License</h4>
         <p>
          {{project.LicenseType}}
         </p>
        </div>
       </div>

      </div>
     </div>
     <div class="footer" style="background-color:#{{project.ProjectColor}}">
      <div class="social-links text-center">
       <a href="#" class="btn btn-primary">
        DETAILS
       </a>
      </div>
     </div>
    </div> <!-- end back panel -->
   </div> <!-- end card -->
  </div> <!-- end card-container -->
 </div> <!-- end col sm 3 -->
</div>
<div class="cp_pl_btns">
 <a href="#/Project/Edit/-1" class="dnnPrimaryAction" data-ng-if="security.submitter == true">@Html.GetLocalizedString("Add")</a>
</div>

