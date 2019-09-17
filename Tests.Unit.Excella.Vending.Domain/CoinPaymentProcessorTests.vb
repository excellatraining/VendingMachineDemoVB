Imports Excella.Vending.DAL
Imports Excella.Vending.Domain
Imports Moq
Imports NUnit.Framework

<TestFixture>
Public Class CoinPaymentProcessorTests
    Dim paymentProcessor As CoinPaymentProcessor
    Dim paymentDao As Mock(Of IPaymentDao)

    <SetUp>
    Public Sub SetUp()
        paymentDao = New Mock(Of IPaymentDao)
        paymentProcessor = New CoinPaymentProcessor(paymentDao.Object)
    End Sub

    <Test>
    Public Sub Payment_WhenNoMoney_ExpectBalanceIsZero()
        paymentDao.Setup(Function(p) p.RetrievePayment()).Returns(0)

        Dim result = paymentProcessor.Payment

        Assert.That(result, [Is].EqualTo(0))
    End Sub

    <Test>
    Public Sub Payment_WhenHasMoney_ExpectBalanceIsNotZero()
        paymentDao.Setup(Function(p) p.RetrievePayment()).Returns(25)

        Dim result = paymentProcessor.Payment

        Assert.That(result, [Is].EqualTo(25))
    End Sub

    <Test>
    Public Sub HasSufficientBalance_WhenNoMoney_ExpectFalse()
        paymentDao.Setup(Function(p) p.RetrievePayment()).Returns(0)

        Dim result = paymentProcessor.HasSufficientBalance()

        Assert.That(result, [Is].EqualTo(False))
    End Sub

    <Test>
    Public Sub HasSufficientBalance_WhenLessThan50Cents_ExpectFalse()
        paymentDao.Setup(Function(p) p.RetrievePayment()).Returns(25)

        Dim actual = paymentProcessor.HasSufficientBalance()

        Assert.That(actual, [Is].EqualTo(False))
    End Sub


    <Test>
    Public Sub HasSufficientBalance_When50Cents_ExpectTrue()
        paymentDao.Setup(Function(p) p.RetrievePayment()).Returns(50)

        Dim actual = paymentProcessor.HasSufficientBalance()

        Assert.That(actual, [Is].EqualTo(True))
    End Sub


    <Test>
    Public Sub HasSufficientBalance_WhenGreaterThan50Cents_ExpectTrue()
        paymentDao.Setup(Function(p) p.RetrievePayment()).Returns(75)

        Dim actual = paymentProcessor.HasSufficientBalance()

        Assert.That(actual, [Is].EqualTo(True))
    End Sub

    <Test>
    Public Sub ProcessPayment_WhenPaymentMade_ExpectSavedToDB()
        paymentDao.Setup(Sub(p) p.SavePayment(It.IsAny(Of Integer)())).Verifiable()

        paymentProcessor.ProcessPayment(25)

        paymentDao.Verify(Sub(p) p.SavePayment(25), Times.Once)
    End Sub


    <Test>
    Public Sub ProcessPurchase_WhenPurchaseMade_ExpectSavedToDb()
        paymentProcessor.ProcessPurchase()

        paymentDao.Verify(Sub(p) p.SavePurchase(), Times.Once)
    End Sub


    <Test>
    Public Sub ClearPayment_WhenPaymentHasBeenMade_TellsDaoToClearPayment()
        paymentDao.Setup(Function(p) p.RetrievePayment()).Returns(25)

        paymentProcessor.ClearPayment()

        paymentDao.Verify(Sub(p) p.ClearPayment(), Times.Once)
    End Sub

    <Test>
    Public Sub ClearPayment_WhenNoPaymentHasBeenMade_DoesNotTellDaoToClearPayment()
        paymentDao.Setup(Function(p) p.RetrievePayment()).Returns(0)

        paymentProcessor.ClearPayment()

        paymentDao.Verify(Sub(p) p.ClearPayment(), Times.Never)
    End Sub

End Class
