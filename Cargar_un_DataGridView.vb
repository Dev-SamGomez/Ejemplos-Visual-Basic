'Cargar un DataGridView con un Datatable
Imports System.Data.SqlClient 'Libreria para uso de clases de SQL

Private Sub LoadGrid()
    Try
        Dim query As String = $"SELECT * FROM Vendors WHERE IDVendor = '{VendorID}'" 'Iniciamos con el query
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
            YouDataGridView.DataSource = Table 'Con la propiedad DataSource de la Grid, seleccionamos la Datatable que cargamos
        Else
            YouDataGridView.DataSource = Nothing
        End If
    Catch ex As Exception
        cnn.Close()
        MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
    End Try
End Sub

'Cargar un DataGridView con una Lista
Private Sub LoadGridWithList()
    Try
        If ListItems.Count > 0 Or ListItems IsNot Nothing Then 'Verificamos mediante una validacion que la lista con la que llenaremos la Grid no este vacia.
            YouDataGridView.DataSource = ListItems.ToList() 'Si la lista contiene mas de 0 elementos entonces usamos la propiedad de la Grid DataSource y la cargamos con la lista.
        Else
            YouDataGridView.DataSource = Nothing
        End If
    Catch ex As Exception
        MessageBox.Show(ex.Message + vbNewLine + ex.ToString)
    End Try
End Sub

'Cargar un DatagridView desde un archivo de Excel
Imports System.Data.OleDb ' Libreria para uso de Access
Private Sub LoadGridByExcel()
    Try
        'En este metodo, cargaremos un Excel usando la clase OpenDialog, esta para generar una ventana donde seleccionaremos un archivo
        ' de Excel y, pasemos la informacion a la DataGridView
        Dim Table As Datatable = New Datatable()
        Dim OpenFileDlg As New Windows.Forms.OpenFileDialog ' Generamos la Clase OpenFileDialog
        Dim result As DialogResult = OpenFileDlg.ShowDialog() ' El aarchivo seleccionado, lo grabaremos en una variable de la Clase Dialog Result
        Dim path As String = OpenFileDlg.FileName 'Este guardamos el Path
        Dim archivo As String = path.ToString
        OpenFileDlg = Nothing 'Matamos el Open Dialog
        'Generamos la conexion, esta sera la de Microsoft OLEDB v.12 
        ' OJO: Asegurate de tener instalada este framework en los equipos clientes de lo contrario tendras excepciones con esta conexion
        ' Puedes descargarla gratis en la pagina de Microsoft
        Dim stringConnection As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" & archivo & "';Extended Properties=""Excel 12.0;HDR=YES"""
        Dim MyConnection As OleDbConnection = New OleDbConnection(stringConnection)
        ' Una vez hecha la conexion, al ser consulta tipo Access, generamos la query seleccionand la Sheet1 
        ' OJO: asegurate de que sea este el nombre de la hoja, puedes cambiarla o, en los ejemplos de Trabajos con Excel, podras encontrar la forma de como extraer previamente
        ' el nombre de la hoja y como ponerla en la query como parametro.
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = New System.Data.OleDb.OleDbDataAdapter("SELECT * FROM [Sheet1$]", MyConnection)
        MyConnection.Open()
        MyCommand.Fill(Table) ' Llenamos la DataTable
        MyConnection.Close()
        If Table.Rows.Count > 0 Then
            YouDataGridView.DataSource = Table
        Else
            YouDataGridView.DataSource = Nothing
        End If
    Catch ex As Exception
        MessageBox.Show(ex.Message)
    End Try
End Sub

'Cargar una DataGridView con un filtro 
' En este ejemplo, la problematica a resolver es, cada vez que el usuario escriba en un TextBox, este vaya filtrando en una Query directo a la 
' DB y a su vez, los resultados se vayan reflejando en la DataGridView

Private Sub txtBuscador_TextChanged_1(sender As Object, e As EventArgs) Handles txtBuscador.TextChanged
    ' Cada vez que ingrese un dato el usuario, esta al ser el event Changed del TextBox, enviara al Metodo filterData
    Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    Dim filter As String = CType(sender, TextBox).Text
    If filter.Trim() <> String.Empty Then
        filterData(filter)
    End If
    Cursor.Current = System.Windows.Forms.Cursors.Default
End Sub
Public Sub filtrarDatos(ByVal buscar As String)
    Try
        Dim da1 As New SqlDataAdapter ' Declaramos la clase SqlDataAdapter
        Dim query As String = "SELECT * FROM Costumers WHERE Costumer like @filter" ' Generamos la query
        da1 = New SqlDataAdapter(query, cnn)
        da1.SelectCommand.Parameters.AddWithValue("@filter", String.Format("%{0}%", buscar))
        ' Enviamos los parametros, el primero siendo el que se encuentra en la query
        ' El segundo el formato que se le dara, usando REGEX, el Tercero el parametro que enviamos desde que el evento TextChanged le envio a este metodo

        Dim table As New DataTable
        da1.Fill(table) 'Llenamos el DataTable

        With YouDataGridView
            .DataSource = table
            .ClearSelection()
        End With
    Catch ex As Exception
        MessageBox.Show(ex.Message)
    End Try
End Sub