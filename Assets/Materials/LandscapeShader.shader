// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Landscape Shader" {

	Properties
	{
		_Color1 ("Water Color", Color) = (1,1,1,1)
		_Color2 ("Ground Color", Color) = (0,0,0,1)
		_Color3 ("Mountain Color", Color) = (0,0,0,1)

		 _Color1Y ("Water Lowest Point", float) = 0.0
		 _Color2Y ("Grass Lowest Point", float) = 5.0
		 _Color3Y ("Mountain Lowest Point", float) = 25.0
	}

	SubShader {
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert

		fixed4 _Color1;
		fixed4 _Color2;
		fixed4 _Color3;
		float _Color1Y;
		float _Color2Y;
		float _Color3Y;

		struct Input {
			float3 viewDir;
			float3 worldPos;
		};

		void surf (Input IN, inout SurfaceOutput o) {

			float4 objectOrigin = mul(unity_ObjectToWorld, float4(0.0,0.0,0.0,1.0) );

			float waterToGrassMultiplier =  ((objectOrigin.y - _Color1Y) / (_Color2Y - _Color1Y));
			float grassToMountainMultiplier = ((objectOrigin.y - _Color2Y) / (_Color3Y - _Color2Y));

			fixed4 waterToGrass = lerp(_Color1, _Color2, waterToGrassMultiplier);
			fixed4 grassToMountain = lerp(_Color2, _Color3, grassToMountainMultiplier);

			fixed4 color = objectOrigin.y < _Color1Y ? _Color1 : objectOrigin.y < _Color2Y ? waterToGrass : objectOrigin.y < _Color3Y ? grassToMountain : _Color3;
			o.Albedo = color;

		}
		ENDCG
	}
	FallBack "Diffuse"
}