'The classes and methods in this file fully implement the guidelines described in "RFC 4180 - Common Format and MIME Type for Comma-Separated Values (CSV) Files"

Imports System.Reflection

Structure CSVColumnHeader
    Public CSVName As String
    Public Member As Reflection.MemberInfo

    ''' <summary>
    ''' Returns an <see cref="IEnumerable(Of CSVColumnHeader)"/> containing a list of all of the members of <c>t</c> which have <see cref="CSVFieldAttribute"/> applied.
    ''' </summary>
    ''' <typeparam name="t"></typeparam>
    ''' <returns><see cref="IEnumerable(Of CSVColumnHeader)"/> containing a list of all of the members of <c>t</c> which have <see cref="CSVFieldAttribute"/> applied.</returns>
    ''' <remarks>The return value of this function should cached since which members of <c>t</c> have <see cref="CSVFieldAttribute"/> will not change at runtime and this function is fairly expensive to run.</remarks>
    Public Shared Function GetReadableColumnHeaders(Of t As New)() As IEnumerable(Of CSVColumnHeader)
        Dim ColumnHeaders = New List(Of CSVColumnHeader)

        Dim a = Sub(member As MemberInfo)
                    Dim fieldattribute = (From i As CSVFieldAttribute In member.GetCustomAttributes(GetType(CSVFieldAttribute), False)).FirstOrDefault
                    If fieldattribute IsNot Nothing Then
                        If fieldattribute.Readable Then
                            ColumnHeaders.Add(New CSVColumnHeader With {.CSVName = If(fieldattribute.CSVFieldName, member.Name), .Member = member})
                        End If
                    End If
                End Sub

        GetType(t).GetProperties().ToList.ForEach(a)
        GetType(t).GetFields().ToList.ForEach(a)

        Return ColumnHeaders
    End Function

    Public Shared Function GetWritableColumnHeaders(Of t As New)() As IEnumerable(Of CSVColumnHeader)
        Dim ColumnHeaders = New List(Of CSVColumnHeader)

        Dim a = Sub(member As MemberInfo)
                    Dim fieldattribute = (From i As CSVFieldAttribute In member.GetCustomAttributes(GetType(CSVFieldAttribute), False)).FirstOrDefault
                    If fieldattribute IsNot Nothing Then
                        If fieldattribute.Writeable Then
                            ColumnHeaders.Add(New CSVColumnHeader With {.CSVName = If(fieldattribute.CSVFieldName, member.Name), .Member = member})
                        End If
                    End If
                End Sub

        GetType(t).GetProperties().ToList.ForEach(a)
        GetType(t).GetFields().ToList.ForEach(a)

        Return ColumnHeaders
    End Function

End Structure

