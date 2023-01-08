using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overflow.src
{
    public class EnemySet
    {
        private static Random random = new Random();

        private Texture2D[] _seeker;
        private Texture2D[] _archer;

        public EnemySet(Texture2D[] seeker, Texture2D[] archer)
        {
            _seeker = seeker;
            _archer = archer;
        }

        public Texture2D[] Seekers
        {
            get { return _seeker; }
        }
        public Texture2D Seeker
        {
            get { return _seeker[random.Next(0, _seeker.Length)]; }
        }

        public Texture2D Archer
        {
            get { return _archer[random.Next(0, _archer.Length)]; }
        }
    }
}
