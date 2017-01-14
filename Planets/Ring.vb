Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System
Imports System.Runtime.InteropServices

Public Class Ring
    Dim X, Y, Z, rad, gblRings, gblSectors As Double
    Dim rotx, roty, rotz As Double
    Dim vl, nl, tl As List(Of Double)
    Dim il As List(Of Integer)
    Dim vert() As Double
    Dim normal() As Double
    Dim texcoords() As Double
    Dim indices() As Integer

    Sub New()
        redraw(1, 15, 32)
    End Sub

    ' //////////////////////////////////////////////////////////////////////////////////////
    Sub New(radius As Double, rings As Double, sectors As Double)
        redraw(radius, rings, sectors)
    End Sub

    ' //////////////////////////////////////////////////////////////////////////////////////
    Sub redraw(radius As Double, rings As Double, sectors As Double)
        Dim R, S As Double
        Dim x, y, z As Double
        rotx = roty = rotz = 0                ' Default rotation For sphears
        rad = radius
        x = y = z = 0
        gblRings = rings
        gblSectors = sectors
        R = 1.0 / CDbl(rings - 1)
        S = 1.0 / CDbl(sectors - 1)

        vl = New List(Of Double)()
        nl = New List(Of Double)()
        tl = New List(Of Double)()
        il = New List(Of Integer)()

        For i As Integer = 5 To 6
            For u As Integer = 0 To (sectors - 1)
                y = 0 'Math.Sin(-MathHelper.PiOver2 + MathHelper.Pi * CDbl(i) * R)
                x = Math.Sin(MathHelper.TwoPi * CDbl(u) * S) * Math.Sin(MathHelper.Pi * CDbl(i) * R)
                z = Math.Cos(MathHelper.TwoPi * CDbl(u) * S) * Math.Sin(MathHelper.Pi * CDbl(i) * R)

                tl.Add(u * S)
                tl.Add((i - 5) * R)

                vl.Add(x * radius) 'yxz
                vl.Add(y * radius)
                vl.Add(z * radius)

                nl.Add(x)
                nl.Add(y)
                nl.Add(z)
            Next
        Next

        For i As Integer = 5 To 5
            For u As Integer = 0 To (sectors - 2)
                il.Add((i - 5) * sectors + u)
                il.Add((i - 5) * sectors + (u + 1))
                il.Add(((i + 1) - 5) * sectors + (u + 1))
                il.Add(((i + 1) - 5) * sectors + u)
            Next
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
    Function getRad() As Double
        Return rad
    End Function

    ' //////////////////////////////////////////////////////////////////////////////////////
    Sub resize(nrad As Double)
        redraw(nrad, gblRings, gblSectors)
    End Sub
End Class