Public NotInheritable Class CSVDeserializer(Of t As New)
    Private Shared _CSVColumnHeaders As IEnumerable(Of CSVColumnHeader)

    Shared Sub New()
        _CSVColumnHeaders = CSVColumnHeader.GetReadableColumnHeaders(Of t)
    End Sub

    Private Sub New()
        'this is a static class
    End Sub

    Public Delegate Sub PostBack(Message As String, Progress As Double, Force As Boolean)

    Public Shared ReadOnly Property CSVColumnHeaders As IEnumerable(Of String)
        Get
            Return _CSVColumnHeaders
        End Get
    End Property

    ''' <summary>
    ''' Deserializes an <see cref="IEnumerable(Of t)"/>. The user will be prompted for a CSV file containing the values for each instance of <c>t</c> and will be prompted to specify the field mapping unless.
    ''' </summary>
    ''' <param name="OwnerForm">Any model windows shown as a result of calling this function will be owned by this form.</param>
    ''' <returns>An <see cref="IEnumerable(Of t)"/> of the objects deserialized, or <c>Nothing</c> if the user selects the Cancel button on the file selection window or mapping window, or <c>Nothing</c> if no data could be deserialized.</returns>
    Shared Function Deserialize(pb As PostBack, Optional OwnerForm As Windows.Forms.Form = Nothing) As IEnumerable(Of t)

        '==overall steps==
        '1: show file selection
        '2: set up file stream
        '3: import all data
        '4: show field mapping
        '5: perform field conversion
        '6: instantiate a new t and populate

        'step 1: show file selection
        Dim OpenWindow As New System.Windows.Forms.OpenFileDialog With {
                                        .Filter = "Comma Separated Values|*.csv|All Files|*.*",
                                        .DefaultExt = "CSV",
                                        .ShowReadOnly = False}

        'Kludge: this Deserialize(...) may or may not be called from the UI thread. The variable 'a' below contains code which must be called on the UI thread
        Dim a = Function()
                    If OpenWindow.ShowDialog(OwnerForm) = System.Windows.Forms.DialogResult.OK Then
                        Return Deserialize(New IO.FileInfo(OpenWindow.FileName), pb, OwnerForm)
                    Else
                        Return Enumerable.Empty(Of t)
                    End If
                End Function

        If OwnerForm IsNot Nothing Then
            Return OwnerForm.Invoke(a)
        Else
            Return a
        End If
    End Function

    ''' <summary>
    ''' Deserializes an <see cref="IEnumerable(Of t)"/>. The user will be prompted to specify the field mapping.
    ''' </summary>
    ''' <param name="CSVFile">An <see cref="IO.FileInfo"/> specifying the CSV file to deserialize.</param>
    ''' <param name="OwnerForm">Any model windows shown as a result of calling this function will be owned by this form.</param>
    ''' <returns>An <see cref="IEnumerable(Of t)"/> of the objects deserialized, or <c>Nothing</c> if the user selects the Cancel button on the mapping window, or <c>Nothing</c> if no data could be deserialized.</returns>
    Shared Function Deserialize(CSVFile As IO.FileInfo, pb As PostBack, Optional OwnerForm As Windows.Forms.Form = Nothing) As IEnumerable(Of t)
        'step 2: set up file stream

        Try

            Using s As IO.Stream = CSVFile.Open(IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Inheritable)
                Return Deserialize(s, pb, OwnerForm)
            End Using
        Catch ex As IO.IOException
            MsgBox(String.Format("An exception occure when opening the selected file '{0}': ""{1}""", CSVFile.Name, ex.Message), MsgBoxStyle.Exclamation)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Deserializes an <see cref="IEnumerable(Of t)"/>. The user will be prompted to specify the field mapping.
    ''' </summary>
    ''' <param name="CSVStream">An <see cref="IO.Stream"/> which allows seeking and reading which gives the function access to the CSV data.</param>
    ''' <param name="OwnerForm">Any model windows shown as a result of calling this function will be owned by this form.</param>
    ''' <returns>An <see cref="IEnumerable(Of t)"/> of the objects deserialized, or <c>Nothing</c> if the user selects the Cancel button on the mapping window, or <c>Nothing</c> if no data could be deserialized.</returns>
    Shared Function Deserialize(CSVStream As IO.Stream, pb As PostBack, Optional OwnerForm As Windows.Forms.Form = Nothing) As IEnumerable(Of t)
        'step 3: import all data
        Dim table As List(Of List(Of String))
        table = ParseCSV(CSVStream, pb)

        pb("Waiting on user (CSV field mapping)...", 0.5, True)

        'normalize the data: Since there might be a different number of columns in each row, make them all the same
        Dim MaxColumns = Aggregate i As List(Of String) In table Into Max(i.Count)
        Dim NonConformingRows = (From NonConformingRow In table Where NonConformingRow.Count < MaxColumns).ToList

        For Each row In NonConformingRows
            Dim i = row.Count
            While i < MaxColumns
                row.Add(String.Empty)
                i += 1
            End While
        Next

        'step 4: show field mapping

        '4.1 set up field mapping dialog
        Dim FieldMappingForm As New FFieldMapping With
                                    {
                                        .Text = String.Format("Field Mapping - ({0})", GetType(t).Name),
                                        .CSVData = table,
                                        .ObjectFields = _CSVColumnHeaders.Select(Function(x) x.CSVName).ToList
                                    }

        '4.2 ask the user how to map the fields
        If FieldMappingForm.ShowDialog(OwnerForm) = Windows.Forms.DialogResult.OK Then
            'step 5 perform field conversion:
            'FFieldMapping.CSVData has the raw data before FFieldMapping.ShowDialog is called. After it's called, FFieldMapping.CSVData has the mapped data
            Dim MappedValues = FieldMappingForm.CSVData
            Dim Res As New List(Of t)

            'step 6: instantiate a new t and populate

            'This section works like this:
            'For each row in the data (except the first row which now has headers):
            '   Create a new T.
            '   For Each value in the row, find the ColumnHeader object With a matching CSVName. From that object, we now know which member of T to set. Convert the     value To the primitive data type (see ConvertType) based on the data type. Use reflection to set the value.
            '   Add the created T to a list
            'Once we're done with all of the rows, return the list
            For Each DataRow In MappedValues
                pb("Populating data", ((MappedValues.IndexOf(DataRow) / MappedValues.Count) / 2) + 0.5, False)

                If DataRow IsNot MappedValues(0) Then
                    Dim Record As New t

                    For i = 0 To DataRow.Count - 1

                        Dim h = MappedValues(0)(i)
                        Dim member = (From csvfield In _CSVColumnHeaders Where csvfield.CSVName = h).First.Member

                        Dim v = DataRow(i)

                        If member.MemberType = MemberTypes.Field Then
                            Dim m = DirectCast(member, FieldInfo)

                            m.SetValue(Record, ConvertTypeFromString(v, m.FieldType))

                        ElseIf member.MemberType = MemberTypes.Property Then
                            Dim m = DirectCast(member, PropertyInfo)

                            m.SetValue(Record, ConvertTypeFromString(v, m.PropertyType), Nothing)
                        End If
                    Next
                    Res.Add(Record)
                End If
            Next

            Return Res
        Else
            Return Enumerable.Empty(Of t)
        End If

    End Function

    Private Shared Function ConvertTypeFromString(value As String, toType As System.Type) As Object
        Try
            If value.Trim = String.Empty Then
                Return Nothing
            Else
                Dim t = If(Nullable.GetUnderlyingType(toType), toType)

                Select Case t
                    Case GetType(String)
                        Return value
                    Case GetType(Date)
                        Return New Date?(Date.Parse(value))
                    Case GetType(Single)
                        Return New Single?(Single.Parse(value))
                    Case GetType(Double)
                        Return New Double?(Double.Parse(value))
                    Case GetType(Integer)
                        Return New Integer?(Integer.Parse(value))
                    Case GetType(Long)
                        Return New Long?(Long.Parse(value))
                    Case GetType(ULong)
                        Return New ULong?(ULong.Parse(value))
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If
        Catch ex As FormatException
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Parse CSV data into <see cref="List(Of List(Of String))"/>.
    ''' </summary>
    ''' <param name="CSVStream">A <see cref="IO.Stream"/> which provides CSV data.</param>
    ''' <exception cref="MalformedCSVException">Thrown if text occurs between a , delimiter and a " quote.</exception>
    ''' <exception cref="ArgumentException">Thrown if the stream provided to the function can't seek or read.</exception>
    ''' <returns>A <see cref="List(Of List(Of String))"/> containing the fully parsed CSV file with each element containing one value. Each <see cref="List(Of String)"/> in the <see cref="List(Of List(Of String))"/> represents one row of values from the CSV data. Each <see cref="String"/> in the <see cref="List(Of String)"/> represents an individual value.</returns>
    ''' <remarks>
    ''' Any combination of one or two of either or both CR and LF are considered a newline. Newlines within quotation marks are treated as literals and are inserted into the particular value it appears in as presented. Newlines outside of quotes are stripped and treated as the end of a record.
    ''' Spaces are not trimmed from records. All values are expected to be separated by a comma (,) without a space.
    ''' Paired double quotes ("") are escaped to a single double quote (").
    ''' </remarks>
    Private Shared Function ParseCSV(CSVStream As IO.Stream, pb As PostBack) As List(Of List(Of String))
        Const DELIM = ","c
        Const QUOTE = """"c

        If Not (CSVStream.CanRead) Or Not (CSVStream.CanSeek) Then
            Throw New ArgumentException("CSVStream must be readable and seekable.")
        End If

        Dim Table As New List(Of List(Of String))

        Dim inQuote As Boolean
        Dim CurrentRowNumber As Long
        Dim CurrentColNumber As Long
        Dim CurrentRecord As String = ""
        Dim buffer As String
        Dim ROW As New List(Of String)

        Using sr As New IO.StreamReader(CSVStream)
            buffer = sr.ReadToEnd()
        End Using

        Dim l = buffer.Length
        Dim c As Char

        For i As Long = 0 To l - 1
            c = buffer(i)

            pb("Parsing CSV", (i / l) / 2, False) 'Progress of this method is reported between 0.0 and 0.5 since is the first half of the operation of deserialization

            Select Case c
                Case DELIM
                    If inQuote Then
                        CurrentRecord &= c
                    Else
                        CurrentColNumber += 1 'move one cell to the right

                        ROW.Add(CurrentRecord)

                        CurrentRecord = String.Empty 'reset the CurrentRecord placeholder
                    End If
                Case QUOTE
                    If inQuote Then
                        If i + 2 < l Then 'edge case: exclude checking for "" if the first " is the last character in the buffer
                            If buffer(i + 1) = QUOTE Then 'edge case: "" escaped to " in a quoted string
                                CurrentRecord &= c
                                i += 1 'skip the next iteration
                            ElseIf (buffer(i + 1) <> DELIM) And (buffer(i + 1) <> vbCr(0)) And (buffer(i + 1) <> vbLf(0)) Then 'edge case: text between closing quote and delimiter/EOL
                                Throw New MalformedCSVException(MalformedCSVException.MalformationTypes.TextAfterCloseQuote, CurrentRowNumber, CurrentColNumber)
                            Else
                                inQuote = False
                            End If
                        Else
                            inQuote = False
                        End If
                    Else
                        inQuote = True
                    End If
                Case vbCr(0), vbLf(0)

                    If inQuote Then 'edge case: CR or LF in quoted record to be treated as literal
                        CurrentRecord &= c
                    Else
                        If i + 2 < l Then 'CR or LF appearing alone or paired, not at the end of the file
                            'treat it as the end of the record...
                            ROW.Add(CurrentRecord)
                            CurrentRecord = String.Empty

                            '...and as a new record
                            Table.Add(ROW)
                            ROW = New List(Of String)

                            CurrentColNumber = 0
                            CurrentRowNumber += 1

                            If ((buffer(i + 1) = vbCr(0)) Or (buffer(i + 1) = vbLf(0))) Then
                                'CR or LF paired. This one is the first of the pair so we skip the second one
                                i += 1
                            End If
                        End If
                    End If
                Case Else
                    If i + 2 < l Then 'edge case: check for text between delimiter and open quote
                        If Not inQuote Then
                            If buffer(i + 1) = QUOTE Then
                                'not in a quote, current character is not a delimiter, next character is ".
                                Throw New MalformedCSVException(MalformedCSVException.MalformationTypes.TextBeforeOpenQuote, CurrentRowNumber, CurrentRecord)
                            Else
                                CurrentRecord &= c
                            End If
                        Else
                            CurrentRecord &= c
                        End If
                    Else
                        CurrentRecord &= c
                    End If

            End Select
        Next

        ROW.Add(CurrentRecord)
        Table.Add(ROW)

        Return Table

    End Function

    Public Class MalformedCSVException
        Inherits Exception

        Public ColumnNumber As Long
        Public MalformationType As MalformationTypes
        Public RecordNumber As Long

        Sub New(MalformationType As MalformationTypes, RecordNumber As Long, ColumnNumber As Long)
            MyBase.New(String.Format("CSV is malformed: Type: {0}, Row: {1}, Field: {2}", MalformationType, RecordNumber, ColumnNumber))
            Me.MalformationType = MalformationType
            Me.RecordNumber = RecordNumber
            Me.ColumnNumber = ColumnNumber
        End Sub

        Public Enum MalformationTypes As Byte
            TextBeforeOpenQuote
            TextAfterCloseQuote
        End Enum

    End Class

End Class

''' <summary>
''' Associates a property or field with a specific column name in a CSV file. Used both for reading and writing CSV.
''' </summary>
<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=False, Inherited:=False)>
Public Class CSVFieldAttribute
    Inherits Attribute

    ''' <summary>
    ''' Name of the column this attribute represents. When writing, this value is case-sensitive. When reading, this value is not case-sensitive.
    ''' </summary>
    Public CSVFieldName As String

    ''' <summary>
    ''' If <c>True</c>, the deserializer will look for this field. If <c>False</c>, the deserializer will not look for this field and will ignore it if present in the CSV file. The user will not be allowed to select this field in the mapping windows.
    ''' </summary>
    Public Readable As Boolean

    ''' <summary>
    ''' If <c>True</c>, the serializer will write this member to the CSV file. If <c>False</c>, the serializer will ignore this member
    ''' </summary>
    Public Writeable As Boolean

    Sub New(Optional CSVFieldName As String = Nothing, Optional Readable As Boolean = True, Optional Writeable As Boolean = True)
        Me.CSVFieldName = CSVFieldName

        If (Readable = False) And (Writeable = False) Then
            Throw New ArgumentException("Readable and Writable cannot both be False.")
        End If
        Me.Readable = Readable
        Me.Writeable = Writeable
    End Sub

End Class

Public NotInheritable Class CSVSerializer(Of t As New)
    Private Shared _CSVColumnHeaders As IEnumerable(Of CSVColumnHeader)

    Shared Sub New()
        _CSVColumnHeaders = CSVColumnHeader.GetWritableColumnHeaders(Of t)
    End Sub

    Private Sub New()
        'this is a static class
    End Sub

    Public Shared ReadOnly Property CSVColumnHeaders As IEnumerable(Of String)
        Get
            Return _CSVColumnHeaders
        End Get
    End Property

    Shared Function Serialize(objects As IEnumerable(Of t)) As String

        'set up result StringBuilder
        'add column headers to CSV result
        'cycle through all the objects in the collection
        '   cycle through all the fields in _CSVColumnHeaders
        '       use reflection to pull the value
        '       convert to string
        '       append to current row
        '   append CRLF and most recent row to end of CSV result
        'return CSV result

        Dim csvResult As New Text.StringBuilder
        Dim csvRow As New Text.StringBuilder

        Dim objList = objects.ToList

        For Each Header In _CSVColumnHeaders
            csvRow.Append(AddQuotesIfNeeded(Header.CSVName))
            If Not _CSVColumnHeaders.Last.CSVName = Header.CSVName Then
                csvRow.Append(",")
            End If
        Next

        csvResult.Append(csvRow)
        csvRow.Clear()

        For Each i In objList
            csvResult.Append(vbCrLf)

            For Each Header In _CSVColumnHeaders
                If Header.Member.MemberType = MemberTypes.Field Then
                    Dim m = DirectCast(Header.Member, FieldInfo)

                    csvRow.Append(AddQuotesIfNeeded(m.GetValue(i)))
                ElseIf Header.Member.MemberType = MemberTypes.Property Then
                    Dim m = DirectCast(Header.Member, PropertyInfo)

                    csvRow.Append(AddQuotesIfNeeded(m.GetValue(i, Nothing)))
                End If

                If Not _CSVColumnHeaders.Last.CSVName = Header.CSVName Then
                    csvRow.Append(",")
                End If
            Next

            csvResult.Append(csvRow)
            csvRow.Clear()
        Next

        Dim result = csvResult.ToString

        Return result

    End Function

    ''' <summary>
    ''' Adds double-quotation marks to the beginning and end of a string only as needed in compliance with RFC 4180.
    ''' </summary>
    ''' <param name="value">String to add double-quotation marks to, if needed.</param>
    ''' <returns>A string which may or may not have double-quotation marks at the beginning and the end.</returns>
    ''' <remarks>As implemented, this function is compliant with RFC 4180. Specifically, if <c>value</c> contains one or more of a double-quotation mark ("), comma (,), carriage return, or linefeed, the return value will be <c>value</c> with double-quotation marks appended to both the beginning and the end.</remarks>
    Private Shared Function AddQuotesIfNeeded(value As String) As String
        If value IsNot Nothing Then
            If value.Intersect({""""c, ","c, vbCr(0), vbLf(0)}).Count > 0 Then 'this uses LINQ to intersect the array of Char with an array of Char containing a double quote, a comma, a CR, and LF. If the count is > 0, this means at least one was found and the value needs to be fully quoted.
                Return """" & value & """"
            Else
                Return value
            End If
        Else
            Return String.Empty
        End If
    End Function

End Class