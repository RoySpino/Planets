Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports OpenTK.Audio.OpenAL
Imports System.Drawing
Imports OpenTK.Input

Public Class Render
    Inherits GameWindow

    Private rtri As Double = 0
    Private rquad As Double = 0
    Dim mat_spec = New Integer() {255, 255, 255, 255}
    Dim mat_shini = New Integer() {50}
    Dim ambiant = New Integer() {37, 0, 147}
    Dim light_Pos = New Integer() {1, 1, 1, 0}
    Dim speed As Double = 0.005

    Dim plan As List(Of SolidSphere)

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub New(w As Integer, h As Integer)
        MyBase.New(w, h)
        Me.X = 100
        Me.Y = 100

        GL.ShadeModel(ShadingModel.Smooth)                               ' enable smooth shading
        GL.ClearColor(0.0F, 0.0F, 0.0F, 0.5F)                            ' black background
        GL.ClearDepth(1.0F)                                              ' depth buffer setup
        GL.Enable(EnableCap.DepthTest)                                   ' enables depth testing
        GL.DepthFunc(DepthFunction.Lequal)                               ' type Of depth test
        GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest)   ' nice perspective calculations

        plan = New List(Of SolidSphere)()
        plan.Add(New SolidSphere(3, 16, 32))
        plan.Add(New SolidSphere(0.3829, 16, 32))
        plan.Add(New SolidSphere(0.9499, 16, 32))
        plan.Add(New SolidSphere(1, 16, 32))
        plan.Add(New SolidSphere(0.533, 16, 32))
        plan.Add(New SolidSphere(2.5, 16, 32))
        plan.Add(New SolidSphere(2.4, 16, 32))
        plan.Add(New SolidSphere(1.4, 16, 32))
        plan.Add(New SolidSphere(1.1, 16, 32))
        plan.Add(New SolidSphere(0.1, 16, 32))

    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        'GL.LoadIdentity()
        GL.MatrixMode(MatrixMode.Projection)

        GL.LoadMatrix(Matrix4.Perspective(45.0F, CDbl(Me.Width) / CDbl(Me.Height), 0.1F, 5000.0F))
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()
    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Protected Overrides Sub OnUpdateFrame(e As FrameEventArgs)
        MyBase.OnUpdateFrame(e)

    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)

        GL.Viewport(ClientRectangle)
        GL.LoadMatrix(Matrix4.Perspective(45.0F, CDbl(Me.Width) / CDbl(Me.Height), 0.1F, 5000.0F))
        GL.MatrixMode(MatrixMode.Modelview)
    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Protected Overrides Sub OnKeyDown(e As KeyboardKeyEventArgs)
        MyBase.OnKeyDown(e)

        Select Case e.Key
            Case OpenTK.Input.Key.Escape
                Me.Exit()
            Case OpenTK.Input.Key.Plus
                speed += 0.01
            Case OpenTK.Input.Key.Minus
                speed -= 0.01
        End Select
    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Protected Overrides Sub OnRenderFrame(e As FrameEventArgs)
        MyBase.OnRenderFrame(e)

        GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)
        GL.ClearColor(Color.Black)


        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()

        GL.Translate(0.0F, 0.0F, -100.0F)

        ' sun
        GL.Color3(1.0F, 0.0F, 0.0F)
        plan(0).rot(0, rquad, 0)
        plan(0).draw(0, 0, 0)

        ' mercury
        GL.Color3(1.0F, 1.0F, 0.0F)
        plan(1).rot(0, (rquad), 0)
        plan(1).draw(6.3 * Math.Sin(rquad * 4.150568), 6.3 * Math.Cos(rquad * 4.150568), 0)

        ' venus
        GL.Color3(8.0F, 0.3F, 0.5F)
        plan(2).rot(0, (rquad), 0)
        plan(2).draw(8 * Math.Sin(rquad * 1.6254934), 8 * Math.Cos(rquad * 1.6254934), 0)

        ' earth
        GL.Color3(0.0F, 0.0F, 0.7F)
        plan(3).rot(0, rquad, 0)
        plan(3).draw(11 * Math.Sin(rquad), 11 * Math.Cos(rquad), 0)
        rquad += 0.02

        ' mars
        GL.Color3(1.0F, 0.0F, 0.1F)
        plan(4).rot(0, rquad, 0)
        plan(4).draw(13 * Math.Sin(rquad * 0.5316818), 13 * Math.Cos(rquad * 0.5316818), 0)

        ' jupiter
        GL.Color3(1.0F, 0.0F, 0.9F)
        plan(5).rot(0, rquad, 0)
        plan(5).draw(17 * Math.Sin(rquad * 0.08430292), 17 * Math.Cos(rquad * 0.08430292), 0)

        ' saturn
        GL.Color3(1.0F, 1.0F, 0.1F)
        plan(6).rot(0, rquad, 0)
        plan(6).draw(30 * Math.Sin(rquad * 0.0338344), 30 * Math.Cos(rquad * 0.0338344), 0)

        ' uranus
        GL.Color3(0.1F, 0.1F, 0.5F)
        plan(7).rot(0, rquad, 0)
        plan(7).draw(40 * Math.Sin(rquad * 0.011901), 40 * Math.Cos(rquad * 0.011901), 0)

        ' neptune
        GL.Color3(0.2F, 0.3F, 0.1F)
        plan(8).rot(0, rquad, 0)
        plan(8).draw(50 * Math.Sin(rquad * 0.006069), 50 * Math.Cos(rquad * 0.006069), 0)

        ' pluto
        GL.Color3(0.3F, 0.3F, 0.3F)
        plan(9).rot(0, rquad, 0)
        plan(9).draw(60 * Math.Sin(rquad * 0.004033), 60 * Math.Cos(rquad * 0.004033), 0)


        rquad += speed
        Me.SwapBuffers()
    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub init()
        Dim matPersp As Matrix4
        GL.Viewport(0, 0, Me.Width, Me.Height)
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

        matPersp = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (Me.Width / Me.Height), 0.1, 900.0)
        GL.LoadMatrix(matPersp)
        GL.Material(MaterialFace.Front, MaterialParameter.Specular, mat_spec)
        GL.Material(MaterialFace.Front, MaterialParameter.Shininess, mat_shini)

        GL.Light(LightName.Light0, LightParameter.Position, light_Pos)
        GL.Light(LightName.Light0, LightParameter.Ambient, ambiant)

        GL.MatrixMode(MatrixMode.Modelview)
        GL.Enable(EnableCap.Lighting)
        GL.Enable(EnableCap.Light0)
    End Sub
End Class
