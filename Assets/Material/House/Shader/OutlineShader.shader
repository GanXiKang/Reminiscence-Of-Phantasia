Shader "Custom/CharacterOutlineShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}  // �����|�ļy���N�D
        _OutlineColor("Outline Color", Color) = (0,0,0,1)  // ����ɫ
        _OutlineThickness("Outline Thickness", Range(0.01, 10)) = 10  // ���ּ������sС
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 200

            // ��һ��Pass��Ⱦ���
            Pass
            {
                Name "OUTLINE"
                Tags { "LightMode" = "ForwardBase" }
                Cull Front  // ���D�����޳�����Ⱦ���

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

                struct appdata
                {
                    float4 vertex : POSITION;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float4 color : COLOR;
                };

                float _OutlineThickness;
                float4 _OutlineColor;

                v2f vert(appdata v)
                {
                    v2f o;
                    float3 norm = normalize(mul((float3x3) unity_ObjectToWorld, v.normal));
                    v.vertex.xyz += norm * _OutlineThickness;  // �{�����ּ�
                    o.pos = UnityObjectToClipPos(v.vertex);  // �D�Q����ÿ��g
                    o.color = _OutlineColor;  // �O������ɫ
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    return i.color;  // ��Ⱦ����ɫ
                }
                ENDCG
            }

            // �ڶ���Pass��Ⱦ���w����
            Pass
            {
                Name "BASE"
                Tags { "LightMode" = "ForwardBase" }
                Cull Back  // �����ı����޳�

                CGPROGRAM
                #pragma vertex vertBase
                #pragma fragment fragBase
                #include "UnityCG.cginc"

                sampler2D _MainTex;  // �y��
                float4 _MainTex_ST;  // �y��s���cƫ��

                struct appdata
                {
                    float4 vertex : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 pos : SV_POSITION;
                };

                v2f vertBase(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);  // Ӌ����cλ��
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);  // Ӌ��y��UV����
                    return o;
                }

                fixed4 fragBase(v2f i) : SV_Target
                {
                    return tex2D(_MainTex, i.uv);  // ����UV���ˏļy��ȡ��
                }
                ENDCG
            }
        }
            FallBack "Diffuse"
}
