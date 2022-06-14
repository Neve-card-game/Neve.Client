Shader "Custom/Card"
{
    Properties
    {
        _CubeMap("CubeMap",Cube) = ""{}
    }
    SubShader
    {

        Pass{
            Tags{"LightMode" = "ForwardBase"}

            CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members temp)
            #include "Lighting.cginc"
            #pragma vertex vert
            #pragma fragment frag

            //把参数定义成结构体以获取更多的输入
            struct a2v {//application to vertex
                float4 vertex:POSITION;
                float3 normal:NORMAL;
            };

            struct v2f {
                float4 position:SV_POSITION;
                fixed3 color : COLOR;
            };


            v2f  vert(a2v v) {//为顶点计算
                v2f f;
                f.position = UnityObjectToClipPos(v.vertex);
                fixed3 normalDir = normalize(mul(v.normal, (float3x3)unity_WorldToObject));//将顶点法线方向转换到世界空间
                fixed3 lightDir =normalize( _WorldSpaceLightPos0.xyz);//对于每个顶点来说，光的位置就是光的方向，因为光是平行光。normalize是取单位向量


                fixed3 diffuse = _LightColor0.rgb*max(0,dot(normalDir,lightDir));//取得第一个直射光的颜色（不包括透明度alpha） _LightColor0 这是在文件Lighting.cginc里面定义的
                f.color = diffuse;
                return f;
            }

            fixed4 frag(v2f f) : SV_Target {

                return fixed4(f.color,1);
            }

            ENDCG

        }
        pass{

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
                float4 vertex : SV_POSITION;
                float4 vertexLocal : TEXCOORD1;
            };
            v2f vert (appdata v)
            {
                v2f o;
                o.vertexLocal = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);

                return o;
            }
            samplerCUBE _CubeMap;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = texCUBE(_CubeMap, normalize(i.vertexLocal.xyz));
                
                return col;
            }
            ENDCG
        }

    }
    FallBack "Diffuse"
}
