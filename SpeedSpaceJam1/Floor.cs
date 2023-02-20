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
            GetTileTransformations();
        }
        public bool IsLineIntersect(Vector2 startPt, Vector2 endPt, out Vector2 collisionPt)
        {
            collisionPt = new Vector2();
            for (int i = 0; i < points.Length - 1; i++)
                if (Raylib.CheckCollisionLines(points[i], points[i + 1], startPt, endPt, ref collisionPt))
                    return true;
            if (Raylib.CheckCollisionLines(points[0], points[points.Length - 1], startPt, endPt, ref collisionPt))
                return true;
            return false;
        }
        public bool FindT(Vector2 pt, out float t)
        {
            t = 0f;
            for (int i = 0; i < points.Length; i++)
            {
                var nextI = i + 1 >= points.Length ? 0 : i + 1;
                if (Raylib.CheckCollisionPointLine(pt, points[i], points[nextI], 5)) {
                    var ptMagnitude = (pt - points[i]).Length();
                    var lineMagnitude = (points[nextI] - points[i]).Length();
                    t = (ptMagnitude / lineMagnitude) + i;
                    return true;
                }
            }
            return false;
        }
        public bool GetPoint(float t, out Vector2 pt)
        {
            if (t < 0f)
            {
                return GetPoint(t + points.Length, out pt);
            }
            else if (t > points.Length)
            {
                return GetPoint(t - points.Length, out pt);
            }
            else
            {
                //Console.WriteLine("T!!! " + t + " plength  !! " + points.Length);
                var i = (int)t;
                if (i == points.Length - 1) pt = Vector2.Lerp(points[points.Length - 1], points[0], t - i);
                else pt = Vector2.Lerp(points[i], points[i + 1], t - i);
                return true;
            }
        }
        public void DrawFloor()
        {
            for (int i = 0; i < points.Length; i++)
            {
                int nextI = i + 1 >= points.Length ? 0 : i + 1;
                //Raylib.DrawLine((int)points[i].X, (int)points[i].Y, (int)points[nextI].X, (int)points[nextI].Y, Color.BLUE);
                //Raylib.DrawRectangle((int)segmentTileRectangles[i].x, (int)segmentTileRectangles[i].y, (int)segmentTileRectangles[i].width, (int)segmentTileRectangles[i].height, Color.BLUE);
                //Raylib.DrawTextureEx(Globals.wallTexture[0], Vector2.One, 0f, 2f, Color.WHITE);
                var texI = segmentTileRotations[i] == 0f ? 1 : segmentTileRotations[i] == 90f ? 2 : segmentTileRotations[i] == 180f ? 3 : segmentTileRotations[i] == 270f ? 0 : 0;
                Raylib.DrawTextureTiled(
                    Globals.wallTexture[texI], 
                    Globals.wallTexture[texI].GetSourceRectangle(),
                    segmentTileRectangles[i],
                    Vector2.Zero,
                    0f,
                    1f,
                    Color.WHITE
                );
                
            }
        }
        Rectangle[] segmentTileRectangles;
        float[] segmentTileRotations;
        float[] segmentTileTanRotations;
        public void GetTileTransformations()
        {
            segmentTileRectangles = new Rectangle[points.Length];
            segmentTileRotations = new float[points.Length];
            segmentTileTanRotations = new float[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                int nextI = i + 1 >= points.Length ? 0 : i + 1;
                var dir = points[nextI] - points[i];
                var inverseNormalAngle = MathF.Atan2(dir.Y, dir.X) + (MathF.PI / 2f);
                var inverseNormalVel = (MathF.SinCos(inverseNormalAngle)).GetVector2();
                segmentTileRotations[i] = inverseNormalAngle * (180f / MathF.PI);
                segmentTileTanRotations[i] = MathF.Atan2(dir.Y, dir.X) * (180f / MathF.PI);
                Vector2 pt1 = points[i], pt2 = points[nextI] + (inverseNormalVel * floorTileSize), size = new Vector2(MathF.Abs(pt1.X - pt2.X), MathF.Abs(pt1.Y - pt2.Y)), startPt = Vector2.Zero;
                startPt.X = pt1.X < pt2.X ? pt1.X : pt2.X;
                startPt.Y = pt1.Y < pt2.Y ? pt1.Y : pt2.Y; 
                segmentTileRectangles[i] = new Rectangle(startPt.X, startPt.Y, size.X, size.Y);
            }
        }
    }
}
