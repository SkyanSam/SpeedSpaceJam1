using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.Color;
using static Raylib_cs.KeyboardKey;
using static Raylib_cs.MouseCursor;
using System.Numerics;
namespace SpeedSpaceJam1
{
    public class UIInputBox
    {
        public int maxInputChars = 9;
        public char[] name;
        public Color textColor;
        public Color backgroundColor;
        public Color borderColor;
        public Rectangle textBox;
        int letterCount = 0;
        bool drawBorders = true;
        public UIInputBox(int _maxInputChars, Rectangle _rect, Color _textColor, Color _backgroundColor, Color _borderColor, bool _drawBorders = true)
        {
            maxInputChars = _maxInputChars;
            name = new char[maxInputChars];
            textBox = _rect;
            textColor = _textColor;
            backgroundColor = _backgroundColor;
            borderColor = _borderColor;
            textBox = _rect; 
            drawBorders = _drawBorders;
        }
        public void Update()
        {
            // Check if more characters have been pressed on the same frame
            int key = GetCharPressed();

            while (key > 0)
            {
                // NOTE: Only allow keys in range [32..125]
                if ((key >= 32) && (key <= 125) && (letterCount < maxInputChars))
                {
                    name[letterCount] = (char)key;
                    letterCount++;
                }

                // Check next character in the queue
                key = GetCharPressed();
            }

            if (IsKeyPressed(KEY_BACKSPACE))
            {
                letterCount -= 1;
                if (letterCount < 0)
                {
                    letterCount = 0;
                }
                name[letterCount] = '\0';
            }
            DrawRectangleRec(textBox, backgroundColor);
            if (drawBorders) DrawRectangleLines((int)textBox.x, (int)textBox.y, (int)textBox.width, (int)textBox.height, borderColor);
            DrawTextEx(Globals.font, new string(name), new Vector2((int)textBox.x + 5, (int)textBox.y + 8), 40, 2, textColor);
        }
    }
}
