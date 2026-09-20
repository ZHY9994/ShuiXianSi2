Shader "Skybox/Horizon With Sun Skybox" {
	Properties {
		_SkyColor1 ("Top Color", Vector) = (0.37,0.52,0.73,0)
		_SkyExponent1 ("Top Exponent", Float) = 8.5
		_SkyColor2 ("Horizon Color", Vector) = (0.89,0.96,1,0)
		_SkyColor3 ("Bottom Color", Vector) = (0.89,0.89,0.89,0)
		_SkyExponent2 ("Bottom Exponent", Float) = 3
		_SkyIntensity ("Sky Intensity", Float) = 1
		_SunColor ("Sun Color", Vector) = (1,0.99,0.87,1)
		_SunIntensity ("Sun Intensity", Float) = 2
		_SunAlpha ("Sun Alpha", Float) = 550
		_SunBeta ("Sun Beta", Float) = 1
		_SunVector ("Sun Vector", Vector) = (0.269,0.615,0.74,0)
		_SunAzimuth ("Sun Azimuth (editor only)", Float) = 20
		_SunAltitude ("Sun Altitude (editor only)", Float) = 38
	}
	SubShader {
		Tags { "Queue"="Background" "RenderType"="Background" "PreviewType"="Skybox" }
		Cull Off
		ZWrite Off
		Fog { Mode Off }

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float3 texcoord : TEXCOORD0;
			};

			half4 _SkyColor1;
			half _SkyExponent1;
			half4 _SkyColor2;
			half4 _SkyColor3;
			half _SkyExponent2;
			half _SkyIntensity;

			half4 _SunColor;
			half _SunIntensity;
			half _SunAlpha;
			half _SunBeta;
			half4 _SunVector;

			v2f vert (appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.vertex.xyz;
				return o;
			}

			half4 frag (v2f i) : SV_Target {
				float3 dir = normalize(i.texcoord);

				// --- Sky gradient ---
				// dir.y > 0 : blend Horizon -> Top
				// dir.y < 0 : blend Horizon -> Bottom
				half topLerp = pow(saturate( dir.y), _SkyExponent1);
				half botLerp = pow(saturate(-dir.y), _SkyExponent2);

				half3 sky = lerp(_SkyColor2.rgb, _SkyColor1.rgb, topLerp);
				sky = lerp(sky, _SkyColor3.rgb, botLerp);
				sky *= _SkyIntensity;

				// --- Sun disc ---
				half3 sunDir = normalize(_SunVector.xyz);
				half sunDot = saturate(dot(dir, sunDir));
				half sunDisc = pow(sunDot, _SunAlpha) * _SunBeta;
				half3 sun = _SunColor.rgb * sunDisc * _SunIntensity;

				half3 col = sky + sun;
				return half4(col, 1);
			}
			ENDCG
		}
	}
	Fallback Off
}