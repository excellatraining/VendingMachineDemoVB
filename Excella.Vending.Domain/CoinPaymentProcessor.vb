Imports Excella.Vending.DAL

Public Class CoinPaymentProcessor
    Implements IPaymentProcessor

    Dim paymentDao As IPaymentDao

    Public ReadOnly Property Payment() As Integer Implements IPaymentProcessor.Payment
        Get
            Return paymentDao.RetrievePayment()
        End Get
    End Property

    Public Sub New(payDao As IPaymentDao)
        paymentDao = payDao
    End Sub


    Public Function HasSufficientBalance() As Boolean Implements IPaymentProcessor.HasSufficientBalance
        Return Payment >= 50
    End Function

    Public Sub ProcessPayment(amount As Integer) Implements IPaymentProcessor.ProcessPayment
        paymentDao.SavePayment(amount)
    End Sub

    Public Sub ProcessPurchase() Implements IPaymentProcessor.ProcessPurchase
        paymentDao.SavePurchase()
    End Sub

    Public Sub ClearPayment() Implements IPaymentProcessor.ClearPayment
        If Payment > 0 Then
            paymentDao.ClearPayment()
        End If
    End Sub
End Class
