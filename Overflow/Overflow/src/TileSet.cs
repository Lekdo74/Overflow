using Microsoft.Xna.Framework.Graphics;
using System;

namespace Overflow.src
{
    public class TileSet
    {
        private static Random random = new Random();

        private int _tileSize;

        private Texture2D[] _topWall;
        private Texture2D[] _rightWall;
        private Texture2D[] _bottomWall;
        private Texture2D[] _leftWall;

        private Texture2D[] _topLeftWall;
        private Texture2D[] _topRightWall;
        private Texture2D[] _bottomRightWall;
        private Texture2D[] _bottomLeftWall;

        private Texture2D[] _topLeftCorner;
        private Texture2D[] _topRightCorner;
        private Texture2D[] _bottomRightCorner;
        private Texture2D[] _bottomLeftCorner;

        private Texture2D[] _terrain;
        private Texture2D[] _terrainWithWall;
        private Texture2D[] _terrainWithWallBorderLeft;
        private Texture2D[] _terrainWithWallBorderRight;

        private Texture2D[] _topDoor;
        private Texture2D[] _rightDoor;
        private Texture2D[] _bottomDoor;
        private Texture2D[] _leftDoor;

        private Texture2D[] _darkTerrain;

        public TileSet(int tileSize, Texture2D[] topWall, Texture2D[] rightWall, Texture2D[] bottomWall, Texture2D[] leftWall, Texture2D[] topLeftWall, Texture2D[] topRightWall, Texture2D[] bottomRightWall, Texture2D[] bottomLeftWall, Texture2D[] topLeftCorner, Texture2D[] topRightCorner, Texture2D[] bottomRightCorner, Texture2D[] bottomLeftCorner, Texture2D[] terrain, Texture2D[] terrainWithWall, Texture2D[] terrainWithWallBorderLeft, Texture2D[] terrainWithWallBorderRight, Texture2D[] topDoor, Texture2D[] rightDoor, Texture2D[] bottomDoor, Texture2D[] leftDoor, Texture2D[] darkTerrain)
        {
            _tileSize = tileSize;

            _topWall = topWall;
            _rightWall = rightWall;
            _bottomWall = bottomWall;
            _leftWall = leftWall;

            _topLeftWall = topLeftWall;
            _topRightWall = topRightWall;
            _bottomRightWall = bottomRightWall;
            _bottomLeftWall = bottomLeftWall;

            _topLeftCorner = topLeftCorner;
            _topRightCorner = topRightCorner;
            _bottomRightCorner = bottomRightCorner;
            _bottomLeftCorner = bottomLeftCorner;

            _terrain = terrain;
            _terrainWithWall = terrainWithWall;
            _terrainWithWallBorderLeft = terrainWithWallBorderLeft;
            _terrainWithWallBorderRight = terrainWithWallBorderRight;

            _topDoor = topDoor;
            _rightDoor = rightDoor;
            _bottomDoor = bottomDoor;
            _leftDoor = leftDoor;

            _darkTerrain = darkTerrain;
        }

        public int TileSize
        {
            get { return _tileSize; }
        }

        public Texture2D TopWall
        {
            get { return _topWall[random.Next(0, _topWall.Length)]; }
        }
        public Texture2D RightWall
        {
            get { return _rightWall[random.Next(0, _rightWall.Length)]; }
        }
        public Texture2D BottomWall
        {
            get { return _bottomWall[random.Next(0, _bottomWall.Length)]; }
        }
        public Texture2D LeftWall
        {
            get { return _leftWall[random.Next(0, _leftWall.Length)]; }
        }

        public Texture2D TopLeftWall
        {
            get { return _topLeftWall[random.Next(0, _topLeftWall.Length)]; }
        }
        public Texture2D TopRightWall
        {
            get { return _topRightWall[random.Next(0, _topRightWall.Length)]; }
        }
        public Texture2D BottomRightWall
        {
            get { return _bottomRightWall[random.Next(0, _bottomRightWall.Length)]; }
        }
        public Texture2D BottomLeftWall
        {
            get { return _bottomLeftWall[random.Next(0, _bottomLeftWall.Length)]; }
        }

        public Texture2D TopLeftCorner
        {
            get { return _topLeftCorner[random.Next(0, _topLeftCorner.Length)]; }
        }
        public Texture2D TopRightCorner
        {
            get { return _topRightCorner[random.Next(0, _topRightCorner.Length)]; }
        }
        public Texture2D BottomRightCorner
        {
            get { return _bottomRightCorner[random.Next(0, _bottomRightCorner.Length)]; }
        }
        public Texture2D BottomLeftCorner
        {
            get { return _bottomLeftCorner[random.Next(0, _bottomLeftCorner.Length)]; }
        }

        public Texture2D Terrain
        {
            get
            {
                if (random.Next(0, 11) <= 9)
                    return _terrain[0];
                return _terrain[random.Next(1, _terrain.Length)];
            }
        }
        public Texture2D TerrainWithWall
        {
            get { return _terrainWithWall[random.Next(0, _terrainWithWall.Length)]; }
        }
        public Texture2D TerrainWithWallBorderLeft
        {
            get { return _terrainWithWallBorderLeft[random.Next(0, _terrainWithWallBorderLeft.Length)]; }
        }
        public Texture2D TerrainWithWallBorderRight
        {
            get { return _terrainWithWallBorderRight[random.Next(0, _terrainWithWallBorderRight.Length)]; }
        }

        public Texture2D TopDoor
        {
            get { return _topDoor[random.Next(0, _topDoor.Length)]; }
        }
        public Texture2D RightDoor
        {
            get { return _rightDoor[random.Next(0, _rightDoor.Length)]; }
        }
        public Texture2D BottomDoor
        {
            get { return _bottomDoor[random.Next(0, _bottomDoor.Length)]; }
        }
        public Texture2D LeftDoor
        {
            get { return _leftDoor[random.Next(0, _leftDoor.Length)]; }
        }

        public Texture2D DarkTerrain
        {
            get { return _darkTerrain[random.Next(0, _leftDoor.Length)]; }
        }
    }
}
