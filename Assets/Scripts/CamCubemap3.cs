// Attach this script to an object that uses a Reflective shader.
// Realtime reflective cubemaps!

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class CamCubemap3 : MonoBehaviour
{

    int cubemapSize = 512;
    bool oneFacePerFrame = false;
    private Camera cam;
    //private GameObject go;
    public Transform playerTrans;
    private RenderTexture rtex;

    //bool yay = false;

    void Start()
    {
        // render all six faces at startup
        //UpdateCubemap(63);
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
            //go = new GameObject("CubemapCamera");
            go.AddComponent<Camera>();
            go.hideFlags = HideFlags.HideAndDontSave;
            //go.transform.position = transform.position;
            //go.transform.rotation = Quaternion.identity;
            cam = go.GetComponent<Camera>();
            cam.transform.position = playerTrans.position;
            //cam.transform.rotation = Quaternion.identity;
            //cam.transform.rotation = playerTrans.rotation;
            cam.nearClipPlane = 0.03f;
            cam.farClipPlane = 10; // don't render very far into cubemap
            cam.fieldOfView = 90;
            cam.enabled = false;
        }

        if (!rtex)
        {
            rtex = new RenderTexture(cubemapSize, cubemapSize, 16);
            rtex.dimension = UnityEngine.Rendering.TextureDimension.Cube;
            rtex.hideFlags = HideFlags.HideAndDontSave;
            cam.cullingMask = 1 << 9;
            GetComponent<Renderer>().sharedMaterial.SetTexture("_Cube", rtex);
        }
        //go.transform.rotation = playerTrans.rotation;

        cam.transform.position = playerTrans.position;
        //cam.transform.position.
        //cam.transform.rotation = Quaternion.Inverse(playerTrans.rotation);
        //cam.transform.rotation = Quaternion.AngleAxis(10.0f, new Vector3(1.0f, 0.0f, 0.0f));
        //Debug.Log(playerTrans.rotation);
        //Debug.Log(cam.transform.rotation);
        cam.RenderToCubemap(rtex, faceMask);
        

        
        
        //Debug.Log(yay);
    }

    void OnDisable()
    {
        DestroyImmediate(cam);
        DestroyImmediate(rtex);
    }
}