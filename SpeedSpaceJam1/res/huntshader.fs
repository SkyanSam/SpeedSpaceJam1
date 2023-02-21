#version 100

precision mediump float;

// Input vertex attributes (from vertex shader)
varying vec2 fragTexCoord;
varying vec4 fragColor;

// Input uniform values
uniform sampler2D texture0;
uniform vec4 colDiffuse;
uniform vec2 position;
uniform vec2 radius;
uniform vec2 box;

// NOTE: Add here your custom variables

const vec2 size = vec2(800, 450);   // render size
const float samples = 5.0;          // pixels per axis; higher = bigger glow, worse performance
const float quality = 2.5;             // lower = smaller glow, better quality

void main()
{
    vec4 sum = vec4(0);
    vec2 sizeFactor = vec2(1) / size * quality;

    // Texel color fetching from texture sampler
    vec4 source = texture2D(texture0, fragTexCoord);
    if (fragTexCoord)

    // Calculate final fragment color
    gl_FragColor = texture2D(texture0, fragTexCoord);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2[] startBoxList = vec2[1] {};
    vec2 coords = vec2(960.0 / 2.0, 540.0 / 2.0);
    vec2 startBox = vec2(960.0 / 2.0, 0);
    vec2 endBox = vec2(960.0, 540.0 / 2.0);
    float radius = 50.0;
    // Normalized pixel coordinates (from 0 to 1)
    vec2 uv = fragCoord/iResolution.xy;
    
    // Time varying pixel color
    vec3 col = 0.5 + 0.5*cos(iTime+uv.xyx+vec3(0,2,4));
    col = vec3(0,0,0);
    if (!(startBox.x < fragCoord.x && startBox.y < fragCoord.y && endBox.x > fragCoord.x && endBox.y > fragCoord.y)) {
        float f = round(fragCoord.x / 10.0)*10.0;
        if (int(fragCoord.x) - int(f) == 0) {
            col = vec3(255,255,255);
        }
    }
    
    if (sqrt(pow(coords.x - fragCoord.x,2.0) + pow(coords.y - fragCoord.y,2.0)) < radius) {
        if (startBox.x < fragCoord.x && startBox.y < fragCoord.y && endBox.x > fragCoord.x && endBox.y > fragCoord.y) {
            col = vec3(0,0,255);
        }
        else {
            col = vec3(0, 0, col.z);
        }
    }
    // Output to screen
    fragColor = vec4(col,1.0);
}