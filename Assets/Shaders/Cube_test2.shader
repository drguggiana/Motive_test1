// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Cube_test2" {
Properties {
    _ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)
    _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
}
SubShader {
    LOD 200
    Tags { "RenderType"="Opaque" }

CGPROGRAM
#pragma surface surf Lambert

samplerCUBE _Cube;

fixed4 _ReflectColor;

struct Input {
    float3 worldRefl;
};

void surf (Input IN, inout SurfaceOutput o) {
	
	

    fixed4 reflcol = texCUBE (_Cube, IN.worldRefl);
    o.Emission = reflcol.rgb * _ReflectColor.rgb;
    o.Alpha = reflcol.a * _ReflectColor.a;
}
ENDCG
}

FallBack "Legacy Shaders/Reflective/VertexLit"
}
