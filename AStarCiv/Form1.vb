
Public Class Form1
    'Gabe Stang
    'Civ-like unit pathfinding w/ A*.
    '"I don't like these things but I stilll did it." 
    'You can create a grid of tiles, grey is impassable, light green is normal, dark green slows the player.  The player (red) finds the fastest way to the endpoint (blue)

    Private player As Player

    Private mdaTiles(8, 8) As Tile  'Holds tile values.

    Private strCurrentFileDirectory As String = IO.Directory.GetCurrentDirectory.Remove(IO.Directory.GetCurrentDirectory.IndexOf("\bin\Debug"), 10) + "\"

    Public imgGrassland As Image
    Public imgMountains As Image
    Public imgHills As Image
    Public imgUnit As Image

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        imgGrassland = Image.FromFile(strCurrentFileDirectory & "GrassLandTile.png")
        imgMountains = Image.FromFile(strCurrentFileDirectory & "MountainTile.png")
        imgHills = Image.FromFile(strCurrentFileDirectory & "HillsTile.png")
        imgUnit = Image.FromFile(strCurrentFileDirectory & "UnitTile.png")

        player = New Player(New Point(0, 0), imgUnit, Controls)

        For indexX As Short = 0 To 7
            For indexY As Short = 0 To 7
                mdaTiles(indexX, indexY) = New Tile(TileType.Normal, New Point(indexX * 64, indexY * 64), imgGrassland, Controls)
            Next
        Next

    End Sub

    Public Sub StartPathFinding(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Space Then
            '
        End If
    End Sub
End Class
