﻿Imports ExifLib

''' <summary>
''' Represents a file which can be associated with a <see cref="Job"/>.
''' </summary>
Public Class PhotoFile
    ''' <summary>
    ''' Creates a new instance of <see cref="PhotoFile"/> from a specified file.
    ''' </summary>
    ''' <param name="File">The file, as specified by an instance of <see cref="IO.FileInfo"/> from which to create the new instance.</param>
    ''' <param name="Project">The <see cref="Project"/> instance containing all other <see cref="PhotoFile"/> instances and <see cref="Job"/> instances.</param>
    Public Sub New(File As IO.FileInfo, Project As Project)
        _File = File
        _Project = Project

        _refreshMetadata()
    End Sub

    Protected Sub New()

    End Sub

    ''' <summary>
    ''' The file represented by the <see cref="PhotoFile"/> instance.
    ''' </summary>
    Public ReadOnly Property File As IO.FileInfo

    ''' <summary>
    ''' Nullable. If GPS information could be read from the file's metadata, this property will contain a <see cref="PointLatLng"/> structure containing this data.
    ''' </summary>
    Public Property GPS As PointLatLng?

    '''' <summary>
    '''' Returns an array of <see cref="Byte"/> containing a Blake2 hash of the file represented by <see cref="PhotoFile.File"/>.
    '''' </summary>
    '''' <remarks>This hash is generated by the internal <see cref="PhotoFile._refreshMetadata()"/> method which is called by the constructor.</remarks>
    'Public ReadOnly Property Hash As Byte()

    '''' <summary>
    '''' Returns a Base64 encoded version of the value of <see cref="PhotoFile.Hash"/> which is appropriate for use in a filename. See remarks.
    '''' </summary>
    '''' <remarks>The returned string has a non-standard formatting of Base64 to support saving the entire string in a filename. The '+', '/', and '=' are replace with '!', '@', and '#', respectively.</remarks>
    'Public ReadOnly Property HashString As String
    '    Get
    '        If Hash Is Nothing Then
    '            Return String.Empty
    '        Else
    '            Return Convert.ToBase64String(_Hash).Replace("+", "!").Replace("/", "@").Replace("=", "#")

    '        End If
    '    End Get
    '    'Set(value As String)
    '    '    _Hash = Convert.FromBase64String(value.Replace("!", "+").Replace("@", "/").Replace("#", "="))
    '    'End Set
    'End Property

    ''' <summary>
    ''' Indicated that this <see cref="PhotoFile"/> instance, and its file, have been deleted by a call to the <see cref="PhotoFile.Delete()"/> method. This is typically done when a duplicate has been found while the application is moving photos on the filesystem.
    ''' </summary>
    ''' <remarks>This property is not equivelent to <see cref="IO.File.Exists(String)"/> or similar methods and does not check the filesystem for the existance of the file.</remarks>
    Public ReadOnly Property IsDeleted As Boolean

    ''' <summary>
    ''' An unenumerated query of the jobs this <see cref="PhotoFile"/> is associated with in <see cref="Project"/>.
    ''' </summary>
    Public ReadOnly Property Jobs As IEnumerable(Of Job)
        Get
            Return From i In _Project.Jobs Where i.Photos.Contains(Me)
        End Get
    End Property

    ''' <summary>
    ''' A reference to an instance of <see cref="Project"/> which contains all of the instances of both <see cref="PhotoFile"/> and <see cref="Job"/> currently loaded. Similar to a DBContext model.
    ''' </summary>
    Public Property Project As Project

    ''' <summary>
    ''' Nullable. If the photo's <c>TakenDate</c> information could be read from the file's metadata, this property will contain a <see cref="Date"/> structure containing this data.
    ''' </summary>
    Public Property TakenDate As Date?

    ''' <summary>
    ''' Rename and moves the file represented by <see cref="File"/> to a new folder. See remarks.
    ''' </summary>
    ''' <param name="folder">The folder to which to move the file.</param>
    ''' <remarks><para>If <see cref="PhotoFile.IsDeleted"/> is <c>True</c>, this method has no effect.</para><para>This method performs the following tasks:<list type="bullet"><item>Rename the file to ensure it is unique. See the <see cref="PhotoFile.ProposedName()"/> method.</item><!--<item>Removes any <see cref="PhotoFile"/> instances from <see cref="PhotoFile.Project"/> and all <see cref="Job"/> instances associated with it where the instance's <see cref="PhotoFile.Hash"/> exactly matches this instance's <see cref="PhotoFile.Hash"/>. This method then calls. <see cref="PhotoFile.Delete()"/> for all such matching instances.</item>--><item>If a file with the same name (after renaming) exists in the specified folder, <!--the method will compare the hash of that file with this instance's hash. If they match, -->the existing file is overwritten. If they do not match, this instance's file is renamed to include the next available number in the form of [filename](n).[extension].</item></list></para></remarks>
    Public Sub RenameAndMove(folder As String)
        '--edge case: Calls to RenameAndMove delete duplicate files in the project as well as in the desitnation folder. This means that THIS photo instance may have been deleted. We can skip this whole method if this instance has been deleted.

        If Not _IsDeleted Then
            Dim ext = _File.Extension

            Dim proposedFullPath As String = IO.Path.Combine(folder, ProposedName)

            If Me._File.FullName.EnsureFilePrepend.ToLower <> proposedFullPath.EnsureFilePrepend.ToLower Then
                If Not IO.Directory.Exists(folder) Then IO.Directory.CreateDirectory(folder)
                Me.MoveTo(proposedFullPath)

            End If


        End If

    End Sub

    ''' <summary>
    ''' Attempts to populat <see cref="PhotoFile.GPS"/> and <see cref="PhotoFile.TakenDate"/> from the file. <!--Also populates <see cref="Hash"/>-->. See remarks.
    ''' </summary>
    ''' <remarks>Currently, the only supported metadata format is EXIF from .jpg files. All other file formats result in <see cref="PhotoFile.GPS"/> and <see cref="PhotoFile.TakenDate"/> having null values.</remarks>
    Private Sub _refreshMetadata()
        Try
            'Dim b = IO.File.ReadAllBytes(_File.FullName.EnsureFilePrepend)
            'Me._Hash = Blake2s.Blake2S.ComputeHash(b)

            Using s = IO.File.Open(_File.FullName.EnsureFilePrepend(), IO.FileMode.Open), reader = New ExifLib.ExifReader(s)

                'Using reader = New ExifLib.ExifReader(_File.FullName.EnsureFilePrepend)
                Dim latitudeDMS As Double() = {0, 0}
                Dim longitudeDMS As Double() = {0, 0}
                Dim latitudeRef = String.Empty
                Dim longitudeRef = String.Empty
                Dim takenDate As New Date

                'reader.GetTagValue returns False if the tag is missing, True otherwise. We want to use a Nullable(Of ...) value to avoid photos without a GPS tag from showing up at 0, 0 on the map.
                Dim _HasGPS =
                reader.GetTagValue(ExifTags.GPSLatitude, latitudeDMS) And
                reader.GetTagValue(ExifTags.GPSLongitude, longitudeDMS) And
                reader.GetTagValue(ExifTags.GPSLatitudeRef, latitudeRef) And
                reader.GetTagValue(ExifTags.GPSLongitudeRef, longitudeRef)

                Dim _HasTakenDate = reader.GetTagValue(Of Date)(ExifLib.ExifTags.DateTimeDigitized, takenDate)

                With Me
                    If _HasGPS Then
                        Me.GPS = New PointLatLng(If(latitudeRef = "N", 1, -1) * (latitudeDMS(0) + (latitudeDMS(1) / 60) + (latitudeDMS(2) / 3600)), If(longitudeRef = "E", 1, -1) * (longitudeDMS(0) + (longitudeDMS(1) / 60) + (longitudeDMS(2) / 3600)))
                    Else
                        Me.GPS = Nothing
                    End If

                    If _HasTakenDate Then
                        .TakenDate = takenDate
                    Else
                        .TakenDate = Nothing
                    End If

                End With


                s.Close()
            End Using
        Catch ex As ExifLibException
            'if exiflib is unable to read the file, we consider the file as not supported.

            Return
        Catch ex As System.IO.EndOfStreamException
            'zero-length files are also unsupported
            Return
            'Catch ex As Exception
            '    If MsgBox(String.Format("Unhandled exception reading file {0}. Exception type is {1}. Message is ""{2}"". Location is Photo.RefreshMetadata. Continue?", File.FullName, ex.GetType.ToString, ex.Message), MsgBoxStyle.YesNo) = MsgBoxResult.No Then Throw
            Return
        End Try

    End Sub


    Private Sub Delete()
        If Not _IsDeleted Then
            IO.File.Delete(_File.FullName)

            _IsDeleted = True
        End If

    End Sub

    Private Sub MoveTo(newlocation As String)
        SyncLock New Object 'I ran into a case where one thread checks to see if a file exists, it didn't, then another thread created the file, then this thread tried to move the file and threw a 'file exists' exception. SyncLock this whole block to prevent this from happening.
            If Not _IsDeleted Then
                Try

                    While IO.File.Exists(newlocation.EnsureFilePrepend)
                        IO.File.Delete(newlocation.EnsureFilePrepend)
                    End While

                    IO.File.Move(_File.FullName.EnsureFilePrepend, newlocation.EnsureFilePrepend)

                    'bug fix: When you use IO.File.Move to move a file, existing IO.FileInfo instances are NOT automatically updated... because why would they be. So we manually update it here.
                    _File = New IO.FileInfo(newlocation.EnsureFilePrepend)

                Catch ex As IO.IOException
                    Select Case ex.HResult
                        Case &H80070002 'file not found
                            'mark as deleted
                            _IsDeleted = True
                        Case &H80070020 'file in use
                            'skip the file
                        Case Else
                            Throw
                    End Select
                Catch ex As System.UnauthorizedAccessException
                    'don't have the necessary access. skip the file

                End Try
            End If
        End SyncLock
    End Sub

    ''' <summary>
    ''' Returns a proposed filename which <see cref="PhotoFile"/> can be renamed to.
    ''' </summary>
    ''' <returns>If <see cref="PhotoFile.GPS"/> or <see cref="PhotoFile.TakenDate"/> have non-null values, the filename will include whichever is available<!-- as well as the value of <see cref="HashString"/>-->. If both <see cref="PhotoFile.GPS"/> and <see cref="PhotoFile.TakenDate"/> have a null value, the original filename is returned.</returns>
    ''' <!--<remarks>If the original filename is returned, the value of <see cref="HashString"/> is NOT included and care must be taken to ensure that two different files with the same name are not populated to the same folder.</remarks>-->
    Private Function ProposedName() As String

        If Not GPS.HasValue And Not TakenDate.HasValue Then
            Return _File.Name
        End If

        Dim t = ""
        Dim g = ""
        If _TakenDate.HasValue Then t = TakenDate.Value.ToString("yyyy-MM-dd HH.mm.ss")
        If _GPS.HasValue Then g = String.Format(" GPS={0}, {1}", GPS.Value.Latitude.ToString("0.000000"), GPS.Value.Longitude.ToString("0.000000"))

        Return t & g & _File.Extension
        'Return String.Join(" ", t, g, HashString & _File.Extension) ' not a typoe: HashString & _File.Extension. We don't want a space " " between those two fields.
    End Function

End Class