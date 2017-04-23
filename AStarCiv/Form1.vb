
Public Class Form1
    'Gabe Stang
    'Civ-like unit pathfinding w/ A*.
    ' 
    'You can create a grid of tiles, grey is impassable, light green is normal, dark green slows the player.  The player (red) finds the fastest way to the endpoint (blue)

    Private player As Player

    Private endPoint As EndPoint

    Private mdaTiles(8, 8) As Tile  'Holds tile values.
    Private lstOpen As New List(Of Point)
    Private lstPath As New List(Of Point)
    Private pntLastTile As Point

    Private strCurrentFileDirectory As String = IO.Directory.GetCurrentDirectory.Remove(IO.Directory.GetCurrentDirectory.IndexOf("\bin\Debug"), 10) + "\"

    'Image vars.
    Public imgGrassland As Image
    Public imgMountains As Image
    Public imgHills As Image
    Public imgUnit As Image
    Public imgEndPoint As Image

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Assigns textures to all of the image variables.
        imgGrassland = Image.FromFile(strCurrentFileDirectory & "GrassLandTile.png")
        imgMountains = Image.FromFile(strCurrentFileDirectory & "MountainTile.png")
        imgHills = Image.FromFile(strCurrentFileDirectory & "HillsTile.png")
        imgUnit = Image.FromFile(strCurrentFileDirectory & "UnitTile.png")
        imgEndPoint = Image.FromFile(strCurrentFileDirectory & "EndPointTile.png")

        player = New Player(New Point(0, 0), imgUnit, Controls)
        endPoint = New EndPoint(New Point(7 * 64, 7 * 64), imgEndPoint, Controls)

        For indexX As Short = 0 To 7
            For indexY As Short = 0 To 7
                mdaTiles(indexX, indexY) = New Tile(TileType.Normal, New Point(indexX * 64, indexY * 64), imgGrassland, Controls)
            Next
        Next

    End Sub

    Private blnIsPathFindingDone = False

    Public Sub StartPathFinding(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Space Then
            If blnIsPathFindingDone = False Then  'Finding path
                Debug.Print(player.GetPositionInGrid().ToString)
                'lstOpen.Add(player.GetPositionInGrid())

                'lstPath.Add(player.GetPositionInGrid())

            Else  'Moving each turn

            End If
        End If
    End Sub

    Public Sub AddAdjacentTilesToOpen()

    End Sub

    Public Sub FindLowestFCost()  'also adds fcost to current tile.

    End Sub

    Public Sub CheckAdjacentTiles()

    End Sub
End Class
