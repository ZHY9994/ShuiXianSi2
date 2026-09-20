Shader "Custom/Molotov Transparent Bottle"
{
    Properties
    {
        _BaseColor ("Base Color", Color) = (0.05, 0.95, 0.24, 0.32)
        _Color ("Color", Color) = (0.05, 0.95, 0.24, 0.32)
        _EmissionColor ("Emission Color", Color) = (0, 0, 0, 1)
        _Smoothness ("Smoothness", Range(0, 1)) = 0.25
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "RenderPipeline" = "UniversalPipeline"
            "IgnoreProjector" = "True"
        }

        Pass
        {
            Name "Forward"
            Tags { "LightMode" = "UniversalForward" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite On
            ZTest LEqual
            Cull Back

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                half4 _Color;
                half4 _EmissionColor;
                half _Smoothness;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                half3 normalWS : TEXCOORD0;
                half3 viewDirWS : TEXCOORD1;
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                VertexPositionInputs positionInputs = GetVertexPositionInputs(input.positionOS.xyz);
                VertexNormalInputs normalInputs = GetVertexNormalInputs(input.normalOS);

                output.positionCS = positionInputs.positionCS;
                output.normalWS = normalInputs.normalWS;
                output.viewDirWS = GetWorldSpaceNormalizeViewDir(positionInputs.positionWS);
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                half4 tint = _BaseColor;

                Light mainLight = GetMainLight();
                half3 normalWS = normalize(input.normalWS);
                half diffuse = saturate(dot(normalWS, mainLight.direction));
                diffuse = lerp(0.45, 1.0, diffuse);

                half3 viewDirWS = normalize(input.viewDirWS);
                half3 halfDir = normalize(mainLight.direction + viewDirWS);
                half specular = pow(saturate(dot(normalWS, halfDir)), lerp(8.0, 80.0, _Smoothness));

                half3 color = tint.rgb * diffuse * mainLight.color;
                color += specular * _Smoothness * 0.35;
                color += _EmissionColor.rgb * 0.15;

                return half4(color, tint.a);
            }
            ENDHLSL
        }
    }
}
