Option Infer On

Public Class EFPaymentDao
    Implements IPaymentDao

    Private ReadOnly context As New VendingMachineContext()

    Public Function RetrievePayment() As Integer Implements IPaymentDao.RetrievePayment
        Dim payment = context.Payments.FirstOrDefault(Function(p) p.Id = 1)

        If payment IsNot Nothing Then
            Return payment.Value
        End If

        Return 0
    End Function

    Public Sub SavePayment(amount As Integer) Implements IPaymentDao.SavePayment
        Dim payment = context.Payments.FirstOrDefault(Function(p) p.Id = 1)

        If payment IsNot Nothing Then
            payment.Value += amount
            context.SaveChanges()
        End If
    End Sub

    Public Sub SavePurchase() Implements IPaymentDao.SavePurchase
        Const PURCHASE_COST = 50
        Dim payment = context.Payments.FirstOrDefault(Function(p) p.Id = 1)

        If payment IsNot Nothing Then
            payment.Value -= PURCHASE_COST
            context.SaveChanges()
        End If
    End Sub

    Public Sub ClearPayment() Implements IPaymentDao.ClearPayment
        Dim payment = context.Payments.FirstOrDefault(Function(p) p.Id = 1)

        If payment IsNot Nothing Then
            payment.Value = 0
            context.SaveChanges()
        End If
    End Sub
End Class
