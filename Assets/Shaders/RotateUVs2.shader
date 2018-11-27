  Shader "Custom/RotateUVs2" {
        Properties {
			_Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
            _RotationSpeed ("Rotation Speed", Float) = 2.0
			_ReflectColor ("Reflection Color", Color) = (1,1,1,0.5)

        }
        SubShader {
            Tags { "RenderType"="Opaque" }
            LOD 200
           
            CGPROGRAM
            #pragma surface surf Lambert
     
			samplerCUBE _Cube;
			fixed4 _ReflectColor;

     
            struct Input {
				float3 worldRefl;
            };
     
           float _RotationSpeed;

            void surf (Input IN, inout SurfaceOutput o) { 
			
				float s = sin ( _RotationSpeed * _Time );
                float c = cos ( _RotationSpeed * _Time );

				float3x3 rotationMatrixX = float3x3( 1, 0, 0, 0, c, -s, 0, s, c);
				IN.worldRefl = mul(IN.worldRefl, rotationMatrixX);

				fixed4 reflcol = texCUBE (_Cube, IN.worldRefl);
				o.Emission = reflcol.rgb * _ReflectColor.rgb;
				o.Alpha = reflcol.a * _ReflectColor.a;

               
            }
            ENDCG
        }
        //FallBack "Diffuse"
    }