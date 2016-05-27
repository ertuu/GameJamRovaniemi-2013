Shader "Custom/GroundShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Repeations ("_Repeations",float)=2.0
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha, One One
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float _Repeations;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			float2 uv=IN.uv_MainTex;
			uv.x*=_Repeations;
			uv.x=fmod(uv.x,1.0);
			half4 c = tex2D (_MainTex,uv);
			o.Albedo = c.rgb;
			//o.Albedo+=0.1;
			o.Alpha=c.a;
			//o.Normal=half3(0.0,0.0,0.0);
		}
		ENDCG
		
	} 
	FallBack "Sprites/Default"
}
