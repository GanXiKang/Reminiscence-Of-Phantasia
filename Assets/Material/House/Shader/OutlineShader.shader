Shader "Custom/CharacterOutlineShaderURP"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {} // 主貼圖
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1) // 輪廓顏色
        _OutlineThickness("Outline Thickness", Range(0.01, 10)) = 1.0 // 輪廓厚度
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalRenderPipeline" }
        LOD 200

        // Outline Pass
        Pass
        {
            Name "Outline"
            Tags { "LightMode" = "UniversalForward" }
            Cull Front // 渲染背面
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 color : COLOR;
            };

            float _OutlineThickness;
            float4 _OutlineColor;

            Varyings vert(Attributes v)
            {
                Varyings o;
                float3 normalWS = TransformObjectToWorldNormal(v.normalOS);
                float3 offset = normalWS * _OutlineThickness;

                float4 positionWS = TransformObjectToWorld(v.positionOS);
                positionWS.xyz += offset;
                o.positionCS = TransformWorldToHClip(positionWS);

                o.color = _OutlineColor;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                return i.color;
            }
            ENDHLSL
        }

        // Base Pass
        Pass
        {
            Name "Base"
            Tags { "LightMode" = "UniversalForward" }
            Cull Back // 渲染正面
            ZWrite On
            ZTest LEqual
            Blend SrcAlpha OneMinusSrcAlpha

            HLSLPROGRAM
            #pragma vertex vertBase
            #pragma fragment fragBase
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            Varyings vertBase(Attributes v)
            {
                Varyings o;
                o.positionCS = TransformObjectToHClip(v.positionOS);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 fragBase(Varyings i) : SV_Target
            {
                return SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
            }
            ENDHLSL
        }
    }
    FallBack "Hidden/InternalErrorShader"
}
