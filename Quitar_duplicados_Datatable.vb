Private Function RemoveDuplicatesRecords(ByVal dt As DataTable) As DataTable
    Dim UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.[Default])
    Dim dt2 As DataTable = UniqueRows.CopyToDataTable()
    Return dt2
End Function