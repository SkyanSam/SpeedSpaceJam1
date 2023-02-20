using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace SpeedSpaceJam1
{
    public class Floor
    {
        public const int floorTileSize = 100;
        public Vector2[] points;
        public Floor(Vector2[] points)
        {
            this.points = points;
        }
        public bool IsLineIntersect(Vector2 startPt, Vector2 endPt, out Vector2 collisionPt)
        {
            collisionPt = new Vector2();
            for (int i = 0; i < points.Length - 1; i++)
                if (Raylib.CheckCollisionLines(points[i], points[i + 1], startPt, endPt, ref collisionPt))
                    return true;
            return false;
        }
        public bool FindT(Vector2 pt, out float t)
        {
            t = 0f;
            for (int i = 0; i < points.Length - 1; i++)
            {
                if (Raylib.CheckCollisionPointLine(pt, points[i], points[i+1], 5)) {
                    var ptMagnitude = (pt - points[i]).Length();
                    var lineMagnitude = (points[i + 1] - points[i]).Length();
                    t = (ptMagnitude / lineMagnitude) + i;
                    return true;
                }
            }
            return false;
        }
        public bool GetPoint(float t, out Vector2 pt)
        {
            pt = new Vector2();
            if (t < 0f || t > points.Length)
            {
                Console.WriteLine($"{t} as t value is not valid because it is not inside range [{0}, {points.Length}]");
                return false;
            }
            else
            {
                var i = (int)t;
                pt = Vector2.Lerp(points[i], points[i + 1], t);
                return true;
            }
        }
        public void DrawFloor()
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Raylib.DrawTextureTiled(
                    Globals.wallTexture, 
                    Globals.wallTexture.GetSourceRectangle(),
                    segmentTileRectangles[i],
                    Vector2.Zero,
                    segmentTileRotations[i],
                    floorTileSize,
                    Color.WHITE
                );
            }
        }
        Rectangle[] segmentTileRectangles;
        float[] segmentTileRotations;
        public void GetTileTransformations()
        {
            segmentTileRectangles = new Rectangle[points.Length - 1];
            segmentTileRotations = new float[points.Length - 1];
            for (int i = 0; i < points.Length - 1; i++)
            {
                var dir = points[i + 1] - points[i];
                var inverseNormalAngle = MathF.Atan2(dir.Y, dir.X) * (180f / MathF.PI) - 90f;
                var inverseNormalVel = MathF.SinCos(inverseNormalAngle).GetVector2();
                segmentTileRotations[i] = inverseNormalAngle;
                Vector2 pt1 = points[i], pt2 = points[i + 1] + (inverseNormalVel * floorTileSize), size = new Vector2(MathF.Abs(pt1.X - pt2.X), MathF.Abs(pt1.Y - pt2.Y)), startPt = Vector2.Zero;
                if (pt1.X < pt2.X && pt1.Y < pt2.Y) startPt = pt1;
                else if (pt1.X > pt2.X && pt1.Y > pt2.Y) startPt = pt2;
                else if (pt1.X < pt2.X && pt1.Y > pt2.Y) startPt = new Vector2(pt1.X, pt2.Y);
                else if (pt1.X > pt2.X && pt1.Y < pt2.Y) startPt = new Vector2(pt2.X, pt1.X);
                segmentTileRectangles[i] = new Rectangle(startPt.X, startPt.Y, size.X, size.Y);
            }
        }
    }
}
