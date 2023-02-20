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

Globals.player = new Player();
AudioManager.Start();
MazeBuilder.Start();


Console.Write("FLOOR LINE : ");
Console.WriteLine(Globals.floors[0].points.ToStringSpecial());

Shader shader;
unsafe
{
    int i = 100;
    shader = LoadShader(0.ToSbyte(), TextFormat($"{Globals.resPath}shaders/glsl{i}/bloom.fs".ToSbyte()));
}
RenderTexture2D target = LoadRenderTexture(1920, 1080);

while (!WindowShouldClose() || !Globals.quitGame)    // Detect window close button or ESC key
{
    AudioManager.Update();
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
        BeginTextureMode(target);
            
            BeginMode2D(camera);
                //DrawText("Congrats! You created your first window!\nyes", 190, 200, 20, Color.BLUE);
                //DrawTexture(LoadTexture(workingDir + "/res/ghost.png"), 250, 250, Color.GREEN);
                //ghostSprite.DrawSprite();
            EndMode2D();
        EndTextureMode();

        BeginShaderMode(shader);
            DrawTextureRec(target.texture, new Rectangle( 0, 0, (float)target.texture.width, (float)-target.texture.height), new Vector2(0, 0), Color.WHITE);
        EndShaderMode();
    EndDrawing();
}

CloseWindow();
return 0;

