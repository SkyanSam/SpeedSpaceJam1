using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
namespace SpeedSpaceJam1
{
    public static class Globals
    {
        public static HttpClient httpClient;
        public static bool quitGame = false;
        public static Texture2D wallTexture;
        public static Floor[] floors = new Floor[0];
        public const string resPath = "C:/Users/samue/OneDrive/Documents/GitHub/SpeedSpaceJam1/SpeedSpaceJam1/res/";
        public static Font font;
        public static Player player;
        public static Rectangle GetSourceRectangle(this Texture2D tex)
        {
            return new Rectangle(0, 0, tex.width, tex.height);
        }
        public static Vector2 GetVector2(this (float Sin, float Cos) vect)
        {
            return new Vector2(vect.Cos, vect.Sin);
        }
        public unsafe static sbyte* ToSbyte(this string str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            return bytes.ToSbyte();
        }
        public unsafe static sbyte* ToSbyte(this int n)
        {
            return (sbyte*)Convert.ToSByte(n);
        }
        public unsafe static sbyte* ToSbyte(this byte[] bytes)
        {
            unsafe
            {
                fixed (byte* p = bytes)
                {
                    sbyte* sp = (sbyte*)p;
                    //SP is now what you want
                    return sp;
                }
            }

        }
        public static float Lerp(float a, float b, float f)
        {
            return a + f * (b - a);
        }
        public static string ToStringSpecial(this Vector2[] list)
        {
            string str = "";
            foreach (var e in list) str += $"({e.X},{e.Y}),";
            return str;
        }
    }
}
