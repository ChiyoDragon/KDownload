Option Explicit On
Option Strict Off
Imports System.IO

'Liste der Channels liegt mit im Projekt

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cookies beim Laden des Programmes löschen
        Dim Dinfo As DirectoryInfo = New DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Cookies))
        Dim Finfo As FileInfo
        For Each Finfo In Dinfo.GetFiles
            Try
                Finfo.Delete()
            Catch ex As Exception
            End Try
        Next
        'Kink.com Login aufrufen
        WebBrowser1.Navigate("http://www.kink.com/login")
        ' Warten bis Webseite geladen
        Do Until WebBrowser1.ReadyState = WebBrowserReadyState.Complete
            Application.DoEvents()
        Loop
        ' System.Threading(4000)
        If WebBrowser1.DocumentText.Contains("chiyodragon") Then
            MsgBox("Eingeloggt")
        End If
        'Auswahlmenü wegclicken
        WebBrowser1.Document.GetElementById("closeViewingPreferences").Focus()
        WebBrowser1.Document.GetElementById("closeViewingPreferences").InvokeMember("click")
    End Sub

    Private Sub Login(ByVal sender As System.Object,
                              ByVal e As System.EventArgs) _
                              Handles LoginButton.Click

        If WebBrowser1.DocumentText.Contains("chiyodragon") Then
            MsgBox("Bereits Eingeloggt!")
            Return
        End If

        WebBrowser1.Document.GetElementById("usernameLogin").InnerText = "ChiyoDragon"
        WebBrowser1.Document.GetElementById("passwordLogin").InnerText = "snakeeater"

        WebBrowser1.Document.GetElementById("loginSubmit").Focus()
        WebBrowser1.Document.GetElementById("loginSubmit").InvokeMember("click")
        Do While WebBrowser1.ReadyState = False     ' Warten bis Webseite vollständig geladen
            Application.DoEvents()
        Loop
        Sleep(5000)
        ' WebBrowser1.Navigate("http://www.kink.com/channel/boundinpublic")
        Do Until WebBrowser1.ReadyState = WebBrowserReadyState.Complete
            Application.DoEvents()
        Loop
        Sleep(5000)
        Try
            If WebBrowser1.DocumentText.Contains("chiyodragon") Then
                Label1.ForeColor() = Color.Green
                Label1.Text = "Eingeloggt"
            End If
        Catch
        End Try
    End Sub
    Public Function Sleep(ByVal msec As Integer)
        Dim myTimer As Date
        myTimer = Now.AddMilliseconds(msec)
        Do While myTimer > Now
            Application.DoEvents()
        Loop
        Return 0
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If WebBrowser1.DocumentText.Contains("chiyodragon") Then
            Label1.ForeColor() = Color.Green
            Label1.Text = "Eingeloggt"
        End If
    End Sub

End Class

