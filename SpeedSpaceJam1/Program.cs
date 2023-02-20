using static Raylib_cs.Raylib;
using SpeedSpaceJam1;
using Raylib_cs;
using System.Numerics;

unsafe
{
    Globals.resPath = new string(GetWorkingDirectory()) + @"\res\";
    Console.WriteLine(Globals.resPath);
}

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

Globals.player = new Player();
Camera2D camera = new Camera2D();

camera.target = Globals.player.position;
camera.zoom = 1f;
camera.rotation = 0f;
camera.offset = new Vector2(0, 0);


AudioManager.Start();
MazeBuilder.Start();
Score.Start();
Globals.behaviours = new List<IBehaviour>();
for (int i = 0; i < Globals.floors.Length; i++)
    for (int j = 0; j < Globals.floors[i].points.Length; j++)
        Globals.behaviours.Add(new Saw(i, 100f, j));
        //Globals.behaviours.Add(new IBehaviour());

Globals.wallTexture = new Texture2D[4];
for (int i = 0; i < Globals.wallTexture.Length; i++) Globals.wallTexture[i] = LoadTexture(Globals.resPath + $"wall-{i}.png");

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
    Score.Update();
    camera.target = Globals.player.position - new Vector2(1920f/2f,1080f/2f);
    //camera.zoom = 0.1f;
    BeginDrawing();

        ClearBackground(Color.BLACK);
        BeginTextureMode(target);
        ClearBackground(new Color(0,0,0,0));
        BeginMode2D(camera);

        if (UIManager.state == UIManager.State.DrawScore)
                {
        //Console.WriteLine("Draw player/thing");
        //Globals.floors[0].DrawFloor();
                    foreach (var floor in Globals.floors) floor.DrawFloor();
                    for (int i = 0; i < Globals.behaviours.Count; i++) Globals.behaviours[i].Update();
                    //Console.WriteLine(Globals.player.position);
                    //Console.WriteLine(camera.target);
                    Globals.player.Update();

                }
        //DrawText("Congrats! You created your first window!\nyes", 190, 200, 20, Color.BLUE);
        //DrawTexture(LoadTexture(workingDir + "/res/ghost.png"), 250, 250, Color.GREEN);
        //ghostSprite.DrawSprite();
        EndMode2D();
        EndTextureMode();
            
        

        //BeginShaderMode(shader);
            DrawTextureRec(target.texture, new Rectangle( 0, 0, (float)target.texture.width, (float)-target.texture.height), new Vector2(0, 0), Color.WHITE);
        //EndShaderMode();
    
    UIManager.DrawUI();

    EndDrawing();
}

CloseWindow();
return 0;

