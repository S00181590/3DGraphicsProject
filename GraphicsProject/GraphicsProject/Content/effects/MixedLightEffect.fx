#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix World,View,Projection;

texture Texture;
bool TextureEnabled = false;

float3 AmbientColor = float3(.15, .15, .15);
float3 DiffuseColor = float3(1, 1, 1);
float3 LColor1 = float3(1, 0, 0);
float3 LColor2 = float3(0, 1, 0);

float3 Position = float3(0, 0, 10);
float3 LPosition1 = float3(0, 0, 10);
float3 LPosition2 = float3(0, 0, 10);
float Attenuation = 20;
float Falloff = 2;

sampler TextureSampler = sampler_state
{
	texture = <Texture>;
};

struct VertexShaderInput
{
	float4 Position : SV_POSITION0;
	float4 LPosition1 : SV_POSITION1;
	float4 LPosition2 : SV_POSITION2;
	float4 Color : COLOR0;
	float4 LColor1 : COLOR1;
	float4 LColor2 : COLOR2;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION0;
	float4 LPosition1 : SV_POSITION1;
	float4 LPosition2 : SV_POSITION2;

	float4 Color : COLOR0;
	float4 LColor1 : COLOR1;
	float4 LColor2 : COLOR2;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, world);
	float4 viewPosition = mul(worldPosition, view);
	float4 worldPosition1 = mul(input.Position1, world);
	float4 viewPosition1 = mul(worldPosition1, View);
	float4 worldPosition2 = mul(input.Position2, world);
	float4 viewPosition2 = mul(worldPosition2, View);

	output.Position = mul(viewPosition, Projection);
	output.LPosition1 = mul(viewPosition1, Projection);
	output.LPosition2 = mul(viewPosition2, Projection);

	return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR0
{
	float3 color = DiffuseColor;

	if (TextureEnabled)
	{
		color *= tex2D(TextureSampler, input.UV);
		float3 lighting = AmbientColor;
		float3 lightDir = normalize(Position - input.WorldPosition);
		float3 normal = normalize(input.Normal);
		float3 diffuse = saturate(dot(normal, lightDir));

		float3 refl = reflect(lightDir, normal);
		float3 view = normalize(input.ViewDirection);
		float d = distance(Position, input.WorldPosition);

		float att = 1 - pow(clamp(d / Attenuation, 0, 1), Falloff);

		lighting1 += diffuse * att * LColor1;

	}

	return float4(color * lighting, 1);
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};