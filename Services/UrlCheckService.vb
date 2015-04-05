Option Strict Off

Imports System.IO
Imports System.Net
Imports System.Threading.Tasks
Imports Connect.DNN.Modules.Projects.Controllers.Urls
Imports Connect.DNN.Modules.Projects.Models.Urls
Imports DotNetNuke.Services.Exceptions
Imports Newtonsoft.Json
Imports Octokit

Namespace Services
 Public Class UrlCheckService
  Inherits DotNetNuke.Services.Scheduling.SchedulerClient

  Private Log As New StringBuilder

  Public Sub New(objScheduleHistoryItem As DotNetNuke.Services.Scheduling.ScheduleHistoryItem)
   MyBase.New()
   Me.ScheduleHistoryItem = objScheduleHistoryItem
  End Sub

  Public Overrides Async Sub DoWork()

   Try

    Me.Progressing() ' Start

    For Each url As UrlBase In UrlsController.GetUrlsToCheck()
     Log.AppendLine(String.Format("Verifying {0}", url.Url))
     Dim urlExists As Boolean = False
     Dim lastModified As DateTime = DateTime.MinValue
     Try
      Dim request As HttpWebRequest = CType(WebRequest.Create(url.Url), HttpWebRequest)
      Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
       If response.StatusCode = HttpStatusCode.OK Then
        urlExists = True
        Log.AppendLine(String.Format("The url exists"))
       End If
      End Using
     Catch exc As Exception
     End Try
     Dim m As Match = Regex.Match(url.Url, "https://github.com/(?<org>[^/]+)/(?<project>[^/]+)")
     If m.Success Then ' try a github API data request to see when the project was last updated
      Log.AppendLine(String.Format("Retrieving Github data"))
      Try
       Dim ghClient As New GitHubClient(New ProductHeaderValue("DNN-Connect-Projects"))
       Dim repo As Repository = Await ghClient.Repository.Get(m.Groups("org").Value, m.Groups("project").Value)
       lastModified = repo.UpdatedAt.Date
       Log.AppendLine(String.Format("Retrieved last modified date: {0}", lastModified))
      Catch ex As Exception
       Log.AppendLine(ex.Message)
      End Try
     End If

     ' Update data
     If urlExists Then
      url.LastChecked = Now
      url.Retries = 5
      If lastModified <> Date.MinValue Then url.LastChange = lastModified
     Else
      url.Retries -= 1
      If url.Retries = 0 Then url.IsDead = True
     End If
     Log.AppendLine(String.Format("Saving url"))
     UrlsController.UpdateUrl(url)

    Next

    Log.AppendLine("Finished")

    Me.ScheduleHistoryItem.AddLogNote(Log.ToString.Replace(vbCrLf, "<br />"))
    Me.ScheduleHistoryItem.Succeeded = True

   Catch ex As Exception
    Me.ScheduleHistoryItem.AddLogNote(Log.ToString & vbCrLf & "Scheduled task failed: " & ex.Message & "(" & ex.StackTrace & ")" & vbCrLf & Log.ToString)
    Me.ScheduleHistoryItem.Succeeded = False
    Me.Errored(ex)
    LogException(ex)
   End Try


  End Sub

  Async Function GetSomething() As Threading.Tasks.Task(Of String)

  End Function

 End Class
End Namespace