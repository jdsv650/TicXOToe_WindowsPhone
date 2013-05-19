Imports System.Windows.Media.Imaging
Imports System.Windows.Point

Partial Public Class Slingshot
    Inherits PhoneApplicationPage

    Dim dragTranslation As TranslateTransform

    Dim tictac As Char(,)
    Dim row, col As Integer
    Dim isPlayerX As Boolean = True
    Dim rand As New Random()
    Dim xobackground As String
    Dim oImg, xImg As String

    Dim beginY, endY As Integer
    Dim beginX, endX As Integer
    Dim beginTouch, endTouch As Point

    ' Constructor
    Public Sub New()
        InitializeComponent()

        turnOffButtons()
        '  bbImage.Visibility = Windows.Visibility.Collapsed

        xobackground = "Images/poolWaterWithXO.png"
        oImg = "Images/poolO.png"
        xImg = "Images/poolX.png"

        InitializeBoard()

        AddHandler StickmanImage.ManipulationDelta, AddressOf Me.Drag_ManipulationDelta
        dragTranslation = New TranslateTransform
        StickmanImage.RenderTransform = Me.dragTranslation

        ContentPanel.Width = 500
        ContentPanel.Height = 700
        
    End Sub

    Public Sub InitializeBoard()

        WinLabel.Visibility = Windows.Visibility.Collapsed
        ResetButton.Visibility = Windows.Visibility.Collapsed

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

        isPlayerX = True

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

        WinLabel.Visibility = Windows.Visibility.Visible
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

        If (isPlayerX) Then
            makeMove(row, col, "X")
            tictac(row, col) = "X"  '/* make move for 'X' */

            If (checkIfWon("X") = 1) Then
                showWin("X")
            ElseIf (isBoardFull()) Then
                showWin("T")
            End If
            isPlayerX = False
        ElseIf (Not isPlayerX) Then
            makeMove(row, col, "O")
            tictac(row, col) = "O"  ' /* make move for 'O' */
            If (checkIfWon("O") = 1) Then
                showWin("O")
            ElseIf (isBoardFull()) Then
                showWin("T")
            End If
            isPlayerX = True
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

    Private Sub ResetButton_Click(sender As Object, e As RoutedEventArgs) Handles ResetButton.Click
        InitializeBoard()
    End Sub


    Public Sub DoRotation()

        bbImage.Visibility = Windows.Visibility.Visible


        Dim duration As Duration = New Duration(TimeSpan.FromSeconds(2))
        Dim sb As Storyboard = New Storyboard

        sb.Duration = duration
        Dim da As DoubleAnimation = New DoubleAnimation

        da.Duration = duration
        sb.Children.Add(da)

        Dim rt As RotateTransform = New RotateTransform()

        Storyboard.SetTarget(da, rt)
        Storyboard.SetTargetProperty(da, New PropertyPath("Angle"))
      
        da.To = -540

        ' StickmanImage.RenderTransform = rt
        ' StickmanImage.RenderTransformOrigin = New Point(0.5, 0.5)

        sb.Begin()
    End Sub


    Public Sub DoTranslation()
        bbImage.Visibility = Windows.Visibility.Visible

        Dim dist As Integer = (beginY - endY)

        Dim sb As Storyboard = New Storyboard
        AddHandler sb.Completed, AddressOf mySB_Completed

        Dim duration As Duration = New Duration(TimeSpan.FromSeconds(1))
        sb.Duration = duration

        Dim da As DoubleAnimation = New DoubleAnimation
        da.Duration = duration
        sb.Children.Add(da)

        Dim tt As TranslateTransform = New TranslateTransform()

        Storyboard.SetTarget(da, tt)
        Storyboard.SetTargetProperty(da, New PropertyPath(TranslateTransform.YProperty))
        '  da.To = -480 '  -27 is high x at -520      -67 is low o at -480
        '  da.From = -200   //row 1 use 480 and  520

        da.To = (dist * 7)

        bbImage.RenderTransform = tt
        bbImage.RenderTransformOrigin = New Point(100, 100)

        '  sb.FillBehavior = FillBehavior.Stop
        sb.FillBehavior = FillBehavior.HoldEnd

        StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot1.png", UriKind.Relative))

        sb.Begin()

    End Sub

    Private Sub ContentPanel_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles ContentPanel.MouseLeftButtonDown

        beginTouch = e.GetPosition(StickmanImage)
        ' beginTouch = e.GetPosition(ContentPanel)

        beginY = beginTouch.Y
        beginX = beginTouch.X

    End Sub

    Private Sub ContentPanel_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles ContentPanel.MouseLeftButtonUp

        Dim p As Point

        p = e.GetPosition(StickmanImage)
        'p = e.GetPosition(ContentPanel)

        endY = p.Y
        endX = p.X

        If (endY > 500) Then

            ' Canvas.SetLeft(bbImage, Canvas.GetLeft(StickmanImage))

            '  e.GetPosition(StickmanImage).X


            DoTranslation()
            '   [self.view setUserInteractionEnabled:NO];

            ' bbImage.Visibility = Windows.Visibility.Visible
            ' [beanBagOutlet setFrame:CGRectMake(slingshotOutlet.center.x-20, slingshotOutlet.center.y-30, 45, 45)];
            '  [self animateBeanBag];
        End If


    End Sub

    Private Sub ContentPanel_MouseMove(sender As Object, e As MouseEventArgs) Handles ContentPanel.MouseMove

        Dim point As Point

        point = e.GetPosition(StickmanImage)
        'point = e.GetPosition(ContentPanel)

        If (point.Y >= 10 And point.Y < 20) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot1.png", UriKind.Relative))

        ElseIf (point.Y >= 20 And point.Y < 30) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot2.png", UriKind.Relative))

        ElseIf (point.Y >= 30 And point.Y < 40) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot3.png", UriKind.Relative))

        ElseIf (point.Y >= 40 And point.Y < 50) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot4.png", UriKind.Relative))

        ElseIf (point.Y >= 50 And point.Y < 60) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot5.png", UriKind.Relative))

        ElseIf (point.Y >= 60 And point.Y < 70) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot6.png", UriKind.Relative))

        ElseIf (point.Y >= 70 And point.Y < 80) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot7.png", UriKind.Relative))

        ElseIf (point.Y >= 80 And point.Y < 90) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot8.png", UriKind.Relative))

        ElseIf (point.Y >= 90 And point.Y < 100) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot9.png", UriKind.Relative))

        ElseIf (point.Y >= 100 And point.Y < 110) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot10.png", UriKind.Relative))

        ElseIf (point.Y >= 110 And point.Y < 120) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot10.png", UriKind.Relative))

        ElseIf (point.Y >= 120) Then

            StickmanImage.Source = New BitmapImage(New Uri("Images/SlingShots/slingshot10.png", UriKind.Relative))
            DoTranslation()
        End If



    End Sub


    Private Sub Drag_ManipulationDelta(ByVal sender As Object, ByVal e As ManipulationDeltaEventArgs)
        'dragTranslation.X = (dragTranslation.X + e.DeltaManipulation.Translation.X)
        dragTranslation.X = e.CumulativeManipulation.Translation.X

        bbImage.SetValue(Canvas.LeftProperty, dragTranslation.X + 200)
        StickmanImage.SetValue(Canvas.LeftProperty, dragTranslation.X) '+200



    End Sub


    Private Sub mySB_Completed(sender As Object, e As System.EventArgs)

        updateButton()
        bbImage.Visibility = Windows.Visibility.Collapsed

        '    If (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos1_1) And
        '          Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos1_1) + 85) Then
        '  Pos1_1_Img.Source = New BitmapImage(New Uri("Images/stickmen_2.png", UriKind.Relative))
        '   End If

        '   If (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos1_2) And
        '   Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos1_2) + 85) Then
        ' Pos1_2_Img.Source = New BitmapImage(New Uri("Images/stickmen_2.png", UriKind.Relative))
        '   End If

        '  If (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos2_1) And
        '   Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos2_1) + 85) Then
        ' Pos2_1_Img.Source = New BitmapImage(New Uri("Images/stickmen_2.png", UriKind.Relative))
        '    End If

    End Sub

    Private Sub updateButton()

        Dim gt As GeneralTransform = ContentPanel.TransformToVisual(bbImage)
        Dim currentPosBB As Point = gt.Transform(New Point(0, 0))
        ' Dim gt2 As GeneralTransform = TransformToVisual(bbImage)
        '  Dim currentPosBB As Point = gt.Transform(New Point(0, 0))

        If (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos1_1) And
            Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos1_1) + 85) Then 'And
            'in 11 bottom half
            If (currentPosBB.Y > (Canvas.GetTop(Pos1_1) - 70)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "O"
                    Pos1_1_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "-"
                    Pos1_1_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "X"
                    Pos1_1_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))
                End If

                'in 11 top half
            ElseIf (currentPosBB.Y >= (Canvas.GetTop(Pos1_1) - 100)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "-"
                    Pos1_1_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "X"
                    Pos1_1_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "O"
                    Pos1_1_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                End If
            End If

        ElseIf (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos1_2) And
                    Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos1_2) + 85) Then

            'in 12 bottom half
            If (currentPosBB.Y > (Canvas.GetTop(Pos1_2) - 70)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "O"
                    Pos1_2_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "-"
                    Pos1_2_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "X"
                    Pos1_2_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))
                End If

                'in 12 top half
            ElseIf (currentPosBB.Y >= (Canvas.GetTop(Pos1_2) - 100)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "-"
                    Pos1_2_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "X"
                    Pos1_2_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "O"
                    Pos1_2_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                End If
            End If

        ElseIf (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos1_3) And
                 Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos1_3) + 85) Then
            'in 13 bottom half
            If (currentPosBB.Y > (Canvas.GetTop(Pos1_3) - 70)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "O"
                    Pos1_3_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "-"
                    Pos1_3_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "X"
                    Pos1_3_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))
                End If

                'in 11 top half
            ElseIf (currentPosBB.Y >= (Canvas.GetTop(Pos1_3) - 100)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "-"
                    Pos1_3_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "X"
                    Pos1_3_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "O"
                    Pos1_3_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                End If
            End If

        ElseIf (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos2_1) And
            Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos2_1) + 85) Then

            'in 21 bottom half
            If (currentPosBB.Y > (Canvas.GetTop(Pos2_1) - 70)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "O"
                    Pos2_1_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "-"
                    Pos2_1_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "X"
                    Pos2_1_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))
                End If

                'in 11 top half
            ElseIf (currentPosBB.Y >= (Canvas.GetTop(Pos2_1) - 100)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "-"
                    Pos2_1_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "X"
                    Pos2_1_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "O"
                    Pos2_1_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                End If
            End If

        ElseIf (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos2_2) And
             Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos2_2) + 85) Then

            'in 22 bottom half
            If (currentPosBB.Y > (Canvas.GetTop(Pos2_2) - 70)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "O"
                    Pos2_2_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "-"
                    Pos2_2_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "X"
                    Pos2_2_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))
                End If

                'in 22 top half
            ElseIf (currentPosBB.Y >= (Canvas.GetTop(Pos2_2) - 100)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "-"
                    Pos2_2_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "X"
                    Pos2_2_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "O"
                    Pos2_2_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                End If
            End If

        ElseIf (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos2_3) And
                         Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos2_3) + 85) Then

            'in 23 bottom half
            If (currentPosBB.Y > (Canvas.GetTop(Pos2_3) - 70)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "O"
                    Pos2_3_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "-"
                    Pos2_3_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))
                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "X"
                    Pos2_3_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))
                End If

                'in 11 top half
            ElseIf (currentPosBB.Y >= (Canvas.GetTop(Pos2_3) - 100)) Then

                If (tictac(0, 0) = "X") Then
                    tictac(0, 0) = "-"
                    Pos2_3_Img.Source = New BitmapImage(New Uri("Images/poolWaterWithXO.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "O") Then
                    tictac(0, 0) = "X"
                    Pos2_3_Img.Source = New BitmapImage(New Uri("Images/poolX.png", UriKind.Relative))

                ElseIf (tictac(0, 0) = "-") Then
                    tictac(0, 0) = "O"
                    Pos2_3_Img.Source = New BitmapImage(New Uri("Images/poolO.png", UriKind.Relative))
                End If
            End If

        ElseIf (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos3_1) And
                 Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos3_1) + 85 And
                 Canvas.GetTop(bbImage) > Canvas.GetLeft(Pos3_1) And
                 Canvas.GetTop(bbImage) < Canvas.GetLeft(Pos3_1) + 85) Then
            'in 31
            '  updateButtonImage(Pos3_1)
        ElseIf (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos3_2) And
                 Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos3_2) + 85 And
                 Canvas.GetTop(bbImage) > Canvas.GetLeft(Pos3_2) And
                 Canvas.GetTop(bbImage) < Canvas.GetLeft(Pos3_2) + 85) Then
            'in 32
            ' updateButtonImage(Pos3_2)
        ElseIf (Canvas.GetLeft(bbImage) > Canvas.GetLeft(Pos3_3) And
                 Canvas.GetLeft(bbImage) < Canvas.GetLeft(Pos3_3) + 85 And
                 Canvas.GetTop(bbImage) > Canvas.GetLeft(Pos3_3) And
                 Canvas.GetTop(bbImage) < Canvas.GetLeft(Pos3_3) + 85) Then
            'in 33
            '   updateButtonImage(Pos3_3)
        End If

        'winner?
        If (checkIfWon("X") = 1) Then
            showWin("X")
        ElseIf (checkIfWon("O") = 1) Then
            showWin("O")
        End If

    End Sub

End Class
