Imports OpenTK.Graphics.OpenGL
Imports OpenTK
Imports System
Imports System.Drawing

Public Class Render
    Inherits GameWindow
    Dim xres As Double
    Dim yres As Double
    Dim mat_spec = New Double() {1.0, 1.0, 1.0, 1.0}
    Dim mat_shini = New Double() {50.0}
    Dim ambiant = New Double() {0.1484, 0.0, 0.5781}
    Dim light_Pos = New Double() {1, 1, 1, .0}


    Public Sub New(x As Integer, y As Integer)
        MyBase.New(x, y)
        xres = x
        yres = y
    End Sub

    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)
    End Sub

    Protected Overrides Sub OnUpdateFrame(e As FrameEventArgs)
        MyBase.OnUpdateFrame(e)
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnRenderFrame(e As FrameEventArgs)
        MyBase.OnRenderFrame(e)
        init()
    End Sub


    Private Sub init()
        GL.Viewport(0, 0, xres, yres)
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()

        GL.Enable(EnableCap.Texture2D)
        GL.Enable(EnableCap.Blend)
        GL.ClearColor(Color.Black)
        GL.ClearDepth(1.0)
        GL.DepthFunc(DepthFunction.Lequal)
        GL.ShadeModel(ShadingModel.Smooth)
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()

        Matrix4.CreatePerspectiveFieldOfView(45.0, xres / yres, 0.1, 900.0)
        GL.Material(MaterialFace.Front, MaterialParameter.Specular, mat_spec)
        GL.Material(MaterialFace.Front, MaterialParameter.Shininess, mat_shini)

        GL.Light(LightName.Light0, LightParameter.Position, light_Pos)
        GL.Light(LightName.Light0, LightParameter.Ambient, ambiant)

        GL.MatrixMode(MatrixMode.Modelview)
        GL.Enable(EnableCap.Lighting)
        GL.Enable(EnableCap.Light0)
    End Sub
End Class
