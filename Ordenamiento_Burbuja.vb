
Dim Sort(20) As Integer = {1,4,19,18,2,3,5,7,6,8,9,11,14,13,12,15,16,10,17,20}
Dim auxSort As Integer
For i As Integer = 0 To Sort.Length - 2
    For j As Integer = 0 To Sort.Length - 2
        If Sort(j) > Sort(j + 1) Then
            auxSort = Sort(j)
            Sort(j) = Sort(j + 1)
            Sort(j + 1) = auxSort
        End If
    Next
Next
