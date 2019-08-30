Public Class VendingMachine
    Private balance As Integer = 0

    Public Function ReleaseChange() As Integer
        Return balance
    End Function

    Public Sub InsertCoin()
        balance += 25
    End Sub

    Public Function BuyProduct() As Product
        If balance >= 50 Then
            Return New Product
        Else
            Return Nothing
        End If
    End Function
End Class
