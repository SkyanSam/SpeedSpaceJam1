using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.MouseButton;
using System.Numerics;
namespace SpeedSpaceJam1
{
    public class UIButton
    {
        public Color textColor;
        public Color backgroundColor;
        public Color borderColor;
        public Rectangle textBox;
        public string text;
        public Action onClick;
        public UIButton(string _text, Rectangle _rect, Color _textColor, Color _backgroundColor, Color _borderColor, Action _onClick)
        {
            text = _text;
            textBox = _rect;
            textColor = _textColor;
            backgroundColor = _backgroundColor;
            borderColor = _borderColor;
            textBox = _rect;
            onClick = _onClick;
        }
        public void Update()
        {
            var mousePoint = GetMousePosition();
            if (CheckCollisionPointRec(mousePoint, textBox))
            {
                if (IsMouseButtonPressed(MOUSE_BUTTON_LEFT))
                {
                    onClick.Invoke();
                }
            }
            DrawRectangleRec(textBox, backgroundColor);
            DrawRectangleLines((int)textBox.x, (int)textBox.y, (int)textBox.width, (int)textBox.height, borderColor);
            DrawTextEx(Globals.font, text, new Vector2((int)textBox.x + 5, (int)textBox.y + 8), 40, 2, textColor);
        }
    }
}
