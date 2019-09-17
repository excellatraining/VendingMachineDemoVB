Public Class FakePaymentDao
    Implements IPaymentDao

    Dim balance As Integer

    Public Function RetrievePayment() As Integer Implements IPaymentDao.RetrievePayment
        Return balance
    End Function

    Public Sub SavePayment(amount As Integer) Implements IPaymentDao.SavePayment
        balance += amount
    End Sub

    Public Sub SavePurchase() Implements IPaymentDao.SavePurchase
        Const PURCHASE_COST As Integer = 50
        balance -= PURCHASE_COST
    End Sub

    Public Sub ClearPayment() Implements IPaymentDao.ClearPayment
        balance = 0
    End Sub
End Class
