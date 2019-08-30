
Imports Excella.Vending.Machine
Imports NUnit.Framework

<TestFixture>
Public Class VendingMachineTests
    Private vendingMachine As VendingMachine

    <SetUp>
    Public Sub Setup()
        vendingMachine = New VendingMachine
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenNoMoneyInserted_ExpectZero()

        Dim result = vendingMachine.ReleaseChange

        Assert.That(result, [Is].EqualTo(0))
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenOneCoinInserted_Expect25()
        vendingMachine.InsertCoin()

        Dim result = vendingMachine.ReleaseChange

        Assert.That(result, [Is].EqualTo(25))
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenTwoCoinInserted_Expect50()
        vendingMachine.InsertCoin()
        vendingMachine.InsertCoin()

        Dim result = vendingMachine.ReleaseChange

        Assert.That(result, [Is].EqualTo(50))
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenThreeCoinInserted_Expect75()
        vendingMachine.InsertCoin()
        vendingMachine.InsertCoin()
        vendingMachine.InsertCoin()

        Dim result = vendingMachine.ReleaseChange

        Assert.That(result, [Is].EqualTo(75))
    End Sub

    <Test>
    Public Sub BuyProduct_WhenNoMoneyInserted_ExpectNull()
        Dim result = vendingMachine.BuyProduct()

        Assert.That(result, [Is].Null)
    End Sub

    <Test>
    Public Sub BuyProduct_WhenPaymentMade_CallsPaymentProcessorToProcessPurchase()
        vendingMachine.InsertCoin()
        vendingMachine.InsertCoin()

        Dim result = vendingMachine.BuyProduct

        Assert.That(result, [Is].Not.Null)
    End Sub
End Class
