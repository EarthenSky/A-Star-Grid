
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
                Debug.Print(FindLowestFCost() & " = F")
                Debug.Print(FindH(player.GetPositionInGrid()) & " = H")

                pntMain = player.GetPositionInGrid()  'Make middle tile the maintile.
                AddToOpen(mdaTiles(player.GetPositionInGrid().X, player.GetPositionInGrid().Y))  'Add current tile to open list
                AddToClosedFromOpen(mdaTiles(player.GetPositionInGrid().X, player.GetPositionInGrid().Y))  'Add middle tile to closed

                AddAdjacentTilesToOpen(player.GetPositionInGrid())

            Else  'Moving each turn (nope)

            End If
        End If
    End Sub

    Public Sub AddToOpen(ByRef MainTile As Tile)  'Use this to add items to the OpenList (Maybe ByVal)
        MainTile.SetIsInOpenList(True)
        lstOpen.Add(MainTile.GetPositionInGrid)
    End Sub

    Public Sub AddToClosedFromOpen(ByRef MainTile As Tile)  'Use this to add items to the ClosedList
        MainTile.SetIsInOpenList(False)
        MainTile.SetIsInClosedList(True)
        lstOpen.Remove(MainTile.GetPositionInGrid)
        lstClosed.Add(MainTile.GetPositionInGrid)
    End Sub

    'Works
    Public Sub AddAdjacentTilesToOpen(ByVal pntMainPoint As Point)  'Adds the 8 blocks adjacent to the main point if they are able to be added.
        If pntMainPoint.Y > 0 Then  'Makes sure that it doesn't look for a non-existant block ABOVE it.
            If mdaTiles(pntMainPoint.X, pntMainPoint.Y - 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X, pntMainPoint.Y - 1).IsInOpenList = False And mdaTiles(pntMainPoint.X, pntMainPoint.Y - 1).IsInClosedList = False Then  'Adds the block if it is walkable and has not already been added
                AddToOpen(mdaTiles(pntMainPoint.X, pntMainPoint.Y - 1))
            End If
            If pntMainPoint.X > 0 Then  'Makes sure that it doesn't look for a non-existant block to the LEFT of it.
                If mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y - 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y - 1).IsInOpenList = False And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y - 1).IsInClosedList = False Then   'Adds the block if it is walkable and has not already been added
                    AddToOpen(mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y - 1))
                End If
            End If
            If pntMainPoint.X < 7 Then  'Makes sure that it doesn't look for a non-existant block to the RIGHT of it.
                If mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y - 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y - 1).IsInOpenList = False And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y - 1).IsInClosedList = False Then   'Adds the block if it is walkable and has not already been added
                    AddToOpen(mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y - 1))
                End If
            End If
        End If

        If pntMainPoint.X > 0 Then  'Makes sure that it doesn't look for a non-existant block to the LEFT of it.
            If mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y).IsInOpenList = False And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y).IsInClosedList = False Then   'Adds the block if it is walkable and has not already been added
                AddToOpen(mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y))
            End If
        End If

        If pntMainPoint.X < 7 Then  'Makes sure that it doesn't look for a non-existant block to the RIGHT of it.
            If mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y).IsInOpenList = False And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y).IsInClosedList = False Then   'Adds the block if it is walkable and has not already been added
                AddToOpen(mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y))
            End If
        End If

        If pntMainPoint.Y < 7 Then  'Makes sure that it doesn't look for a non-existant block BELLOW it.
            If mdaTiles(pntMainPoint.X, pntMainPoint.Y + 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X, pntMainPoint.Y + 1).IsInOpenList = False And mdaTiles(pntMainPoint.X, pntMainPoint.Y + 1).IsInClosedList = False Then  'Adds the block if it is walkable and has not already been added
                AddToOpen(mdaTiles(pntMainPoint.X, pntMainPoint.Y + 1))
            End If
            If pntMainPoint.X > 0 Then  'Makes sure that it doesn't look for a non-existant block to the LEFT of it.
                If mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y + 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y + 1).IsInOpenList = False And mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y + 1).IsInClosedList = False Then   'Adds the block if it is walkable and has not already been added
                    AddToOpen(mdaTiles(pntMainPoint.X - 1, pntMainPoint.Y + 1))
                End If
            End If
            If pntMainPoint.X < 7 Then  'Makes sure that it doesn't look for a non-existant block to the RIGHT of it.
                If mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y + 1).GetTileType <> TileType.Unwalkable And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y + 1).IsInOpenList = False And mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y + 1).IsInClosedList = False Then   'Adds the block if it is walkable and has not already been added
                    AddToOpen(mdaTiles(pntMainPoint.X + 1, pntMainPoint.Y + 1))
                End If
            End If
        End If
    End Sub

    Public Function FindLowestFCost() As Short 'also adds f-cost to current tile.
        Dim shtMainF As Short = 32000
        Dim pntValue As Point

        For Each pntOpen In lstOpen
            Dim shtTempF As Short = 0

            Dim blnCutOffFirstValue As Boolean = False
            For Each pntClosed In lstClosed  'Calculates the G cost of the tiles before it (In closed list)
                If blnCutOffFirstValue = False Then
                    blnCutOffFirstValue = True
                    Continue For
                End If
                If mdaTiles(pntClosed.X, pntClosed.Y).GetTileType = TileType.Normal Then
                    shtTempF += 10
                ElseIf mdaTiles(pntClosed.X, pntClosed.Y).GetTileType = TileType.Hindering Then
                    shtTempF += 20
                ElseIf mdaTiles(pntClosed.X, pntClosed.Y).GetTileType = TileType.Dangerous Then
                    shtTempF += 40
                End If
            Next

            'Calculates the G cost of the current open tile.
            If mdaTiles(pntOpen.X, pntOpen.Y).GetTileType = TileType.Normal Then
                shtTempF += 10
            ElseIf mdaTiles(pntOpen.X, pntOpen.Y).GetTileType = TileType.Hindering Then
                shtTempF += 20
            ElseIf mdaTiles(pntOpen.X, pntOpen.Y).GetTileType = TileType.Dangerous Then
                shtTempF += 40
            End If

            shtTempF += FindH(pntOpen)

            If shtTempF <= shtMainF Then 'If the current open tile's f is the smallest make it the main F
                shtMainF = shtTempF
                pntValue = pntOpen
            End If
        Next
        Return shtMainF
    End Function

    Public Function FindH(ByVal pntOpen As Point) As Short  'This is the heuristic algorithim.  This is what can get better
        Return -1 * 10 * ((pntOpen.X - endPoint.GetPositionInGrid.X) + (pntOpen.Y - endPoint.GetPositionInGrid.Y)) 'TODO: make this better.
    End Function

    Public Sub CheckAdjacentTiles()

    End Sub
End Class
