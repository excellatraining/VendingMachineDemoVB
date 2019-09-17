Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq

Namespace Migrations

    Friend NotInheritable Class Configuration 
        Inherits DbMigrationsConfiguration(Of VendingMachineContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
        End Sub

        Protected Overrides Sub Seed(context As VendingMachineContext)
            ' Using SQL text here because EF's AddOrUpdate() couldn't make use of IDENTITY_INSERT Without being in a distinct
            ' Transaction scope, which appeared to require MSDTC, which Is overkill. 
            Const SQL_TO_ADD_OR_UPDATE_ID_ROW = "
                BEGIN
	                IF EXISTS (select * from Payment where id = 1)
	                  BEGIN
		                UPDATE PAYMENT SET Value = 0 WHERE Id = 1
	                  END
	                ELSE
		                BEGIN
		                    SET IDENTITY_INSERT [dbo].[Payment] ON
		                    INSERT INTO Payment (Id, Value) VALUES (1,0)
		                    SET IDENTITY_INSERT [dbo].[Payment] OFF
		                END
	                END"

            context.Database.ExecuteSqlCommand(SQL_TO_ADD_OR_UPDATE_ID_ROW)
        End Sub

    End Class

End Namespace
