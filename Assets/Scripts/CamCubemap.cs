// Attach this script to an object that uses a Reflective shader.
// Realtime reflective cubemaps!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CamCubemap : MonoBehaviour {

	int cubemapSize = 128;
	bool oneFacePerFrame = false;
	private Camera cam;
	private RenderTexture rtex;

	void Start () {
		// render all six faces at startup
		UpdateCubemap( 63 );
	}

	void LateUpdate () {
		if (oneFacePerFrame) {
			int faceToRender = Time.frameCount % 6;
			int faceMask = 1 << faceToRender;
			UpdateCubemap (faceMask);
		} else {
			UpdateCubemap (63); // all six faces
		}
	}

	void UpdateCubemap (int faceMask) {
		if (!cam) {
			GameObject go = new GameObject ("CubemapCamera");
			go.AddComponent<Camera>();
			go.hideFlags = HideFlags.HideAndDontSave;
			go.transform.position = transform.position;
			go.transform.rotation = Quaternion.identity;
			cam = go.GetComponent<Camera>();
			cam.farClipPlane = 100; // don't render very far into cubemap
			cam.enabled = false;
		}

		if (!rtex) {
			rtex = new RenderTexture (cubemapSize, cubemapSize, 16);
			rtex.dimension = UnityEngine.Rendering.TextureDimension.Cube;
			rtex.hideFlags = HideFlags.HideAndDontSave;
			GetComponent<Renderer>().sharedMaterial.SetTexture ("_Cube", rtex);
		}

		cam.transform.position = transform.position;
		cam.RenderToCubemap (rtex, faceMask);
	}

	void OnDisable () {
		DestroyImmediate (cam);
		DestroyImmediate (rtex);
	}
}