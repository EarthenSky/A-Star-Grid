
Public Class Form1
    'Gabe Stang
    'Civ-like unit pathfinding w/ A*.
    ' 
    'You can create a grid of tiles, grey is impassable, light green is normal, dark green slows the player.  The player (red) finds the fastest way to the endpoint (blue)

    Private player As Player

    Private endPoint As EndPoint

    Private mdaTiles(8, 8) As Tile  'Holds tile values.
    Private lstOpen As New List(Of Point)
    Private lstClosed As New List(Of Point)
    Private pntMain As Point

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

                'Add current tile to list
                pntMain = player.GetPositionInGrid()
                lstOpen.Add(player.GetPositionInGrid())

                AddAdjacentTilesToOpen(player.GetPositionInGrid())

            Else  'Moving each turn

            End If
        End If
    End Sub

    Public Sub AddToOpen(ByRef MainTile As Tile)  'Use this to add items to the OpenList
        MainTile.SetIsInOpenList(True)
        lstOpen.Add(MainTile.GetPositionInGrid)
    End Sub

    Public Sub AddToClosedFromOpen(ByRef MainTile As Tile)  'Use this to add items to the ClosedList
        MainTile.SetIsInOpenList(False)
        lstOpen.Remove(MainTile.GetPositionInGrid)
        lstClosed.Add(MainTile.GetPositionInGrid)
    End Sub

    'Works
    Public Sub AddAdjacentTilesToOpen(ByVal pntMainPoint As Point)  'Adds the 8 blocks adjacent to the main point if they are able to be added.
        If pntMainPoint.Y > 0 Then  'Makes sure that it doesn't look for a non-existant block ABOVE it.
            If mdaTiles(pntMainPoint.X, pntMainPoint.Y - 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X, pntMainPoint.Y - 1).IsInOpenList = False Then  'Adds the block if it is walkable and has not already been added
                AddToOpen(mdaTiles(pntMainPoint.X, pntMainPoint.Y - 1))
            End If
            If pntMainPoint.X > 0 Then  'Makes sure that it doesn't look for a non-existant block to the LEFT of it.
                If mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y - 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y - 1).IsInOpenList = False Then   'Adds the block if it is walkable and has not already been added
                    AddToOpen(mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y - 1))
                End If
            End If
            If pntMainPoint.X < 7 Then  'Makes sure that it doesn't look for a non-existant block to the RIGHT of it.
                If mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y - 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y - 1).IsInOpenList = False Then   'Adds the block if it is walkable and has not already been added
                    AddToOpen(mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y - 1))
                End If
            End If
        End If

        If pntMainPoint.X > 0 Then  'Makes sure that it doesn't look for a non-existant block to the LEFT of it.
            If mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y).IsInOpenList = False Then   'Adds the block if it is walkable and has not already been added
                AddToOpen(mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y))
            End If
        End If

        If pntMainPoint.X < 7 Then  'Makes sure that it doesn't look for a non-existant block to the RIGHT of it.
            If mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y).IsInOpenList = False Then   'Adds the block if it is walkable and has not already been added
                AddToOpen(mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y))
            End If
        End If

        If pntMainPoint.Y < 7 Then  'Makes sure that it doesn't look for a non-existant block BELLOW it.
            If mdaTiles(pntMainPoint.X, pntMainPoint.Y + 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X, pntMainPoint.Y + 1).IsInOpenList = False Then  'Adds the block if it is walkable and has not already been added
                AddToOpen(mdaTiles(pntMainPoint.X, pntMainPoint.Y + 1))
            End If
            If pntMainPoint.X > 0 Then  'Makes sure that it doesn't look for a non-existant block to the LEFT of it.
                If mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y + 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y + 1).IsInOpenList = False Then   'Adds the block if it is walkable and has not already been added
                    AddToOpen(mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y + 1))
                End If
            End If
            If pntMainPoint.X < 7 Then  'Makes sure that it doesn't look for a non-existant block to the RIGHT of it.
                If mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y + 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y + 1).IsInOpenList = False Then   'Adds the block if it is walkable and has not already been added
                    AddToOpen(mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y + 1))
                End If
            End If
        End If
    End Sub

    Public Sub FindLowestFCost()  'also adds f-cost to current tile.

    End Sub

    Public Sub CheckAdjacentTiles()

    End Sub
End Class
