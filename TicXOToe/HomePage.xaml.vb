Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Navigation
Imports System.Windows.Shapes


Partial Public Class HomePage
    Inherits PhoneApplicationPage

    Public Sub New()
        InitializeComponent()

        TwoPlayerSlingshotButton.Visibility = Windows.Visibility.Collapsed
        p2_slingLabel.Visibility = Windows.Visibility.Collapsed
    End Sub

    Private Sub OnePlayerButton_Click(sender As Object, e As RoutedEventArgs) Handles OnePlayerButton.Click

        CType(Application.Current, App).gametype = 1
        NavigationService.Navigate(New Uri("/MainPage.xaml", UriKind.Relative))
    End Sub

    Private Sub TwoPlayerButton_Click(sender As Object, e As RoutedEventArgs) Handles TwoPlayerButton.Click
        CType(Application.Current, App).gametype = 2
        NavigationService.Navigate(New Uri("/MainPage.xaml", UriKind.Relative))
    End Sub

    Private Sub TwoPlayerSlingshotButton_Click(sender As Object, e As RoutedEventArgs) Handles TwoPlayerSlingshotButton.Click
        CType(Application.Current, App).gametype = 3
        NavigationService.Navigate(New Uri("/Slingshot.xaml", UriKind.Relative))
    End Sub

   




End Class
