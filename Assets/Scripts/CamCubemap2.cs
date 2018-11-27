// Attach this script to an object that uses a Reflective shader.
// Realtime reflective cubemaps!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CamCubemap2 : MonoBehaviour
{

    int cubemapSize = 128;
    bool oneFacePerFrame = false;
    public Camera cam;
    public Transform playerTrans;
    private RenderTexture rtex;

    //bool yay = false;

    void Start()
    {
        // render all six faces at startup
        UpdateCubemap(63);
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (oneFacePerFrame)
        {
            int faceToRender = Time.frameCount % 6;
            int faceMask = 1 << faceToRender;
            UpdateCubemap(faceMask);
        }
        else
        {
            UpdateCubemap(63); // all six faces
        }
    }

    void UpdateCubemap(int faceMask)
    {
        if (!cam)
        {
            //GameObject go = new GameObject("CubemapCamera");
            //Camera cam2 = go.AddComponent<Camera>();
            //go.hideFlags = HideFlags.HideAndDontSave;
            //go.transform.position = transform.position;
            //go.transform.rotation = Quaternion.identity;
            //cam = go.GetComponent<Camera>();
            cam.transform.position = playerTrans.position;
            cam.transform.rotation = Quaternion.identity;
            cam.farClipPlane = 100; // don't render very far into cubemap
            //cam.enabled = false;
        }

        if (!rtex)
        {
            rtex = new RenderTexture(cubemapSize, cubemapSize, 16);
            rtex.dimension = UnityEngine.Rendering.TextureDimension.Cube;
            rtex.hideFlags = HideFlags.HideAndDontSave;
            GetComponent<Renderer>().sharedMaterial.SetTexture("_Cube", rtex);
        }

        cam.transform.position = playerTrans.position;
        cam.RenderToCubemap(rtex, faceMask);
        //Debug.Log(yay);
    }

    void OnDisable()
    {
        //DestroyImmediate(cam);
        DestroyImmediate(rtex);
    }
}