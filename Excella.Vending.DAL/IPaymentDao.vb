Public Interface IPaymentDao
    Function RetrievePayment() As Integer
    Sub SavePayment(amount As Integer)
    Sub SavePurchase()
    Sub ClearPayment()
End Interface
