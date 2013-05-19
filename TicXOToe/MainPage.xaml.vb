Imports System.Windows.Media.Imaging

Partial Public Class MainPage
    Inherits PhoneApplicationPage

    Dim tictac As Char(,)
    Dim row, col As Integer
    Dim isVersusComp As Boolean = True
    Dim isPlayerX As Boolean = True
    Dim level As Integer = 1 '0,1,2 start at Medium
    Dim rand As New Random()
    Dim xobackground As String
    Dim oImg, xImg As String

    ' Constructor
    Public Sub New()
        InitializeComponent()

        xobackground = "Images/poolWater.png"
        oImg = "Images/poolO.png"
        xImg = "Images/poolX.png"

        InitializeBoard()
    End Sub

    Public Sub InitializeBoard()

        If (CType(Application.Current, App).gametype = 1) Then
            isVersusComp = True
        Else
            isVersusComp = False
        End If
        WinLabel.Visibility = Windows.Visibility.Collapsed
        ResetButton.Visibility = Windows.Visibility.Collapsed

        If (CType(Application.Current, App).gametype = 1) Then
            LevelButton.Visibility = Windows.Visibility.Visible
        Else
            LevelButton.Visibility = Windows.Visibility.Collapsed
        End If
        turnOnButtons()

        Pos1_1_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))
        Pos1_2_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))
        Pos1_3_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))
        Pos2_1_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))
        Pos2_2_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))
        Pos2_3_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))
        Pos3_1_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))
        Pos3_2_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))
        Pos3_3_Img.Source = New BitmapImage(New Uri(xobackground, UriKind.Relative))

        ReDim tictac(2, 2)

        For i = 0 To 2
            For j = 0 To 2
                tictac(i, j) = "-"
            Next
        Next

        ' isPlayerX = True

    End Sub

    Public Sub computerMove() '/* if no win or block make a move */

        Dim rowInd, colInd As Integer

        If (level = 2) Then
            If (tictac(2, 1) = "X" And tictac(1, 2) = "X" And tictac(2, 2) = "-") Then
                tictac(2, 2) = "O"
                Pos3_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(1, 1) = "-") Then
                tictac(1, 1) = "O"
                Pos2_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(0, 0) = "-" And (Not tictac(0, 2) = "X" Or Not tictac(2, 0) = "X")) Then
                tictac(0, 0) = "O"
                Pos1_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(2, 2) = "-" And (Not tictac(0, 2) = "X" Or Not tictac(2, 0) = "X")) Then
                tictac(2, 2) = "O"
                Pos3_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(2, 0) = "-" And (Not tictac(0, 0) = "X" Or Not tictac(2, 2) = "X")) Then
                tictac(2, 0) = "O"
                Pos3_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(0, 2) = "-" And (Not tictac(0, 0) = "X" Or Not tictac(2, 2) = "X")) Then
                tictac(0, 2) = "O"
                Pos1_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(1, 0) = "-") Then
                tictac(1, 0) = "O"
                Pos2_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(2, 1) = "-") Then
                tictac(2, 1) = "O"
                Pos3_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(1, 2) = "-") Then
                tictac(1, 2) = "O"
                Pos2_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))

            ElseIf (tictac(0, 1) = "-") Then
                tictac(0, 1) = "O"
                Pos1_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
            Else
                showWin("T")
            End If
        End If

        If (level = 0) Then
            If (isBoardFull()) Then
                showWin("T")
                Return
            End If

            rowInd = rand.Next(0, 3)
            colInd = rand.Next(0, 3)

            While (Not tictac(rowInd, colInd) = "-")
                rowInd = rand.Next(0, 3)
                colInd = rand.Next(0, 3)
            End While

            tictac(rowInd, colInd) = "O"
            makeMove(rowInd, colInd, "O")
        End If

        If (level = 1) Then
            If (isBoardFull()) Then
                showWin("T")
                Return
            End If
            If (winPossible("O")) Then
                Return
            ElseIf (winPossible("X")) Then
                Return
            Else
                rowInd = rand.Next(0, 3)
                colInd = rand.Next(0, 3)

                While (Not tictac(rowInd, colInd) = "-")
                    rowInd = rand.Next(0, 3)
                    colInd = rand.Next(0, 3)
                End While

                tictac(rowInd, colInd) = "O"
                makeMove(rowInd, colInd, "O")
            End If
        End If

    End Sub

    Private Sub turnOffButtons()
        Pos1_1.IsEnabled = False
        Pos1_2.IsEnabled = False
        Pos1_3.IsEnabled = False
        Pos2_1.IsEnabled = False
        Pos2_2.IsEnabled = False
        Pos2_3.IsEnabled = False
        Pos3_1.IsEnabled = False
        Pos3_2.IsEnabled = False
        Pos3_3.IsEnabled = False

    End Sub

    Private Sub turnOnButtons()
        Pos1_1.IsEnabled = True
        Pos1_2.IsEnabled = True
        Pos1_3.IsEnabled = True
        Pos2_1.IsEnabled = True
        Pos2_2.IsEnabled = True
        Pos2_3.IsEnabled = True
        Pos3_1.IsEnabled = True
        Pos3_2.IsEnabled = True
        Pos3_3.IsEnabled = True
    End Sub


    Private Sub showWin(ch As Char)

        Dim message As String

        turnOffButtons()
        WinLabel.Visibility = Windows.Visibility.Visible
        LevelButton.Visibility = Windows.Visibility.Collapsed
        ResetButton.Visibility = Windows.Visibility.Visible

        If (ch = "T") Then
            message = "Cat's Game"
        ElseIf (ch = "O") Then
            message = "O Wins"
        ElseIf (ch = "X") Then
            message = "X Wins"
        Else
            message = ""
        End If

        WinLabel.Text = message

        WinLabel.Visibility = Windows.Visibility.Visible
    End Sub

    Function checkIfWon(ByVal letter As Char) As Integer

        Dim i, j, winner As Integer
        winner = 0

        For i = 0 To 2
            For j = 0 To 2
                If (tictac(i, 0) = letter And tictac(i, 1) = letter And tictac(i, 2) = letter) Then
                    winner = 1
                End If
            Next
        Next

        For i = 0 To 2
            For j = 0 To 2
                If (tictac(0, j) = letter And tictac(1, j) = letter And tictac(2, j) = letter) Then
                    winner = 1
                End If
            Next
        Next

        For i = 0 To 2
            For j = 0 To 2
                If (tictac(0, 0) = letter And tictac(1, 1) = letter And tictac(2, 2) = letter) Then
                    winner = 1
                End If
            Next
        Next

        For i = 0 To 2
            For j = 0 To 2
                If (tictac(0, 2) = letter And tictac(1, 1) = letter And tictac(2, 0) = letter) Then
                    winner = 1
                End If
            Next
        Next

        Return (winner)
    End Function

    Function winPossible(ByVal letter As Char) As Integer

        Dim row, col As Integer

        For row = 0 To 2
            For col = 0 To 2

                If (tictac(row, col) = "-") Then
                    tictac(row, col) = letter

                    If (checkIfWon(letter) = 0) Then
                        tictac(row, col) = "-"
                    Else

                        tictac(row, col) = "O"

                        'block
                        If (row = 0 And col = 0) Then
                            Pos1_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        If (row = 0 And col = 1) Then
                            Pos1_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        If (row = 0 And col = 2) Then
                            Pos1_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        If (row = 1 And col = 0) Then
                            Pos2_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        If (row = 1 And col = 1) Then
                            Pos2_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        If (row = 1 And col = 2) Then
                            Pos2_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        If (row = 2 And col = 0) Then
                            Pos3_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        If (row = 2 And col = 1) Then
                            Pos3_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        If (row = 2 And col = 2) Then
                            Pos3_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                        End If
                        Return (1)
                    End If
                End If
            Next
        Next

        Return (0)
    End Function

    Private Sub GetUserMove(ByVal num As Integer)

        If (num = 11) Then
            row = 0
            col = 0
        ElseIf (num = 12) Then
            row = 0
            col = 1
        ElseIf (num = 13) Then
            row = 0
            col = 2
        ElseIf (num = 21) Then
            row = 1
            col = 0
        ElseIf (num = 22) Then
            row = 1
            col = 1
        ElseIf (num = 23) Then
            row = 1
            col = 2
        ElseIf (num = 31) Then
            row = 2
            col = 0
        ElseIf (num = 32) Then
            row = 2
            col = 1
        ElseIf (num = 33) Then
            row = 2
            col = 2
        End If

        If (Not tictac(row, col) = "-") Then
            Return
        End If

        If (isPlayerX And isVersusComp = False) Then
            makeMove(row, col, "X")
            tictac(row, col) = "X"  '/* make move for 'X' */

            If (checkIfWon("X") = 1) Then
                showWin("X")
            ElseIf (isBoardFull()) Then
                showWin("T")
            End If
            isPlayerX = False
        ElseIf (Not isPlayerX And isVersusComp = False) Then

            makeMove(row, col, "O")
            tictac(row, col) = "O"  ' /* make move for 'O' */
            If (checkIfWon("O") = 1) Then
                showWin("O")
            ElseIf (isBoardFull()) Then
                showWin("T")
            End If
            isPlayerX = True

        Else  '// versus computer

            makeMove(row, col, "X")
            tictac(row, col) = "X"  ' /* make move for 'X' */
        End If

        If (isVersusComp = True And level = 2) Then

            If (checkIfWon("X") = 1) Then
                showWin("X")
            ElseIf (winPossible("O") = 1) Then
                showWin("O")
            ElseIf (winPossible("X") = 0) Then
                ' /* if opponent does not have to be blocked make move */
                computerMove()
            End If

        ElseIf (isVersusComp = True And level = 0) Then

            If (checkIfWon("X") = 1) Then
                showWin("X")
            ElseIf (checkIfWon("O") = 1) Then
                showWin("O")
            Else
                computerMove()
                If (checkIfWon("O") = 1) Then
                    showWin("O")
                End If
            End If

        ElseIf (isVersusComp = True And level = 1) Then '//medium diff

            If (checkIfWon("X") = 1) Then
                showWin("X")
            ElseIf (checkIfWon("O") = 1) Then
                showWin("O")
            Else
                computerMove()
                If (checkIfWon("O") = 1) Then
                    showWin("O")
                End If
            End If

        End If

    End Sub

    Private Sub makeMove(ByVal row As Integer, ByVal col As Integer, ByVal xoChar As Char)

        If (xoChar = "O") Then

            If (row = 0) Then
                If (col = 0) Then
                    Pos1_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                ElseIf (col = 1) Then
                    Pos1_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                ElseIf (col = 2) Then
                    Pos1_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                End If
            ElseIf (row = 1) Then
                If (col = 0) Then
                    Pos2_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                ElseIf (col = 1) Then
                    Pos2_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                ElseIf (col = 2) Then
                    Pos2_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                End If
            ElseIf (row = 2) Then
                If (col = 0) Then
                    Pos3_1_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                ElseIf (col = 1) Then
                    Pos3_2_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                ElseIf (col = 2) Then
                    Pos3_3_Img.Source = New BitmapImage(New Uri(oImg, UriKind.Relative))
                End If
            End If

        ElseIf (xoChar = "X") Then

		   If (row = 0) Then
                If (col = 0) Then
                    Pos1_1_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                ElseIf (col = 1) Then
                    Pos1_2_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                ElseIf (col = 2) Then
                    Pos1_3_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                End If
            ElseIf (row = 1) Then
                If (col = 0) Then
                    Pos2_1_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                ElseIf (col = 1) Then
                    Pos2_2_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                ElseIf (col = 2) Then
                    Pos2_3_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                End If
            ElseIf (row = 2) Then
                If (col = 0) Then
                    Pos3_1_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                ElseIf (col = 1) Then
                    Pos3_2_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                ElseIf (col = 2) Then
                    Pos3_3_Img.Source = New BitmapImage(New Uri(xImg, UriKind.Relative))
                End If
            End If
        End If

    End Sub

    Private Function isBoardFull() As Boolean
        Dim isFull As Boolean = True

        For i = 0 To 2
            For j = 0 To 2
                If (tictac(i, j) = "-") Then
                    isFull = False
                End If
            Next
        Next

        Return isFull
    End Function


    ' Private Sub vsCompCeckbox_Checked(sender As Object, e As RoutedEventArgs) Handles vsCompCeckbox.Checked, vsCompCeckbox.Unchecked
    '      If vsCompCeckbox.IsChecked Then
    '          isVersusComp = True
    '          InitializeBoard()
    '      Else
    '          isVersusComp = False
    '           InitializeBoard()
    '       End If
    '   End Sub



    Private Sub Pos1_1_Click(sender As Object, e As RoutedEventArgs) Handles Pos1_1.Click
        GetUserMove(11)
    End Sub


    Private Sub Pos1_2_Click(sender As Object, e As RoutedEventArgs) Handles Pos1_2.Click
        GetUserMove(12)

    End Sub

    Private Sub Pos1_3_Click(sender As Object, e As RoutedEventArgs) Handles Pos1_3.Click
        GetUserMove(13)
    End Sub

    Private Sub Pos2_1_Click(sender As Object, e As RoutedEventArgs) Handles Pos2_1.Click
        GetUserMove(21)
    End Sub

    Private Sub Pos2_2_Click(sender As Object, e As RoutedEventArgs) Handles Pos2_2.Click
        GetUserMove(22)
    End Sub

    Private Sub Pos2_3_Click(sender As Object, e As RoutedEventArgs) Handles Pos2_3.Click
        GetUserMove(23)
    End Sub

    Private Sub Pos3_1_Click(sender As Object, e As RoutedEventArgs) Handles Pos3_1.Click
        GetUserMove(31)
    End Sub

    Private Sub Pos3_2_Click(sender As Object, e As RoutedEventArgs) Handles Pos3_2.Click
        GetUserMove(32)
    End Sub

    Private Sub Pos3_3_Click(sender As Object, e As RoutedEventArgs) Handles Pos3_3.Click
        GetUserMove(33)
    End Sub

    Private Sub ResetButton_Click(sender As Object, e As RoutedEventArgs) Handles ResetButton.Click
        InitializeBoard()
    End Sub

    Private Sub LevelButton_Click(sender As Object, e As RoutedEventArgs) Handles LevelButton.Click
        If (level = 1) Then
            level = 2
            LevelButton.Content = "Hard"
            InitializeBoard()
        ElseIf (level = 2) Then
            level = 0
            LevelButton.Content = "Easy"
            InitializeBoard()
        Else 'level =0 
            level = 1
            LevelButton.Content = "Medium"
            InitializeBoard()
        End If
    End Sub
End Class

