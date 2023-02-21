using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using Unglide;
using static System.Net.Mime.MediaTypeNames;

namespace SpeedSpaceJam1
{
    public class Player
    {
        public int hp = 3;
        public Texture2D[] textures;
        public Vector2 position = new Vector2(1f,1f);
        public Vector2 scale = Vector2.One;
        public Vector2 baseScale;
        public float rotation = 0f;
        public Rectangle sourceRect;
        public Rectangle destinationRect;
        FloorFollower floorFollower;
        public Vector2 velocity = new Vector2(0,0);
        public float acceleration = 7;
        public float maxVelocityX = 1000;
        public float heightInAir = 0;
        public float jumpSpeed = 25;
        public float gravity = 100;
        public SpriteRenderer spriteRenderer;
        public int hitBoxSize = 25;
        float timeSinceHit = 0f;
        public float restHeight = 0f;
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
                textures[i] = Raylib.LoadTexture(Globals.resPath + $"ghost-{i}.png");
            floorFollower = new FloorFollower();
            floorFollower.floorID = 0;
            floorFollower.t = 0.1f;
            spriteRenderer = new SpriteRenderer();
            spriteRenderer.position = position;
            spriteRenderer.SetTexture2D(textures[hp]);
            spriteRenderer.SetSizeMultiplier(Vector2.One);
            spriteRenderer.rotation = GetRotation();
            
        }
        public void Update()
        {
            Console.WriteLine($"{velocity},{heightInAir}");
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT)) velocity.X += acceleration * maxVelocityX * Raylib.GetFrameTime();
            else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT)) velocity.X -= acceleration * maxVelocityX * Raylib.GetFrameTime();
            else if (velocity.X > 0)
            {
                velocity.X -= acceleration * maxVelocityX * Raylib.GetFrameTime();
                if (velocity.X < 0) velocity.X = 0;
            }
            else if (velocity.X < 0)
            {
                velocity.X += acceleration * maxVelocityX * Raylib.GetFrameTime();
                if (velocity.X > 0) velocity.X = 0;
            }
            velocity.X = velocity.X > maxVelocityX ? maxVelocityX : velocity.X;
            velocity.X = velocity.X < -maxVelocityX ? -maxVelocityX : velocity.X;
            
            if (velocity.Y > 0 && heightInAir > restHeight)
            {
                heightInAir = restHeight;
                velocity.Y = 0;
            }
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) && heightInAir == restHeight) velocity.Y = -jumpSpeed;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE) && heightInAir == restHeight)
            {
                for (int i = 0; i < Globals.floors.Length; i++)
                {
                    Vector2 collisionPt;
                    var oppVect = floorFollower.GetNormalVector2();
                    oppVect.Y *= -1;
                    oppVect.X *= -1;
                    var pt1 = position + (oppVect * 10);
                    var pt2 = position + (oppVect * (MazeBuilder.mazeInnerHalfSize * 2 + 50));
                    Raylib.DrawLine((int)pt1.X, (int)pt1.Y, (int)pt2.X, (int)pt2.Y, Color.GREEN);
                    var hit = Globals.floors[i].IsLineIntersect(pt1, pt2, out collisionPt);
                    if (hit)
                    {
                        float outT;
                        if (Globals.floors[i].FindT(collisionPt, out outT)) {
                            floorFollower.floorID = i;
                            floorFollower.t = outT;
                            heightInAir = -(collisionPt - position).Length();
                        }
                    }
                }
            }
            velocity.Y += gravity * Raylib.GetFrameTime();
            heightInAir += velocity.Y;

            floorFollower.speed = velocity.X;
            floorFollower.floorOffset = heightInAir;
            floorFollower.Update();
            position = floorFollower.position;

            spriteRenderer.position = position;
            spriteRenderer.rotation = GetRotation();
            spriteRenderer.SetTexture2D(textures[hp]);
            spriteRenderer.SetSize(new Vector2(100,100));
            spriteRenderer.DrawSprite();

            timeSinceHit += Raylib.GetFrameTime();
        }
        float switchfloorT;
        public float GetRotation()
        {
            rotation = MoveTowards(rotation, GetTargetRotation(), 200f, 360f);
            return rotation;
            /*
            switch(floorState)
            {
                case FloorState.Normal:
                    return Ease.CircOut(MathF.Abs(velocity.X / maxVelocityX)) * 45f * (velocity.X > 0?1f:-1f);
                case FloorState.Switch:
                    return Globals.Lerp(45f, 135f, switchfloorT);
                case FloorState.Opposite:
                    return ((velocity.X / maxVelocityX) * -45f) + 180f;
            }*/
            return 0;
        }
        public void Hit()
        {
            //Console.WriteLine("HIT!");
            if (timeSinceHit >= 1f && hp > 0)
            {
                hp -= 1;
                timeSinceHit = 0f;
            }
            if (hp == 0)
            {
                // replace later restarting the entire process takes too much time
                Globals.Restart();
            }
        }
        public float GetTargetRotation()
        {
            return (45f * (velocity.X > 0 ? 1f : velocity.X < 0? -1f : 0f)) + (floorFollower.GetNormalAngle() * 180f / MathF.PI) - 90f;
        }
        public float MoveTowards(float current, float target, float speed, float wrap, float margin = 1f)
        {
            if (MathF.Abs(current - target) < margin) return target;
            if (current == target) return target;
            if (current < 0f) current += wrap;
            if (current > wrap) current -= wrap;
            if (target < 0f) target += wrap;
            if (target > wrap) target -= wrap;
            var rightTarget = current < target? target : target + wrap;
            var leftTarget = current < target ? target - wrap : target;
            var rightDist = MathF.Abs(rightTarget - current);
            var leftDist = MathF.Abs(leftTarget - current);
            if (rightDist < leftDist)
            {
                return current + (speed * Raylib.GetFrameTime());
            }
            else if (leftDist < rightDist)
            {
                return current - (speed * Raylib.GetFrameTime());
            }
            return current;
        }
    }
}
