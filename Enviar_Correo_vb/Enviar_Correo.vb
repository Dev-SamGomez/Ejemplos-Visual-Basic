Private Sub EnviaCorreo()
        Try
            Dim FromTO As String = "mailTo@example.com" ' Marcamos el correo destino
            Dim SendBy As String = "no-reply@example.com" ' Marcamos el correo que envia
            ' Cuerpo del correo
            Dim SendMail As String = "Hello, this is an email sent from a desktop program in visual basic language."

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

            _Message.IsBodyHtml = False
            'ENVIO
            _SMTP.Send(_Message) 'Enviamos
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub