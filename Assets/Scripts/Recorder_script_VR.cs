using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using System.Collections;

public class Recorder_script_VR : MonoBehaviour {

	public OptitrackStreamingClient StreamingClient;
	public Int32 RigidBodyId;
	private OptitrackHiResTimer.Timestamp reference;

	private Vector3 Mouse_Position;
	private Vector3 Mouse_Orientation;
	private Vector3 Cricket;
	private GameObject CricketObj;
	private float Time_stamp;
	private StreamWriter writer;

	// Use this for initialization
	void Start () {
		// Get the reference for the timestamp
		reference = OptitrackHiResTimer.Now ();
		// Set up the camera (so it doesn't clip objects too close to the mouse)
		Camera cam = GetComponentInChildren<Camera>();
		cam.nearClipPlane = 0.000001f;
		// Find the cricket object
		CricketObj = GameObject.Find ("Cricket");
		// Set the writer
		writer = new StreamWriter(Paths.recording_path, true);

	}

	// Update is called once per frame
	void Update () {

		// Process the mouse position as the other scripts
		OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId);
		if (rbState != null)
		{
			Mouse_Position = rbState.Pose.Position;
			this.transform.localPosition = Mouse_Position;

			this.transform.localRotation = rbState.Pose.Orientation;
			Mouse_Orientation = this.transform.eulerAngles;
			Time_stamp = rbState.DeliveryTimestamp.SecondsSince(reference);

		}
		// Get the VR cricket position
		Cricket = CricketObj.transform.position;
		// Write the mouse and VR cricket info
		writer.WriteLine(string.Concat(Time_stamp.ToString(), ',', Mouse_Position.x.ToString(), ',', Mouse_Position.y.ToString(), ',', Mouse_Position.z.ToString(), ',',
			Mouse_Orientation.x.ToString(), ',', Mouse_Orientation.y.ToString(), ',', Mouse_Orientation.z.ToString(), ',',
			Cricket.x.ToString(), ',', Cricket.y.ToString(), ',', Cricket.z.ToString()));

	}

	void OnReceiveStop(OscMessage message){
		// Close the writer
		writer.Close();
		// Kill the application
		Application.Quit ();
	}

}
