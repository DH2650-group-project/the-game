

Shader "Hidden/Pixelize"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white"
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque" "RenderPipeline" = "UniversalPipeline"
        }

        HLSLINCLUDE
        #pragma vertex vert
        #pragma fragment frag

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        struct Attributes
        {
            float4 positionOS : POSITION;
            float2 uv : TEXCOORD0;
        };

        struct Varyings
        {
            float4 positionHCS : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        TEXTURE2D(_MainTex);
        float4 _MainTex_TexelSize;
        float4 _MainTex_ST;

        SamplerState sampler_point_clamp;
        
        uniform float2 _BlockSize;
        uniform float2 _OriginalSize;
        


        Varyings vert(Attributes IN)
        {
            Varyings OUT;
            OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
            OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
            return OUT;
        }

        ENDHLSL

        Pass
        {
            Name "Pixelation"

            HLSLPROGRAM
            half4 frag(Varyings IN) : SV_TARGET
            {
                // how many original pixels are in new pixel
                float2 pixelSize = _OriginalSize / _BlockSize;


                half4 color = half4(0,0,0,0);
                // get the average color of the pixels in the new pixel
                for (int i = 0; i < pixelSize.x; i++)
                {
                    for (int j = 0; j < pixelSize.y; j++)
                    {
                        color += SAMPLE_TEXTURE2D(_MainTex, sampler_point_clamp, IN.uv + float2(i,j) * _MainTex_TexelSize.xy);
                    }
                }
                color /= pixelSize.x * pixelSize.y;

                return color;                
            }
            ENDHLSL

        }        
    }
}