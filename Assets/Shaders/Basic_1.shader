Shader "Unlit/Basic_1"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,0.5,0.5)
	}
	SubShader
	{
		Pass {
			Material{
				Diffuse [_Color]

			}
			Lighting On
		}
	}
}
