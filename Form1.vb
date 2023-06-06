Imports System.ComponentModel

Public Class Form1
    Private orders As Dictionary(Of String, List(Of Integer))
    Public connectionString As String = login.connectionString
    Private connection As OleDb.OleDbConnection
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        resetOrders()
        connection = New OleDb.OleDbConnection(connectionString)
        Try
            connection.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        loadInventoryList()
        salesButton.PerformClick()
    End Sub

    Private Sub resetOrders()
        orders = New Dictionary(Of String, List(Of Integer)) From {
            {"Pork Cut", New List(Of Integer) From {0, 190, 0}},
            {"Menudo Cut", New List(Of Integer) From {0, 200, 0}},
            {"Adobo Cut", New List(Of Integer) From {0, 190, 0}},
            {"Lean Ground Pork", New List(Of Integer) From {0, 190, 0}},
            {"Pork Chop", New List(Of Integer) From {0, 190, 0}},
            {"Tenderloin", New List(Of Integer) From {0, 230, 0}},
            {"Belly Slice", New List(Of Integer) From {0, 230, 0}},
            {"Sinigang Cut", New List(Of Integer) From {0, 300, 0}},
            {"Pata Slice", New List(Of Integer) From {0, 310, 0}},
            {"Bacon", New List(Of Integer) From {0, 280, 0}},
            {"Longganisa", New List(Of Integer) From {0, 150, 0}},
            {"Tocino", New List(Of Integer) From {0, 150, 0}},
            {"Cheese Hotdog", New List(Of Integer) From {0, 150, 0}},
            {"Classic Hotdog", New List(Of Integer) From {0, 140, 0}},
            {"Crispy Pata", New List(Of Integer) From {0, 350, 0}},
            {"Fiesta Ham", New List(Of Integer) From {0, 2000, 0}},
            {"White Egg", New List(Of Integer) From {0, 190, 0}},
            {"Brown Egg", New List(Of Integer) From {0, 270, 0}},
            {"Beef Steak", New List(Of Integer) From {0, 350, 0}},
            {"Lean Ground Beef", New List(Of Integer) From {0, 250, 0}},
            {"Beef Caldereta", New List(Of Integer) From {0, 250, 0}},
            {"Ox Tripe", New List(Of Integer) From {0, 200, 0}},
            {"Beef Short Ribs", New List(Of Integer) From {0, 470, 0}},
            {"Beef Shank", New List(Of Integer) From {0, 470, 0}},
            {"Chicken Skin", New List(Of Integer) From {0, 110, 0}},
            {"Spicy Neck", New List(Of Integer) From {0, 100, 0}},
            {"Torikaraage Teriyaki", New List(Of Integer) From {0, 225, 0}},
            {"Torikaraage Sweet Sour", New List(Of Integer) From {0, 225, 0}},
            {"Marinado Spicy", New List(Of Integer) From {0, 125, 0}},
            {"Marinado Sweet", New List(Of Integer) From {0, 125, 0}},
            {"Thigh Fillet", New List(Of Integer) From {0, 300, 0}},
            {"Drumsticks", New List(Of Integer) From {0, 190, 0}},
            {"Wings", New List(Of Integer) From {0, 190, 0}},
            {"Whole Chicken", New List(Of Integer) From {0, 210, 0}},
            {"Chicken Sticks", New List(Of Integer) From {0, 100, 0}},
            {"Chicken Nuggets", New List(Of Integer) From {0, 90, 0}},
            {"Leg Quarter", New List(Of Integer) From {0, 190, 0}},
            {"Breast Fillet", New List(Of Integer) From {0, 300, 0}}
        }
    End Sub

    Private Sub updateOrders()
        orderList.Items.Clear()
        Dim price = 0
        For Each item In orders.Keys
            If orders(item)(0) > 0 Then
                Dim temp = Val(orders(item)(0)) * Val(orders(item)(1))
                price += temp
                orderList.Items.Add(New ListViewItem(New String() {item, orders(item)(0), temp}))
            End If
        Next
        totalField.Text = price
    End Sub

    Private Sub resetButtons()
        For Each item In Panel1.Controls
            item.Backcolor = Color.FromArgb(233, 25, 104)
            item.ForeColor = Color.White
        Next
    End Sub

    Private Sub salesButton_Click(sender As Object, e As EventArgs) Handles salesButton.Click
        resetButtons()
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)
        salesPanel.Visible = True
        inventoryPanel.Visible = False
        historyPanel.Visible = False
        settingsPane.Visible = False
        resetOrders()
        loadInventoryList()
    End Sub

    Private Sub salesButton_MouseEnter(sender As Object, e As EventArgs) Handles salesButton.MouseEnter
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)
    End Sub

    Private Sub salesButton_MouseLeave(sender As Object, e As EventArgs) Handles salesButton.MouseLeave
        If Not salesPanel.Visible Then
            sender.Backcolor = Color.FromArgb(233, 25, 104)
            sender.ForeColor = Color.White
        End If
    End Sub

    Private Sub InventoryButton_Click(sender As Object, e As EventArgs) Handles InventoryButton.Click
        resetButtons()
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)
        salesPanel.Visible = False
        inventoryPanel.Visible = True
        historyPanel.Visible = False
        settingsPane.Visible = False

        Dim command As New OleDb.OleDbCommand("SELECT [Product Name], [Price], [Quantity] FROM inventory", connection)
        Dim table = New DataTable
        table.Load(command.ExecuteReader)
        inventoryGridView.DataSource = table

    End Sub
    Private Sub InventoryButton_MouseEnter(sender As Object, e As EventArgs) Handles InventoryButton.MouseEnter
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)
    End Sub

    Private Sub InventoryButton_MouseLeave(sender As Object, e As EventArgs) Handles InventoryButton.MouseLeave
        If Not inventoryPanel.Visible Then
            sender.Backcolor = Color.FromArgb(233, 25, 104)
            sender.ForeColor = Color.White
        End If
    End Sub

    Private Sub HistoryButton_Click(sender As Object, e As EventArgs) Handles HistoryButton.Click
        resetButtons()
        salesPanel.Visible = False
        inventoryPanel.Visible = False
        historyPanel.Visible = True
        settingsPane.Visible = False
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)

        Dim command As New OleDb.OleDbCommand("SELECT * FROM transactions", connection)
        Dim table = New DataTable
        table.Load(command.ExecuteReader)
        historyGridView.DataSource = table
    End Sub

    Private Sub HistoryButton_MouseEnter(sender As Object, e As EventArgs) Handles HistoryButton.MouseEnter
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)
    End Sub

    Private Sub HistoryButton_MouseLeave(sender As Object, e As EventArgs) Handles HistoryButton.MouseLeave
        If Not historyPanel.Visible Then
            sender.Backcolor = Color.FromArgb(233, 25, 104)
            sender.ForeColor = Color.White
        End If
    End Sub

    Private Sub SettingsButton_Click(sender As Object, e As EventArgs) Handles SettingsButton.Click
        resetButtons()
        salesPanel.Visible = False
        inventoryPanel.Visible = False
        historyPanel.Visible = False
        settingsPane.Visible = True
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)

        Dim command As New OleDb.OleDbCommand("SELECT * FROM users", connection)
        Dim reader = command.ExecuteReader()
        userSelection.Items.Clear()

        While reader.Read()
            userSelection.Items.Add(reader.GetString(1))
        End While
        clearUserButton.Visible = False
        saveUserChangesButton.Visible = False
    End Sub
    Private Sub SettingsButton_MouseEnter(sender As Object, e As EventArgs) Handles SettingsButton.MouseEnter
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)
    End Sub

    Private Sub SettingsButtonn_MouseLeave(sender As Object, e As EventArgs) Handles SettingsButton.MouseLeave
        sender.Backcolor = Color.FromArgb(233, 25, 104)
        sender.ForeColor = Color.White
    End Sub

    Private Sub loadInventoryList()
        Dim command As New OleDb.OleDbCommand("SELECT * FROM inventory", connection)
        Dim reader = command.ExecuteReader()
        While reader.Read()
            orders(reader.GetString(0))(2) += Val(reader.GetString(3))
        End While
        For Each child In FlowLayoutPanel1.Controls
            For Each childchild In child.Controls
                If TypeOf childchild Is Label Then
                    childchild.Text = "Left: " & Double.Parse(orders(child.Text)(2))
                ElseIf TypeOf childchild Is TextBox Then
                    childchild.Text = "0"
                ElseIf childchild.Text.Equals("-") And TypeOf childchild Is Button Then
                    childchild.Enabled = False
                    childchild.Visible = False
                ElseIf childchild.Text.Equals("+") And TypeOf childchild Is Button Then
                    If orders(child.Text)(2) = 0 Then
                        childchild.Enabled = False
                        childchild.Visible = False
                    Else
                        childchild.Enabled = True
                        childchild.Visible = True
                    End If
                End If
            Next
        Next
    End Sub

    Private Sub removeOrder(sender As Object, e As EventArgs) Handles porkCutRemove.Click, menudoCutRemove.Click, adoboCutRmove.Click, leanPorkRemove.Click, porkChopRemove.Click, tenderloinRemove.Click, bellySliceRemove.Click, sinigangRemove.Click, pataRemove.Click, baconRemove.Click, logganisaRemove.Click, tocinoRemove.Click, cHotdogRemove.Click, hotdogRemove.Click, cPataRemove.Click, fiestaRemove.Click, wEggRemove.Click, bEggRemove.Click, bSteakRemove.Click, leanBeefRemove.Click, calderetaRemove.Click, oxRemove.Click, shortRibsRemove.Click, shankRemove.Click, cSkinRemove.Click, neckRemove.Click, tteriyakiRemove.Click, tSweetRemove.Click, mSpicyRemove.Click, mSweetRemove.Click, thighRemove.Click, drumRemove.Click, wingRemove.Click, chickenRemove.Click, sticksRemove.Click, nuggetsRemove.Click, legRemove.Click, filletRemove.Click
        If orders(sender.Parent.Text)(0) - 1 >= 0 Then
            For Each child In sender.Parent.Controls
                If TypeOf child Is TextBox Then
                    child.Text = Val(child.Text) - 1
                    If Val(child.Text) = 0 Then
                        sender.Enabled = False
                        sender.Visible = False
                    End If
                End If
                If child.Text.Equals("+") Then
                    child.Enabled = True
                    child.Visible = True
                End If
            Next
            orders(sender.Parent.Text)(0) -= 1
            updateOrders()
        End If
    End Sub

    Private Sub addOrder(sender As Object, e As EventArgs) Handles porkCutAdd.Click, menudoCutAdd.Click, adoboCutAdd.Click, leanPorkAdd.Click, porkChopAdd.Click, TenderloinAdd.Click, bellySliceAdd.Click, sinigangAdd.Click, pataAdd.Click, baconAdd.Click, longganisaAdd.Click, tocinoAdd.Click, cHotdogAdd.Click, hotdogAdd.Click, cPataAdd.Click, fiestaAdd.Click, wEggAdd.Click, bEggAdd.Click, bSteakAdd.Click, leanBeefAdd.Click, calderetaAdd.Click, oxAdd.Click, shortRibsAdd.Click, shankAdd.Click, cSkinAdd.Click, neckAdd.Click, tteriyakiAdd.Click, tSweetAdd.Click, mSpicyAdd.Click, mSweetAdd.Click, thighAdd.Click, drumAdd.Click, wingAdd.Click, chickenAdd.Click, sticksAdd.Click, nuggetsAdd.Click, legAdd.Click, filletRemove.Click
        If orders(sender.Parent.Text)(2) >= orders(sender.Parent.Text)(0) + 1 Then
            For Each child In sender.Parent.Controls
                If TypeOf child Is TextBox Then
                    child.Text = Val(child.Text) + 1
                End If
                If Not child.Enabled Then
                    child.Enabled = True
                    child.Visible = True
                End If
            Next
            If orders(sender.Parent.Text)(2) = orders(sender.Parent.Text)(0) + 1 Then
                sender.Enabled = False
                sender.Visible = False
            End If
            orders(sender.Parent.Text)(0) += 1
            updateOrders()
        End If
    End Sub

    Private Sub dashboardSearch_TextChanged(sender As Object, e As EventArgs) Handles dashboardSearch.TextChanged
        For Each child In FlowLayoutPanel1.Controls
            If child.Text.Contains(dashboardSearch.Text) Then
                child.Visible = True
            Else
                child.Visible = False
            End If
        Next
    End Sub

    Private Sub commitSale_Click(sender As Object, e As EventArgs) Handles commitSale.Click
        If Val(cashField.Text) >= Val(totalField.Text) Then
            For Each item In orderList.Items
                Dim command As New OleDb.OleDbCommand("INSERT INTO [transactions] ([user], [date/time], [item], [amount], [quantity]) VALUES ('" & login.user & "', '" + Format(Now(), "short Date") + "', '" + item.SubItems(0).Text + "', '" + item.SubItems(2).Text + "', '" + item.SubItems(1).Text + "')", connection)
                command.ExecuteNonQuery()


                command.CommandText = "SELECT * FROM [inventory] WHERE [Product Name]='" + item.SubItems(0).Text + "';"
                Dim reader = command.ExecuteReader()
                reader.Read()
                Dim current = Val(reader.GetString(3))
                reader.Close()


                Dim quantity = current - Val(item.SubItems(1).Text)
                Dim asdasda = "UPDATE [inventory] SET [DateModified]='" & Format(Now(), "short Date") & "', [quantity]='" & quantity.ToString & "' WHERE [Product Name]='" + item.SubItems(0).Text & "';"
                command.CommandText = "UPDATE [inventory] SET [DateModified]='" & Format(Now(), "short Date") & "', [quantity]='" & quantity.ToString & "' WHERE [Product Name]='" + item.SubItems(0).Text & "';"
                command.ExecuteNonQuery()
                orderList.Items.Clear()
                totalField.Clear()
                cashField.Clear()
            Next
            MessageBox.Show("SALE RECORDED :)")
        End If
    End Sub

    Private Sub saveChanges_Click(sender As Object, e As EventArgs) Handles saveChanges.Click
        inventorySearch.Clear()
        For Each row In inventoryGridView.Rows
            Dim commands As New OleDb.OleDbCommand("UPDATE [inventory] SET [DateModified]='" & Format(Now(), "short Date") & "', [Quantity]='" & row.Cells(2).Value & "' WHERE [Product Name]='" & row.Cells(0).Value & "';", connection)
            commands.ExecuteNonQuery()
        Next
        MessageBox.Show("Updates Successful")
    End Sub

    Private Sub inventorySearch_TextChanged(sender As Object, e As EventArgs) Handles inventorySearch.TextChanged
        Dim command As New OleDb.OleDbCommand("SELECT [Product Name], [Price], [Quantity] FROM inventory WHERE [Product Name] like '%" & inventorySearch.Text & "%'", connection)
        Dim table = New DataTable
        table.Load(command.ExecuteReader)
        inventoryGridView.DataSource = table
    End Sub

    Private Sub historySearch_TextChanged(sender As Object, e As EventArgs) Handles historySearch.TextChanged
        Dim command As New OleDb.OleDbCommand("SELECT * FROM transactions WHERE [user] like '%" & historySearch.Text & "%' OR [date/time] like '%" & historySearch.Text & "%' [item] like '%" & historySearch.Text & "%' [amount] like '%" & historySearch.Text & "%'", connection)
        Dim table = New DataTable
        table.Load(command.ExecuteReader)
        historyGridView.DataSource = table
    End Sub

    Private Sub userSelection_SelectedIndexChanged(sender As Object, e As EventArgs) Handles userSelection.SelectedIndexChanged
        If userSelection.SelectedIndex >= 0 Then
            clearUserButton.Visible = True
            saveUserChangesButton.Visible = True
            addUserButton.Visible = False

            Dim command As New OleDb.OleDbCommand("SELECT * FROM users WHERE [Username]='" & userSelection.SelectedItem.ToString & "'", connection)
            Dim reader = command.ExecuteReader()
            reader.Read()
            usernameField.Text = reader.GetString(1)
            passwordField.Text = reader.GetString(2)
        End If
    End Sub

    Private Sub clearUserButton_Click(sender As Object, e As EventArgs) Handles clearUserButton.Click
        userSelection.ClearSelected()
        addUserButton.Visible = True
        saveUserChangesButton.Visible = False
        sender.Visible = False
        usernameField.Clear()
        passwordField.Clear()
    End Sub

    Private Sub addUserButton_Click(sender As Object, e As EventArgs) Handles addUserButton.Click
        If usernameField.TextLength > 0 And passwordField.TextLength > 0 Then
            Dim command As New OleDb.OleDbCommand("INSERT INTO [users] VALUES ('" & usernameField.Text & "' , '" & passwordField.Text & "'", connection)
            command.ExecuteNonQuery()
            MessageBox.Show("User Added Succesfully")
            SettingsButton.PerformClick()
        End If
    End Sub

    Private Sub saveUserChangesButton_Click(sender As Object, e As EventArgs) Handles saveUserChangesButton.Click
        If usernameField.TextLength > 0 And passwordField.TextLength > 0 Then
            Dim command As New OleDb.OleDbCommand("UPDATE [users] SET [Username]='" & usernameField.Text & "', [Password]='" & passwordField.Text & "' WHERE [Username]='" & userSelection.SelectedItem.ToString & "'", connection)
            command.ExecuteNonQuery()
            MessageBox.Show("User Detauls Updated Succesfully")
            SettingsButton.PerformClick()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Hide()
            login.Show()
        End If
    End Sub

    Private Sub Button4_MouseEnter(sender As Object, e As EventArgs) Handles Button4.MouseEnter
        sender.Backcolor = Color.White
        sender.ForeColor = Color.FromArgb(233, 25, 104)
    End Sub

    Private Sub Button4_MouseLeave(sender As Object, e As EventArgs) Handles Button4.MouseLeave
        sender.Backcolor = Color.FromArgb(233, 25, 104)
        sender.ForeColor = Color.White
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirmation", MessageBoxButtons.YesNo)
        If result = DialogResult.Yes Then
            Hide()
            login.Show()
        End If
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        salesButton.PerformClick()
        If Not login.user = "admin" Then
            SettingsButton.Visible = False
        Else
            SettingsButton.Visible = True
        End If
    End Sub
End Class
