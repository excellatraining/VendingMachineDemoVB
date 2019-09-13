Public Class CoinPaymentProcessor
    Implements IPaymentProcessor

    Public ReadOnly Property Payment As Integer Implements IPaymentProcessor.Payment

    Public Function HasSufficientBalance() As Boolean Implements IPaymentProcessor.HasSufficientBalance
        Return Payment >= 50
    End Function

    Public Sub ProcessPayment(amount As Integer) Implements IPaymentProcessor.ProcessPayment
        _Payment += amount
    End Sub

    Public Sub ProcessPurchase() Implements IPaymentProcessor.ProcessPurchase
        If Payment >= 50 Then
            _Payment -= 50
        End If
    End Sub

    Public Sub ClearPayment() Implements IPaymentProcessor.ClearPayment
        If Payment > 0 Then
            _Payment = 0
        End If
    End Sub
End Class
