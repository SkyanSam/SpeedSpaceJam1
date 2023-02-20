using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SpeedSpaceJam1
{ using Raylib_cs;
    class FloorFollower
    {
        public int floorID;
        public float speed = 0;
        float tSpeed = 0;
        public float t = 0;
        public float floorOffset = 0;
        public Vector2 position = Vector2.Zero;
        public Vector2 dir = Vector2.One;
        public void Start()
        {
            UpdatePosition();
        }
        public void Update()
        {
            //Console.WriteLine(GetTangentAngle());
            //Console.WriteLine("norm :  " + GetNormalAngle() * 180f / MathF.PI);
            UpdateTSpeed();
            UpdateT();
            UpdatePosition();
        }
        public void UpdateTSpeed()
        {
            var nextI = (int)t + 1 >= Globals.floors[floorID].points.Length ? 0 : (int)t + 1;
            var segmentDistance = (Globals.floors[floorID].points[nextI] - Globals.floors[floorID].points[(int)t]).Length();
            tSpeed = speed / segmentDistance;
        }
        public void UpdateT()
        {
            t += tSpeed * Raylib.GetFrameTime();
            if (t < 0f) t += Globals.floors[floorID].points.Length;
            if (t > Globals.floors[floorID].points.Length) t -= Globals.floors[floorID].points.Length;
        }
        public void UpdatePosition(bool updateDir = true)
        {
            if (Globals.floors[floorID].GetPoint(t, out Vector2 outPoint))
                position = outPoint + (GetNormalVector2() * floorOffset);

            var nextI = (int)t + 1 >= Globals.floors[floorID].points.Length ? 0 : (int)t + 1;
            if (updateDir)
            {
                var thisDir = Globals.floors[floorID].points[nextI] - Globals.floors[floorID].points[(int)t];
                dir = thisDir / thisDir.Length();
            }
        }
        public float GetTangentAngle()
        {
            //Console.WriteLine($"dir{dir.Y},{dir.X}");
            return MathF.Atan2(dir.Y, dir.X);
        }
        public float GetNormalAngle()
        {
            return GetTangentAngle() + (MathF.PI / 2f);
        }
        public Vector2 GetNormalVector2()
        {
            var vect = MathF.SinCos(GetNormalAngle());
            return new Vector2(vect.Cos, vect.Sin);
        }
    }
}
