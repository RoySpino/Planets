Imports OpenTK
Imports OpenTK.Graphics.OpenGL
Imports System.Drawing
Imports System.Drawing.Imaging

Public Class LoadBMP
    Function load(path As String) As Integer
        Dim texture As Integer = GL.GenTexture()
        Dim bitImage As Bitmap
        Dim data As BitmapData

        GL.BindTexture(TextureTarget.Texture2D, texture)
        bitImage = New Bitmap(path)
        data = bitImage.LockBits(New Rectangle(0, 0, bitImage.Width, bitImage.Height),
                                 ImageLockMode.ReadOnly,
                                 System.Drawing.Imaging.PixelFormat.Format32bppArgb)
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width,
                      data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0)
        bitImage.UnlockBits(data)

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, CInt(TextureWrapMode.Repeat))
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, CInt(TextureWrapMode.Repeat))
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, CInt(TextureMinFilter.Linear))
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, CInt(TextureMagFilter.Linear))

        Return texture
    End Function
End Class
