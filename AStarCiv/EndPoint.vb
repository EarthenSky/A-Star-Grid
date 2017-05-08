Public Class EndPoint

    Private WithEvents pbxEndPoint As PictureBox  'Holds position and stuff.

    Sub New(ByVal pntPosition As Point, ByRef controls As Control.ControlCollection)
        pbxEndPoint = New PictureBox()
        pbxEndPoint.BackColor = Color.CornflowerBlue
        pbxEndPoint.Location = pntPosition
        pbxEndPoint.Size = New Size(64, 64)

        controls.Add(pbxEndPoint)
    End Sub

    Private blnMouseDown As Boolean = False

    Public Sub Clicked(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbxEndPoint.MouseDown
        If e.Button = MouseButtons.Left Then
            blnMouseDown = True
        End If
    End Sub

    Public Sub UnClicked(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbxEndPoint.MouseUp  'Moves the endpoint when mouse goes up
        If e.Button = MouseButtons.Left And blnMouseDown = True Then
            blnMouseDown = False
            pbxEndPoint.Location = New Point(((Cursor.Position.X - Form1.Location.X - 3) \ 64) * 64, ((Cursor.Position.Y - Form1.Location.Y - 25) \ 64) * 64)   'Sets player position in grid.
        End If
    End Sub

    Public Function GetPositionInGrid() As Point
        Return New Point(pbxEndPoint.Location.X \ 64, pbxEndPoint.Location.Y \ 64)
    End Function

    Public Sub SetPositionInGrid(ByVal shtX As Short, ByVal shtY As Short)
        pbxEndPoint.Location = New Point(shtX * 64, shtY * 64)
    End Sub
End Class
