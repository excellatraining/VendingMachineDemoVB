Imports Excella.Vending.DAL
Imports NUnit.Framework

<TestFixture>
Public Class FakePaymentDaoTests
    Dim paymentDao As FakePaymentDao

    <SetUp>
    Public Sub SetUp()
        paymentDao = New FakePaymentDao()
    End Sub

    <Test>
    Public Sub RetrievePayment_WhenNoPayment_ExpectBalanceIsZero()
        Dim result = paymentDao.RetrievePayment()

        Assert.That(result, [Is].EqualTo(0))
    End Sub

    <Test>
    Public Sub RetrievePayment_WhenPaymentSaved_ExpectBalanceIsNotZero()
        paymentDao.SavePayment(25)

        Dim result = paymentDao.RetrievePayment

        Assert.That(result, [Is].EqualTo(25))
    End Sub

    <Test>
    Public Sub SavePayment_WhenPaymentMade_ExpectBalanceUpdated()
        paymentDao.SavePayment(25)

        Assert.That(paymentDao.RetrievePayment(), [Is].EqualTo(25))
    End Sub


    <Test>
    Public Sub ProcessPurchase_WhenPurchaseMade_ExpectBalanceReduced()
        paymentDao.SavePayment(75)

        paymentDao.SavePurchase()

        Assert.That(paymentDao.RetrievePayment(), [Is].EqualTo(25))
    End Sub


    <Test>
    Public Sub ClearPayment_WhenPaymentHasBeenMade_ExpectBalanceIsZero()
        paymentDao.SavePayment(25)

        paymentDao.ClearPayment()

        Assert.That(paymentDao.RetrievePayment(), [Is].EqualTo(0))
    End Sub
End Class
