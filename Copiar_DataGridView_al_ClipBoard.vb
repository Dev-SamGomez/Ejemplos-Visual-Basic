Private Sub btnCopyCWO_Click(sender As Object, e As EventArgs) Handles btnCopyCWO.Click
    Try
        If YouDataGridView.RowCount > 0 Then ' Validamos que existan datos en una DataGridView
            ' Usando la propiedad ClipboardCopyMode de la DataGridView, hacemos todo disponible en conjunto con los encabezados
            YouDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
            Dim dataObj As DataObject 'Creamos un objeto
            YouDataGridView.SelectAll() 'Seleccionamos toda la DataGridView

            'Al objeto creado el agregamos lo que pusimos como disponible arriba, todo el contenido en el ClipBoard
            dataObj = YouDataGridView.GetClipboardContent()

            ' Seteamos el metodo SetDataObject de la Clase Clipboard, la cual en su constructor ya viene la informacion de la DataGridView
            Clipboard.SetDataObject(dataObj)
            YouDataGridView.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText
            MsgBox("Added Grid to ClipBoard!")
        End If
    Catch ex As Exception
        MessageBox.Show(ex.Message, "Error Added To ClipBoard")
    End Try
End Sub