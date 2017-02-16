Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System
Imports System.Drawing

Public Class FrontMenu
    Dim prompt1, prompt2, welcom As GLPrint
    Dim plane As Plane = New Plane(500, 500)
    Dim strRaw As String = ""
    Dim agePrompt, weightPrompt As String
    Dim ageCompleate As Boolean = False
    Dim ofset As Integer

    Dim vitex, textTex As Integer
    Dim gettex As LoadBMP = New LoadBMP()

    Public Sub New()
        agePrompt = "How old are you: "
        weightPrompt = "how much do you weigh: "
        prompt1 = New GLPrint(50)
        prompt2 = New GLPrint(50)
        welcom = New GLPrint(50)

        ofset = 0
        vitex = gettex.load(".\..\..\Images\VI_Nyota_Welcome.png")
        textTex = gettex.load(".\..\..\Images\Consolas.png")
    End Sub

    Public Sub menu(x As Integer, y As Integer)
        If y <= 1031 Then
            ofset = -100
        Else
            ofset = 0
        End If

        GL.Color3(Color.CornflowerBlue)

        GL.Disable(EnableCap.Lighting)
        GL.BindTexture(TextureTarget.Texture2D, vitex)
        plane.draw(((x / 2) - 280), ((y / 2) - 280), 0)
        GL.BindTexture(TextureTarget.Texture2D, 0)
        GL.Enable(EnableCap.Lighting)

        GL.BindTexture(TextureTarget.Texture2D, textTex)
        welcom.print((x / 4), (y / 4) + ofset, "Welcome to the planetary calculator")

        If ageCompleate = False Then
            prompt1.print((x / 4), (y / 5) + ofset, agePrompt & strRaw)
        Else
            prompt1.print((x / 4), (y / 5) + ofset, weightPrompt & strRaw)
        End If
        GL.BindTexture(TextureTarget.Texture2D, 0)

        GL.Color3(Color.White)
    End Sub

    Public Sub userInput(raw As String)
        strRaw = raw
    End Sub

    Public Sub SetText()
        Dim arr1() As String = agePrompt.Split(":")

        ' on first call ageprompt will have only a space 
        ' On the second element when it Is split
        If IsNumeric(arr1(1).Trim()) = False Then
            agePrompt &= strRaw
            ageCompleate = True
        Else
            weightPrompt &= strRaw
        End If

        strRaw = ""
    End Sub

    Sub answerRetry()

    End Sub
End Class