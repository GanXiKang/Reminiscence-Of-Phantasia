Shader "Custom/OutlineShader2"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}  // 主材質的紋理貼圖
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)  // 外框顏色
        _OutlineThickness ("Outline Thickness", Range (0.01, 10)) = 10  // 外框粗細範圍縮小
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        // 第一個Pass渲染外框
        Pass
        {
            Name "OUTLINE"
            Tags { "LightMode"="ForwardBase" }
            Cull Front  // 反轉背面剔除以渲染外框

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

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
                v.vertex.xyz += norm * _OutlineThickness;  // 調整外框粗細
                o.pos = UnityObjectToClipPos(v.vertex);  // 轉換為剪裁空間
                o.color = _OutlineColor;  // 設定外框顏色
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return i.color;  // 渲染外框顏色
            }
            ENDCG
        }

        // 第二個Pass渲染物體本身
        Pass
        {
            Name "BASE"
            Tags { "LightMode"="ForwardBase" }
            Cull Back  // 正常的背面剔除

            CGPROGRAM
            #pragma vertex vertBase
            #pragma fragment fragBase
            #include "UnityCG.cginc"

            sampler2D _MainTex;  // 紋理
            float4 _MainTex_ST;  // 紋理縮放與偏移

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
                o.pos = UnityObjectToClipPos(v.vertex);  // 計算頂點位置
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);  // 計算紋理UV坐標
                return o;
            }

            fixed4 fragBase(v2f i) : SV_Target
            {
                return tex2D(_MainTex, i.uv);  // 根據UV坐標從紋理取樣
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
