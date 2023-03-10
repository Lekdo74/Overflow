using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Overflow.src
{
    public static class PathFinding
    {
        public static Vector2[] FindPath(Vector2 StartPosition, Vector2 TargetPosition, Room room)
        {
            foreach(Tile tile in room.Tiles)
            {
                if(tile != null)
                {
                    tile.Parent = null;
                    tile.GCost = 0;
                    tile.HCost = 0;
                }
            }

            Tile startTile = room.Tiles[(int)StartPosition.X, (int)StartPosition.Y];
            Tile targetTile = room.Tiles[(int)TargetPosition.X, (int)TargetPosition.Y];

            List<Tile> tilesToCheck = new List<Tile>();
            HashSet<Tile> tilesAlreadyChecked = new HashSet<Tile>();

            tilesToCheck.Add(startTile);

            while(tilesToCheck.Count > 0)
            {
                Tile currentTile = tilesToCheck[0];
                for(int i = 1; i < tilesToCheck.Count; i++)
                {
                    if (tilesToCheck[i].FCost < currentTile.FCost || (tilesToCheck[i].FCost == currentTile.FCost && tilesToCheck[i].HCost < currentTile.HCost))
                    {
                        currentTile = tilesToCheck[i];
                    }
                }

                tilesToCheck.Remove(currentTile);
                tilesAlreadyChecked.Add(currentTile);

                if(currentTile == targetTile)
                {
                    List<Tile> path = RetracePath(startTile, targetTile);
                    return SymplifyPath(path);
                }

                foreach(Tile neighbour in room.GetNeighbours(currentTile))
                {
                    if(neighbour.Type == "Wall" || tilesAlreadyChecked.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentTile.GCost + GetDistance(currentTile, neighbour);
                    if(newMovementCostToNeighbour < currentTile.GCost || !tilesAlreadyChecked.Contains(neighbour))
                    {
                        neighbour.GCost = newMovementCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetTile);
                        neighbour.Parent = currentTile;

                        if (!tilesAlreadyChecked.Contains(neighbour))
                        {
                            tilesToCheck.Add(neighbour);
                        }
                    }
                }
            }
            return new Vector2[] { targetTile.Position };
        }

        private static List<Tile> RetracePath(Tile startTile, Tile tartgetTile)
        {
            List<Tile> path = new List<Tile>();
            Tile currentTile = tartgetTile;

            while(currentTile != startTile)
            {
                path.Add(currentTile);
                currentTile = currentTile.Parent;
            }

            path.Reverse();
            return path;
        }

        private static Vector2[] SymplifyPath(List<Tile> path)
        {
            List<Vector2> waypoints = new List<Vector2>();

            Vector2 directionOld = Vector2.Zero;

            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].MapX - path[i].MapX, path[i - 1].MapY - path[i].MapY);
                if (directionNew != directionOld)
                {
                    waypoints.Add(path[i].Position);
                }
                directionOld = directionNew;
            }

            return waypoints.ToArray();
        }

        private static int GetDistance(Tile tileA, Tile tileB)
        {
            int dstX = Math.Abs(tileA.MapX - tileB.MapX);
            int dstY = Math.Abs(tileA.MapY - tileB.MapY);

            if(dstX > dstY)
            {
                return 14 * dstY + 10 * (dstX - dstY);
            }
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}
