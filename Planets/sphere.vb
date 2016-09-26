Imports OpenTK.Graphics.OpenGL
Imports OpenTK
Imports System
Imports System.Drawing


Public Class sphere
    Dim rad As Double
    Dim vert As List(Of Double) = New List(Of Double)()
    Dim normal As List(Of Double) = New List(Of Double)()
    Dim texcoords As List(Of Double) = New List(Of Double)()
    Dim indices As List(Of Integer) = New List(Of Integer)()
    Dim rx, ry, rz As Double
    Dim lx, ly, lz As Double

    Public Sub New()
        redraw(1, 16, 32)
    End Sub

    Public Sub New(radius As Double, rings As Integer, sectors As Integer)
        redraw(radius, rings, sectors)
    End Sub

    Private Sub redraw(radius As Double, rings_ As Integer, sectors_ As Integer)
        rad = radius
        Dim RINGS, SECTORS As Integer
        Dim r, s, y, x, z As Double
        r = 1.0 / (RINGS - 1.0)
        s = 1.0 / (SECTORS - 1.0)

        vert.Clear()
        normal.Clear()
        texcoords.Clear()

        RINGS = rings_
        SECTORS = sectors_


        For i As Integer = 0 To (RINGS - 1)
            For u As Integer = 0 To (SECTORS - 1)
                y = Math.Sin((Math.PI * 2) - (Math.PI * 2) + Math.PI * i * r)
                x = Math.Cos(2 * Math.PI * u * s) * Math.Sin(Math.PI * i * r)
                z = Math.Sin(2 * Math.PI * u * s) * Math.Sin(Math.PI * i * r)
                texcoords.Add(u * s)
                texcoords.Add(i * r)

                vert.Add(x * radius)
                vert.Add(y * radius)
                vert.Add(z * radius)

                normal.Add(x)
                normal.Add(y)
                normal.Add(z)
            Next
        Next

        indices.Clear()
        For i As Integer = 0 To (RINGS - 2)
            For u As Integer = 0 To (SECTORS - 1)
                indices.Add(i * SECTORS + u)
                indices.Add(i * SECTORS + (u + 1))
                indices.Add((i + 1) * SECTORS + (u + 1))
                indices.Add((i + 1) * SECTORS + u)
            Next
        Next
    End Sub

    Public Sub draw()
        GL.MatrixMode(MatrixMode.Modelview)
        GL.PushMatrix()

        ' change position
        GL.Translate(lx, ly, lz)

        GL.Rotate(rx, 1, 0, 0)
        GL.Rotate(ry, 0, 1, 0)
        GL.Rotate(rz, 0, 0, 1)

        GL.EnableClientState(ArrayCap.VertexArray)
        GL.EnableClientState(ArrayCap.NormalArray)
        GL.EnableClientState(ArrayCap.TextureCoordArray)

        GL.VertexPointer(3, VertexPointerType.Double, 0, vert.ToArray())
        GL.NormalPointer(NormalPointerType.Double, 0, normal.ToArray())
        GL.TexCoordPointer(2, TexCoordPointerType.Double, 0, texcoords.ToArray())
        GL.DrawElements(BeginMode.Triangles, indices.Count, DrawElementsType.UnsignedInt, indices.ToArray())
        GL.PopMatrix()
    End Sub

    Public Sub rot(x As Double, y As Double, z As Double)
        rx = x
        ry = y
        rz = z
    End Sub

    Public Sub loc(x As Double, y As Double, z As Double)
        lx = x
        ly = y
        lz = z
    End Sub
End Class
