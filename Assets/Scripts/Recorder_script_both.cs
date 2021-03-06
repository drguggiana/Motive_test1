using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Recorder_script_both : MonoBehaviour
{

    public OptitrackStreamingClient StreamingClient;
    public Int32 RigidBodyId;

    private Vector3 Mouse_Position;
    private Vector3 Mouse_Orientation;
    private Vector3 Cricket;
    private float Time_stamp;
    private OptitrackHiResTimer.Timestamp reference;
    public GameObject tracking_square;
    private float color_factor = 0.0f;
    private Color new_color;
    public OSC osc;
    StreamWriter writer;

    // Use this for initialization
    void Start()
    {

        // get the reference timer
        reference = OptitrackHiResTimer.Now();
        // Set up the camera (so it doesn't clip objects too close to the mouse)
        Camera cam = GetComponentInChildren<Camera>();
        cam.nearClipPlane = 0.000001f;
        // set the OSC communication
        osc.SetAddressHandler("/Close", OnReceiveStop);
        // Set the writer
        writer = new StreamWriter(Paths.recording_path, true);

    }

    // Update is called once per frame
    void Update()
    {


        // create the color for the square
        new_color = new Color(color_factor, color_factor, color_factor, 1f);
        // put it on the square
        tracking_square.GetComponent<Renderer>().material.SetColor("_Color", new_color);

        // Define the color for the next iteration (switch it)
        if (color_factor > 0.0f)
        {
            color_factor = 0.0f;
        }
        else
        {
            color_factor = 1.0f;
        }

        // Get the status of the rigid body
        OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId);
        if (rbState != null)
        {
            // get the position of the mouse RB
            Mouse_Position = rbState.Pose.Position;
            // change the transform position of the game object
            this.transform.localPosition = Mouse_Position;
            // also change its rotation
            this.transform.localRotation = rbState.Pose.Orientation;
            // turn the angles into Euler (for later printing)
            Mouse_Orientation = this.transform.eulerAngles;
            // get the timestamp 
            Time_stamp = rbState.DeliveryTimestamp.SecondsSince(reference);

        }

        // get the position of single trackable points in the arena, used for labeled real crickets
        List<OptitrackMarkerState> markerStates = StreamingClient.GetLatestMarkerStates();
        // for each detected marker
        foreach (OptitrackMarkerState marker in markerStates)
        {
            // if it's not part of a rigid body, save the position as cricket
            if (marker.Labeled == false)
            {
                Cricket = marker.Position;
            }
            else
            {
                // if not, ignore it
                Cricket = new Vector3(10.0f, 10.0f, 10.0f);
            }
        }

        // Write the line of data
        writer.WriteLine(string.Concat(Time_stamp.ToString(), ',', Mouse_Position.x.ToString(), ',', Mouse_Position.y.ToString(), ',', Mouse_Position.z.ToString(), ',',
            Mouse_Orientation.x.ToString(), ',', Mouse_Orientation.y.ToString(), ',', Mouse_Orientation.z.ToString(), ',',
            Cricket.x.ToString(), ',', Cricket.y.ToString(), ',', Cricket.z.ToString(), ',', color_factor.ToString()));
    }

    void OnReceiveStop(OscMessage message)
    {
        // Close the writer
        writer.Close();
        // Kill the application
        Application.Quit();
    }

}
