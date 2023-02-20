using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
namespace SpeedSpaceJam1
{
    class Player
    {
        public int hp;
        public Texture2D[] textures;
        public Vector2 position;
        public Vector2 scale;
        public Vector2 baseScale;
        public float rotation;
        public Rectangle sourceRect;
        public Rectangle destinationRect;
        FloorFollower floorFollower;
        public Vector2 velocity;
        public float acceleration;
        public float maxVelocityX;
        public float heightInAir;
        public float jumpSpeed;
        public float gravity;
        public void Update()
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) velocity.X += acceleration * maxVelocityX * Raylib.GetFrameTime();
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) velocity.X -= acceleration * maxVelocityX * Raylib.GetFrameTime();
            velocity.X = velocity.X > maxVelocityX ? maxVelocityX : velocity.X;
            velocity.X = velocity.X < -maxVelocityX ? -maxVelocityX : velocity.X;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) && heightInAir == 0) velocity.Y = -jumpSpeed * Raylib.GetFrameTime();
            if (velocity.Y > 0 && heightInAir > 0)
            {
                heightInAir = 0;
                velocity.Y = 0;
            }
            velocity.Y -= gravity * Raylib.GetFrameTime();
            heightInAir += velocity.Y;
            floorFollower.speed = velocity.X;
            floorFollower.floorOffset = heightInAir;
        }
    }
}
