Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System
Imports System.Runtime.InteropServices

Public Class Plane
    Dim X, Y, Z, rad, gblRings, gblSectors As Double
    Dim rotx, roty, rotz As Double
    Dim vl, nl, tl As List(Of Double)
    Dim il As List(Of Integer)
    Dim vert() As Double
    Dim normal() As Double
    Dim texcoords() As Double
    Dim indices() As Integer

    Sub New(Optional height As Double = 1, Optional width As Double = 1)
        redraw(height, width)
    End Sub

    ' //////////////////////////////////////////////////////////////////////////////////////
    Sub redraw(height As Double, width As Double)
        rotx = roty = rotz = 0                ' Default rotation For sphears

        vl = New List(Of Double)()
        nl = New List(Of Double)()
        tl = New List(Of Double)()
        il = New List(Of Integer)()

        vl.Add(0)
        vl.Add(0)
        vl.Add(0)
        vl.Add(0)
        vl.Add(height)
        vl.Add(0)
        vl.Add(width)
        vl.Add(height)
        vl.Add(0)
        vl.Add(width)
        vl.Add(0)
        vl.Add(0)

        tl.Add(0)
        tl.Add(0)
        tl.Add(0)
        tl.Add(1)
        tl.Add(1)
        tl.Add(1)
        tl.Add(1)
        tl.Add(0)

        For i As Integer = 0 To 3
            nl.Add(0)
            nl.Add(0)
            nl.Add(1)
        Next

        For i As Integer = 0 To 7
            il.Add(i)
        Next

        texcoords = tl.ToArray()
        vert = vl.ToArray()
        normal = nl.ToArray()
        indices = il.ToArray()

        vl.Clear()
        nl.Clear()
        tl.Clear()
        il.Clear()
    End Sub

    ' //////////////////////////////////////////////////////////////////////////////////////
    Sub draw(x As Double, y As Double, z As Double)
        'GL.MatrixMode(MatrixMode.Modelview)

        GL.PushMatrix()

        ' change position
        GL.Translate(x, y, z)
        Me.X = x
        Me.Y = y
        Me.Z = z

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
    End Sub

    ' //////////////////////////////////////////////////////////////////////////////////////
    Sub rot(x As Double, y As Double, z As Double)
        rotx = x
        roty = y
        rotz = z
    End Sub

    ' //////////////////////////////////////////////////////////////////////////////////////
    Sub loc(x As Double, y As Double, z As Double)
        Me.X = x
        Me.Y = y
        Me.Z = z
    End Sub

    ' //////////////////////////////////////////////////////////////////////////////////////
    Function getX() As Double
        Return X
    End Function

    ' //////////////////////////////////////////////////////////////////////////////////////
    Function getY() As Double
        Return Y
    End Function

    ' //////////////////////////////////////////////////////////////////////////////////////
    Function getZ() As Double
        Return Z
    End Function

    ' //////////////////////////////////////////////////////////////////////////////////////
    Sub resize(nheight As Double, nwidth As Double)
        redraw(nheight, nwidth)
    End Sub
End Class