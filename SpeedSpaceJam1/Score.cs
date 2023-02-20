using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpeedSpaceJam1
{
    class Score
    {
        public HttpClient httpClient = new HttpClient();
        public static float time;
        public static float bestTime;
        public static List<string> leaderboardList;
        public static void Start()
        {
            time = 0f;
        }
        public static void Update()
        {
            time += Raylib.GetFrameTime();
        }
        public string GetTimeString()
        {
            return $"{MathF.Round(time * 100f) / 100f}s";
        }
        public void DrawTimeWidget()
        {
            Raylib.DrawText(GetTimeString(), 10, 10, 50, Color.WHITE);
        }
        public void DrawFinalTimePrompt()
        {
            
        }
        public void DrawSubmitScorePrompt()
        {

        }
        public void DrawLeaderboard()
        {

        }
        public void GetLeaderboard()
        {
            DreamloGetScores(httpClient);
        }
        public static async void DreamloAddScore(string user, int score, HttpClient client)
        {
            await client.GetAsync($"http://dreamlo.com/lb/aDTa9CXtLE6ifGfPqTX3xQGVdZrBsIOUSic4shRKVH4Q/add/{user}/{score}");
        }
        public static async void DreamloGetScores(HttpClient client)
        {
            var json = await client.GetStringAsync("http://dreamlo.com/lb/63f171438f4202768095bfb4/json-asc/5");
            JObject jObj = JObject.Parse(json);
            var entries = jObj["dreamlo"]["leaderboard"]["entry"];
            leaderboardList = new List<string>();
            for (int i = 0; entries.Contains($"{i}"); i++)
            {
                leaderboardList[i] = $"{entries[i]["name"]} : {entries[i]["score"]}";
            }
        }
        public static string GetFormattedNames()
        {
            return "";
        }
        public static string GetFormattedScores()
        {
            return "";
        }
        public static string GetFormattedScore()
        {
            return "";
        }
        public static string GetBestScore()
        {
            return "";
        }
    }
}
