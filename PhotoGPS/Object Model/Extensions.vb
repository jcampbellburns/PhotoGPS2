Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

'###Todo: Documentation

Module Util

    ''' <summary>
    ''' Determines if a file or folder's attributes have it flagged as Hidden or System.
    ''' </summary>
    ''' <param name="FileSystemObject">The file or folder being checked.</param>
    ''' <returns>True if the Hidden and System attributes are cleared. False if either or both the Hidden or System attributes are set.</returns>
    Function FileNotSystemOrHidden(FileSystemObject As IO.FileSystemInfo) As Boolean
        'If the file or folder passed as f as both it's Hidden and System flag cleared, this function returns True
        Return (Not (FileSystemObject.Attributes And FileAttributes.Hidden) > 1) Or (Not (FileSystemObject.Attributes Or FileAttributes.System) > 1)
    End Function

    ''' <summary>
    ''' Run a program, open a file, or open a URI with it's default handler.
    ''' </summary>
    ''' <param name="startstring">The path of the program, file, or URI to run or open.</param>
    Public Sub Start(startstring As String)
        Dim a = New Process
        Dim si = New ProcessStartInfo(startstring)

        si.UseShellExecute = True
        si.WindowStyle = ProcessWindowStyle.Normal

        a.StartInfo = si
        a.Start()
    End Sub

    ''' <summary>
    ''' Compare two arrays of <see cref="Byte"/> by treating them as <see cref="Long"/> integer arrays instead. This is significantly faster than comparing the elements of the array one at a time
    ''' </summary>
    ''' <param name="a1">The first byte array to compare.</param>
    ''' <param name="a2">The second byte array to compare.</param>
    ''' <returns><c>True</c> if the entirety of the two byte arrays exactly match. <c>False</c> if there is any degree of difference between the arrays.</returns>
    ''' <remarks>The length of the array is compared before comparing the content of the arrays. This is done to allow different length arrays to 'short-circuit' the comparison of content.</remarks>
    Function SequenceEqualUnsafe(ByVal a1 As Byte(), ByVal a2 As Byte()) As Boolean

        If a1.Length <> a2.Length Then Return False
        Dim longSize = CInt(Math.Floor(a1.Length / 8.0))
        Dim long1 = Runtime.CompilerServices.Unsafe.[As](Of Long())(a1)
        Dim long2 = Runtime.CompilerServices.Unsafe.[As](Of Long())(a2)

        For i = 0 To longSize - 1
            If long1(i) <> long2(i) Then Return False
        Next

        For i = longSize * 8 To a1.Length - 1
            If a1(i) <> a2(i) Then Return False
        Next

        Return True
    End Function

End Module

Module GraphicsExtension

    ''' <summary>
    ''' Draw an image to fit within a specified box, scaling the image proportionally.
    ''' </summary>
    ''' <param name="g">The <see cref="Graphics"/> object this method will draw to.</param>
    ''' <param name="rect"><see cref="Rectangle"/> structure that specifies the location and size within which to fit the drawn image.</param>
    ''' <param name="image"><see cref="Image"/> to draw.</param>
    <Extension>
    Public Sub DrawImageScaled(ByVal g As Graphics, ByVal rect As RectangleF, ByVal image As Image)
        Dim oldRegion = g.Clip.Clone
        g.SetClip(rect, Drawing2D.CombineMode.Intersect)
        Dim DrawRect = GetBestFitRect(rect, New RectangleF(0, 0, image.Width, image.Height))
        g.DrawImage(image, DrawRect)
        g.Clip = oldRegion
    End Sub

    ''' <summary>
    ''' Called by <see cref="DrawImageScaled"/>. Scale a rectangle to fit within another rectange while maintaining it's aspect ratio.
    ''' </summary>
    ''' <param name="CanvasRect">The rectangle within which to fit <c>ImageRect</c>.</param>
    ''' <param name="ImageRect">The rectange which will be scaled to fit within <c>CanvasRect</c>.</param>
    ''' <returns>A new <see cref="Rectangle"/> structure which fits within <c>CanvasRect</c> but which has the same aspect ratio as <c>ImageRect</c>.</returns>
    Private Function GetBestFitRect(ByVal CanvasRect As RectangleF, ByVal ImageRect As RectangleF) As RectangleF
        Dim origImageRatio = ImageRect.Width / ImageRect.Height
        Dim canvasRatio = CanvasRect.Width / CanvasRect.Height
        Dim DrawRect As RectangleF
        If (origImageRatio > canvasRatio) Then
            DrawRect = New RectangleF(CanvasRect.Left, CanvasRect.Top, CanvasRect.Width, CanvasRect.Width * (1 / origImageRatio))
        Else
            DrawRect = New RectangleF(CanvasRect.Left, CanvasRect.Top, CanvasRect.Height * origImageRatio, CanvasRect.Height)
        End If
        DrawRect.X += ((CanvasRect.Width - DrawRect.Width) / 2)
        DrawRect.Y += ((CanvasRect.Height - DrawRect.Height) / 2)
        Return DrawRect
    End Function

