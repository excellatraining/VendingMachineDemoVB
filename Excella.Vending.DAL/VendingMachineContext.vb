Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions
Imports Excella.Vending.DAL.Models

Public Class VendingMachineContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("VendingMachineContext")
    End Sub

    Public Property Payments As DbSet(Of Payment)

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        modelBuilder.Conventions.Remove(Of PluralizingTableNameConvention)()
    End Sub
End Class
