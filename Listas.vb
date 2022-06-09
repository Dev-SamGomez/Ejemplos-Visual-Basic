'Crear lista a partir de una clase con Objetos
Public ListOrdenesDeCompra As List(Of OrdenesDeCompra) = New List(Of OrdenesDeCompra)

'Agregar por medio de un Datatable
Private Sub InsertTag(Tag As String)
        Try
            Dim dr As SqlDataReader
            Dim cmd As SqlCommand
            Dim t As DataTable = New DataTable()
            cmd = New SqlCommand("select Orden_de_Compra,Cliente,Pais from OrdenesDeCompra", cnn)
            cmd.CommandType = CommandType.Text
            cnn.Open()
            dr = cmd.ExecuteReader
            t.Load(dr)
            If t.Rows.Count > 0 Then
                Dim ls As List(Of OrdenesDeCompra) = t.AsEnumerable().[Select](Function(m) New OrdenesDeCompra() With {
                    .Orden_de_Compra = m.Field(Of String)("Orden_de_Compra"),
                    .Cliente = m.Field(Of String)("Cliente")
                    .Pais = m.Field(Of String)("Pais")
                }).ToList()
                ListOrdenesDeCompra.AddRange(ls)
            End If
            cnn.Close()
        Catch ex As Exception
            cnn.Close()
            MsgBox(ex.Message)
        End Try
    End Sub

'Agregar desde elementos
Dim OrdenCompra As OrdenesDeCompra = New OrdenesDeCompra()
OrdenCompra.Orden_de_Compra = 10
OrdenCompra.Cliente = "Samuel"
OrdenCompra.Pais = "Mexico"
ListOrdenesDeCompra.AddRange(OrdenCompra)

'Clase de objetos para la lista
Public Class OrdenesDeCompra
    Public Property Orden_de_Compra As String
    Public Property Cliente As String
    Public Property Pais As String
End Class

'Filtrar con LINQ un elemento de la lista
'Nos dira cuantas veces se encuentra
Dim Filtro1 = ListOrdenesDeCompra.Where(Function(d) d.Pais.Equals("Mexico")).ToList().Count()
'Seleccionar un elemento
Dim Filtro2 As String = ListOrdenesDeCompra.Where(Function(d) d.Pais.Equals("Mexico")).Select(Function(valor) valor.Pais.Equals("Mexico")).ToString

'Recorrer con ForEach
' OJO: No es mutable
ListOrdenesDeCompra.ForEach(
                            Function(item) 'Metodo Func o Action, no retorna valores, se puede trabajar con predicados
                                Return Nothing
                            End Function
                            )

'Obtener el indice de un valor de la lista
Dim index As Integer = ListOrdenesDeCompra.FindIndex(Function(d) d.Cliente.Equals("Samuel"))
