﻿#ExternalChecksum("C:\Users\James\Documents\Visual Studio 2012\Projects\TicXOToe\TicXOToe\HomePage.xaml","{406ea660-64cf-4c82-b6f0-42d48172a799}","614722DBD010FE13DC9C7E82117D4EE6")
'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18010
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports Microsoft.Phone.Controls
Imports System
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Automation.Peers
Imports System.Windows.Automation.Provider
Imports System.Windows.Controls
Imports System.Windows.Controls.Primitives
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Ink
Imports System.Windows.Input
Imports System.Windows.Interop
Imports System.Windows.Markup
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Media.Imaging
Imports System.Windows.Resources
Imports System.Windows.Shapes
Imports System.Windows.Threading



<Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>  _
Partial Public Class HomePage
    Inherits Microsoft.Phone.Controls.PhoneApplicationPage
    
    Friend WithEvents LayoutRoot As System.Windows.Controls.Grid
    
    Friend WithEvents ContentPanel As System.Windows.Controls.Grid
    
    Friend WithEvents OnePlayerButton As System.Windows.Controls.Button
    
    Friend WithEvents P1 As System.Windows.Controls.Image
    
    Friend WithEvents TwoPlayerButton As System.Windows.Controls.Button
    
    Friend WithEvents P2 As System.Windows.Controls.Image
    
    Friend WithEvents TwoPlayerSlingshotButton As System.Windows.Controls.Button
    
    Friend WithEvents P2_sling As System.Windows.Controls.Image
    
    Friend WithEvents OnePLabel As System.Windows.Controls.TextBlock
    
    Friend WithEvents p2_slingLabel As System.Windows.Controls.TextBlock
    
    Private _contentLoaded As Boolean
    
    '''<summary>
    '''InitializeComponent
    '''</summary>
    <System.Diagnostics.DebuggerNonUserCodeAttribute()>  _
    Public Sub InitializeComponent()
        If _contentLoaded Then
            Return
        End If
        _contentLoaded = true
        System.Windows.Application.LoadComponent(Me, New System.Uri("/TicXOToe;component/HomePage.xaml", System.UriKind.Relative))
        Me.LayoutRoot = CType(Me.FindName("LayoutRoot"),System.Windows.Controls.Grid)
        Me.ContentPanel = CType(Me.FindName("ContentPanel"),System.Windows.Controls.Grid)
        Me.OnePlayerButton = CType(Me.FindName("OnePlayerButton"),System.Windows.Controls.Button)
        Me.P1 = CType(Me.FindName("P1"),System.Windows.Controls.Image)
        Me.TwoPlayerButton = CType(Me.FindName("TwoPlayerButton"),System.Windows.Controls.Button)
        Me.P2 = CType(Me.FindName("P2"),System.Windows.Controls.Image)
        Me.TwoPlayerSlingshotButton = CType(Me.FindName("TwoPlayerSlingshotButton"),System.Windows.Controls.Button)
        Me.P2_sling = CType(Me.FindName("P2_sling"),System.Windows.Controls.Image)
        Me.OnePLabel = CType(Me.FindName("OnePLabel"),System.Windows.Controls.TextBlock)
        Me.p2_slingLabel = CType(Me.FindName("p2_slingLabel"),System.Windows.Controls.TextBlock)
    End Sub
End Class
