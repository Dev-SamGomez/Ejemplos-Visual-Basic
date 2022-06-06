Public Sub GenerateColumns()
        Me.DataGridView1.Columns.Add("Title 1", "Work Order")
        Me.DataGridView1.Columns.Add("Title 1", "Order")
        Me.DataGridView1.Columns.Add("Title 2", "Work Order")
        Me.DataGridView1.Columns.Add("Title 2", "Order")
        For j As Integer = 0 To Me.DataGridView1.ColumnCount - 1
            Me.DataGridView1.Columns(j).Width = 80
            Me.DataGridView1.Columns(j).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        Me.DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Me.DataGridView1.ColumnHeadersHeight = Me.DataGridView1.ColumnHeadersHeight * 2
        Me.DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter
End Sub
Private Sub dataGridView1_ColumnWidthChanged(ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs) Handles DataGridView1.ColumnWidthChanged
        Dim rtHeader As Rectangle = Me.DataGridView1.DisplayRectangle
        rtHeader.Height = Me.DataGridView1.ColumnHeadersHeight / 2
        Me.DataGridView1.Invalidate(rtHeader)
End Sub
Private Sub dataGridView1_Scroll(ByVal sender As Object, ByVal e As ScrollEventArgs) Handles DataGridView1.Scroll
        Dim rtHeader As Rectangle = Me.DataGridView1.DisplayRectangle
        rtHeader.Height = Me.DataGridView1.ColumnHeadersHeight / 2
        Me.DataGridView1.Invalidate(rtHeader)
End Sub
Private Sub dataGridView1_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles DataGridView1.Paint
        If Me.DataGridView1.ColumnCount > 0 Then
            Dim Maq As String() = {"Title 1","Title 2"}
            Dim j As Integer = 0
            While j < 4
                Dim r1 As Rectangle = Me.DataGridView1.GetCellDisplayRectangle(j, -1, True)
                Dim w2 As Integer = Me.DataGridView1.GetCellDisplayRectangle(j, -1, True).Width
                r1.X += 1
                r1.Y += 1
                r1.Width = r1.Width + w2 - 2
                r1.Height = r1.Height / 2 - 2
                e.Graphics.FillRectangle(New SolidBrush(Me.DataGridView1.ColumnHeadersDefaultCellStyle.BackColor), r1)
                Dim format As StringFormat = New StringFormat()
                format.Alignment = StringAlignment.Center
                format.LineAlignment = StringAlignment.Center
                e.Graphics.DrawString(Maq(j / 2), Me.DataGridView1.ColumnHeadersDefaultCellStyle.Font, New SolidBrush(Me.DataGridView1.ColumnHeadersDefaultCellStyle.ForeColor), r1, format)
                j += 2
            End While
        End If
End Sub
Public Sub dataGridView1_CellPainting(ByVal sender As Object, ByVal e As DataGridViewCellPaintingEventArgs) Handles DataGridView1.CellPainting
        If e.RowIndex = -1 AndAlso e.ColumnIndex > -1 Then
            Dim r2 As Rectangle = e.CellBounds
            r2.Y += e.CellBounds.Height / 2
            r2.Height = e.CellBounds.Height / 2
            e.PaintBackground(r2, True)
            e.PaintContent(r2)
            e.Handled = True
        End If
End Sub