using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Raylib_cs;
namespace SpeedSpaceJam1
{
    class Saw : IBehaviour
    {
        FloorFollower floorFollower;
        SpriteRenderer sprite;
        public Saw(int floorID, float speed, float t)
        {
            type = "saw";
            floorFollower = new FloorFollower();
            floorFollower.floorID = floorID;
            floorFollower.speed = speed;
            floorFollower.t = t;
            floorFollower.Start();
            position = floorFollower.position;
            scale = new Vector2(150, 150);
            sprite = new SpriteRenderer();
            sprite.LoadTexture2D(Globals.resPath + "sawwheel.png");
            sprite.position = position;
            sprite.SetSize(scale);
        }
        public override void Update()
        {
            floorFollower.Update();
            position = floorFollower.position;
            sprite.position = position;
            sprite.rotation += 1000f * Raylib.GetFrameTime();
            sprite.DrawSprite();
            
            //Console.WriteLine(Raylib.CheckCollisionCircles(position, scale.X / 2f, Globals.player.position, Globals.player.hitBoxSize));
            //var hit = Raylib.CheckCollisionPointCircle(Globals.player.position, position, scale.X / 2f);
            var hit = Raylib.CheckCollisionCircles(position, scale.X / 2f, Globals.player.position, Globals.player.hitBoxSize);
            if (hit)
                Globals.player.Hit();
        }
    }
}
