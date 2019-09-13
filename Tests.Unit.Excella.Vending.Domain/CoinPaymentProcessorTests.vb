Imports Excella.Vending.Domain
Imports NUnit.Framework

<TestFixture>
Public Class CoinPaymentProcessorTests
    Dim paymentProcessor As CoinPaymentProcessor

    <SetUp>
    Public Sub SetUp()
        paymentProcessor = New CoinPaymentProcessor()
    End Sub

    <Test>
    Public Sub Payment_WhenNoMoney_ExpectBalanceIsZero()
        Dim result = paymentProcessor.Payment

        Assert.That(result, [Is].EqualTo(0))
    End Sub

    <Test>
    Public Sub Payment_WhenHasMoney_ExpectBalanceIsNotZero()
        paymentProcessor.ProcessPayment(25)

        Dim result = paymentProcessor.Payment

        Assert.That(result, [Is].EqualTo(25))
    End Sub

    <Test>
    Public Sub HasSufficientBalance_WhenNoMoney_ExpectFalse()
        Dim result = paymentProcessor.HasSufficientBalance()

        Assert.That(result, [Is].EqualTo(False))
    End Sub

    <Test>
    Public Sub HasSufficientBalance_WhenLessThan50Cents_ExpectFalse()
        paymentProcessor.ProcessPayment(25)

        Dim actual = paymentProcessor.HasSufficientBalance()

        Assert.That(actual, [Is].EqualTo(False))
    End Sub


    <Test>
    Public Sub HasSufficientBalance_When50Cents_ExpectTrue()
        paymentProcessor.ProcessPayment(50)

        Dim actual = paymentProcessor.HasSufficientBalance()

        Assert.That(actual, [Is].EqualTo(True))
    End Sub


    <Test>
    Public Sub HasSufficientBalance_WhenGreaterThan50Cents_ExpectTrue()
        paymentProcessor.ProcessPayment(75)

        Dim actual = paymentProcessor.HasSufficientBalance()

        Assert.That(actual, [Is].EqualTo(True))
    End Sub

    <Test>
    Public Sub ProcessPayment_WhenPaymentMade_ExpectBalanceUpdated()
        paymentProcessor.ProcessPayment(25)

        Assert.That(paymentProcessor.Payment, [Is].EqualTo(25))
    End Sub


    <Test>
    Public Sub ProcessPurchase_WhenPurchaseMade_ExpectBalanceReduced()
        paymentProcessor.ProcessPayment(75)

        paymentProcessor.ProcessPurchase()

        Assert.That(paymentProcessor.Payment, [Is].EqualTo(25))
    End Sub


    <Test>
    Public Sub ClearPayment_WhenPaymentHasBeenMade_ExpectBalanceIsZero()
        paymentProcessor.ProcessPayment(25)

        paymentProcessor.ClearPayment()

        Assert.That(paymentProcessor.Payment, [Is].EqualTo(0))
    End Sub


End Class
