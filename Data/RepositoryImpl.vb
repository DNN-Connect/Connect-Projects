Imports DotNetNuke.Collections
Imports DotNetNuke.Data

Namespace Data
 Public MustInherit Class RepositoryImpl(Of T As Class)
  Implements IRepository(Of T)

  Public Overridable Sub Delete(item As T) Implements IRepository(Of T).Delete
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    repo.Delete(item)
   End Using
  End Sub

  Public Overridable Sub Delete(sqlCondition As String, ParamArray args As Object()) Implements IRepository(Of T).Delete
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    repo.Delete(sqlCondition, args)
   End Using
  End Sub

  Public Overridable Function Find(sqlCondition As String, ParamArray args As Object()) As IEnumerable(Of T) Implements IRepository(Of T).Find
   Dim list As IEnumerable(Of T) = Nothing
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    list = repo.Find(sqlCondition, args)
   End Using
   Return list
  End Function

  Public Overridable Function Find(pageIndex As Integer, pageSize As Integer, sqlCondition As String, ParamArray args As Object()) As IPagedList(Of T) Implements IRepository(Of T).Find
   Dim list As IPagedList(Of T) = Nothing
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    list = repo.Find(pageIndex, pageSize, sqlCondition, args)
   End Using
   Return list
  End Function

  Public Overridable Function [Get]() As IEnumerable(Of T) Implements IRepository(Of T).[Get]
   Dim list As IEnumerable(Of T) = Nothing
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    list = repo.[Get]()
   End Using
   Return list
  End Function

  Public Overridable Function [Get](Of TScopeType)(scopeValue As TScopeType) As IEnumerable(Of T) Implements IRepository(Of T).[Get]
   Dim list As IEnumerable(Of T) = Nothing
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    list = repo.[Get](Of TScopeType)(scopeValue)
   End Using
   Return list
  End Function

  Public Overridable Function GetById(Of TProperty)(id As TProperty) As T Implements IRepository(Of T).GetById
   Dim item As T = Nothing
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    item = repo.GetById(Of TProperty)(id)
   End Using
   Return item
  End Function

  Public Overridable Function GetById(Of TProperty, TScopeType)(id As TProperty, scopeValue As TScopeType) As T Implements IRepository(Of T).GetById
   Dim item As T = Nothing
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    item = repo.GetById(Of TProperty, TScopeType)(id, scopeValue)
   End Using
   Return item
  End Function

  Public Overridable Function GetPage(pageIndex As Integer, pageSize As Integer) As IPagedList(Of T) Implements IRepository(Of T).GetPage
   Dim list As IPagedList(Of T) = Nothing
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    list = repo.GetPage(pageIndex, pageSize)
   End Using
   Return list
  End Function

  Public Overridable Function GetPage(Of TScopeType)(scopeValue As TScopeType, pageIndex As Integer, pageSize As Integer) As IPagedList(Of T) Implements IRepository(Of T).GetPage
   Dim list As IPagedList(Of T) = Nothing
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    list = repo.GetPage(Of TScopeType)(scopeValue, pageIndex, pageSize)
   End Using
   Return list
  End Function

  Public Overridable Sub Insert(item As T) Implements IRepository(Of T).Insert
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    repo.Insert(item)
   End Using
  End Sub

  Public Overridable Sub Update(item As T) Implements IRepository(Of T).Update
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    repo.Update(item)
   End Using
  End Sub

  Public Overridable Sub Update(sqlCondition As String, ParamArray args As Object()) Implements IRepository(Of T).Update
   Using db As IDataContext = DataContext.Instance()
    Dim repo As IRepository(Of T) = db.GetRepository(Of T)()
    repo.Update(sqlCondition, args)
   End Using
  End Sub

 End Class
End Namespace