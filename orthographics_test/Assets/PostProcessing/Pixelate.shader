// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Pixelate"{
	Properties{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader{
		Cull Off ZWrite Off ZTest Always
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			uniform float2 _BlockSize;

			struct appdata{
				float4 vertex:POSITION;
				float2 uv:TEXCOORD0;
			};

			struct v2f{
				float2 uv:TEXCOORD0;
				float4 vertex:SV_POSITION;
			};

			v2f vert(appdata v){
				v2f o;
				o.vertex=UnityObjectToClipPos(v.vertex);
				o.uv=v.uv;
				return o;
			}

			fixed4 frag(v2f i):SV_Target{
				fixed4 rescol=float4(0,0,0,0);
				
				float2 CurrentBlock=float2(
				(floor(i.uv.x/_BlockSize.x)*_BlockSize.x),
				(floor(i.uv.y/_BlockSize.y)*_BlockSize.y)
				);
				rescol=tex2D(_MainTex,CurrentBlock+_BlockSize/2);
				rescol+=tex2D(_MainTex,CurrentBlock+float2(_BlockSize.x/4,_BlockSize.y/4));
				rescol+=tex2D(_MainTex,CurrentBlock+float2(_BlockSize.x/2,_BlockSize.y/4));
				rescol+=tex2D(_MainTex,CurrentBlock+float2((_BlockSize.x/4)*3,_BlockSize.y/4));
				rescol+=tex2D(_MainTex,CurrentBlock+float2(_BlockSize.x/4,_BlockSize.y/2));
				rescol+=tex2D(_MainTex,CurrentBlock+float2((_BlockSize.x/4)*3,_BlockSize.y/2));
				rescol+=tex2D(_MainTex,CurrentBlock+float2(_BlockSize.x/4,(_BlockSize.y/4)*3));
				rescol+=tex2D(_MainTex,CurrentBlock+float2(_BlockSize.x/2,(_BlockSize.y/4)*3));
				rescol+=tex2D(_MainTex,CurrentBlock+float2((_BlockSize.x/4)*3,(_BlockSize.y/4)*3));
				rescol/=9;
				
				return rescol;
			}

			ENDCG
		}
	}
}
