using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SpeedSpaceJam1
{
    public class IBehaviour
    {
        public Vector2 position = new Vector2(0, 0);
        public Vector2 scale = new Vector2(1, 1);
        public float rotation = 0f;
        public string type = "";
        public virtual void Update()
        {

        }
    }
}
