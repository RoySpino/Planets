Imports OpenTK.Graphics.OpenGL
Imports OpenTK
Imports System
Imports System.Drawing

Public Class Render
    Inherits GameWindow
    Dim xres As Double
    Dim yres As Double
    Dim mat_spec = New Integer() {255, 255, 255, 255}
    Dim mat_shini = New Integer() {50}
    Dim ambiant = New Integer() {37, 0, 147}
    Dim light_Pos = New Integer() {1, 1, 1, 0}


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
        Dim matPersp As Matrix4
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

        matPersp = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (xres / yres), 0.1, 900.0)
        GL.LoadMatrix(matPersp)
        GL.Material(MaterialFace.Front, MaterialParameter.Specular, mat_spec)
        GL.Material(MaterialFace.Front, MaterialParameter.Shininess, mat_shini)

        GL.Light(LightName.Light0, LightParameter.Position, light_Pos)
        GL.Light(LightName.Light0, LightParameter.Ambient, ambiant)

        GL.MatrixMode(MatrixMode.Modelview)
        GL.Enable(EnableCap.Lighting)
        GL.Enable(EnableCap.Light0)
    End Sub

    Sub set2D()
        GL.Disable(EnableCap.DepthTest)
        GL.Viewport(0, 0, xres, yres)
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()
        GL.Ortho(0, xres, 0, yres, -1, 1)
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()
    End Sub
End Class
