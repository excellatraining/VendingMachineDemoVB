Imports System.Configuration
Imports System.Data.SqlClient

Public Class ADOPaymentDao
    Implements IPaymentDao

    Private Function GetConnection() As SqlConnection
        Dim connectionString As String = ConfigurationManager.ConnectionStrings("VendingMachineContext").ConnectionString

        Return New SqlConnection(connectionString)
    End Function

    Public Function RetrievePayment() As Integer Implements IPaymentDao.RetrievePayment
        Using connection As SqlConnection = GetConnection()
            Dim payment As Integer = 0

            Dim command As New SqlCommand("SELECT Value FROM Payment WHERE ID = 1;", connection)
            connection.Open()

            Dim reader As SqlDataReader = command.ExecuteReader()

            If reader.HasRows Then
                While reader.Read()
                    payment = reader.GetInt32(0)
                End While
            Else
                Console.WriteLine("No rows found.")
            End If

            reader.Close()
            Return payment
        End Using
    End Function

    Public Sub SavePayment(payment As Integer) Implements IPaymentDao.SavePayment
        Using connection As SqlConnection = GetConnection()
            Dim sqlCommandString As String = String.Format("UPDATE Payment SET Value = Value + {0} WHERE ID = 1;", payment)
            Dim command As New SqlCommand(sqlCommandString, connection)
            connection.Open()

            Dim rowsChanged As Integer = command.ExecuteNonQuery()

            If rowsChanged < 1 Then
                Console.WriteLine("No rows found.")
            End If
        End Using
    End Sub

    Public Sub SavePurchase() Implements IPaymentDao.SavePurchase
        Const PURCHASE_PRICE As Integer = 50
        Using connection As SqlConnection = GetConnection()
            Dim commandText = String.Format("UPDATE Payment SET Value = Value - {0} WHERE ID = 1;", PURCHASE_PRICE)
            Dim command As New SqlCommand(commandText, connection)
            connection.Open()

            Dim rowsChanged As Integer = command.ExecuteNonQuery()

            If rowsChanged < 1 Then
                Console.Write("No rows found.")
            End If
        End Using
    End Sub

    Public Sub ClearPayment() Implements IPaymentDao.ClearPayment
        Using connection As SqlConnection = GetConnection()
            Dim command As New SqlCommand("UPDATE Payment SET Value = 0 WHERE ID = 1;", connection)
            connection.Open()

            command.ExecuteNonQuery()
        End Using
    End Sub
End Class
