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
    Dim daySpeed As Double = 0.5
    Dim planID, prevID As Integer
    Dim CamAnimationFrame(3) As Double
    Dim calculon As Calculate = New Calculate()
    Dim Texture As List(Of Int32)
    Dim getBMP As LoadBMP = New LoadBMP()
    Dim framCount As Integer
    Dim doAnimation, planetToPlanet As Boolean
    Dim plutox, plutoY As Double
    Dim camloc(3) As Double
    Dim age, wei As Double

    Dim plan As List(Of SolidSphere)
    Dim rings As List(Of Ring)

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

        camloc(2) = -100
        planID = -1

        Texture = New List(Of Integer)()
        plan = New List(Of SolidSphere)()
        plan.Add(New SolidSphere(10, 16, 32))
        plan.Add(New SolidSphere(0.3829, 16, 32))
        plan.Add(New SolidSphere(0.9499, 16, 32))
        plan.Add(New SolidSphere(1, 16, 32))
        plan.Add(New SolidSphere(0.533, 16, 32))
        plan.Add(New SolidSphere(5.2, 16, 32))
        plan.Add(New SolidSphere(4.2, 16, 32))
        plan.Add(New SolidSphere(3.2, 16, 32))
        plan.Add(New SolidSphere(3, 16, 32))
        plan.Add(New SolidSphere(0.2, 16, 32))
        plan.Add(New SolidSphere(0.23, 16, 32))
        plan.Add(New SolidSphere(0.1, 8, 16))
        plan.Add(New SolidSphere(0.1, 4, 8))
        plan.Add(New SolidSphere(0.08, 4, 8))

        rings = New List(Of Ring)()
        rings.Add(New Ring(6.4, 16, 32))
        rings.Add(New Ring(7.4, 16, 32))
        rings.Add(New Ring(5.9, 16, 32))
        rings.Add(New Ring(5.5, 16, 32))

        Texture.Add(getBMP.load("D:\Pictures\sun.png"))
        Texture.Add(getBMP.load("D:\Pictures\Mercury.png"))
        Texture.Add(getBMP.load("D:\Pictures\Venus.png"))
        Texture.Add(getBMP.load("D:\Pictures\Earth.png"))
        Texture.Add(getBMP.load("D:\Pictures\Mars.png"))
        Texture.Add(getBMP.load("D:\Pictures\Jupiter.png"))
        Texture.Add(getBMP.load("D:\Pictures\Saturn.png"))
        Texture.Add(getBMP.load("D:\Pictures\Uranus.png"))
        Texture.Add(getBMP.load("D:\Pictures\Neptune.png"))
        Texture.Add(getBMP.load("D:\Pictures\Pluto.png"))
        Texture.Add(getBMP.load("D:\Pictures\Moon.png"))
        Texture.Add(getBMP.load("D:\Pictures\Charon.png"))
        Texture.Add(getBMP.load("D:\Pictures\Phobos.png"))

        init()
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

        ' prevents reassignment after animation startup
        If doAnimation = False Then
            prevID = planID
        End If

        Select Case e.Key
            Case OpenTK.Input.Key.Escape
                Me.Exit()
            Case OpenTK.Input.Key.Q
                If speed - 0.01 > 2 Then
                    speed += 0.01
                    daySpeed += 0.01
                Else
                    speed += 0.01
                    daySpeed += 0.01
                End If
            Case OpenTK.Input.Key.A
                If speed - 0.01 > 0 Then
                    speed -= 0.01
                    daySpeed -= 0.01
                Else
                    speed -= 0.0001
                    daySpeed -= 0.0001
                End If
            Case OpenTK.Input.Key.Plus
                camloc(2) += 1
            Case OpenTK.Input.Key.Minus
                camloc(2) -= 1

            Case OpenTK.Input.Key.Number1
                calculon.calc(0, wei, age)
                planID = 1
                'camloc(2) = plan(1).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.Number2
                calculon.calc(0, wei, age)
                planID = 2
                'camloc(2) = plan(2).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.Number3
                calculon.calc(0, wei, age)
                planID = 3
                'camloc(2) = plan(3).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.Number4
                calculon.calc(0, wei, age)
                planID = 4
                'camloc(2) = plan(4).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.Number5
                calculon.calc(0, wei, age)
                planID = 5
                'camloc(2) = plan(5).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.Number6
                calculon.calc(0, wei, age)
                planID = 6
                'camloc(2) = plan(6).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.Number7
                calculon.calc(0, wei, age)
                planID = 7
                'camloc(2) = plan(7).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.Number8
                calculon.calc(0, wei, age)
                planID = 8
                'camloc(2) = plan(8).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.Number9
                calculon.calc(0, wei, age)
                planID = 9
                'camloc(2) = plan(9).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.L
                calculon.calc(0, wei, age)
                planID = 10
                'camloc(2) = plan(10).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.K
                calculon.calc(0, wei, age)
                planID = 11
                'camloc(2) = plan(10).getRad() * -5
                getFramInc()
                doAnimation = True
            Case OpenTK.Input.Key.B
                planID = -1
                getFramInc()
                doAnimation = True
        End Select

        If planID >= 0 And prevID >= 0 Then
            planetToPlanet = True
            getFramInc()
        End If
    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Protected Overrides Sub OnRenderFrame(e As FrameEventArgs)
        MyBase.OnRenderFrame(e)

        GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)
        GL.ClearColor(Color.Black)


        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()

        GL.Translate(camloc(0), camloc(1), camloc(2))

        plutox = (190 * Math.Sin(rquad * 0.004033))
        plutoY = (190 * Math.Cos(rquad * 0.004033))

        ' sun
        'GL.Color3(1.0F, 1.0F, 0.1F)
        GL.Disable(EnableCap.Lighting)
        GL.BindTexture(TextureTarget.Texture2D, Texture(0))
        plan(0).draw(0, 0, 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)
        GL.Enable(EnableCap.Lighting)

        ' mercury
        'GL.Color3(1.0F, 1.0F, 0.0F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(1))
        plan(1).rot(0, (rtri * 0.0170502), 0)
        plan(1).draw(30 * Math.Sin(rquad * 4.150568), 30 * Math.Cos(rquad * 4.150568), 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' venus
        'GL.Color3(8.0F, 0.3F, 0.5F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(2))
        plan(2).rot(0, (rtri * -0.004115), 0)
        plan(2).draw(35 * Math.Sin(rquad * 1.6254934), 35 * Math.Cos(rquad * 1.6254934), 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' earth
        'GL.Color3(0.0F, 0.0F, 0.7F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(3))
        plan(3).rot(0, rtri, 0)
        plan(3).draw(40 * Math.Sin(rquad), 40 * Math.Cos(rquad), 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' mars
        'GL.Color3(1.0F, 0.0F, 0.1F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(4))
        plan(4).rot(0, (rtri * 1.03), 0)
        plan(4).draw(50 * Math.Sin(rquad * 0.5316818), 50 * Math.Cos(rquad * 0.5316818), 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' jupiter
        'GL.Color3(1.0F, 0.0F, 0.9F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(5))
        plan(5).rot(0, (rtri * 2.44897), 0)
        plan(5).draw(70 * Math.Sin(rquad * 0.08430292), 70 * Math.Cos(rquad * 0.08430292), 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' saturn
        'GL.Color3(0.5F, 0.5F, 0.01F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(6))
        plan(6).rot(0, (rtri * 2.39294), 0)
        plan(6).draw(110 * Math.Sin(rquad * 0.0338344), 110 * Math.Cos(rquad * 0.0338344), 0)
        rings(0).rot(13, 0, 13)
        rings(0).draw(plan(6).getX, plan(6).getY, plan(6).getZ)
        rings(1).rot(13, 0, 13)
        rings(1).draw(plan(6).getX, plan(6).getY, plan(6).getZ)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' uranus
        'GL.Color3(0.1F, 0.1F, 0.5F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(7))
        plan(7).rot((rtri * 1.340782), 0, 90)
        plan(7).draw(130 * Math.Sin(rquad * 0.011901), 130 * Math.Cos(rquad * 0.011901), 0)
        rings(2).rot(0, 20, 90)
        rings(2).draw(plan(7).getX(), plan(7).getY(), plan(7).getZ())
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' neptune
        'GL.Color3(0.2F, 0.3F, 0.1F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(8))
        plan(8).rot(0, (rtri * 1.256544), 0)
        plan(8).draw(160 * Math.Sin(rquad * 0.006069), 160 * Math.Cos(rquad * 0.006069), 0)

        rings(3).rot(13, 0, 0)
        rings(3).draw(plan(8).getX, plan(8).getY, plan(8).getZ)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' pluto
        'GL.Color3(0.3F, 0.3F, 0.3F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(9))
        plan(9).rot(0, (rtri * 0.1443), 0)
        plan(9).draw(0.17 * Math.Sin(rquad * 13.3693265) + plutox, 0.17 * Math.Cos(rquad * 13.3693265) + plutoY, 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' luna
        'GL.Color3(0.3F, 0.3F, 0.3F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(10))
        plan(10).rot(0, (rtri * 0.064887), 0)
        plan(10).draw(2 * Math.Sin(rquad * 13.3693265) + plan(3).getX(), 2 * Math.Cos(rquad * 13.3693265) + plan(3).getY(), 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' charon
        'GL.Color3(0.3F, 0.3F, 0.3F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(11))
        plan(11).rot(0, (rtri * 0.1443), 0)
        plan(11).draw(0.75 * Math.Cos(rquad * 13.3693265) + plutox, -0.75 * Math.Sin(rquad * 13.3693265) + plutoY, 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' phobos
        'GL.Color3(0.3F, 0.3F, 0.3F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(12))
        plan(12).rot(0, (rtri * 0.1443), 0)
        plan(12).draw(0.75 * Math.Cos(rquad * 2154.86) + plan(4).getX(), -0.75 * Math.Sin(rquad * 2154.86) + plan(4).getY, 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        ' demos
        'GL.Color3(0.3F, 0.3F, 0.3F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(12))
        plan(13).rot(0, (rtri * 0.1443), 0)
        plan(13).draw(1.1 * Math.Cos(rquad * 545.21) + plan(4).getX(), -1.1 * Math.Sin(rquad * 545.21) + plan(4).getY, 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        If doAnimation = False Then
            getCamLock()
            rquad += speed
            rtri += daySpeed
        Else
            If camAnimation() = True And planetToPlanet = True Then
                planetToPlanet = False
                getFramInc()
                doAnimation = True
            End If
        End If

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

    Sub setOrth()
        GL.Disable(EnableCap.DepthTest)
        GL.Viewport(0, 0, Me.Width, Me.Height)
        GL.MatrixMode(MatrixMode.Projection)
        GL.LoadIdentity()
        GL.Ortho(0, Me.Width, 0, Me.Height, -1, 1)
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()
    End Sub

    Sub getCamLock()
        If planID > -1 Then
            camloc(0) = -plan(planID).getX()
            camloc(1) = -plan(planID).getY()
        Else
            camloc(0) = 0
            camloc(1) = 0
            camloc(2) = -100
        End If
    End Sub

    Sub getFramInc()
        If planID >= 0 And planetToPlanet = False Then
            CamAnimationFrame(0) = (camloc(0) - plan(planID).getX()) / 60
            CamAnimationFrame(1) = (camloc(1) - plan(planID).getY()) / 60
            CamAnimationFrame(2) = (camloc(2) - (plan(planID).getRad() * -5)) / 60
        Else
            CamAnimationFrame(0) = -camloc(0) / 60
            CamAnimationFrame(1) = -camloc(1) / 60
            CamAnimationFrame(2) = (camloc(2) + 100) / 60
        End If
    End Sub

    Function camAnimation() As Boolean
        camloc(0) += CamAnimationFrame(0)
        camloc(1) += CamAnimationFrame(1)
        camloc(2) -= CamAnimationFrame(2)
        framCount += 1

        If framCount > 59 Then
            doAnimation = False
            framCount = 0
            Return True
        End If
        Return False
    End Function

    Sub getAge(a As Double)
        Me.age = a
    End Sub

    Sub getWei(w As Double)
        Me.wei = w
    End Sub
End Class
