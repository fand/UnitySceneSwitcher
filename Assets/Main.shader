﻿Shader "Unlit/Main"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _NextTex ("Next Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _NextTex_ST;
            float _Level;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;
                float time = _Time.y;

                // uv.x += sin(uv.y * 37. + time) * sin(uv.y * 717. + time * 0.3) * 0.1;
                // uv = frac(uv);

                fixed4 color = tex2D(_MainTex, uv);
                color.rgb *= _Level;

                return color;
            }
            ENDCG
        }
    }
}
