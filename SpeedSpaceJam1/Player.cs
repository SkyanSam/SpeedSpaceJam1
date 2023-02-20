using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
namespace SpeedSpaceJam1
{
    public class Player
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
        public SpriteRenderer spriteRenderer;
        public enum FloorState
        {
            Normal,
            Switch,
            Opposite
        }
        public FloorState floorState = FloorState.Normal;
        public Player()
        {
            textures = new Texture2D[4];
            for (int i = 0; i < textures.Length; i++) 
                textures[i] = Raylib.LoadTexture(Globals.resPath + $"ghost-{i}");
            spriteRenderer = new SpriteRenderer();
            spriteRenderer.position = position;
            spriteRenderer.SetSizeMultiplier(Vector2.One);
            spriteRenderer.rotation = GetRotation();
            floorFollower = new FloorFollower();
            floorFollower.floorID = 0;
        }
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
            floorFollower.Update();
            position = floorFollower.position;

            spriteRenderer.position = position;
            spriteRenderer.SetSizeMultiplier(Vector2.One);
            spriteRenderer.rotation = GetRotation();

            spriteRenderer.DrawSprite();
        }
        float switchfloorT;
        public float GetRotation()
        {
            switch(floorState)
            {
                case FloorState.Normal:
                    return (velocity.X / maxVelocityX) * 45f;
                case FloorState.Switch:
                    return Globals.Lerp(45f, 135f, switchfloorT);
                case FloorState.Opposite:
                    return ((velocity.X / maxVelocityX) * -45f) + 180f;
            }
            return 0;
        }

    }
}
