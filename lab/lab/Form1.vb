' Option Strict and Option Explicit help prevent common coding errors.
Option Strict On
Option Explicit On

Imports System.Data.OleDb ' Import the namespace for OLE DB data access
Imports System.IO          ' Import for file operations (checking if picture file exists)
Imports System.Drawing     ' Import for Image class

Public Class Form1

    ' --- Database and Data Handling Objects ---
    ' Connection string for Microsoft Access (.accdb).
    ' Adjust the Data Source path to where you placed your ColoringCompetition.accdb file.
    ' This path assumes the database file is in the same directory as the application's executable (bin\Debug).
    ' The |DataDirectory| substitution requires AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath)
    Private dbConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\ColoringCompetition.accdb;Persist Security Info=False;"
    ' For a fixed path (e.g., in your project root or a subfolder):
    ' Private dbConnectionString As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\YourProjectFolder\ColoringCompetition.accdb;Persist Security Info=False;"

    Private dbConnection As OleDbConnection ' Represents a connection to the data source
    Private dbDataAdapter As OleDbDataAdapter ' Represents a set of data commands and a database connection
    Private dtSubmissions As DataTable ' Holds data from the database in memory
    Private bsSubmissions As BindingSource ' Links controls to the DataTable and manages currency
    Private dbCommandBuilder As OleDbCommandBuilder ' Automatically generates SQL commands (Insert, Update, Delete)

    ' --- Form Load: Setup database connection and data binding ---
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ' Set DataDirectory to the application's startup path (bin\Debug)
        ' This makes the |DataDirectory| placeholder work for finding the DB file
        ' NOTE: When deploying, the DB file might need to be in the same directory as the .exe
        AppDomain.CurrentDomain.SetData("DataDirectory", Application.StartupPath)

        Try
            ' Initialize database objects
            dbConnection = New OleDbConnection(dbConnectionString)
            ' Ensure the table name 'Submissions' matches the table you created in Access
            dbDataAdapter = New OleDbDataAdapter("SELECT * FROM Submissions", dbConnection)
            dtSubmissions = New DataTable()
            bsSubmissions = New BindingSource()

            ' Fill the DataTable with data from the database
            dbDataAdapter.Fill(dtSubmissions)

            ' Set the BindingSource's DataSource to the DataTable
            bsSubmissions.DataSource = dtSubmissions

            ' Automatically generate Insert, Update, Delete commands based on the SELECT statement
            ' This works because the SELECT statement is simple and includes the primary key (SubmissionID)
            dbCommandBuilder = New OleDbCommandBuilder(dbDataAdapter)

            ' --- Data Binding: Link form controls to the BindingSource ---
            ' Use the DataBindings.Add method for each control property you want to bind
            ' DataSourceUpdateMode.OnPropertyChanged ensures changes are pushed to the DataRow as you type/change value

            ' TextBoxes
            txtParticipantName.DataBindings.Add("Text", bsSubmissions, "ParticipantName", True, DataSourceUpdateMode.OnPropertyChanged)
            txtJuryName.DataBindings.Add("Text", bsSubmissions, "JuryName", True, DataSourceUpdateMode.OnPropertyChanged)
            ' TotalMark is calculated, but we bind it so it's saved with the record
            txtTotalMark.DataBindings.Add("Text", bsSubmissions, "TotalMark", True, DataSourceUpdateMode.OnPropertyChanged)
            txtPicturePath.DataBindings.Add("Text", bsSubmissions, "PicturePath", True, DataSourceUpdateMode.OnPropertyChanged) ' Bind path TextBox

            ' NumericUpDowns (Bind the Value property)
            nudColorScheme.DataBindings.Add("Value", bsSubmissions, "Mark_ColorScheme", True, DataSourceUpdateMode.OnPropertyChanged)
            nudAttractiveness.DataBindings.Add("Value", bsSubmissions, "Mark_Attractiveness", True, DataSourceUpdateMode.OnPropertyChanged)
            nudAccuracy.DataBindings.Add("Value", bsSubmissions, "Mark_Accuracy", True, DataSourceUpdateMode.OnPropertyChanged)
            nudCreativity.DataBindings.Add("Value", bsSubmissions, "Mark_Creativity", True, DataSourceUpdateMode.OnPropertyChanged)

            ' PictureBox (Bind the ImageLocation property to the PicturePath)
            ' This binding tells the PictureBox to load the image from the path specified in the DataRow column
            picSubmission.DataBindings.Add("ImageLocation", bsSubmissions, "PicturePath", True, DataSourceUpdateMode.OnPropertyChanged)
            picSubmission.ErrorImage = Nothing ' Set an image to show if loading fails, or Nothing to just show blank


            ' --- Event Handlers ---
            ' Add a handler to update navigation button states when the position changes
            AddHandler bsSubmissions.PositionChanged, AddressOf bsSubmissions_PositionChanged
            ' Add a handler to ensure PictureBox updates when the current record changes (can be redundant with binding but good check)
            AddHandler bsSubmissions.CurrentItemChanged, AddressOf bsSubmissions_CurrentItemChanged


            ' Call the PositionChanged handler initially to set button states and display the first record (if any)
            bsSubmissions_PositionChanged(bsSubmissions, EventArgs.Empty)


        Catch ex As Exception
            ' Display a clear error message if database loading fails
            MessageBox.Show("Error loading data from database: " & ex.Message & ControlChars.CrLf & "Please ensure 'ColoringCompetition.accdb' is in the application's folder and the Access Database Engine is installed.", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Disable controls if data loading fails so the user can't interact with invalid data
            EnableControls(False)
        End Try
    End Sub

    ' --- BindingSource Event Handler: Update Picture Box when record changes ---
    ' This handler provides a safety net or alternative way to load the picture,
    ' though the ImageLocation binding often suffices. We'll use it to explicitly clear
    ' the image if the path is invalid or empty.
    Private Sub bsSubmissions_CurrentItemChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' This event fires when the binding source moves to a different record
        Try
            If bsSubmissions.Current IsNot Nothing Then
                Dim drv As DataRowView = CType(bsSubmissions.Current, DataRowView)
                Dim picturePath As Object = drv("PicturePath") ' Get the value as Object to check for DBNull

                ' Check if the value is DBNull or an empty string
                If picturePath Is DBNull.Value OrElse String.IsNullOrEmpty(picturePath.ToString()) Then
                    picSubmission.Image = Nothing ' Clear the picture box
                    txtPicturePath.Clear() ' Ensure path textbox is clear
                Else
                    Dim actualPath As String = picturePath.ToString()
                    ' Check if the file actually exists before attempting to load
                    If System.IO.File.Exists(actualPath) Then
                        ' The ImageLocation binding should handle loading, but if it fails,
                        ' we could potentially add explicit loading here with better error handling.
                        ' For now, rely on the binding and ensure the textbox/picture is correct.
                        txtPicturePath.Text = actualPath ' Ensure path textbox is correct
                        ' Re-trigger the ImageLocation binding update if needed (often not necessary, but can help)
                        picSubmission.DataBindings("ImageLocation").ReadValue()
                    Else
                        ' File does not exist at the path in the database
                        ' FIX: Replace interpolated string with String.Format
                        MessageBox.Show(String.Format("Picture file not found at: {0}", actualPath), "Picture Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        picSubmission.Image = Nothing ' Clear the picture box
                        ' Optional: You might want to clear the path in the DB or show a placeholder image
                        ' drv("PicturePath") = DBNull.Value ' Be cautious doing this automatically
                        txtPicturePath.Clear() ' Clear the path textbox to reflect status
                    End If
                End If
            Else
                ' If there's no current item (e.g., after deleting the last record), clear the picture box and path textbox
                picSubmission.Image = Nothing
                txtPicturePath.Clear()
            End If

        Catch ex As Exception
            ' Handle errors during image loading (e.g., invalid image format, permission issues)
            MessageBox.Show("Error loading picture: " & ex.Message, "Picture Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            picSubmission.Image = Nothing ' Clear the invalid image
            txtPicturePath.Clear() ' Clear the path textbox if there was an error loading
        End Try
    End Sub


    ' --- BindingSource Event Handler: Update Navigation Button States ---
    Private Sub bsSubmissions_PositionChanged(ByVal sender As Object, ByVal e As EventArgs)
        ' Enable/disable navigation buttons based on the current position
        btnFirst.Enabled = bsSubmissions.Position > 0
        btnPrevious.Enabled = bsSubmissions.Position > 0
        btnNext.Enabled = bsSubmissions.Position < bsSubmissions.Count - 1
        btnLast.Enabled = bsSubmissions.Position < bsSubmissions.Count - 1

        ' Enable/disable Delete button only if there are records and not on a "New" unsaved record
        ' Check bsSubmissions.Current is not null before accessing its properties
        btnDelete.Enabled = bsSubmissions.Count > 0 AndAlso _
                            bsSubmissions.Current IsNot Nothing AndAlso _
                            Not CType(bsSubmissions.Current, DataRowView).Row.RowState = DataRowState.Added

        ' The CurrentItemChanged event is also called after PositionChanged,
        ' which should handle the picture display update.

    End Sub

    ' --- Button Click Handlers ---

    ' Calculate Total Mark
    Private Sub btnCalculateTotal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCalculateTotal.Click
        Try
            ' Ensure a record is selected or being added
            If bsSubmissions.Current Is Nothing Then
                MessageBox.Show("Cannot calculate total. No record selected or being added.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Get marks from NumericUpDown controls
            Dim colorSchemeMark As Integer = CInt(nudColorScheme.Value)
            Dim attractivenessMark As Integer = CInt(nudAttractiveness.Value)
            Dim accuracyMark As Integer = CInt(nudAccuracy.Value)
            Dim creativityMark As Integer = CInt(nudCreativity.Value)

            ' Validate ranges (NumericUpDown does this visually, but code reinforces)
            If colorSchemeMark < 0 OrElse colorSchemeMark > 20 OrElse
               attractivenessMark < 0 OrElse attractivenessMark > 30 OrElse
               accuracyMark < 0 OrElse accuracyMark > 30 OrElse
               creativityMark < 0 OrElse creativityMark > 20 Then
                MessageBox.Show("Please enter marks within the specified ranges.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If


            ' Calculate total
            Dim totalMark As Integer = colorSchemeMark + attractivenessMark + accuracyMark + creativityMark

            ' Display total in the read-only TextBox
            txtTotalMark.Text = totalMark.ToString()

            ' Update the TotalMark column in the current DataRow
            ' Use CType to get the underlying DataRowView and then access its column
            Dim currentRowView As DataRowView = CType(bsSubmissions.Current, DataRowView)
            ' Check if column exists before assigning (good practice, though expected here)
            If currentRowView.Row.Table.Columns.Contains("TotalMark") Then
                currentRowView("TotalMark") = totalMark ' Set the value in the DataRow
            Else
                ' Handle case where column is missing (e.g., database structure mismatch)
                MessageBox.Show("Error: 'TotalMark' column not found in data table.", "Database Structure Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If


        Catch ex As Exception
            MessageBox.Show("Error calculating total: " & ex.Message, "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Browse for Submission Picture
    Private Sub btnBrowsePicture_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBrowsePicture.Click
        ' Ensure a record is selected or being added before browsing
        If bsSubmissions.Current Is Nothing Then
            MessageBox.Show("Create a new entry or select an existing one before browsing for a picture.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End If

        ' Configure the OpenFileDialog
        ofdPicture.Title = "Select Submission Picture"
        ' Filter already set in designer: Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif
        ' ofdPicture.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
        ofdPicture.FilterIndex = 1
        ofdPicture.RestoreDirectory = True ' Remember the last opened directory

        ' Show the dialog and check if the user selected a file
        If ofdPicture.ShowDialog() = DialogResult.OK Then
            Dim selectedFilePath As String = ofdPicture.FileName

            Try
                ' Check if the selected file actually exists (should always be true if dialog returned OK, but good practice)
                If System.IO.File.Exists(selectedFilePath) Then
                    ' Display the selected file path in the TextBox
                    txtPicturePath.Text = selectedFilePath

                    ' Load the selected image into the PictureBox
                    ' Using FromFile can potentially lock the file. For simple applications, it's often fine.
                    ' For more robust applications, consider loading into a MemoryStream and then into Image.FromStream.
                    ' Dispose of the old image if necessary before loading a new one to prevent memory leaks
                    If picSubmission.Image IsNot Nothing Then
                        picSubmission.Image.Dispose()
                    End If
                    picSubmission.Image = Image.FromFile(selectedFilePath)

                    ' Update the PicturePath column in the current DataRow
                    Dim currentRowView As DataRowView = CType(bsSubmissions.Current, DataRowView)
                    ' Check if column exists
                    If currentRowView.Row.Table.Columns.Contains("PicturePath") Then
                        currentRowView("PicturePath") = selectedFilePath
                    Else
                        MessageBox.Show("Error: 'PicturePath' column not found in data table.", "Database Structure Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        ' Clear controls if column missing
                        txtPicturePath.Clear()
                        If picSubmission.Image IsNot Nothing Then
                            picSubmission.Image.Dispose()
                            picSubmission.Image = Nothing
                        End If
                    End If


                Else
                    MessageBox.Show("Selected file not found!", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    txtPicturePath.Clear()
                    ' Dispose old image before setting to Nothing
                    If picSubmission.Image IsNot Nothing Then
                        picSubmission.Image.Dispose()
                    End If
                    picSubmission.Image = Nothing
                    If bsSubmissions.Current IsNot Nothing Then
                        Dim currentRowView = CType(bsSubmissions.Current, DataRowView)
                        If currentRowView.Row.Table.Columns.Contains("PicturePath") Then
                            currentRowView("PicturePath") = DBNull.Value ' Clear path in DataRow
                        End If
                    End If
                End If

            Catch ex As Exception
                MessageBox.Show("Error loading picture: " & ex.Message, "Picture Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPicturePath.Clear() ' Clear the path if loading failed
                ' Dispose old image before setting to Nothing
                If picSubmission.Image IsNot Nothing Then
                    picSubmission.Image.Dispose()
                End If
                picSubmission.Image = Nothing ' Clear the picture box
                If bsSubmissions.Current IsNot Nothing Then
                    Dim currentRowView = CType(bsSubmissions.Current, DataRowView)
                    If currentRowView.Row.Table.Columns.Contains("PicturePath") Then
                        currentRowView("PicturePath") = DBNull.Value ' Store null in DB if error
                    End If
                End If
            End Try
        End If
    End Sub


    ' Save (Create/Update)
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click
        Try
            ' Ensure there's a record to save and it's not null (e.g., after deleting the last one)
            If bsSubmissions.Current Is Nothing Then
                MessageBox.Show("No current record to save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Validate basic required fields (Add more robust validation as needed)
            If txtParticipantName.Text.Trim() = "" Or txtJuryName.Text.Trim() = "" Then
                MessageBox.Show("Participant Name and Jury Name are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                ' Prevent saving by not calling EndEdit and Update
                Return
            End If

            ' Ensure changes from bound controls are pushed into the DataTable row
            ' This is crucial before saving, especially if DataSourceUpdateMode is not Always
            bsSubmissions.EndEdit()

            ' Use the DataAdapter to update the database with changes from the DataTable
            ' The CommandBuilder automatically provides the necessary INSERT, UPDATE, DELETE commands
            Dim rowsAffected As Integer = dbDataAdapter.Update(dtSubmissions)

            ' Commit changes to the DataTable (optional but good practice after successful update)
            ' This clears the RowState flags (Added, Modified, Deleted)
            dtSubmissions.AcceptChanges()

            MessageBox.Show("Changes saved successfully!", "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Refresh navigation button states (implicitly called by PositionChanged after AcceptChanges)
            ' Also explicitly call the CurrentItemChanged handler to ensure picture display is correct after saving
            bsSubmissions_PositionChanged(bsSubmissions, EventArgs.Empty)


        Catch ex As Exception
            ' Provide specific error messages for database errors
            Dim errorMessage As String = "Error saving data: " & ex.Message
            If TypeOf ex Is OleDbException Then
                errorMessage &= ControlChars.CrLf & "Database Error Code: " & CType(ex, OleDbException).ErrorCode.ToString()
            End If
            errorMessage &= ControlChars.CrLf & "Please ensure you have write access to the database file."
            MessageBox.Show(errorMessage, "Database Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            dtSubmissions.RejectChanges() ' Discard pending changes in DataTable if save failed
            ' If saving a new record failed, RejectChanges removes the new row.
            ' This might leave the form blank, which is intended if the save failed.
            ' Need to manually refresh UI after RejectChanges if it was a new row
            If bsSubmissions.Count > 0 Then
                bsSubmissions.ResetCurrentItem() ' Refresh current item display
            Else
                ClearControls() ' If RejectChanges made the table empty
            End If

        End Try
    End Sub

    ' New Entry
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew.Click
        ' Check if the current record has unsaved changes before adding a new one
        ' If it's a new record that hasn't been saved yet, let the user know
        ' Also check for changes on an existing row
        Dim changes As DataTable = dtSubmissions.GetChanges()

        If bsSubmissions.Current IsNot Nothing AndAlso CType(bsSubmissions.Current, DataRowView).Row.RowState = DataRowState.Added Then
            MessageBox.Show("You are already adding a new entry. Save or discard changes before adding another.", "Unsaved New Entry", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return ' Don't add another new row
        End If

        ' Check for changes on an existing row before adding a new one
        If changes IsNot Nothing Then
            Dim saveResult As DialogResult = MessageBox.Show("You have unsaved changes. Do you want to save them before creating a new entry?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If saveResult = DialogResult.Yes Then
                Try
                    bsSubmissions.EndEdit()
                    dbDataAdapter.Update(dtSubmissions)
                    dtSubmissions.AcceptChanges()
                Catch ex As Exception
                    MessageBox.Show("Error saving changes: " & ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    ' If save fails, don't proceed with adding a new row
                    Return
                End Try
            ElseIf saveResult = DialogResult.Cancel Then
                Return ' Cancel adding new entry
            Else ' Result is No - discard changes
                dtSubmissions.RejectChanges()
                ' Reset current item display after rejecting changes on an existing row
                If bsSubmissions.Count > 0 Then
                    bsSubmissions.ResetCurrentItem()
                End If
            End If
        End If


        ' Add a new blank row to the DataTable and move the BindingSource position to it
        bsSubmissions.AddNew()

        ' Clear the picture box and path text for the new entry
        ' Dispose old image before setting to Nothing
        If picSubmission.Image IsNot Nothing Then
            picSubmission.Image.Dispose()
        End If
        picSubmission.Image = Nothing
        txtPicturePath.Clear()

        ' Clear Total Mark for the new entry
        txtTotalMark.Clear()

        ' Reset NumericUpDowns to their minimum value (usually 0)
        nudColorScheme.Value = nudColorScheme.Minimum
        nudAttractiveness.Value = nudAttractiveness.Minimum
        nudAccuracy.Value = nudAccuracy.Minimum
        nudCreativity.Value = nudCreativity.Minimum


        ' Optionally set focus to the first input field
        txtParticipantName.Focus()

        ' Navigation buttons will be updated by PositionChanged event
    End Sub

    ' Delete Entry
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        ' Check if there is a record selected
        If bsSubmissions.Current IsNot Nothing Then
            Dim currentRowView As DataRowView = CType(bsSubmissions.Current, DataRowView)

            ' Check if it's a new unsaved record
            If currentRowView.Row.RowState = DataRowState.Added Then
                ' If it's a new record not yet saved, just cancel the add operation
                bsSubmissions.CancelEdit()
                ' Remove the row from the DataTable
                currentRowView.Row.Delete()
                ' Manually call AcceptChanges to remove the deleted row from the DataTable view
                ' Note: This is different from deleting a saved row. RejectChanges also works here.
                dtSubmissions.AcceptChanges() ' Or dtSubmissions.RejectChanges()

                MessageBox.Show("New entry discarded.", "Discarded", MessageBoxButtons.OK, MessageBoxIcon.Information)

                ' Re-bind or move to the last record if the table isn't empty
                If bsSubmissions.Count > 0 Then
                    bsSubmissions.MoveLast() ' Move to the last saved record
                Else
                    ' If the table is empty, clear controls
                    ClearControls()
                End If

                ' Update UI state
                bsSubmissions_PositionChanged(bsSubmissions, EventArgs.Empty)

                Return ' Exit the delete handler
            End If

            ' If it's an existing saved record, ask for confirmation
            Dim confirmResult As DialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If confirmResult = DialogResult.Yes Then
                Try
                    ' Mark the current row for deletion in the DataTable
                    ' RemoveCurrent calls EndEdit internally if the current item is being edited.
                    bsSubmissions.RemoveCurrent()

                    ' Use the DataAdapter to update the database (executes the DELETE command)
                    dbDataAdapter.Update(dtSubmissions)

                    ' Commit the deletion in the DataTable (removes the row from the DataTable)
                    dtSubmissions.AcceptChanges()

                    MessageBox.Show("Record deleted successfully!", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' The PositionChanged event handles updating navigation buttons and picture display
                    ' after the row is removed and the BindingSource position potentially changes.
                    ' If the table is now empty, PositionChanged will disable buttons.
                    ' We should manually clear controls if the table becomes empty.
                    If bsSubmissions.Count = 0 Then
                        ClearControls()
                    End If
                    ' No need to explicitly call PositionChanged or CurrentItemChanged here,
                    ' BindingSource.RemoveCurrent() and the subsequent update/AcceptChanges
                    ' will trigger PositionChanged if the position actually changes
                    ' (which it does unless you delete the last remaining row).


                Catch ex As Exception
                    MessageBox.Show("Error deleting record: " & ex.Message, "Database Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    dtSubmissions.RejectChanges() ' Discard the pending deletion in DataTable if database update fails
                    ' If the deletion failed, the row is still in the DataTable with RowState.Deleted.
                    ' RejectChanges reverts it to its original state (e.g., Unchanged).
                    bsSubmissions.ResetCurrentItem() ' Refresh controls for the item that failed to delete
                    bsSubmissions_PositionChanged(bsSubmissions, EventArgs.Empty) ' Ensure button states are correct after failure
                End Try
            End If
        Else
            MessageBox.Show("No record selected to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    ' --- Navigation Buttons ---
    Private Sub btnFirst_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFirst.Click
        ' End any ongoing edit before moving
        bsSubmissions.EndEdit()
        bsSubmissions.MoveFirst() ' Move to the first record
        ' PositionChanged and CurrentItemChanged events will handle UI updates
    End Sub

    Private Sub btnPrevious_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnPrevious.Click
        ' End any ongoing edit before moving
        bsSubmissions.EndEdit()
        bsSubmissions.MovePrevious() ' Move to the previous record
        ' PositionChanged and CurrentItemChanged events will handle UI updates
    End Sub

    Private Sub btnNext_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNext.Click
        ' End any ongoing edit before moving
        bsSubmissions.EndEdit()
        bsSubmissions.MoveNext() ' Move to the next record
        ' PositionChanged and CurrentItemChanged events will handle UI updates
    End Sub

    Private Sub btnLast_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLast.Click
        ' End any ongoing edit before moving
        bsSubmissions.EndEdit()
        bsSubmissions.MoveLast() ' Move to the last record
        ' PositionChanged and CurrentItemChanged events will handle UI updates
    End Sub

    ' --- Helper Method to Enable/Disable Controls ---
    Private Sub EnableControls(ByVal enable As Boolean)
        ' Enable/Disable primary input and action controls
        txtParticipantName.Enabled = enable
        txtJuryName.Enabled = enable
        nudColorScheme.Enabled = enable
        nudAttractiveness.Enabled = enable
        nudAccuracy.Enabled = enable
        nudCreativity.Enabled = enable
        btnBrowsePicture.Enabled = enable
        btnCalculateTotal.Enabled = enable
        btnSave.Enabled = enable
        btnNew.Enabled = enable ' Keep New enabled unless DB failed completely
        ' btnDelete.Enabled state is managed by PositionChanged

        ' Navigation button enabled/disabled state is managed by bsSubmissions_PositionChanged
        ' But we might want to disable them all if the database failed to load initially
        If Not enable Then
            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            btnNext.Enabled = False
            btnLast.Enabled = False
            btnDelete.Enabled = False ' Ensure delete is off too
            btnNew.Enabled = False ' Can't add new if DB connection is broken
        End If


        ' txtTotalMark and txtPicturePath remain read-only but their enabled state can be set
        txtTotalMark.Enabled = enable
        txtPicturePath.Enabled = enable ' Although read-only, might grey it out
        picSubmission.Enabled = enable ' Maybe enable/disable picture box interaction?
    End Sub

    ' --- Helper method to clear controls when there's no data ---
    Private Sub ClearControls()
        txtParticipantName.Clear()
        txtJuryName.Clear()
        txtTotalMark.Clear()
        txtPicturePath.Clear()
        nudColorScheme.Value = nudColorScheme.Minimum
        nudAttractiveness.Value = nudAttractiveness.Minimum
        nudAccuracy.Value = nudAccuracy.Minimum
        nudCreativity.Value = nudCreativity.Minimum
        ' Dispose old image before setting to Nothing
        If picSubmission.Image IsNot Nothing Then
            picSubmission.Image.Dispose()
        End If
        picSubmission.Image = Nothing

        ' Manually update button states when controls are cleared due to no data
        btnFirst.Enabled = False
        btnPrevious.Enabled = False
        btnNext.Enabled = False
        btnLast.Enabled = False
        btnDelete.Enabled = False
        ' btnNew should remain enabled to add the first record
        btnNew.Enabled = True

    End Sub


    ' --- Form Closing: Clean up database connection ---
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Attempt to save any pending changes before closing
        ' Use GetChanges() IsNot Nothing to check for unsaved changes
        If dtSubmissions IsNot Nothing AndAlso dtSubmissions.GetChanges() IsNot Nothing Then
            Dim saveResult As DialogResult = MessageBox.Show("You have unsaved changes. Do you want to save them before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning)
            If saveResult = DialogResult.Yes Then
                Try
                    bsSubmissions.EndEdit() ' Push final changes from current row
                    dbDataAdapter.Update(dtSubmissions)
                    dtSubmissions.AcceptChanges()
                Catch ex As Exception
                    MessageBox.Show("Error saving changes before closing: " & ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    e.Cancel = True ' Cancel closing if save fails
                    Return
                End Try
            ElseIf saveResult = DialogResult.Cancel Then
                e.Cancel = True ' Cancel closing
                Return
            Else ' Result is No - discard changes
                dtSubmissions.RejectChanges()
                ' No need to call AcceptChanges or manually clear controls here,
                ' we are closing anyway.
            End If
        End If

        ' Dispose database objects to release resources
        ' FIX: Replace ?.Dispose() with If IsNot Nothing Then Dispose()
        If dbDataAdapter IsNot Nothing Then
            dbDataAdapter.Dispose()
        End If
        If dbConnection IsNot Nothing Then
            dbConnection.Dispose()
        End If
        If dtSubmissions IsNot Nothing Then
            dtSubmissions.Dispose()
        End If
        If bsSubmissions IsNot Nothing Then
            bsSubmissions.Dispose()
        End If
        If dbCommandBuilder IsNot Nothing Then
            dbCommandBuilder.Dispose() ' Dispose the command builder too
        End If

        ' Dispose the image in the picture box to free resources
        If picSubmission.Image IsNot Nothing Then
            picSubmission.Image.Dispose()
            picSubmission.Image = Nothing
        End If

    End Sub

End Class