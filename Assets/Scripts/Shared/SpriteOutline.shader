Shader "Sprites/Outline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
        _OutlineSize ("Outline Size", Range(0,10)) = 1
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off

        Pass
        {
            Name "OUTLINE"
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _OutlineColor;
            float _OutlineSize;

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                float alpha = tex2D(_MainTex, i.uv).a;

                // Outline
                float2 offsets[8] = {
                    float2(1,0), float2(-1,0),
                    float2(0,1), float2(0,-1),
                    float2(1,1), float2(-1,1),
                    float2(1,-1), float2(-1,-1)
                };

                float outline = 0;
                for(int x = 0; x < 8; x++)
                {
                    float2 uvOffset = i.uv + offsets[x] * _OutlineSize * _MainTex_TexelSize.xy;
                    outline += tex2D(_MainTex, uvOffset).a;
                }

                if(alpha <= 0 && outline > 0)
                    return _OutlineColor;

                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
