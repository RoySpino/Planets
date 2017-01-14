Public Class Calculate
    Function calc(planIndex As Integer, wei As Double, age As Double) As List(Of Double)
        Dim ret As List(Of Double) = New List(Of Double)()

        Select Case planIndex
            Case 1
                ret.Add(wei * 0.38)
                ret.Add((age * 365.25) / 88)
            Case 2
                ret.Add(wei * 0.95)
                ret.Add((age * 365.25) / 224)
            Case 3
                ret.Add(wei)
                ret.Add(age)
            Case 4
                ret.Add(wei * 0.376)
                ret.Add((age * 365.25) / 686.97)
            Case 5
                ret.Add(wei * 2.528)
                ret.Add(age / 11.86)
            Case 6
                ret.Add(wei * 1.065)
                ret.Add(age / 29.46)
            Case 7
                ret.Add(wei * 0.886)
                ret.Add(age / 84.02)
            Case 8
                ret.Add(wei * 1.14)
                ret.Add(age / 164.8)
            Case 9
                ret.Add(wei * 0.035)
                ret.Add(age / 247.84)
            Case 10
                ret.Add(wei * 0.1654)
                ret.Add((age * 365.25) / 27.32)
        End Select

        Return ret
    End Function
End Class