End Module

Module StringExtensions

    ''' <summary>
    ''' Ensures that a path which is passed to this function as a string is prepended precisely once with "\\?\". This is to affect a bugfix where file operation method in the <see cref="IO"/> namespace can operate on files and directories longer than 260 characters in length.
    ''' </summary>
    ''' <param name="path">The path to prepend "\\?\" to, if it's not already present.</param>
    ''' <returns>The value of <c>path</c> with "\\?\" prepended to it, if it was not already prepended with "\\?\".</returns>
    <Extension, System.Diagnostics.DebuggerStepThrough()>
    Function EnsureFilePrepend(path As String) As String
        Return IIf(path.StartsWith("\\?\"), path, "\\?\" & path)
    End Function

End Module

Module ListBoxExtension

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As IntPtr) As IntPtr
    End Function

    ''' <summary>
    ''' Select all items in a <see cref="ListBox"/> control.
    ''' </summary>
    ''' <param name="lb">The <see cref="ListBox"/> control in which to select all items.</param>
    ''' <remarks>Calls to this method suppres any selection changing related events on the <see cref="ListBox"/> on which this is called. Also, methods and properties which obtain the selected items or indeces will return incorrect results. Keep track of calls to this method. Any code which relies on a selection change event should be called immediately after a call to this method. Any code which relies on the currently selected items should instead assume all items are selected after a call to this method.</remarks>
    ''' <!--<exception cref="ArgumentOutOfRangeException">The specified index was outside the range of valid values.</exception>
    ''' <exception cref="InvalidOperationException">The <see cref="ListBox.SelectionMode"/> property was set to <c>None</c>.</exception>-->
    <Extension>
    Sub SelectAll(lb As ListBox)
        lb.BeginUpdate()

        Const WM_LBSelectAll As Integer = 389
        SendMessage(lb.Handle, WM_LBSelectAll, CType(1, IntPtr), CType(-1, IntPtr))

        lb.EndUpdate()

    End Sub

End Module

