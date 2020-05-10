Shader "Custom/loadingscreenshader"
{
    Properties
    {}
    SubShader
    {
		PASS
		{
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			//#pragma debug
			//#pragma target 3.0

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				//UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;

				UNITY_VERTEX_OUTPUT_STEREO
			};

			//sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;

				UNITY_SETUP_INSTANCE_ID(v); //Insert
				UNITY_INITIALIZE_OUTPUT(v2f, o); //Insert
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o); //Insert

				o.vertex = UnityObjectToClipPos(v.vertex);

				o.uv = v.uv;

				return o;
			}

			UNITY_DECLARE_SCREENSPACE_TEXTURE(_MainTex);
			fixed box_numbers;
			float4 frag(v2f v) : SV_Target
			{
				//UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(v); //Insert
				//fixed4 col = UNITY_SAMPLE_SCREENSPACE_TEXTURE(_MainTex, v.uv);

				float t = _Time.y;
				float4 iResolution = _ScreenParams;
				
				float2 resolution = float2(1280,720);

				float2 p = v.uv;// / resolution.xy;
				p = p * 2.0 - 1.0;
				p.x *= resolution.x / resolution.y;
				float3 destColor = float3(0, 0, 0);
				for (float i = 1.0; i < box_numbers; i++) {
					float j = i + 1.0;
					float2 q = p + float2(cos(t * j), sin(t * j)) * .53;
					float l = 0.02 / abs(length(q * q) - 0.2 * i * abs(tan(t)));
					destColor += float3(abs(tan(t)) * l, abs(sin(2.0 * t)) * l, abs(cos(t)) * l * l);
				}
				return float4(destColor,1.0);
			}
			ENDCG
		}
    }
    FallBack "Diffuse"
}
