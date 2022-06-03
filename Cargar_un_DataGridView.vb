'Cargar un DataGridView con un Datatable
Private Sub LoadGrid()
        Try
            Dim query As String = $"select * from Vendors where IDVendor = '{VendorID}'" 'Iniciamos con el query
            Dim Table As Datatable
            Dim cmd As SqlCommand
            Dim dr As SqlDataReader
            cmd = New SqlCommand(query, cnn) 'En la clase de SqlCommand en su constructor, enviamos el query y la conexion a nuestra DB
            cmd.CommandType = CommandType.Text 'Seleccionamos el tipo de comando, en este caso el comando sera tipo texto
            cnn.Open()
            dr = cmd.ExecuteReader 'Al abrir la conexion, el datareader lo inicializamos con la ejecucion del comando de sql, con la funcion ExecuteReader
            Table.Load(dr) 'A la funcion Load de la clase Datatable, enviamos el datareader como parametro, para que con este se cargue el Datatable
            cnn.Close()
            If Table.Rows.Count > 0 Then 'Validamos si hay registros en el Datatable que acabamos de cargar, si hay datos, cargamos la Grid
                TuDataGridView.DataSource = Table 'Con la propiedad DataSource de la Grid, seleccionamos la Datatable que cargamos
            Else
                TuDataGridView.DataSource = Nothing
            End If
        Catch ex As Exception
            cnn.Close()
            MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
        End Try
End Sub