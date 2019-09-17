Imports Excella.Vending.DAL
Imports Excella.Vending.Machine
Imports NUnit.Framework
Imports System.Transactions
Imports Excella.Vending.Domain

Public Class VendingMachineTestsADO
    Dim vendingMachine As VendingMachine
    Dim paymentDao As New ADOPaymentDao()
    Dim transactionScope as TransactionScope

    <OneTimeSetup>
    Public Sub FixtureSetup()
        paymentDao.ClearPayment()
    End Sub

    <SetUp>
    Public Sub Setup()
        paymentDao = New ADOPaymentDao()
        transactionScope = New TransactionScope()
        Dim paymentProcessor As IPaymentProcessor = New CoinPaymentProcessor(paymentDao)
        vendingMachine = New VendingMachine(paymentProcessor)
    End Sub

    <TearDown>
    Public Sub Teardown()
        transactionScope.Dispose()
    End Sub

    <Test>
    Public Sub InsertCoin_WhenOneCoinInserted_ExpectIncreaseOf25()
        vendingMachine.InsertCoin()

        Dim currentBalance As Integer = vendingMachine.Balance

        Assert.That(currentBalance, [Is].EqualTo(25))
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenNoMoneyInserted_Expect0()
        Dim change = vendingMachine.ReleaseChange()

        Assert.That(change, [Is].EqualTo(0))
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenOneCoinInserted_Expect25()
        vendingMachine.InsertCoin()

        Dim change = vendingMachine.ReleaseChange()

        Assert.That(change, [Is].EqualTo(25))
    End Sub

    <Test>
    Public Sub ReleaseChange_WhenThreeCoinsInsertedAndProductPurchased_Expect25()
        vendingMachine.InsertCoin()
        vendingMachine.InsertCoin()
        vendingMachine.InsertCoin()
        vendingMachine.BuyProduct()

        Dim change = vendingMachine.ReleaseChange()

        Assert.That(change, [Is].EqualTo(25))
    End Sub
End Class
