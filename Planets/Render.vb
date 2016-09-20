Imports OpenTK.Graphics.OpenGL
Imports OpenTK
Imports System
Imports System.Drawing

Public Class Render
    Inherits GameWindow

    Public Sub New(x As Integer, y As Integer)
        MyBase.New(x, y)
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
    End Sub

End Class
