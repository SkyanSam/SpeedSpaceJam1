using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace SpeedSpaceJam1
{
    public static class MazeBuilder
    {
        public const int mazeTileSize = 1000;
        public const int mazeInnerHalfSize = 250; // height above tile bounds
        public static void Start()
        {
            Globals.floors = new Floor[3];
            Globals.floors[0] = new Floor(GetMultiple("A2II,B2II,B0II,B0I,B2I,C2II,C1II,D1II,D0III,C0III,C0II,D0I,D1I,E1II,E0II,F0I,F2I,G2I,G2IV,F2IV,F4IV,C4III,C3III,B3IV,B4IV,B4III,B2III,A2III", inverse:true));
            Globals.floors[1] = new Floor(GetMultiple("B2IV,C2IV,C1IV,E1IV,E0IV,F0III,F4II,E4I,E3I,D3I,D2IV,E2IV,E2I,D2II,D3II,B3I"));
            Globals.floors[2] = new Floor(GetMultiple("C3IV,E3III,E4II,C4I"));
        }
        public static Vector2 GetInner(Vector2 position, Vector2 origin)
        {
            return (position * mazeTileSize) + (origin * mazeInnerHalfSize);
        }
        public static Vector2 GetOuter(Vector2 position, Vector2 origin)
        {
            return (position * mazeTileSize) + (origin * mazeTileSize / 2f);
        }
        public static Vector2 Get(string code)
        {
            Vector2 position = Vector2.Zero;
            Vector2 origin = Vector2.Zero;
            switch(code[0])
            {
                case 'A': position.X = 0f; break;
                case 'B': position.X = 1f; break;
                case 'C': position.X = 2f; break;
                case 'D': position.X = 3f; break;
                case 'E': position.X = 4f; break;
                case 'F': position.X = 5f; break;
                case 'G': position.X = 6f; break;
            }
            switch (code[1])
            {
                case '0': position.Y = 0f; break;
                case '1': position.Y = 1f; break;
                case '2': position.Y = 2f; break;
                case '3': position.Y = 3f; break;
                case '4': position.Y = 4f; break;
                case '5': position.Y = 5f; break;
                case '6': position.Y = 6f; break;
            }
            string quadrant = "";
            for (int i = 2; i < code.Length; i++)
                quadrant += code[i];
            switch (quadrant)
            {
                case "I": origin = new Vector2(1,-1); break;
                case "II": origin = new Vector2(-1, -1); break;
                case "III": origin = new Vector2(-1, 1); break;
                case "IV": origin = new Vector2(1, 1); break;
            }
            return GetInner(position, origin);
        }
        public static Vector2[] GetMultiple(string sequence, bool inverse=false)
        {
            var split = sequence.Split(',');
            var pts = new Vector2[split.Length];
            if (inverse) for (int i = 0; i < split.Length; i++) pts[i] = Get(split[split.Length - 1 - i]);
            else for (int i = 0; i < split.Length; i++) pts[i] = Get(split[i]);
            return pts;
        }
    }
}
