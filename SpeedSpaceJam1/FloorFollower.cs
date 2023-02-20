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
        public float speed;
        public float tSpeed;
        public float t;
        public float floorOffset;
        public Vector2 position;
        public Vector2 dir = Vector2.One;
        public void Start()
        {
            UpdatePosition(updateDir: false);
        }
        public void Update()
        {
            UpdateTSpeed();
            UpdateT();
            UpdatePosition();
        }
        public void UpdateTSpeed()
        {
            var segmentDistance = (Globals.floors[floorID].points[(int)t + 1] - Globals.floors[floorID].points[(int)t]).Length();
            tSpeed = speed / segmentDistance;
        }
        public void UpdateT()
        {
            t += tSpeed * Raylib.GetFrameTime();
        }
        public void UpdatePosition(bool updateDir = true)
        {
            var oldPosition = position;
            if (Globals.floors[floorID].GetPoint(t, out Vector2 outPoint))
                position = outPoint + (GetNormalVector2() * floorOffset);

            if (updateDir)
            {
                var thisDir = position - oldPosition;
                dir = thisDir / thisDir.Length();
            }
        }
        public float GetTangentAngle()
        {
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
