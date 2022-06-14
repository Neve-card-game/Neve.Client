Shader "Unlit/OutLine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutLineSize("OutLineSize",Range(1.,200.)) = 100.
        _OutLineColor("OutLineColor",Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"}

        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _OutLineSize;
            float4 _OutLineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float x= sqrt((_Time.y)%1)*_OutLineSize;

                float Up = tex2D(_MainTex,i.uv+fixed2(0,1)*_MainTex_TexelSize.xy*x).a;
                float Down = tex2D(_MainTex,i.uv+fixed2(0,-1)*_MainTex_TexelSize.xy*x).a;
                float Left = tex2D(_MainTex,i.uv+fixed2(-1,0)*_MainTex_TexelSize.xy*x).a;
                float Right = tex2D(_MainTex,i.uv+fixed2(1,0)*_MainTex_TexelSize.xy*x).a;

                float Up_Left = tex2D(_MainTex,i.uv+fixed2(-1,1)*_MainTex_TexelSize.xy*x).a;
                float Up_Right = tex2D(_MainTex,i.uv+fixed2(1,1)*_MainTex_TexelSize.xy*x).a;
                float Down_Left = tex2D(_MainTex,i.uv+fixed2(-1,-1)*_MainTex_TexelSize.xy*x).a;
                float Down_Right = tex2D(_MainTex,i.uv+fixed2(1,-1)*_MainTex_TexelSize.xy*x).a;

                if(col.a<0.1&&(Up>0.1||Down>0.1||Left>0.1||Right>0.1||Up_Left>0.1||Up_Right>0.1||Down_Left>0.1||Down_Right>0.1)){
                    return fixed4(_OutLineColor.x,_OutLineColor.y,_OutLineColor.z,lerp(0.8,0.01,_Time.y%1));
                }
                else if(col.a>0.1){
                    return col;
                }
                else{
                    return col*0;
                }

            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _OutLineSize;
            float4 _OutLineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float x= sqrt((_Time.y+0.25)%1)*_OutLineSize;

                float Up = tex2D(_MainTex,i.uv+fixed2(0,1)*_MainTex_TexelSize.xy*x).a;
                float Down = tex2D(_MainTex,i.uv+fixed2(0,-1)*_MainTex_TexelSize.xy*x).a;
                float Left = tex2D(_MainTex,i.uv+fixed2(-1,0)*_MainTex_TexelSize.xy*x).a;
                float Right = tex2D(_MainTex,i.uv+fixed2(1,0)*_MainTex_TexelSize.xy*x).a;

                float Up_Left = tex2D(_MainTex,i.uv+fixed2(-1,1)*_MainTex_TexelSize.xy*x).a;
                float Up_Right = tex2D(_MainTex,i.uv+fixed2(1,1)*_MainTex_TexelSize.xy*x).a;
                float Down_Left = tex2D(_MainTex,i.uv+fixed2(-1,-1)*_MainTex_TexelSize.xy*x).a;
                float Down_Right = tex2D(_MainTex,i.uv+fixed2(1,-1)*_MainTex_TexelSize.xy*x).a;

                if(col.a<0.1&&(Up>0.1||Down>0.1||Left>0.1||Right>0.1||Up_Left>0.1||Up_Right>0.1||Down_Left>0.1||Down_Right>0.1)){
                    return fixed4(_OutLineColor.x,_OutLineColor.y,_OutLineColor.z,lerp(0.8,0.01,(_Time.y+0.25)%1));
                }
                else if(col.a>0.1){
                    return col;
                }
                else{
                    return col*0;
                }

            }
            ENDCG
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _MainTex_TexelSize;
            float _OutLineSize;
            float4 _OutLineColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float x= sqrt((_Time.y+0.5)%1)*_OutLineSize;

                float Up = tex2D(_MainTex,i.uv+fixed2(0,1)*_MainTex_TexelSize.xy*x).a;
                float Down = tex2D(_MainTex,i.uv+fixed2(0,-1)*_MainTex_TexelSize.xy*x).a;
                float Left = tex2D(_MainTex,i.uv+fixed2(-1,0)*_MainTex_TexelSize.xy*x).a;
                float Right = tex2D(_MainTex,i.uv+fixed2(1,0)*_MainTex_TexelSize.xy*x).a;

                float Up_Left = tex2D(_MainTex,i.uv+fixed2(-1,1)*_MainTex_TexelSize.xy*x).a;
                float Up_Right = tex2D(_MainTex,i.uv+fixed2(1,1)*_MainTex_TexelSize.xy*x).a;
                float Down_Left = tex2D(_MainTex,i.uv+fixed2(-1,-1)*_MainTex_TexelSize.xy*x).a;
                float Down_Right = tex2D(_MainTex,i.uv+fixed2(1,-1)*_MainTex_TexelSize.xy*x).a;

                if(col.a<0.1&&(Up>0.1||Down>0.1||Left>0.1||Right>0.1||Up_Left>0.1||Up_Right>0.1||Down_Left>0.1||Down_Right>0.1)){
                    return fixed4(_OutLineColor.x,_OutLineColor.y,_OutLineColor.z,lerp(0.8,0.01,(_Time.y+0.5)%1));
                }
                else if(col.a>0.1){
                    return col;
                }
                else{
                    return col*0;
                }

            }
            ENDCG
        }
    }
}
