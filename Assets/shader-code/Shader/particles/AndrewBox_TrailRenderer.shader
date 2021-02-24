Shader "AndrewBox/TrailRenderer" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_MainCol ("Self Color Value", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Transparent"  "IgnoreProjector"="True"  "Queue"="Transparent"}
		LOD 200
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma surface surf NoLight vertex:vert alpha noforwardadd

		//光照方程，名字为Lighting接#pragma suface后的光照方程名称 
  		//lightDir :顶点到光源的单位向量
		//viewDir  :顶点到摄像机的单位向量   
		//atten	   :关照的衰减系数 
  		float4 LightingNoLight(SurfaceOutput s, float3 lightDir,half3 viewDir, half atten) 
  		{ 
  		 	float4 c ; 
  		 	c.rgb =  s.Albedo;
  			c.a = s.Alpha; 
			return c; 
  		}

		sampler2D _MainTex;
		fixed4 _MainCol;

		struct Input 
		{
			float2 uv_MainTex;
			float4 vertColor;
		};

		void vert(inout appdata_full v, out Input o)
		{
			o.vertColor = v.color;
			o.uv_MainTex = v.texcoord;
		}

		void surf (Input IN, inout SurfaceOutput o) 
		{
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			//c.a为贴图上的透明度，使用它构成条带的图形轮廓
			//_MainCol.a为主颜色的透明度，用于对整个条带透明度控制
			//IN.vertColor.a由顶点颜色获得，由Colors数组设置，用于控制条带不同位置的透明度度变化
			o.Alpha = c.a * _MainCol.a* IN.vertColor.a;
			//_MainCol.rgb为主颜色的颜色值，用于显示主颜色部分
			//IN.vertColor.rgb为顶点颜色的颜色值，由Colors数组设置，用于控制条带不同位置的颜色变化
			o.Albedo = _MainCol.rgb*IN.vertColor.rgb;
		}

		
		ENDCG
	} 
	FallBack "Diffuse"
}
