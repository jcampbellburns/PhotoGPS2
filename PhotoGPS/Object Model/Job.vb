''' <summary>
''' Represents a specific job. this refers to a Dispatch in salesforce.
'''
''' </summary>
Public Class Job 'to be renamed to Dispatch in v3

    ''' <summary>
    ''' Nullable. The last date on which work was scheduled.
    ''' </summary>
    <CSVField()> Public [End] As Date?

    ''' <summary>
    ''' Nullable. The longitude of the job site.
    ''' </summary>
    <CSVField(AltReadName:="Longitude")> Public [Long] As Double?

    ''' <summary>
    ''' A unique identifier for this job. Equivelent to the DispatchNumber field in Salesforce. This is used for naming folder when moving photos.
    ''' </summary>
    <CSVField("Dispatch Number")> Public DispatchNumber As String

    ''' <summary>
    ''' A globally unique identifier for this job. Equivelent to record ID in Salesforce. This is used for importing data into Salesforce.
    ''' </summary>
    <CSVField("Dispatch: ID")> Public ID As String

    ''' <summary>
    ''' Nullable. The latitude of the job site.
    ''' </summary>
    <CSVField(AltReadName:="Latitude")> Public Lat As Double?

    ''' <summary>
    ''' A reference to an instance of <see cref="Project"/> which contains all of the instances of both <see cref="PhotoFile"/> and <see cref="Job"/> currently loaded. Similar to a DBContext model.
    ''' </summary>
    Public Project As Project

    ''' <summary>
    ''' Indicates that this job was manually added on the manual correlation scren and should not be included when exporting the CSV
    ''' </summary>
    Public ExcludeFromSerialization As Boolean

    ''' <summary>
    ''' Nullable. The first date on which work was scheduled.
    ''' </summary>
    <CSVField()> Public Start As Date?

    ''' <summary>
    ''' All of the instances of <see cref="PhotoFile"/> associated with this particular <see cref="Job"/> instance.
    ''' </summary>
    Private _Photos As List(Of PhotoFile)

    ''' <summary>
    ''' Nullable. The GPS coordinates of the jobsite.
    ''' </summary>
    Public Property GPS As PointLatLng?
        Get
            If Lat.HasValue And [Long].HasValue Then
                Return New PointLatLng(Lat.Value, [Long].Value)
            Else
                Return New PointLatLng?
            End If
        End Get
        Set(value As PointLatLng?)
            If value.HasValue Then
                Lat = value.Value.Latitude
                [Long] = value.Value.Longitude
            Else
                Lat = New Double?
                [Long] = New Double?
            End If

        End Set
    End Property

    ''' <summary>
    ''' A <see cref="Boolean"/> indicating if both <see cref="Start"/> and <see cref="[End]"/> are not <c>null</c>.
    ''' </summary>
    ''' <returns><c>True</c> if both <see cref="Start"/> and <see cref="[End]"/> have values. <c>False</c> if either <see cref="Start"/>, <see cref="[End]"/>, or both have a value of <c>null</c>.</returns>
    Public ReadOnly Property HasDates As Boolean
        Get
            Return (Start.HasValue And [End].HasValue)
        End Get
    End Property

    ''' <summary>
    ''' A <see cref="Boolean"/> indicating if this job has photos.
    ''' </summary>
    ''' <returns><c>True</c> if <see cref="PhotoCount"/> has a value greater than <c>0</c>. <c>False</c> if <see cref="PhotoCount"/> is <c>0</c> or if <see cref="Photos"/> is <c>null</c></returns>
    <CSVField(AltReadName:="Photos returned?", Readable:=False, Writeable:=True)>
    Public ReadOnly Property HasPhotos As Boolean
        Get
            Return PhotoCount > 0
        End Get
    End Property

    ''' <summary>
    ''' An <see cref="Integer"/> indicating the number of photos associated with this job.
    ''' </summary>
    ''' <returns>The number of photos associate with this job. If <see cref="Photos"/> is <c>null</c>, this method returns <c>0</c>.</returns>
    <CSVField(AltReadName:="Photo Count", Readable:=False, Writeable:=True)> Public ReadOnly Property PhotoCount As Integer
        Get
            If _Photos Is Nothing Then
                Return 0
            Else
                Return _Photos.Count
            End If
        End Get
    End Property

    ''' <summary>
    ''' All of the instances of <see cref="PhotoFile"/> associated with this particular <see cref="Job"/> instance.
    ''' </summary>
    Public Property Photos As List(Of PhotoFile)
        Get
            If _Photos Is Nothing Then
                _Photos = New List(Of PhotoFile)
            End If

            Return _Photos

        End Get
        Set(value As List(Of PhotoFile))
            _Photos = value
        End Set
    End Property

    ''' <summary>
    ''' Compare the various properties of <see cref="PhotoFile"/> with this instance of <see cref="Job"/> to see if the specified  photos can be automatically associated with this job.
    ''' </summary>
    ''' <param name="Photo">The <see cref="PhotoFile"/> to compare.</param>
    ''' <returns><c>True </c>if <see cref="PhotoFile.GPS"/> is within 0.5 miles of <see cref="Job.GPS"/> and <see cref="PhotoFile.TakenDate"/> occurs on or between <see cref="Job.Start"/> and <see cref="Job.End"/>. Otherwise <c>False</c>.</returns>
    Public Function ComparePhoto(Photo As PhotoFile) As Boolean
        If GPS.HasValue Then
            Dim datesMatch As Boolean = False

            If HasDates Then
                'a Job can start and end on the same day. Since a photo can occur at any time between the start and end of any day, we need to make sure to look between the beginning of Start and the end of End. Best way to do that is to make the range of Start and End 24-hours.
                Dim realEnd As Date = [End].Value.AddDays(1)

                If (Photo.TakenDate >= Start) And (Photo.TakenDate <= realEnd) Then
                    datesMatch = True
                Else
                    datesMatch = False
                End If
            Else
                datesMatch = True 'all photos match a Job with no dates so long as the GPS DOES match
            End If

            Dim gpsMatch = (GPS.Value.DistanceTo(Photo.GPS, PointLatLng.DistanceToUnits.Miles) < 0.5)
            Return datesMatch And gpsMatch
        Else
            Return False 'no photos match a Job with no GPS
        End If
    End Function

End Class