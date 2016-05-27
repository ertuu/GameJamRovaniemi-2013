Shader "Custom/AnimationShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_HorizontalTotal ("_HorizontalTotal",float)=1
		_VerticalTotal ("_VerticalTotal",float)=1
		_HorizontalCoord ("_HorizontalCoord",float)=1
		_VerticalCoord ("_VerticalCoord",float)=1
		_Inverted ("_Inverted",float)=0//*bool*, 0-false, 1-true
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha, One One
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		float _HorizontalTotal;
		float _VerticalTotal;
		float _HorizontalCoord;
		float _VerticalCoord;
		float _Inverted;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			float2 uv=IN.uv_MainTex;
			if (_Inverted==0)
			{
				uv.x=(uv.x/_HorizontalTotal)+(_HorizontalCoord/_HorizontalTotal);
			}
			else
			{
			uv.x=((_HorizontalCoord+1)/_HorizontalTotal)-(uv.x/_HorizontalTotal);
			}
			uv.y=(uv.y/_VerticalTotal)+(_VerticalCoord/_VerticalTotal);
			half4 c = tex2D (_MainTex, uv);
			o.Albedo = c.rgb;
			//o.Alpha=0.5;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
