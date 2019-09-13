Public Interface IPaymentProcessor
    ReadOnly Property Payment As Integer
    Function HasSufficientBalance() As Boolean
    Sub ProcessPayment(amount As Integer)
    Sub ProcessPurchase()
    Sub ClearPayment()
End Interface
