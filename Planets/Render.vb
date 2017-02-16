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
    Dim speed As Double = 0.0009
    Dim daySpeed As Double = 0.5
    Dim planID, prevID, textTex, enterCnt As Integer
    Dim CamAnimationFrame(3) As Double
    Dim calculon As Calculate = New Calculate()
    Dim Texture As List(Of Int32)
    Dim getBMP As LoadBMP = New LoadBMP()
    Dim framCount, menuX, menuY As Integer
    Dim doAnimation, planetToPlanet As Boolean
    Dim plutox, plutoY As Double
    Dim camloc(3) As Double
    Dim age, wei As Double
    Dim rawString As String = ""
    Dim viMessage, viMesageFormat, sunMessage As String
    Dim calcResult As List(Of String)
    Dim menu As FrontMenu = New FrontMenu()

    Dim plan As List(Of SolidSphere)
    Dim rings As List(Of Ring)
    Dim print As GLPrint = New GLPrint(32)
    Dim comment As GLPrint = New GLPrint(32)
    Dim vi As Plane = New Plane(300, 300)

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Public Sub New(w As Integer, h As Integer)
        MyBase.New(w, h)
        Me.X = 100
        Me.Y = 100

        enterCnt = 0
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
        plan.Add(New SolidSphere(0.08, 4, 8))

        rings = New List(Of Ring)()
        rings.Add(New Ring(6.4, 16, 32))
        rings.Add(New Ring(7.4, 16, 32))
        rings.Add(New Ring(5.9, 16, 32))
        rings.Add(New Ring(5.5, 16, 32))

        Texture.Add(getBMP.load(".\..\..\Images\sun.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Mercury.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Venus.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Earth.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Mars.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Jupiter.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Saturn.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Uranus.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Neptune.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Pluto.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Moon.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Charon.png"))
        Texture.Add(getBMP.load(".\..\..\Images\Phobos.png"))
        textTex = getBMP.load(".\..\..\Images\Consolas.png")

        init()

        viMesageFormat = "{0}" & vbNewLine &
                         "On {0} you are {2} years old {3}" & vbNewLine &
                         "   Also you weigh {1}{4}"

        menuX = Me.Size.Width - 500
        menuY = 700
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

        menuX = Me.Size.Width - 500
        menuY = 700
    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Protected Overrides Sub OnKeyDown(e As KeyboardKeyEventArgs)
        MyBase.OnKeyDown(e)

        If wei = 0 Then
            Select Case e.Key
                Case OpenTK.Input.Key.Enter
                    If enterCnt = 1 And rawString <> "" Then
                        menu.SetText()
                        Try
                            wei = Convert.ToDouble(rawString)
                        Catch ex As Exception
                            menu.answerRetry()
                        End Try
                        rawString = ""
                    Else
                        If rawString <> "" Then
                            ' send age to menu class and switch to weight
                            menu.SetText()
                            Try
                                age = Convert.ToDouble(rawString)
                            Catch ex As Exception
                                menu.answerRetry()
                            End Try
                            rawString = ""
                            enterCnt += 1
                        End If
                    End If
                Case OpenTK.Input.Key.A
                    rawString += "A"
                Case OpenTK.Input.Key.B
                    rawString += "B"
                Case OpenTK.Input.Key.C
                    rawString += "C"
                Case OpenTK.Input.Key.D
                    rawString += "D"
                Case OpenTK.Input.Key.E
                    rawString += "E"
                Case OpenTK.Input.Key.F
                    rawString += "F"
                Case OpenTK.Input.Key.G
                    rawString += "G"
                Case OpenTK.Input.Key.H
                    rawString += "H"
                Case OpenTK.Input.Key.I
                    rawString += "I"
                Case OpenTK.Input.Key.J
                    rawString += "J"
                Case OpenTK.Input.Key.K
                    rawString += "K"
                Case OpenTK.Input.Key.L
                    rawString += "L"
                Case OpenTK.Input.Key.M
                    rawString += "M"
                Case OpenTK.Input.Key.N
                    rawString += "N"
                Case OpenTK.Input.Key.O
                    rawString += "O"
                Case OpenTK.Input.Key.P
                    rawString += "P"
                Case OpenTK.Input.Key.Q
                    rawString += "Q"
                Case OpenTK.Input.Key.R
                    rawString += "R"
                Case OpenTK.Input.Key.S
                    rawString += "S"
                Case OpenTK.Input.Key.T
                    rawString += "T"
                Case OpenTK.Input.Key.U
                    rawString += "U"
                Case OpenTK.Input.Key.V
                    rawString += "V"
                Case OpenTK.Input.Key.W
                    rawString += "W"
                Case OpenTK.Input.Key.X
                    rawString += "X"
                Case OpenTK.Input.Key.Y
                    rawString += "Y"
                Case OpenTK.Input.Key.Z
                    rawString += "Z"
                Case OpenTK.Input.Key.Number0
                    rawString += "0"
                Case OpenTK.Input.Key.Number1
                    rawString += "1"
                Case OpenTK.Input.Key.Number2
                    rawString += "2"
                Case OpenTK.Input.Key.Number3
                    rawString += "3"
                Case OpenTK.Input.Key.Number4
                    rawString += "4"
                Case OpenTK.Input.Key.Number5
                    rawString += "5"
                Case OpenTK.Input.Key.Number6
                    rawString += "6"
                Case OpenTK.Input.Key.Number7
                    rawString += "7"
                Case OpenTK.Input.Key.Number8
                    rawString += "8"
                Case OpenTK.Input.Key.Number9
                    rawString += "9"
                Case OpenTK.Input.Key.Period
                    rawString += "."
                Case OpenTK.Input.Key.BackSpace
                    If rawString.Length > 0 Then
                        rawString = rawString.Substring(0, rawString.Length - 1)
                    End If
                Case OpenTK.Input.Key.Escape
                    Me.Exit()
            End Select

            menu.userInput(rawString)
        Else


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
                    planID = 1
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(1).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.Number2
                    planID = 2
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(2).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.Number3
                    planID = 3
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(3).getRad() * -5
                    getFramInc()
                    doAnimation = True

                    viMessage = "Earth" & vbNewLine &
                            "on Earth, well if you dont already know " & vbNewLine &
                            "   this, what did you enter for your age" & vbNewLine &
                            "   and weight?"
                Case OpenTK.Input.Key.Number4
                    planID = 4
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(4).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.Number5
                    planID = 5
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(5).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.Number6
                    planID = 6
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(6).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.Number7
                    planID = 7
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(7).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.Number8
                    planID = 8
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(8).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.Number9
                    planID = 9
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(9).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.Number0
                    planID = 0
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(9).getRad() * -5
                    getFramInc()
                    If Convert.ToDouble(calcResult(2)) > 1 Then
                        sunMessage = vbNewLine & "   Woah, ok now I know your lying to me."
                    Else
                        sunMessage = vbNewLine & "   Oh my, you really let yourself go."
                    End If
                    doAnimation = True
                    viMessage = String.Format("Sol" & vbNewLine &
                            "   on The Sun you are: {0} years old" & vbNewLine &
                            "   also you weight: {1} {2}", calcResult(2), calcResult(1), sunMessage)
                Case OpenTK.Input.Key.L
                    planID = 10
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(10).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.K
                    planID = 11
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(10).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.J
                    planID = 14
                    calcResult = calculon.calc(planID, wei, age)
                    'camloc(2) = plan(10).getRad() * -5
                    getFramInc()
                    doAnimation = True
                    viMessage = String.Format(viMesageFormat, calcResult(0), calcResult(1), calcResult(2), calcResult(3), calcResult(4))
                Case OpenTK.Input.Key.B
                    planID = -1
                    getFramInc()
                    doAnimation = True
            End Select

            If planID >= 0 And prevID >= 0 Then
                planetToPlanet = True
                getFramInc()
            End If
        End If
    End Sub

    ' ///////////////////////////////////////////////////////////////////////////////////////////////////////
    Protected Overrides Sub OnRenderFrame(e As FrameEventArgs)
        MyBase.OnRenderFrame(e)

        GL.Clear(ClearBufferMask.ColorBufferBit Or ClearBufferMask.DepthBufferBit)
        GL.ClearColor(Color.Black)


        ' start planetary calculator
        GL.MatrixMode(MatrixMode.Modelview)
        GL.LoadIdentity()


        ' show only opening menu to get age and weight
        If age <= 0 Or wei <= 0 Then
            setOrth()
            menu.menu(Me.Size.Width, Me.Size.Height)
            Me.SwapBuffers()
            Return
        End If


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

        ' hallys comet
        'GL.Color3(0.3F, 0.3F, 0.3F)
        GL.BindTexture(TextureTarget.Texture2D, Texture(12))
        plan(14).rot(0, (rtri * 0.1443), 0)
        plan(14).draw(165 * Math.Cos(rquad * 0.011901) - 132, -33 * Math.Sin(rquad * 0.011901), 0)
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

        setOrth()
        GL.BindTexture(TextureTarget.Texture2D, textTex)
        print.print(menuX, menuY,
                 "Planetary Calculater" & vbNewLine &
                    "  1) go to Mercury" & vbNewLine &
                    "  2) go to Venus" & vbNewLine &
                    "  3) go to Earth" & vbNewLine &
                    "  4) go to Mars" & vbNewLine &
                    "  5) go to Juptier" & vbNewLine &
                    "  6) go to Saturn" & vbNewLine &
                    "  7) go to Uranus" & vbNewLine &
                    "  8) go to Neptune" & vbNewLine &
                    "  9) go to Pluto" & vbNewLine &
                    "  l) go to Luna" & vbNewLine &
                    "  k) go to charon" & vbNewLine &
                    "  l) go to Hallys commet" & vbNewLine &
                    "  0) go to Sol" & vbNewLine &
                    "  b) go back")

        comment.print(350, 200, viMessage)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        GL.BindTexture(TextureTarget.Texture2D, 0)
        'vi.draw(30, Me.Size.Height - 350, 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)

        init()
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

        GL.Enable(EnableCap.DepthTest)
        GL.Enable(EnableCap.Texture2D)
        GL.Enable(EnableCap.Blend)
        GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha)
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
