Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitialCreation
        Inherits DbMigration

        Public Overrides Sub Up()
            CreateTable(
                "dbo.Payment",
                Function(c) New With
                           {
                           .Id = c.Int(nullable:=False, identity:=True),
                           .Value = c.Int(nullable:=False)
                           }) _
                .PrimaryKey(Function(t) t.Id)
        End Sub

        Public Overrides Sub Down()
            DropTable("dbo.Payment")
        End Sub
    End Class
End Namespace
