using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace SpeedSpaceJam1
{
    public class SpriteRenderer
    {
        Texture2D texture;
        public Vector2 position;
        public float rotation;
        Vector2 size;
        Rectangle sourceRect;
        public void DrawSprite()
        {
            var destinationRect = new Rectangle(position.X, position.Y, size.X, size.Y);
            Console.WriteLine($"{destinationRect.width},{destinationRect.height}");
            Raylib.DrawTexturePro(texture, sourceRect, destinationRect, size / 2f, rotation, Color.WHITE);
        }
        public void LoadTexture2D(string path)
        {
            texture = Raylib.LoadTexture(path);
            sourceRect = new Rectangle(0f, 0f, texture.width, texture.height);
        }
        public void SetTexture2D(Texture2D _texture)
        {
            texture = _texture;
            sourceRect = new Rectangle(0f, 0f, texture.width, texture.height);
        }
        public void SetSizeMultiplier(Vector2 _sizeM)
        {
            size = new Vector2(texture.width * _sizeM.X, texture.height * _sizeM.Y);
        }
        public void SetSize(Vector2 _size)
        {
            size = _size;
        }
        public Vector2 GetSize()
        {
            return size;
        }
        public Vector2 GetSizeMultiplier()
        {
            return size / new Vector2(texture.width, texture.height);
        }
    }
}
