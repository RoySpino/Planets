Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System
Imports System.Runtime.InteropServices

Public Class GLPrint
    ' /////////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Class tp
        Public xul, xur, xll, xlr As Double
        Public yul, yur, yll, ylr As Double

        Sub New(xa, ya, xb, yb, xc, yc, xd, yd)
            xul = xa
            yul = ya

            xur = xb
            yur = yb

            xll = xc
            yll = yc

            xlr = xd
            ylr = yd
        End Sub
    End Class
    ' /////////////////////////////////////////////////////////////////////////////////////////////////////////
    Dim X, Y, Z, size, gblRings, gblSectors As Double
    Dim localDrawString As String
    Dim rotx, roty, rotz As Double
    Dim vl, nl, tl As List(Of Double)
    Dim il As List(Of Integer)
    Dim txtcordList As List(Of tp)
    Dim vert() As Double
    Dim normal(3) As Double
    Dim indices(1), lines, increment As Integer
    Dim texcoords(8) As Double

    Public Sub New(Optional size As Integer = 1)
        Dim modVal, intDivVal As Double
        Me.size = size / 2
        txtcordList = New List(Of tp)()
        localDrawString = Nothing

        ' generate pre-made texture corordinate map
        For i As Integer = 0 To (1023)
            ' x cord calculation]
            modVal = 0.0625 * CDbl(i Mod 16)

            ' y cord calculation
            intDivVal = 0.0625 * Math.Floor(i / 16)


            txtcordList.Add(New tp(
                            1 - modVal,
                            (1 - (intDivVal + 0.0625)),
                            1 - (modVal + 0.0625),
                            (1 - (intDivVal + 0.0625)),
                            1 - (modVal + 0.0625),
                            (1 - intDivVal),
                            1 - modVal,
                            (1 - intDivVal)))
        Next

        il = New List(Of Integer)()
        tl = New List(Of Double)()
        vl = New List(Of Double)()
        nl = New List(Of Double)()
    End Sub

    ' /////////////////////////////////////////////////////////////////////////////////////////////////////////
    Sub print(lx As Double, ly As Double, data As String)
        ' on init or new text given redraw mesh
        If localDrawString <> data And data <> Nothing Then
            localDrawString = data
            tl.Clear()
            vl.Clear()
            nl.Clear()
            il.Clear()
            lines = 0
            increment = 0

            ' redraw text quad strip
            For i As Integer = 0 To (data.Length - 1)
                rectangle(data(i))

                il.Add(i * 4)
                il.Add((i * 4) + 1)
                il.Add((i * 4) + 2)
                il.Add((i * 4) + 3)
            Next

            texcoords = tl.ToArray()
            vert = vl.ToArray()
            normal = nl.ToArray()
            indices = il.ToArray()
        End If

        '========================================================================================
        ' draw to screen
        GL.Disable(EnableCap.Lighting)
        GL.PushMatrix()

        GL.Translate(lx, ly, 0)
        GL.Rotate(rotx, 1, 0, 0)
        GL.Rotate(roty, 0, 1, 0)
        GL.Rotate(rotz, 0, 0, 1)

        GL.EnableClientState(ArrayCap.VertexArray)
        GL.EnableClientState(ArrayCap.NormalArray)
        GL.EnableClientState(ArrayCap.TextureCoordArray)

        GL.VertexPointer(3, VertexPointerType.Double, 0, vert)
        GL.NormalPointer(NormalPointerType.Double, 0, normal)
        GL.TexCoordPointer(2, TexCoordPointerType.Double, 0, texcoords)
        GL.DrawElements(PrimitiveType.Quads, indices.Length, DrawElementsType.UnsignedInt, indices)

        GL.DisableClientState(ArrayCap.VertexArray)
        GL.DisableClientState(ArrayCap.NormalArray)
        GL.DisableClientState(ArrayCap.TextureCoordArray)

        GL.PopMatrix()
        GL.Enable(EnableCap.Lighting)
    End Sub

    ' /////////////////////////////////////////////////////////////////////////////////////////////////////////
    Sub rectangle(str As String)
        Dim index As Integer = Asc(str)

        ' add new lines do not print
        Select Case index
            Case 13
                Return
            Case 10
                lines -= Me.size * 2.1
                increment = 0
                Return
        End Select

        increment += 1
        vl.Add(size * increment)
        vl.Add(0 + lines)
        vl.Add(0)
        tl.Add(txtcordList(index).xul)
        tl.Add(txtcordList(index).yul)
        'GL.Vertex3((size * increment), 0.0F, 0.0F)
        'GL.TexCoord2(txtcordList(index).xul, txtcordList(index).yul)

        vl.Add(size + (size * increment))
        vl.Add(0 + lines)
        vl.Add(0)
        tl.Add(txtcordList(index).xur)
        tl.Add(txtcordList(index).yur)
        'GL.Vertex3(size + (size * increment), 0.0F, 0.0F)
        'GL.TexCoord2(txtcordList(index).xur, txtcordList(index).yur)

        vl.Add(size + (size * increment))
        vl.Add((size * 2) + lines)
        vl.Add(0)
        tl.Add(txtcordList(index).xll)
        tl.Add(txtcordList(index).yll)
        'GL.Vertex3(size + (size * increment), (size * 2), 0.0F)
        'GL.TexCoord2(txtcordList(index).xll, txtcordList(index).yll)

        vl.Add((size * increment))
        vl.Add((size * 2) + lines)
        vl.Add(0)
        tl.Add(txtcordList(index).xlr)
        tl.Add(txtcordList(index).ylr)
        'GL.Vertex3((size * increment), (size * 2), 0.0F)
        'GL.TexCoord2(txtcordList(index).xlr, txtcordList(index).ylr)

        nl.Add(0)
        nl.Add(0)
        nl.Add(1)
    End Sub

End Class