'Holds different tile types.
Public Enum TileType
    Unwalkable = 0
    Hindering = 1
    Normal = 2
    Dangerous = 3
End Enum

Public Class Tile
    Private tileType As TileType  'Holds the type of tile this is.
    Public WithEvents pbxTile As PictureBox  'Holds position and stuff.
    Public lblID As Label  'Holds label stuff for debug

    Sub New(ByVal tileType As TileType, ByVal pntPosition As Point, ByVal imgTexture As Image, ByRef controls As Control.ControlCollection)
        Me.tileType = tileType

        lblID = New Label()
        lblID.Location = New Point(pntPosition.X, pntPosition.Y + 48) 'set your location
        lblID.Size = New System.Drawing.Size(32, 16) 'set your size (if required)
        lblID.Text = "On" 'set the text for your label

        controls.Add(lblID)

        pbxTile = New PictureBox()
        pbxTile.Image = imgTexture
        pbxTile.Location = pntPosition
        pbxTile.Size = New Size(64, 64)

        controls.Add(pbxTile)
    End Sub

    Public Sub Clicked(ByVal sender As Object, ByVal e As MouseEventArgs) Handles pbxTile.MouseDown
        If e.Button = MouseButtons.Left Then
            If tileType = AStarCiv.TileType.Unwalkable Then
                tileType = AStarCiv.TileType.Normal
            Else
                tileType = AStarCiv.TileType.Unwalkable
            End If
            ChangeTexture(tileType)
        ElseIf e.Button = MouseButtons.Right Then
            If tileType = AStarCiv.TileType.Hindering Then
                tileType = AStarCiv.TileType.Normal
            Else
                tileType = AStarCiv.TileType.Hindering
            End If
            ChangeTexture(tileType)
        End If
    End Sub

    Public Sub ChangeTexture(ByVal tileType As TileType)
        Select Case (tileType)
            Case AStarCiv.TileType.Unwalkable
                pbxTile.Image = Form1.imgMountains
            Case AStarCiv.TileType.Hindering
                pbxTile.Image = Form1.imgHills
            Case AStarCiv.TileType.Normal
                pbxTile.Image = Form1.imgGrassland
        End Select
    End Sub

    Public Function GetTileType() As TileType
        Return tileType
    End Function

    Public Function GetPositionInGrid() As Point
        Return New Point(pbxTile.Location.X \ 64, pbxTile.Location.Y \ 64)
    End Function

    Private pntLastTile As Point  'Holds last tile position
    Public Sub SetLastTilePoint(ByVal pntLastPoint As Point)
        pntLastTile = pntLastPoint
    End Sub

    Public Function GetLastTilePoint() As Point
        Return pntLastTile
    End Function

    Private isInOpen As Boolean  'Holds if it is part of the open list
    Public Sub SetIsInOpenList(ByVal blnValue As Boolean)
        isInOpen = blnValue
        pbxTile.Size = New Size(32, 32)
    End Sub

    Public Function IsInOpenList() As Boolean
        Return isInOpen
    End Function

    Private isInClosed As Boolean  'Holds if it is part of the open list
    Public Sub SetIsInClosedList(ByVal blnValue As Boolean)
        isInClosed = blnValue
        pbxTile.Size = New Size(16, 16)
    End Sub

    Public Function IsInClosedList() As Boolean
        Return isInClosed
    End Function

End Class
