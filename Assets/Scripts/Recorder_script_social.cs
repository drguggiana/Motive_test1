using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
//using System.Collections;

public class Recorder_script_social : MonoBehaviour {

	public OptitrackStreamingClient StreamingClient;
	public Int32 RigidBodyId;
	public Int32 RigidBodyId2;

	private Vector3 Mouse_Position;
	private Vector3 Mouse_Orientation;
	private Vector3 Mouse_Position2;
	private Vector3 Mouse_Orientation2;
	private float Time_stamp;
	private OptitrackHiResTimer.Timestamp reference;
	public GameObject tracking_square;
	private float color_factor = 0.0f;
	private Color new_color;
	public OSC osc;
	StreamWriter writer;
	GameObject mouse2;


	// Use this for initialization
	void Start () {

		reference = OptitrackHiResTimer.Now ();
		// Get the camera component
		Camera cam = GetComponentInChildren<Camera>();
		cam.nearClipPlane = 0.000001f;
		// set the OSC communication
		osc.SetAddressHandler("/Close", OnReceiveStop);
		writer = new StreamWriter(Paths.recording_path, true);
		mouse2 = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		mouse2.SetActive(false);

	}

	// Update is called once per frame
	void Update () {


		// set the color of the square for tracking the frames
		new_color = new Color(color_factor, color_factor, color_factor, 1f);
		tracking_square.GetComponent<Renderer> ().material.SetColor("_Color", new_color);


		if (color_factor > 0.0f) {
			color_factor = 0.0f;
		} else {
			color_factor = 1.0f;
		}
			
		OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId);
		if (rbState != null)
		{
			Mouse_Position = rbState.Pose.Position;
			this.transform.localPosition = Mouse_Position;
			this.transform.localRotation = rbState.Pose.Orientation;
			Mouse_Orientation = this.transform.eulerAngles;
			Time_stamp = rbState.DeliveryTimestamp.SecondsSince(reference);

		}

		OptitrackRigidBodyState rbState2 = StreamingClient.GetLatestRigidBodyState(RigidBodyId2);
		if (rbState2 != null)
		{
			Mouse_Position2 = rbState2.Pose.Position;
			mouse2.transform.localPosition = Mouse_Position2;
			mouse2.transform.localRotation = rbState2.Pose.Orientation;
			Mouse_Orientation2 = mouse2.transform.eulerAngles;

		}
			
		writer.WriteLine(string.Concat(Time_stamp.ToString(), ',', Mouse_Position.x.ToString(), ',', Mouse_Position.y.ToString(), ',', Mouse_Position.z.ToString(), ',',
						Mouse_Orientation.x.ToString(), ',', Mouse_Orientation.y.ToString(), ',', Mouse_Orientation.z.ToString(), ',',
			Mouse_Position2.x.ToString(), ',', Mouse_Position2.y.ToString(), ',', Mouse_Position2.z.ToString(), ',',
			Mouse_Orientation2.x.ToString(), ',', Mouse_Orientation2.y.ToString(), ',', Mouse_Orientation2.z.ToString(), ',', color_factor.ToString()));
	}

	void OnReceiveStop(OscMessage message){
		writer.Close();
		Destroy (mouse2);
		Application.Quit ();
	}
		
}
