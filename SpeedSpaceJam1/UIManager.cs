using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
namespace SpeedSpaceJam1
{
    public static class UIManager
    {
        public static UIButton leaderboard_backBtn;
        public static UIButton finalScore_submitScoreBtn;
        public static UIButton finalScore_leaderboardBtn;
        public static UIButton finalScore_playBtn;
        public static UIButton finalScore_exitBtn;
        public static UIInputBox submitScore_nameField;
        public static UIButton submitScore_submitBtn;
        public static UIButton submitScore_backBtn;
        public static class LeaderboardUI
        {
            public static Rectangle panel;
            public static Rectangle title;
            public static Rectangle players;
            public static Rectangle scores;
            public static UIButton backBtn;
        }
        public static class FinalScoreUI 
        {
            public static Rectangle panel;
            public static Rectangle title;
            public static int titleSize = 96;
            public static Rectangle text;
            public static int textSize = 200;
            
        }
        static Color inputBgColor;
        static Color panelColor;
        static Color textColor;
        static Color buttonColor;
        public enum State
        {
            DrawScore,
            DrawFinalScore,
            DrawSubmitScore,
            DrawLeaderboard,
        }
        public static State state;
        public static void Start()
        {
            inputBgColor = new Color(29, 29, 29, 255);
            panelColor = new Color(45,45,45, 255);
            textColor = Color.WHITE;
            buttonColor = new Color(255, 0, 245, 255);

            leaderboard_backBtn = new UIButton(
                "Back", 
                new Rectangle(710, 900, 500, 100), 
                textColor, 
                buttonColor, 
                buttonColor, 
                delegate() { leaderboard_backBtn_click(); }
            );
            finalScore_submitScoreBtn = new UIButton(
                "SUBMIT SCORE",
                new Rectangle(710, 540, 500, 100),
                textColor,
                buttonColor,
                buttonColor,
                delegate () { finalScore_submitScoreBtn_click(); }
            );
            finalScore_leaderboardBtn = new UIButton(
                "LEADERBOARD",
                new Rectangle(710, 665, 500, 100),
                textColor,
                buttonColor,
                buttonColor,
                delegate () { finalScore_leaderboardBtn_click(); }
            );
            finalScore_playBtn = new UIButton(
                "PLAY AGAIN",
                new Rectangle(710, 790, 500, 100),
                textColor,
                buttonColor,
                buttonColor,
                delegate () { finalScore_leaderboardBtn_click(); }
            );
            finalScore_exitBtn = new UIButton(
                "EXIT GAME",
                new Rectangle(710, 921, 500, 100),
                textColor,
                buttonColor,
                buttonColor,
                delegate () { finalScore_leaderboardBtn_click(); }
            );
            submitScore_nameField = new UIInputBox(
                15,
                new Rectangle(660,359,600,150),
                textColor,
                inputBgColor,
                Color.WHITE,
                _drawBorders: false
            );
            submitScore_submitBtn = new UIButton(
                "SUBMIT",
                new Rectangle(710,641,500,100),
                textColor,
                buttonColor,
                buttonColor,
                delegate () { submitScore_submitBtn_click(); }
            );
            submitScore_backBtn = new UIButton(
                "SUBMIT",
                new Rectangle(710, 791, 500, 100),
                textColor,
                buttonColor,
                buttonColor,
                delegate () { submitScore_backBtn_click(); }
            );
        }
        public static void DrawLeaderboard()
        {
            DrawRectangle(600, 109, 720, 720, panelColor);
            DrawTextEx(Globals.font, "TOP LEADERBOARD", new Vector2(666, 150), 96, 2, textColor);
            DrawTextEx(Globals.font, Score.GetFormattedNames(), new Vector2(703,280), 50, 2, textColor);
            DrawTextEx(Globals.font, Score.GetFormattedScores(), new Vector2(703, 280), 50, 2, textColor); // if possible make text right centered
            leaderboard_backBtn.Update();
        }
        public static void DrawFinalScore()
        {
            DrawRectangle(600, 50, 720, 450, panelColor);
            DrawTextEx(Globals.font, "YOUR SCORE IS", new Vector2(750, 75), 96, 2, textColor);
            DrawTextEx(Globals.font, Score.GetFormattedScore(), new Vector2(750, 168), 200, 2, buttonColor);
            DrawTextEx(Globals.font, $"BEST SCORE: {Score.GetBestScore()}", new Vector2(655, 355), 96, 2, textColor);
            finalScore_submitScoreBtn.Update();
            finalScore_leaderboardBtn.Update();
            finalScore_playBtn.Update();
            finalScore_exitBtn.Update();
        }
        public static void DrawSubmitScore()
        {
            DrawRectangle(600, 188, 720, 400, panelColor);
            DrawTextEx(Globals.font, "SUBMIT SCORE AS", new Vector2(666, 239), 96, 2, textColor);
            submitScore_nameField.Update();
            submitScore_submitBtn.Update();
            submitScore_backBtn.Update();
        }
        public static void leaderboard_backBtn_click()
        {
            state = State.DrawFinalScore;
        }
        public static void finalScore_submitScoreBtn_click()
        {
            state = State.DrawFinalScore;
        }
        public static void finalScore_leaderboardBtn_click()
        {
            state = State.DrawLeaderboard;
        }
        public static void finalScore_playBtn_click()
        {
            state = State.DrawScore;
        }
        public static void finalScore_exitBtn_click()
        {
            Globals.quitGame = true;
        }
        private static void submitScore_submitBtn_click()
        {
            Score.DreamloAddScore(new string(submitScore_nameField.name), (int)(Score.time * 100), Globals.httpClient);
        }
        private static void submitScore_backBtn_click()
        {
            state = State.DrawFinalScore;
        }
    }
}
