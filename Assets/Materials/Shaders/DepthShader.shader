Shader "Custom/DepthShader"
{
	Properties
	{
        //Main texture
		_MainTex ("Color Texture", 2D) = "white" {}

        //Depth texture
        _DepthTex ("Depth Texture", 2D) = "white" {}

        //Depth texture
        _MaskTex ("Mask Texture", 2D) = "white" {}

        //How much displacement to apply
        _Displacement ("Displacement", Range (0.0,0.5)) = 0.0

        //Mix between color texture and depth texture
        _ColorDepthMixer ("Color/Depth Mix", Range(0.0, 1.0)) = 0.0

        //Mask transperency
        _MaskTransperency("Mask Transperency", Range(0.0, 1.0)) = 0.0
	}
	SubShader
	{
        Tags {"Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha

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

            //GUI Uniforms
            float _Displacement;
            float _ColorDepthMixer;
            float _MaskTransperency;

            //Textures
			sampler2D _MainTex;
            sampler2D _DepthTex;
            sampler2D _MaskTex;

			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;

                //Read the texture
                half4 depth = tex2Dlod(_DepthTex, float4(v.uv.xy,0,0));

                //Displace and expose a scalar uniform to the GUI
                v.vertex.z -= (depth.r * _Displacement);

                //Bring back the plane to origin after displacement
//                v.vertex.z += _Displacement;

                //Calculate the vertex information to be passed to the fragment shader
				o.vertex = UnityObjectToClipPos(v.vertex);

                //Pack UV's
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                //Unity stuff
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				// sample the depth
				fixed4 depth = tex2D(_DepthTex, i.uv);

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);

				//Read the mask texture
				fixed4 maskTexel = tex2D(_MaskTex, i.uv);

				//Compute the mask based on the GUI param
				float mask = lerp(maskTexel.r, 1.0, _MaskTransperency);

				col.a = mask;
				depth.a = mask;

                //Return the mixed pixel color
				return lerp(col, depth, _ColorDepthMixer);
			}
			ENDCG
		}
	}
}
