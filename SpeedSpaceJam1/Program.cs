using static Raylib_cs.Raylib;
using SpeedSpaceJam1;
using Raylib_cs;
using System.Numerics;



// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var workingDir = "C:/Users/samue/OneDrive/Documents/GitHub/SpeedSpaceJam1/SpeedSpaceJam1";
InitWindow(1920, 1080, "raylib [core] example - basic window");
SetTargetFPS(60);

Globals.font = LoadFont(workingDir + "/res/JollyLodger-Regular.ttf");

var ghostSprite = new SpriteRenderer();
ghostSprite.LoadTexture2D(workingDir + "/res/ghost.png");

ghostSprite.position = new Vector2(1920 / 2f, 1080 / 2f);
ghostSprite.SetSizeMultiplier(new Vector2(2f, 2f));
ghostSprite.rotation = 45f;


Vector2 player = Vector2.One;

Camera2D camera = new Camera2D();

camera.target = player;
camera.zoom = 1f;
camera.rotation = 0f;
camera.offset = new Vector2(0, 0);
while (!WindowShouldClose() || !Globals.quitGame)    // Detect window close button or ESC key
{
    var frameTime = GetFrameTime() * 200f;
    if (IsKeyDown(KeyboardKey.KEY_W))
    {
        player.Y -= frameTime;
    }
    if (IsKeyDown(KeyboardKey.KEY_A))
    {
        player.X -= frameTime;
    }
    if (IsKeyDown(KeyboardKey.KEY_S))
    {
        player.Y += frameTime;
    }
    if (IsKeyDown(KeyboardKey.KEY_D))
    {
        player.X += frameTime;
    }
    if (IsKeyDown(KeyboardKey.KEY_SPACE))
    {
        camera.zoom += GetFrameTime() * 0.25f;
    }
    camera.target = player;
    BeginDrawing();
        ClearBackground(Color.RED);
        BeginMode2D(camera);
            DrawText("Congrats! You created your first window!\nyes", 190, 200, 20, Color.BLUE);
            DrawTexture(LoadTexture(workingDir + "/res/ghost.png"), 250, 250, Color.GREEN);
            ghostSprite.DrawSprite();
        EndMode2D();
    EndDrawing();
}

CloseWindow();
return 0;

