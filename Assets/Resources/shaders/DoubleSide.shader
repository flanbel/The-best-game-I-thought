Shader "Custom/DoubleSide" {
	//プロパティ.
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	//中身.
	SubShader {
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		//αブレンドするよー
		//Blend SrcAlpha OneMinusSrcAlpha
		LOD 200
		//両面描画
		Cull off
		
		//Cg言語での開始の意味。このなかではCg言語で記述していく
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard alpha:fade

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		

		struct Input {
			float2 uv_MainTex;
			float4 color : COLOR;
		};
		//プロパティと同じ名前にする
		sampler2D _MainTex;
		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a * IN.color.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
