Public Class Calculate
    Function calc(planIndex As Integer, wei As Double, age As Double) As List(Of String)
        Dim ret As List(Of String) = New List(Of String)()

        Select Case planIndex
            Case 0
                ret.Add("Sol")
                ret.Add((wei * 27.94).ToString("f3"))
                ret.Add((age / 225000000.0).ToString("f3"))
            Case 1
                ret.Add("Mercury")
                ret.Add((wei * 0.38).ToString("f3"))
                ret.Add(((age * 365.25) / 88).ToString("f3"))
            Case 2
                ret.Add("Venus")
                ret.Add((wei * 0.95).ToString("f3"))
                ret.Add(((age * 365.25) / 224).ToString("f3"))
            Case 3
                ret.Add("Earth")
                ret.Add(wei.ToString("f3"))
                ret.Add(age.ToString("f3"))
            Case 4
                ret.Add("Mars")
                ret.Add((wei * 0.376).ToString("f3"))
                ret.Add(((age * 365.25) / 686.97).ToString("f3"))
            Case 5
                ret.Add("Jupiter")
                ret.Add((wei * 2.528).ToString("f3"))
                ret.Add((age / 11.86).ToString("f3"))
            Case 6
                ret.Add("Saturn")
                ret.Add((wei * 1.065).ToString("f3"))
                ret.Add((age / 29.46).ToString("f3"))
            Case 7
                ret.Add("Uranus")
                ret.Add((wei * 0.886).ToString("f3"))
                ret.Add((age / 84.02).ToString("f3"))
            Case 8
                ret.Add("Neptune")
                ret.Add((wei * 1.14).ToString("f3"))
                ret.Add((age / 164.8).ToString("f3"))
            Case 9
                ret.Add("Pluto")
                ret.Add((wei * 0.035).ToString("f3"))
                ret.Add((age / 247.84).ToString("f3"))
            Case 10
                ret.Add("Luna")
                ret.Add((wei * 0.1654).ToString("f3"))
                ret.Add(((age * 365.25) / 27.32).ToString("f3"))
            Case 11
                ret.Add("Charon")
                ret.Add((wei * 0.03402).ToString("f3"))
                ret.Add(((age * 365.25) / 6.3872304).ToString("f3"))
            Case 14
                ret.Add("Halley's Comet")
                ret.Add((wei * 0.000301).ToString("f3"))
                ret.Add((age / 75.316).ToString("f3"))
        End Select

        ' get sly remark on age
        ret.Add(ageRemark(Convert.ToDouble(ret(2))))

        ' get sly remark on weight
        ret.Add(weightRemark(Convert.ToDouble(ret(1)), wei))

        Return ret
    End Function

    Function weightRemark(w As Double, original As Double) As String
        If w > (original * 5) Then
            Return vbNewLine & "   I found your picture in the dictionary, under fat."
        Else
            If w >= (original * 1.2) And w <= (original * 5) Then
                Return vbNewLine & "   looks like someone's eating one too meny pies."
            Else
                If w > original And w <= (original * 1.2) Then
                    Return vbNewLine & "   a little bit tubby, but it could be worse."
                Else
                    If w >= (original * 0.6) And w <= original Then
                        Return vbNewLine & "   I see you finally went on a diet."
                    Else
                        If w <= (original * 0.6) Then
                            Return vbNewLine & "   even super models would call you thin."
                        End If
                    End If
                End If
            End If
        End If

        Return ""
    End Function

    Function ageRemark(a As Double) As String
        If a > 70 Then
            Return vbNewLine & "   Hello grampa."
        Else
            If a >= 1 And a <= 2 Then
                Return vbNewLine & "   who let a baby on a computer?"
            Else
                If a < 1 Then
                    Return vbNewLine & "   what is this? We have a fetus on a computer?"
                End If
            End If
        End If

        Return ""
    End Function
End Class
