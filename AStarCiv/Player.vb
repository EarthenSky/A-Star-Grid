Public Class Player
    Private WithEvents pbxPlayer As PictureBox  'Holds position and stuff.

    Sub New(ByVal pntPosition As Point, ByVal imgTexture As Image, ByRef controls As Control.ControlCollection)
        pbxPlayer = New PictureBox()
        pbxPlayer.Image = imgTexture
        pbxPlayer.Location = pntPosition
        pbxPlayer.Size = New Size(64, 64)

        controls.Add(pbxPlayer)
    End Sub

    Private blnMouseDown As Boolean = False

    Public Sub Clicked(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbxPlayer.MouseDown
        If e.Button = MouseButtons.Left Then
            blnMouseDown = True
        End If
    End Sub

    Public Sub UnClicked(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbxPlayer.MouseUp  'Moves the player when mouse goes up
        If e.Button = MouseButtons.Left And blnMouseDown = True Then
            blnMouseDown = False
            pbxPlayer.Location = New Point(((Cursor.Position.X - Form1.Location.X - 3) \ 64) * 64, ((Cursor.Position.Y - Form1.Location.Y - 25) \ 64) * 64)  'Sets player position in grid.
        End If
    End Sub

    Public Function GetPositionInGrid()
        Return New Point(pbxPlayer.Location.X \ 64, pbxPlayer.Location.Y \ 64)
    End Function

    Public Sub SetPositionInGrid(ByVal shtX As Short, ByVal shtY As Short)
        pbxPlayer.Location = New Point(shtX * 64, shtY * 64)
    End Sub
End Class
