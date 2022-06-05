Imports System
Imports System.IO

'Creamos un evento publico de la clase FileSystemWatcher
Public WithEvents FSW As New System.IO.FileSystemWatcher

Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Cursor.Current = Cursors.WaitCursor
        Try() 'Invocamos metodo donde activamos el vigilante
        Cursor.Current = Cursors.Default
End Sub

Public Sub Try()
        FSW.Path = "C:\Try" 'Le especificamos la ruta a vigilar
        FSW.IncludeSubdirectories = True 'Especificamos a true todos los subdirectorios de esa carpeta
        FSW.EnableRaisingEvents = True 'Habilitamos todos los eventos contenidos dentro de nuestra clase
    End Sub

' Eventos
Private Sub FSW_Created(sender As Object, e As IO.FileSystemEventArgs) Handles FSW.Created
    MsgBox("Se ha creado un nuevo fichero " & e.Name, MsgBoxStyle.Information)
End Sub
Private Sub FSW_Changed(sender As Object, e As IO.FileSystemEventArgs) Handles FSW.Changed
    MsgBox("Se ha modificado un fichero " & e.Name, MsgBoxStyle.Information)
End Sub
Private Sub FSW_Created(sender As Object, e As IO.FileSystemEventArgs) Handles FSW.Created
    MsgBox("Se ha creado un fichero " & e.Name, MsgBoxStyle.Information)
End Sub
Private Sub FSW_Deleted(sender As Object, e As IO.FileSystemEventArgs) Handles FSW.Deleted
    MsgBox("Se ha eliminado un fichero " & e.Name, MsgBoxStyle.Information)
End Sub
Private Sub FSW_Renamed(sender As Object, e As IO.RenamedEventArgs) Handles FSW.Renamed
    MsgBox("Se ha Cambiado el nombre de un fichero de " & e.OldName & " a " & e.Name, MsgBoxStyle.Information)
End Sub