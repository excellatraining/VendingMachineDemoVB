Imports Excella.Vending.Domain

Public Class VendingMachine
    Private paymentProcessor As IPaymentProcessor

    Public ReadOnly Property Balance As Double
        Get
            Return paymentProcessor.Payment
        End Get
    End Property

    Public Property Message As String

    Public Sub New(payProcessor As IPaymentProcessor)
        paymentProcessor = payProcessor
    End Sub


    Public Function ReleaseChange() As Integer
        Dim change = paymentProcessor.Payment

        If change > 0 Then
            paymentProcessor.ClearPayment()
        End If

        Return change
    End Function

    Public Sub InsertCoin()
        paymentProcessor.ProcessPayment(25)
    End Sub

    Public Function BuyProduct() As Product
        If paymentProcessor.HasSufficientBalance() Then
            paymentProcessor.ProcessPurchase()
            Message = "Enjoy!"
            Return New Product
        End If

        Message = "Please insert money"
        Return Nothing
    End Function
End Class
