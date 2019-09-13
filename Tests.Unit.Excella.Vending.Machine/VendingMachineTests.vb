
Imports Excella.Vending.Domain
Imports Excella.Vending.Machine
Imports Moq
Imports NUnit.Framework

<TestFixture>
Public Class VendingMachineTests
    Private vendingMachine As VendingMachine
    Private paymentProcessor As Mock(Of IPaymentProcessor)

    <SetUp>
    Public Sub Setup()
        paymentProcessor = New Mock(Of IPaymentProcessor)
        vendingMachine = New VendingMachine(paymentProcessor.Object)
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenNoMoneyInserted_ExpectZero()
        paymentProcessor.Setup(Function(p) p.Payment).Returns(0)

        Dim result = vendingMachine.ReleaseChange

        Assert.That(result, [Is].EqualTo(0))
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenOneCoinInserted_Expect25()
        paymentProcessor.Setup(Function(p) p.Payment).Returns(25)

        Dim result = vendingMachine.ReleaseChange

        Assert.That(result, [Is].EqualTo(25))
    End Sub

    <Test>
    Public Sub BuyProduct_WhenNoMoneyInserted_ExpectNull()
        paymentProcessor.Setup(Function(p) p.HasSufficientBalance()).Returns(False)

        Dim result = vendingMachine.BuyProduct()

        Assert.That(result, [Is].Null)
    End Sub

    <Test>
    Public Sub BuyProduct_WhenEnoughMoneyInserted_CallsPaymentProcessorToProcessPurchase()
        paymentProcessor.Setup(Function(p) p.HasSufficientBalance()).Returns(True)

        vendingMachine.BuyProduct()

        paymentProcessor.Verify(Sub(p) p.ProcessPurchase(), Times.Once())
    End Sub

    <Test>
    Public Sub BuyProduct_WhenNoMoneyInserted_DoesNotCallPaymentProcessorToProcessPurchase()
        paymentProcessor.Setup(Function(p) p.HasSufficientBalance()).Returns(False)

        vendingMachine.BuyProduct()

        paymentProcessor.Verify(Sub(p) p.ProcessPurchase(), Times.Never())
    End Sub

    <Test>
    Public Sub BuyProduct_WhenEnoughMoneyInserted_ExpectProduct()
        paymentProcessor.Setup(Function(p) p.HasSufficientBalance()).Returns(True)

        Dim result = vendingMachine.BuyProduct()

        Assert.That(result, [Is].Not.Null)
    End Sub

    <Test>
    Public Sub GetMessage_WhenNoMoneyInserted_ExpectMoneyPrompt()
        paymentProcessor.Setup(Function(p) p.HasSufficientBalance()).Returns(False)

        vendingMachine.BuyProduct()

        Assert.That(vendingMachine.Message, [Is].EqualTo("Please insert money"))
    End Sub

    <Test>
    Public Sub GetMessage_WhenEnoughMoneyInserted_ExpectEnjoyPrompt()
        paymentProcessor.Setup(Function(p) p.HasSufficientBalance()).Returns(True)

        vendingMachine.BuyProduct()

        Dim message = vendingMachine.Message

        Assert.That(message, [Is].EqualTo("Enjoy!"))
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenCoinInserted_CallsPaymentProcessorToResetPayment()
        paymentProcessor.Setup(Function(p) p.Payment).Returns(25)

        vendingMachine.ReleaseChange()

        paymentProcessor.Verify(Sub(p) p.ClearPayment(), Times.Once)
    End Sub


    <Test>
    Public Sub ReleaseChange_WhenNoCoinInserted_DoesNotCallPaymentProcessorToResetPayment()
        vendingMachine.ReleaseChange()

        paymentProcessor.Verify(Sub(p) p.ClearPayment(), Times.Never)
    End Sub

End Class
