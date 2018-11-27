// Attach this script to an object that uses a Reflective shader.
// Realtime reflective cubemaps!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CamCubemap5 : MonoBehaviour
{

    int cubemapSize = 512;
    bool oneFacePerFrame = false;
    private Camera cam;
    public Transform playerTrans;
    private RenderTexture rtex;
    private RenderTexture cubetex;
    //private Cubemap cubetemp;

    void Start()
    {
        // render all six faces at startup
        UpdateCubemap(63);
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
            GameObject go = new GameObject("CubemapCamera");
            go.AddComponent<Camera>();
            go.hideFlags = HideFlags.HideAndDontSave;
            //go.transform.rotation = playerTrans.rotation;
            //go.transform.rotation = Quaternion.Euler(40f,40f,40f);

            cam = go.GetComponent<Camera>();
            cam.transform.position = playerTrans.position;
            //cam.transform.localRotation = playerTrans.rotation;

            cam.nearClipPlane = 0.03f;
            cam.farClipPlane = 10; // don't render very far into cubemap
            cam.fieldOfView = 90;
            cam.aspect = 1;
            cam.enabled = false;
            cam.cullingMask = 1 << 9;

        }

        if (!rtex)
        {
            rtex = new RenderTexture(cubemapSize, cubemapSize, 16);
            //rtex = new Cubemap(cubemapSize, TextureFormat.ARGB32, false);

            rtex.dimension = UnityEngine.Rendering.TextureDimension.Cube;
            rtex.hideFlags = HideFlags.HideAndDontSave;
            GetComponent<Renderer>().sharedMaterial.SetTexture("_Cube", rtex);
        }

        if (!cubetex)
        {
            cubetex = new RenderTexture(cubemapSize, cubemapSize, 16);
            cubetex.dimension = UnityEngine.Rendering.TextureDimension.Cube;
            //cubetex = new Cubemap(cubemapSize, TextureFormat.ARGB32, false);
            cubetex.hideFlags = HideFlags.HideAndDontSave;
        }

        //if (!cubetemp)
        //{
        //    cubetemp = new Cubemap(cubemapSize, TextureFormat.ARGB32, false);
        //    cubetemp.hideFlags = HideFlags.HideAndDontSave;
        //}

        cam.transform.position = playerTrans.position;

        //Graphics.SetRenderTarget(rtex, 1, CubemapFace.NegativeX);

        //cam.RenderToCubemap(cubetex, 1 << 0);

        //Graphics.Blit(cubetex, rtex);

        cam.RenderToCubemap(rtex, faceMask);

        //Graphics.CopyTexture(cubetex, rtex);
        //Graphics.ConvertTexture(cubetex, 0, rtex, 0);
        //Graphics.Blit(cubetex, rtex);

        //cubetemp.SetPixels(cubetex.GetPixels(CubemapFace.PositiveX), CubemapFace.PositiveX);
        //cubetemp.SetPixels(cubetex.GetPixels(CubemapFace.PositiveY), CubemapFace.PositiveY);
        //cubetemp.SetPixels(cubetex.GetPixels(CubemapFace.PositiveZ), CubemapFace.PositiveZ);
        //cubetemp.SetPixels(cubetex.GetPixels(CubemapFace.NegativeX), CubemapFace.NegativeX);
        //cubetemp.SetPixels(cubetex.GetPixels(CubemapFace.NegativeY), CubemapFace.NegativeY);
        //cubetemp.SetPixels(cubetex.GetPixels(CubemapFace.NegativeZ), CubemapFace.NegativeZ);

        //rtex = cubetemp;


        //cam.RenderToCubemap(rtex, 1 << 0);

        //UnityEngine.Graphics.SetRenderTarget(rtex, 0, CubemapFace.PositiveY);

        //cam.RenderToCubemap(rtex, 1 << 1);

        //UnityEngine.Graphics.SetRenderTarget(rtex, 0, CubemapFace.PositiveZ);

        //cam.RenderToCubemap(rtex, 1 << 2);

        //UnityEngine.Graphics.SetRenderTarget(rtex, 0, CubemapFace.NegativeX);

        //cam.RenderToCubemap(rtex, 1 << 4);

        //UnityEngine.Graphics.SetRenderTarget(rtex, 0, CubemapFace.PositiveX);

        //cam.RenderToCubemap(rtex, 1 << 5);

    }

    void OnDisable()
    {
        DestroyImmediate(cam);
        DestroyImmediate(rtex);
        DestroyImmediate(cubetex);
        //DestroyImmediate(cubetemp);
    }
}