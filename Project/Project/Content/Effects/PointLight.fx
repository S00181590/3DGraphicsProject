#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix World,View,Projection;

float3 AmbientColor = float3(0.15f, 0.15f, 0.15f);
float3 DiffuseColor = float3(1, 1, 1);
float3 LightColor = float3(1, 1, 1);

float3 Position = float3(0, 0, 0);
float Attenuation = 40;
float FallOff = 2;

Texture2D ModelTexture;
sampler TextureSampler
{
};

struct VertexShaderInput
{
    float4 Position : POSITION0;
    float2 UV : TEXCOORD0;
    float3 Normal : NORMAL0;
};

struct VertexShaderOutput
{
    float4 Position : SV_POSITION;
    float2 UV : TEXCOORD0;
    float3 Normal : TEXCOORD1;
    float3 WorldPosition : TEXCOORD2;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 world = mul(input.Position, World);
    float4 view = mul(world, View);
    float4 proj = mul(view, Projection);

    output.Position = proj;
    output.UV = input.UV;
    output.Normal = normalize(mul(input.Normal, World));
    output.WorldPosition = world;

    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float3 color = DiffuseColor;
    float3 textureColor = ModelTexture.Sample(TextureSampler, input.UV);
    color *= textureColor;

    float3 lightingColor = AmbientColor;
    float3 lightDirection = normalize(Position - input.WorldPosition);
    float dist = distance(Position, input.WorldPosition);

    //reflectance function
    float3 angle = saturate(dot(input.Normal, lightDirection));
    //inverse square law
    float atten = 1 - pow(clamp(dist / Attenuation, 0, 1), FallOff);

    lightingColor += saturate(angle * atten * LightColor);
    return float4(lightingColor * color, 1);
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};