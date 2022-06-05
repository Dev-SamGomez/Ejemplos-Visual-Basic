Private Sub EnviaCorreo()
        Try
            Dim FromTO As String = "mailTo@example.com" ' Marcamos el correo destino
            Dim SendBy As String = "no-reply@example.com" ' Marcamos el correo que envia
            Dim Date As String = Now.ToString("dd-MMM-yyyy")
            Dim FileAttach As String = CreaCSV(tabla) 'Invocamos metodo que crea el archivo excel
            ' Cuerpo del correo
            Dim SendMail As String = "Hello, this is an email sent from a desktop program in visual basic language, attaching an excel."

            ' Declaramos la clase MailMessage
            Dim _Message As New System.Net.Mail.MailMessage()
            ' Declaramos la clase SmtpClient
            Dim _SMTP As New System.Net.Mail.SmtpClient

            ' En la clase SMTP, con el metodo Credentials, creamos una nueva clase de NetorkCredential
            ' en esta le enviaremos en el construnctor, el correo que envia y su contraseña
            _SMTP.Credentials = New System.Net.NetworkCredential(SendBy, "YourPassword")
            ' Marcamos el Host, OJO: este deberas poner el host de tu provedor de correo
            _SMTP.Host = "smtp.example.com"
            ' Seleccionas el puerto y habilitas el envio con certificado SSL
            _SMTP.Port = 587
            _SMTP.EnableSsl = True

            'En el metodo de la clase que declaramos _Message, en su constructor, seleccionamos el correo destino
            _Message.[To].Add(FromTO)
            _Message.From = New System.Net.Mail.MailAddress(SendBy, "", System.Text.Encoding.UTF8)
            _Message.Subject = "No Reply" 'Seleccionamos el Subject
            _Message.SubjectEncoding = System.Text.Encoding.UTF8

            _Message.Body = SendMail ' Colocamos cuerpo del correo
            _Message.BodyEncoding = System.Text.Encoding.UTF8
            _Message.Priority = System.Net.Mail.MailPriority.High
                If FileAttach <> "" Then 'Adjunta un archivo
                    Dim oAttachment As New System.Net.Mail.Attachment(FileAttach)
                    _Message.Attachments.Add(oAttachment)
                End If
            _Message.IsBodyHtml = False
            'ENVIO
            _SMTP.Send(_Message) 'Enviamos
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub
    Private Function CreaCSV(aTabla As DataTable)
        Dim DateNow As Date = CDate(Now)
        Dim NameFileCreated As String = "name of the file created on the day " + Convert.ToDateTime(DateNow.ToString()) 'Colocamos el nombre del archivo
        Dim SetPath As String = Path.GetTempPath() & ArchivoNombre 'Creamos el archivo temporal
        If aTabla.Rows.Count > 0 Then
            Try
                Dim AUX As String = ""
                Dim fs As FileStream = File.Create(SetPath) 'Enviamos la direccion temporal a la clase FileStream
                Dim HeadersText As String = "Aqui los encabezados del DataTable" + vbNewLine
                Dim Titles As Byte() = New UTF8Encoding(True).GetBytes(HeadersText) 'Creamos el array con los encabezados del DataTable
                fs.Write(Titles, 0, Titles.Length)
                HeadersText = ""
                For NM As Integer = 0 To aTabla.Rows.Count - 1
                    AUX = aTabla.Rows(NM).Item("Title").ToString.Trim() 'Limpiamos los encabezados
                    HeadersText += AUX + ","
                    HeadersText += vbNewLine
                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(HeadersText)
                    fs.Write(info, 0, info.Length) ' y los escribimos en el documento
                Next
                fs.Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message.ToString + vbNewLine + "Error in CreaCSV", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        If SetPath <> "" Then
        Return SetPath 'Retornamos el archivo temporal
        End If
        Return Nothing
    End Function