''' <summary>
''' Structure for containing a GPS coordinate
''' </summary>
Public Structure PointLatLng

    ''' <summary>
    ''' Create a structure of <see cref="PointLatLng"/> with <see cref="Latitude"/> and <see cref="Longitude"/> set.
    ''' </summary>
    ''' <param name="Latitude">The latitude of the GPS coordinate.</param>
    ''' <param name="Longitude">The longitude of the GPS coordinate.</param>
    Public Sub New(Latitude As Double, Longitude As Double)
        Me.Latitude = Latitude
        Me.Longitude = Longitude

    End Sub

    ''' <summary>
    ''' The latitude of the GPS coordinate.
    ''' </summary>
    Public Property Latitude As Double

    ''' <summary>
    ''' The longitude of the GPS coordinate.
    ''' </summary>
    Public Property Longitude As Double

    ''' <summary>
    ''' Compare two GPS coordinates for eqality.
    ''' </summary>
    ''' <param name="a">The first GPS coordinate to compare.</param>
    ''' <param name="b">The second GPS coordinate to compare.</param>
    ''' <returns><c>True</c> if both GPS coordinates exactly match. <c>False</c> if both coordinates do not exactly match.</returns>
    Public Shared Operator =(a As PointLatLng, b As PointLatLng) As Boolean
        Return (a.Longitude = b.Longitude) And (a.Latitude = b.Latitude)
    End Operator

    ''' <summary>
    ''' Compare two GPS coordinates for ineqality.
    ''' </summary>
    ''' <param name="a">The first GPS coordinate to compare.</param>
    ''' <param name="b">The second GPS coordinate to compare.</param>
    ''' <returns><c>True</c> if both GPS coordinates do not exactly match. <c>False</c> if both coordinates exactly match.</returns>
    Public Shared Operator <>(a As PointLatLng, b As PointLatLng) As Boolean
        Return Not a = b
    End Operator

    ''' <summary>
    ''' Compare the distance between two <see cref="PointLatLng"/> values.
    ''' </summary>
    ''' <param name="Start">A <see cref="PointLatLng"/> value representing the starting point for the measurement.</param>
    ''' <param name="Destination">A <see cref="PointLatLng"/> value representing the ending point for the measurement.</param>
    ''' <param name="Units">Optional. One of the values from <see cref="DistanceToUnits"/> indicating what units the return value should be specified in. If this parameter is not specified, the default value of <see cref="DistanceToUnits.Miles"/> is assumed.</param>
    ''' <returns></returns>
    Public Shared Function DistanceTo(Start As PointLatLng, Destination As PointLatLng, Optional Units As DistanceToUnits = DistanceToUnits.Miles) As Double
        Const METERSPERKILOMETER As Integer = 1000
        Const FEETPERMETER As Double = 3.28084
        Const FEETPERYARD As Integer = 3
        Const FEETPERMILE As Integer = 5280

        Dim distancemeters = distVincenty(Start.Latitude, Start.Longitude, Destination.Latitude, Destination.Longitude)

        Select Case Units
            Case DistanceToUnits.Feet
                Return distancemeters * FEETPERMETER
            Case DistanceToUnits.Kilometers
                Return distancemeters / METERSPERKILOMETER
            Case DistanceToUnits.Meters
                Return distancemeters
            Case DistanceToUnits.Miles
                Return (distancemeters * FEETPERMETER) / FEETPERMILE
            Case DistanceToUnits.Yards
                Return (distancemeters * FEETPERMETER) / FEETPERYARD
            Case Else
                Throw New NotImplementedException()
        End Select
    End Function

    ''' <summary>
    ''' Compare the distance between two <see cref="PointLatLng"/> values.
    ''' </summary>
    ''' <param name="Destination">A <see cref="PointLatLng"/> value representing the ending point for the measurement.</param>
    ''' <param name="Units">Optional. One of the values from <see cref="DistanceToUnits"/> indicating what units the return value should be specified in. If this parameter is not specified, the default value of <see cref="DistanceToUnits.Miles"/> is assumed.</param>
    ''' <returns></returns>
    Public Function DistanceTo(Destination As PointLatLng, Optional Units As DistanceToUnits = DistanceToUnits.Miles) As Double
        Return DistanceTo(Me, Destination, Units)
    End Function

    ''' <summary>
    ''' Used to indicate which measurement unit to return from <see cref="DistanceTo(PointLatLng, PointLatLng, DistanceToUnits)"/>.
    ''' </summary>
    Public Enum DistanceToUnits

        ''' <summary>
        ''' The return value is measured in meters.
        ''' </summary>
        Meters = 0

        ''' <summary>
        ''' The return value is measured in kilometers.
        ''' </summary>
        Kilometers = 1 '0.001

        ''' <summary>
        ''' The return value is measured in feet.
        ''' </summary>
        Feet = 2

        ''' <summary>
        ''' The return value is measured in yards.
        ''' </summary>
        Yards = 3

        ''' <summary>
        ''' The return value is measured in miles.
        ''' </summary>
        Miles = 4

    End Enum

    ''' <summary>
    ''' Compare two sets of longitude and latitude coordinates and returnes the geodesic distance between the two coordinates.
    ''' </summary>
    ''' <param name="lat1">The latitude of the first coordinate to compare.</param>
    ''' <param name="lon1">The longitude of the first coordinate to compare.</param>
    ''' <param name="lat2">The latitude of the second coordinate to compare.</param>
    ''' <param name="lon2">The longitude of the second coordinate to compare.</param>
    ''' <returns>Returns the distance in meters between the specified sets of coordinates.</returns>
    Private Shared Function distVincenty(ByVal lat1 As Double, ByVal lon1 As Double, ByVal lat2 As Double, ByVal lon2 As Double) As Double
        'INPUTS: Latitude and Longitude of initial and
        '           destination points in decimal format.
        'OUTPUT: Distance between the two points in Meters.
        '
        '======================================
        ' Calculate geodesic distance (in m) between two points specified by
        ' latitude/longitude (in numeric [decimal] degrees)
        ' using Vincenty inverse formula for ellipsoids
        '======================================
        ' Code has been ported by lost_species from www.aliencoffee.co.uk to VBA
        ' from javaScript published at:
        ' http://www.movable-type.co.uk/scripts/latlong-vincenty.html
        ' * from: Vincenty inverse formula - T Vincenty, "Direct and Inverse Solutions
        ' *       of Geodesics on the Ellipsoid with application
        ' *       of nested equations", Survey Review, vol XXII no 176, 1975
        ' *       http://www.ngs.noaa.gov/PUBS_LIB/inverse.pdf
        'Additional Reference: http://en.wikipedia.org/wiki/Vincenty%27s_formulae
        '======================================
        ' Copyright lost_species 2008 LGPL
        ' http://www.fsf.org/licensing/licenses/lgpl.html
        '======================================
        ' Code modifications to prevent "Formula Too Complex" errors
        ' in Excel (2010) VBA implementation
        ' provided by Jerry Latham, Microsoft MVP Excel Group, 2005-2011
        ' July 23 2011
        '======================================

        Dim low_a As Double
        Dim low_b As Double
        Dim f As Double
        Dim L As Double
        Dim U1 As Double
        Dim U2 As Double
        Dim sinU1 As Double
        Dim sinU2 As Double
        Dim cosU1 As Double
        Dim cosU2 As Double
        Dim lambda As Double
        Dim lambdaP As Double
        Dim iterLimit As Integer
        Dim sinLambda As Double
        Dim cosLambda As Double
        Dim sinSigma As Double
        Dim cosSigma As Double
        Dim sigma As Double
        Dim sinAlpha As Double
        Dim cosSqAlpha As Double
        Dim cos2SigmaM As Double
        Dim C As Double
        Dim uSq As Double
        Dim upper_A As Double
        Dim upper_B As Double
        Dim deltaSigma As Double
        Dim s As Double ' final result, will be returned rounded to 3 decimals (mm).
        'added by JLatham to break up "Too Complex" formulas
        'into pieces to properly calculate those formulas as noted below
        'and to prevent overflow errors when using
        'Excel 2010 x64 on Windows 7 x64 systems
        Dim P1 As Double ' used to calculate a portion of a complex formula
        Dim P2 As Double ' used to calculate a portion of a complex formula
        Dim P3 As Double ' used to calculate a portion of a complex formula

        'See http://en.wikipedia.org/wiki/World_Geodetic_System
        'for information on various Ellipsoid parameters for other standards.
        'low_a and low_b in meters
        ' === GRS-80 ===
        ' low_a = 6378137
        ' low_b = 6356752.314245
        ' f = 1 / 298.257223563
        '
        ' === Airy 1830 ===  Reported best accuracy for England and Northern Europe.
        ' low_a = 6377563.396
        ' low_b = 6356256.910
        ' f = 1 / 299.3249646
        '
        ' === International 1924 ===
        ' low_a = 6378388
        ' low_b = 6356911.946
        ' f = 1 / 297
        '
        ' === Clarke Model 1880 ===
        ' low_a = 6378249.145
        ' low_b = 6356514.86955
        ' f = 1 / 293.465
        '
        ' === GRS-67 ===
        ' low_a = 6378160
        ' low_b = 6356774.719
        ' f = 1 / 298.247167

        '=== WGS-84 Ellipsoid Parameters ===
        low_a = 6378137       ' +/- 2m
        low_b = 6356752.3142
        f = 1 / 298.257223563
        '====================================
        L = toRad(lon2 - lon1)
        U1 = Math.Atan((1 - f) * Math.Tan(toRad(lat1)))
        U2 = Math.Atan((1 - f) * Math.Tan(toRad(lat2)))
        sinU1 = Math.Sin(U1)
        cosU1 = Math.Cos(U1)
        sinU2 = Math.Sin(U2)
        cosU2 = Math.Cos(U2)

        lambda = L
        lambdaP = 2 * Math.PI
        iterLimit = 100 ' can be set as low as 20 if desired.

        While (Math.Abs(lambda - lambdaP) > Double.Epsilon) And (iterLimit > 0)
            iterLimit = iterLimit - 1

            sinLambda = Math.Sin(lambda)
            cosLambda = Math.Cos(lambda)
            sinSigma = Math.Sqrt(((cosU2 * sinLambda) ^ 2) + ((cosU1 * sinU2 - sinU1 * cosU2 * cosLambda) ^ 2))
            If sinSigma = 0 Then
                distVincenty = 0  'co-incident points
                Exit Function
            End If
            cosSigma = sinU1 * sinU2 + cosU1 * cosU2 * cosLambda
            sigma = Atan2(cosSigma, sinSigma)
            sinAlpha = cosU1 * cosU2 * sinLambda / sinSigma
            cosSqAlpha = 1 - sinAlpha * sinAlpha

            If cosSqAlpha = 0 Then 'check for a divide by zero
                cos2SigmaM = 0 '2 points on the equator
            Else
                cos2SigmaM = cosSigma - 2 * sinU1 * sinU2 / cosSqAlpha
            End If

            C = f / 16 * cosSqAlpha * (4 + f * (4 - 3 * cosSqAlpha))
            lambdaP = lambda

            'the original calculation is "Too Complex" for Excel VBA to deal with
            'so it is broken into segments to calculate without that issue
            'the original implementation to calculate lambda
            'lambda = L + (1 - C) * f * sinAlpha *   (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma *   (-1 + 2 * (cos2SigmaM ^ 2))))
            'calculate portions
            P1 = -1 + 2 * (cos2SigmaM ^ 2)
            P2 = (sigma + C * sinSigma * (cos2SigmaM + C * cosSigma * P1))
            'complete the calculation
            lambda = L + (1 - C) * f * sinAlpha * P2

        End While

        'If iterLimit < 1 Then
        '    'MsgBox "iteration limit has been reached, something didn't work."
        '    Throw New Exception("iteration limit has been reached, something didn't work.")
        '    'Exit Function
        'End If

        uSq = cosSqAlpha * (low_a ^ 2 - low_b ^ 2) / (low_b ^ 2)

        'the original calculation is "Too Complex" for Excel VBA to deal with
        'so it is broken into segments to calculate without that issue
        'the original implementation to calculate upper_A
        'upper_A = 1 + uSq / 16384 * (4096 + uSq *     (-768 + uSq * (320 - 175 * uSq)))
        'calculate one piece of the equation
        P1 = (4096 + uSq * (-768 + uSq * (320 - 175 * uSq)))
        'complete the calculation
        upper_A = 1 + uSq / 16384 * P1

        'oddly enough, upper_B calculates without any issues - JLatham
        upper_B = uSq / 1024 * (256 + uSq * (-128 + uSq * (74 - 47 * uSq)))

        'the original calculation is "Too Complex" for Excel VBA to deal with
        'so it is broken into segments to calculate without that issue
        'the original implementation to calculate deltaSigma
        'deltaSigma = upper_B * sinSigma * (cos2SigmaM + upper_B / 4 *     (cosSigma * (-1 + 2 * cos2SigmaM ^ 2)       - upper_B / 6 * cos2SigmaM * (-3 + 4 * sinSigma ^ 2) *         (-3 + 4 * cos2SigmaM ^ 2)))
        'calculate pieces of the deltaSigma formula
        'broken into 3 pieces to prevent overflow error that may occur in
        'Excel 2010 64-bit version.
        P1 = (-3 + 4 * sinSigma ^ 2) * (-3 + 4 * cos2SigmaM ^ 2)
        P2 = upper_B * sinSigma
        P3 = (cos2SigmaM + upper_B / 4 * (cosSigma * (-1 + 2 * cos2SigmaM ^ 2) _
    - upper_B / 6 * cos2SigmaM * P1))
        'complete the deltaSigma calculation
        deltaSigma = P2 * P3

        'calculate the distance
        s = low_b * upper_A * (sigma - deltaSigma)
        'round distance to millimeters
        distVincenty = Math.Round(s, 3)

    End Function

    ''' <summary>
    ''' Converts degrees to radians.
    ''' </summary>
    ''' <param name="degrees">The angle in degrees to convert.</param>
    ''' <returns>The angle converted radians.</returns>
    Private Shared Function toRad(ByVal degrees As Double) As Double
        toRad = degrees * (Math.PI / 180)
    End Function

    ''' <summary>
    ''' Calculate the angle in all four quadrants of a vector
    ''' </summary>
    Private Shared Function Atan2(ByVal X As Double, ByVal Y As Double) As Double
        ' code nicked from:
        ' http://en.wikibooks.org/wiki/Programming:Visual_Basic_Classic/Simple_Arithmetic#Trigonometrical_Functions
        ' If you re-use this watch out: the x and y have been reversed from typical use.
        If Y > 0 Then
            If X >= Y Then
                Atan2 = Math.Atan(Y / X)
            ElseIf X <= -Y Then
                Atan2 = Math.Atan(Y / X) + Math.PI
            Else
                Atan2 = Math.PI / 2 - Math.Atan(X / Y)
            End If
        Else
            If X >= -Y Then
                Atan2 = Math.Atan(Y / X)
            ElseIf X <= Y Then
                Atan2 = Math.Atan(Y / X) - Math.PI
            Else
                Atan2 = -Math.Atan(X / Y) - Math.PI / 2
            End If
        End If
    End Function

End Structure

Module ConcurrentBagExtensions

    ''' <summary>
    ''' Adds the elements of the specified collection to the into a <see cref="Concurrent.ConcurrentBag(Of T)"/>.
    ''' </summary>
    ''' <typeparam name="t">The type of elements in the list.</typeparam>
    ''' <param name="list">The <see cref="Concurrent.ConcurrentBag(Of T)"/> to add the items to.</param>
    ''' <param name="collection">The collection whose elements should be added into the <see cref="Concurrent.ConcurrentBag(Of T)"/>. The collection itself cannot be <c>null</c>, but it can contain elements that are <c>null</c>, if type <c>T</c> is a reference type.</param>
    ''' <remarks><para>While the items are added to the <see cref="Concurrent.ConcurrentBag(Of T)"/> in the order specified by <c>collection</c>, there is no guarantee that the items will be contiguous or where the items will appear in the <see cref="Concurrent.ConcurrentBag(Of T)"/>.</para><para>this method enumerates the items in <c>collection</c>.</para></remarks>
    ''' <exception cref="ArgumentNullException"><c>collection</c> is <c>null</c>.</exception>
    <Extension()>
    Sub AddRange(Of t)(list As Collections.Concurrent.ConcurrentBag(Of t), collection As IEnumerable(Of t))
        If collection Is Nothing Then Throw New ArgumentNullException
        For Each i As t In collection
            list.Add(i)
        Next
    End Sub

End Module

Module IEnumerableExtensions

    ''' <summary>
    ''' Returns an <see cref="IEnumerable(Of T)"/> which reports progress when enumerated.
    ''' </summary>
    ''' <typeparam name="T">The type of items contained in the <see cref="IEnumerable(Of T)"/></typeparam>
    ''' <param name="sequence">The sequence on which to report enumeration progress.</param>
    ''' <param name="reportProgress">The <see cref="Action(Of T1, T2)"/> containing the method which will be called for each item in the sequence when the sequence is enumerated.</param>
    ''' <returns>Returns an <see cref="IEnumerable(Of T)"/> which reports on the progress when being enumerated.</returns>
    <Extension()>
    Function WithProgressReporting(Of T)(ByVal sequence As IEnumerable(Of T), ByVal reportProgress As Action(Of Integer, Integer)) As IEnumerable(Of T)
        If sequence Is Nothing Then
            Throw New ArgumentNullException("sequence")
        End If

        Dim collection As ICollection(Of T) = TryCast(sequence, ICollection(Of T))

        If collection Is Nothing Then
            collection = New List(Of T)(sequence)
        End If

        Dim total As Integer = collection.Count
        Return collection.WithProgressReporting(total, reportProgress)
    End Function

    ''' <summary>
    ''' Returns an <see cref="IEnumerable(Of T)"/> which reports progress when enumerated.
    ''' </summary>
    ''' <typeparam name="T">The type of items contained in the <see cref="IEnumerable(Of T)"/></typeparam>
    ''' <param name="sequence">The sequence on which to report enumeration progress.</param>
    ''' <param name="itemCount">The total number of items in the sequence.</param>
    ''' <param name="reportProgress">The <see cref="Action(Of T1, T2)"/> containing the method which will be called for each item in the sequence when the sequence is enumerated.</param>
    ''' <returns>Returns an <see cref="IEnumerable(Of T)"/> which reports on the progress when being enumerated.</returns>
    <Extension()>
    Iterator Function WithProgressReporting(Of T)(ByVal sequence As IEnumerable(Of T), ByVal itemCount As Long, ByVal reportProgress As Action(Of Integer, Integer)) As IEnumerable(Of T)
        If sequence Is Nothing Then
            Throw New ArgumentNullException("sequence")
        End If

        Dim completed As Integer = 0

        For Each item In sequence
            Yield item
            completed += 1
            reportProgress(itemCount, completed)
        Next
    End Function

End Module