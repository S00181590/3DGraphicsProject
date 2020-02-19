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

float3 Direction = float3(0, 1, 0);

Texture2D ModelTexture;
sampler TextureSampler
{
    //ModelTexture = <Texture2D>
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
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 world = mul(input.Position, World);
    float4 view = mul(world, View);
    float4 proj = mul(view, Projection);

    output.Position = proj;
    output.UV = input.UV;
   
    float3 normalWorld = mul(input.Normal, World);
    output.Normal = normalize(normalWorld);

    return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	//starting color
    float3 color = DiffuseColor;
    float3 lightDirection = normalize(Direction);
    //color if no light recieved
    float3 lightColor = AmbientColor;
    float3 textureColor = ModelTexture.Sample(TextureSampler, input.UV);

    //reflectance angle
    float3 angle = saturate(dot(input.Normal, lightDirection));
    lightColor += saturate(angle * color * textureColor * LightColor);

    return float4(lightColor, 1);
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};