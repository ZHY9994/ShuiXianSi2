Shader "MK/Toon/URP/Particles/Unlits" {
	Properties {
		[Enum(MK.Toon.Surface)] _Surface ("", Float) = 1
		_Blend ("", Float) = 0
		[Toggle] _AlphaClipping ("", Float) = 0
		[Enum(MK.Toon.RenderFace)] [HideInInspector] _RenderFace ("", Float) = 2
		_AlbedoColor ("", Vector) = (1,1,1,1)
		_AlphaCutoff ("", Range(0, 1)) = 0.5
		_AlbedoMap ("", 2D) = "white" {}
		_AlbedoMapIntensity ("", Range(0, 1)) = 1
		_Contrast ("", Float) = 1
		[MKToonSaturation] _Saturation ("", Float) = 1
		[MKToonBrightness] _Brightness ("", Float) = 1
		[Enum(MK.Toon.ColorGrading)] _ColorGrading ("", Float) = 0
		[Toggle] _VertexAnimationStutter ("", Float) = 0
		[Enum(MK.Toon.VertexAnimation)] _VertexAnimation ("", Float) = 0
		_VertexAnimationIntensity ("", Range(0, 1)) = 0.05
		_VertexAnimationMap ("", 2D) = "white" {}
		_NoiseMap ("", 2D) = "white" {}
		[MKToonVector3Drawer] _VertexAnimationFrequency ("", Vector) = (2.5,2.5,2.5,1)
		[Enum(MK.Toon.Dissolve)] _Dissolve ("", Float) = 0
		_DissolveMapScale ("", Float) = 1
		_DissolveMap ("", 2D) = "white" {}
		_DissolveAmount ("", Range(0, 1)) = 0.5
		_DissolveBorderSize ("", Range(0, 1)) = 0.25
		_DissolveBorderRamp ("", 2D) = "white" {}
		[HDR] _DissolveBorderColor ("", Vector) = (1,1,1,1)
		[Enum(MK.Toon.BlendFactor)] [HideInInspector] _BlendSrc ("", Float) = 1
		[Enum(MK.Toon.BlendFactor)] [HideInInspector] _BlendDst ("", Float) = 0
		[Enum(MK.Toon.BlendFactor)] [HideInInspector] _BlendSrcAlpha ("", Float) = 1
		[Enum(MK.Toon.BlendFactor)] [HideInInspector] _BlendDstAlpha ("", Float) = 0
		[Enum(MK.Toon.ZWrite)] _ZWrite ("", Float) = 1
		[Enum(MK.Toon.ZTest)] _ZTest ("", Float) = 4
		[MKToonRenderPriority] _RenderPriority ("", Range(-50, 50)) = 0
		[Enum(MK.Toon.Stencil)] _Stencil ("", Float) = 1
		[MKToonStencilRef] _StencilRef ("", Range(0, 255)) = 0
		[MKToonStencilReadMask] _StencilReadMask ("", Range(0, 255)) = 255
		[MKToonStencilWriteMask] _StencilWriteMask ("", Range(0, 255)) = 255
		[Enum(MK.Toon.StencilComparison)] _StencilComp ("", Float) = 8
		[Enum(MK.Toon.StencilOperation)] _StencilPass ("", Float) = 0
		[Enum(MK.Toon.StencilOperation)] _StencilFail ("", Float) = 0
		[Enum(MK.Toon.StencilOperation)] _StencilZFail ("", Float) = 0
		[Toggle] _Flipbook ("", Float) = 0
		[Toggle] _SoftFade ("", Float) = 0
		[MKToonSoftFadeNearDistance] _SoftFadeNearDistance ("", Float) = 0
		[MKToonSoftFadeFarDistance] _SoftFadeFarDistance ("", Float) = 1
		_SoftFadeModeParams ("", Vector) = (0,0,0,0)
		[Enum(MK.Toon.ColorBlend)] _ColorBlend ("", Float) = 0
		[Toggle] _CameraFade ("", Float) = 0
		_CameraFadeParams ("", Vector) = (0,0,0,0)
		[MKToonCameraFadeNearDistance] _CameraFadeNearDistance ("", Float) = 1
		[MKToonCameraFadeFarDistance] _CameraFadeFarDistance ("", Float) = 2
		[HideInInspector] _Initialized ("", Float) = 0
		[HideInInspector] _OptionsTab ("", Float) = 1
		[HideInInspector] _InputTab ("", Float) = 1
		[HideInInspector] _StylizeTab ("", Float) = 0
		[HideInInspector] _AdvancedTab ("", Float) = 0
		[HideInInspector] _ParticlesTab ("", Float) = 0
		[HideInInspector] _Cutoff ("", Range(0, 1)) = 0.5
		[HideInInspector] _MainTex ("", 2D) = "white" {}
		[HideInInspector] _Color ("", Vector) = (1,1,1,1)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_ObjectToWorld;
			float4x4 unity_MatrixVP;
			float4 _MainTex_ST;

			struct Vertex_Stage_Input
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct Vertex_Stage_Output
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.uv = (input.uv.xy * _MainTex_ST.xy) + _MainTex_ST.zw;
				output.pos = mul(unity_MatrixVP, mul(unity_ObjectToWorld, input.pos));
				return output;
			}

			Texture2D<float4> _MainTex;
			SamplerState sampler_MainTex;
			float4 _Color;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(sampler_MainTex, input.uv.xy) * _Color;
			}

			ENDHLSL
		}
	}
	//CustomEditor "MK.Toon.Editor.URP.ParticlesUnlitEditor"
}