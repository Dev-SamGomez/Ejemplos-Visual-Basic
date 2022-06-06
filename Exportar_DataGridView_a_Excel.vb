Private Sub BtnExportExcel_Click(sender As Object, e As EventArgs) Handles BtnExportExcel.Click
        Cursor.Current = Cursors.WaitCursor
        Try
            If YouDataGridView.Rows.Count > 0 Then 'Validamos que haya elementos en la DataGridView
                'Iniciamos la app con la clase Excel.Aplication
                Dim exApp As New Microsoft.Office.Interop.Excel.Application
                'Iniciamos objeto Libro con la clase Excel.Workbook
                Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
                'Iniciamos objeto Hoja con la clase Excel.Worksheet
                Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet

                'Agregamos el libro
                exLibro = exApp.Workbooks.Add
                'Agregamos la hoja enviando el nombre de dicha hoja
                exHoja = exLibro.Worksheets("Sheet1")

                ' obtenemos en variables la cantidad de lineas y columnas de la DataGridView
                Dim NCol As Integer = YouDataGridView.ColumnCount
                Dim NRow As Integer = YouDataGridView.RowCount
                Try
                'Recorremos las columnas y las agregamos a las celdas de la hoja
                    For i As Integer = 1 To NCol
                        exHoja.Cells.Item(1, i) = YouDataGridView.Columns(i - 1).DataPropertyName.ToString
                    Next
                    ' Recorremos las filas, y las columnas para agregar la fila en la columna adecuada
                    For Fila As Integer = 0 To NRow - 1
                        For Col As Integer = 0 To NCol - 1
                            exHoja.Cells.Item(Fila + 2, Col + 1) = YouDataGridView.Rows(Fila).Cells(Col).FormattedValue
                        Next
                    Next
                    'Ajustamos las columnas
                    exHoja.Columns.AutoFit()
                    'Hacemos visible la aplicacion
                    exApp.Application.Visible = True
                    exHoja = Nothing
                    exLibro = Nothing
                    exApp = Nothing
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            Else
                MsgBox("There are no items to export!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Cursor.Current = Cursors.Default
    End Sub