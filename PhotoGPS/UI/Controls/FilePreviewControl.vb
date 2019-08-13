#If DEBUG Then

Partial Class null
    'This just makes sure this file doesn't open in a designer
End Class

#End If

Public Class FilePreviewControl
    Inherits Control

    Private _File As IO.FileInfo
    Private _image As Drawing.Image
    Private _imagebytes As Byte()
    Private _imagestream As IO.MemoryStream

    Sub New()

    End Sub

    Public Property File As IO.FileInfo
        Get
            Return _File
        End Get
        Set
            _File = Value

            _FreeImage()

            Try
                '_image = Image.FromFile("\\?\" & _File.FullName) 'this locks the file for writing until this reference is disposed. This can cause problems later on when moving the file so we'll load the whole thing to memory instead.
                _imagebytes = IO.File.ReadAllBytes(_File.FullName.EnsureFilePrepend)
                _imagestream = New IO.MemoryStream(_imagebytes)
                _image = Image.FromStream(_imagestream)
            Catch ex As Exception
                _FreeImage()
            Finally
                Refresh()
            End Try

        End Set
    End Property

    Protected Overrides Sub OnDoubleClick(e As EventArgs)
        If _File IsNot Nothing Then
            If FileSafeToOpen(_File) Then
                Start(_File.FullName.EnsureFilePrepend)

            End If

        End If

        MyBase.OnClick(e)
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim invalid As Boolean = (_File Is Nothing) Or (_image Is Nothing)

        If invalid Then
            Dim t As String = ""

            If (_File Is Nothing) Then
                t = "(No file selected)"
            ElseIf (_File IsNot Nothing) And (_image Is Nothing) Then
                t = String.Format("Cannot display file '{0}'.{1}", _File.Name, IIf(FileSafeToOpen(_File), " Double-click here to try opening the file.", ""))
            End If

            Dim f = New StringFormat

            f.Alignment = StringAlignment.Center
            f.LineAlignment = StringAlignment.Center
            f.FormatFlags = StringFormatFlags.FitBlackBox

            e.Graphics.DrawString(t, Me.Font, SystemBrushes.GrayText, New RectangleF(Point.Empty, Me.Size), f)
        ElseIf _image IsNot Nothing Then
            e.Graphics.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic 'high quality bitmap scaling
            e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality 'high quality vector rendering (when will we ever run into a vector image?)

            Dim imagerect As New RectangleF(Point.Empty, _image.Size)

            'e.Graphics.DrawImage(_image, imagerect.ScaleToFit(New RectangleF(Point.Empty, Me.Size)))

            e.Graphics.DrawImageScaled(New RectangleF(Point.Empty, Me.Size), _image)

        End If
    End Sub

    Protected Overrides Sub OnSizeChanged(e As EventArgs)
        MyBase.OnSizeChanged(e)

        Refresh()
    End Sub

    Private Shared Function FileSafeToOpen(File As IO.FileInfo) As Boolean
        Return (From i In {".exe", ".bat", ".vbs", ".jar", ".js", ".ps1", ".com"} Where File.Extension = i).Count = 0
    End Function

    Private Sub _FreeImage()
        If _image IsNot Nothing Then
            _image.Dispose()
            _image = Nothing
        End If

        If _imagestream IsNot Nothing Then
            _imagestream.Close()
            _imagestream.Dispose()
        End If
    End Sub

    'Public Overrides Sub Refresh()
    '    MyBase.Refresh()

    'End Sub
End Class