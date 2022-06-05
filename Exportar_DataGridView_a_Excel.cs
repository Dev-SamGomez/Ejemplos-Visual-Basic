private void BtnExportExcel_Click(object sender, EventArgs e)
{
    Cursor.Current = Cursors.WaitCursor;
    try
    {
        if (YouDataGridView.Rows.Count > 0)
        {
            // Iniciamos la app con la clase Excel.Aplication
            Microsoft.Office.Interop.Excel.Application exApp = new Microsoft.Office.Interop.Excel.Application();
            // Iniciamos objeto Libro con la clase Excel.Workbook
            Microsoft.Office.Interop.Excel.Workbook exLibro;
            // Iniciamos objeto Hoja con la clase Excel.Worksheet
            Microsoft.Office.Interop.Excel.Worksheet exHoja;

            // Agregamos el libro
            exLibro = exApp.Workbooks.Add;
            // Agregamos la hoja enviando el nombre de dicha hoja
            exHoja = exLibro.Worksheets("Sheet1");

            // obtenemos en variables la cantidad de lineas y columnas de la DataGridView
            int NCol = YouDataGridView.ColumnCount;
            int NRow = YouDataGridView.RowCount;
            try
            {
                // Recorremos las columnas y las agregamos a las celdas de la hoja
                for (int i = 1; i <= NCol; i++)
                    exHoja.Cells.Item(1, i) = YouDataGridView.Columns(i - 1).DataPropertyName.ToString;
                // Recorremos las filas, y las columnas para agregar la fila en la columna adecuada
                for (int Fila = 0; Fila <= NRow - 1; Fila++)
                {
                    for (int Col = 0; Col <= NCol - 1; Col++)
                        exHoja.Cells.Item(Fila + 2, Col + 1) = YouDataGridView.Rows(Fila).Cells(Col).FormattedValue;
                }
                // Ajustamos las columnas
                exHoja.Columns.AutoFit();
                // Hacemos visible la aplicacion
                exApp.Application.Visible = true;
                exHoja = null;
                exLibro = null;
                exApp = null;
            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.ToString());
            }
        }
        else
            Interaction.MsgBox("There are no items to export!", MsgBoxStyle.Information);
    }
    catch (Exception ex)
    {
        Interaction.MsgBox(ex.ToString());
    }
    Cursor.Current = Cursors.Default;
}
