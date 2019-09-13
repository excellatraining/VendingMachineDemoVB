Public Class VendingMachine
    Private balance As Integer = 0
    Public Property Message As String

    Public Function ReleaseChange() As Integer
        Dim change = balance
        balance = 0

        Return change
    End Function

    Public Sub InsertCoin()
        balance += 25
    End Sub

    Public Function BuyProduct() As Product
        If balance >= 50 Then
            Message = "Enjoy!"
            Return New Product
        End If

        Message = "Please insert money"
        Return Nothing
    End Function
End Class
