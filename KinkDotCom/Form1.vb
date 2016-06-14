Option Explicit On
Option Strict Off
Imports System.IO

'Liste der Channels liegt mit im Projekt

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CoockieHandler()
        'GetDirectory()

        Dim Dinfo As DirectoryInfo = New DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Cookies))
        Dim Finfo As FileInfo 'Sowohl die Coockie-löschung und getDirectory sind ausgelagert, schmeißen aber.
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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim pagenumber As Integer
        pagenumber = 1

        WebBrowser1.Navigate("http://www.kink.com/channel/boundinpublic/latest")
        Sleep(5000)

        Do
            WebBrowser1.Navigate("http://www.kink.com/channel/boundinpublic/latest/" & pagenumber)
            Sleep(5000)
        Loop Until pagenumber = 0

    End Sub

    Private Function GetDirectory() 'InvalidCastException, Angeblich irgendwo INT
        Dim Dinfo As String = Environment.GetFolderPath(Environment.CurrentDirectory)
        Return Dinfo
    End Function

    Private Sub CoockieHandler()
        'Cookies beim Laden des Programmes löschen
        Dim Dinfo As DirectoryInfo = New DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Cookies))
        Dim Finfo As FileInfo
        For Each Finfo In Dinfo.GetFiles
            Try
                Finfo.Delete()
            Catch ex As Exception
            End Try
        Next
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        WebBrowser1.Navigate("http://www.kink.com/channel/boundinpublic/latest/page/1")
        Sleep(5000)
        'If WebBrowser1.Document.GetElementById("shootID").ToString = "test" Then

        'End If
        Dim input As String = WebBrowser1.DocumentText.ToString
        Dim shootIndex(300) As String
        Dim i As Integer
        i = -1
        'Dim pfad As String = GetDirectory()
        If My.Computer.FileSystem.FileExists("C:\Users\Chiyo\Source\Repos\KDownload\KinkDotCom\obj\Debug\test.txt") Then
            My.Computer.FileSystem.DeleteFile("C:\Users\Chiyo\Source\Repos\KDownload\KinkDotCom\obj\Debug\test.txt")
        End If

        My.Computer.FileSystem.WriteAllText("C:\Users\Chiyo\Source\Repos\KDownload\KinkDotCom\obj\Debug\test.txt", "" & input, True)
        For Each line As String In IO.File.ReadAllLines("C:\Users\Chiyo\Source\Repos\KDownload\KinkDotCom\obj\Debug\test.txt")
            If line.Contains("data-shootId") And line.Contains("class= & chr(34) & shoot & chr(34)") Then
                i = i + 1
                shootIndex(i) = line
                TextBox1.Text = line & vbCrLf & TextBox1.Text
            End If
        Next
        ReDim shootIndex(i)
        MsgBox("" & shootIndex.Length)
    End Sub
End Class

