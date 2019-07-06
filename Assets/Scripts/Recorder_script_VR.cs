using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using System.Collections;

public class Recorder_script_VR : MonoBehaviour {

	//	public Transform playerObj;
	//	public Transform cricketObj;
	public OptitrackStreamingClient StreamingClient;
	public Int32 RigidBodyId;

	private Vector3 Mouse_Position;
	private Vector3 Mouse_Orientation;
	private Vector3 Cricket;
	private GameObject CricketObj;
	private Int64 Time_stamp;
	private System.DateTime curr_datetime;
	private string string_datetime;
//	public Vector2 x_bound; 
//	public Vector2 y_bound;
//	public Vector2 z_bound;
	//	private Int64 Time_ref;

	//string writePath = "C:/Users/drguggiana/Documents/Motive_test1/etc/test.txt";
	//StreamWriter writer = new StreamWriter("C:/Users/drguggiana/Documents/Motive_test1/etc/test.txt", true);

	// Use this for initialization
	void Start () {
		//		Time_ref = OptitrackHiResTimer.Timestamp.Now ();
		// Get the current time and date string
		curr_datetime = System.DateTime.Now;
		string_datetime = curr_datetime.ToString ("yyyyMMddTHHmmss");
		Camera cam = GetComponentInChildren<Camera>();
		cam.nearClipPlane = 0.000001f;

		CricketObj = GameObject.Find ("Cricket");

	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId);
		if (rbState != null)
		{
			Mouse_Position = rbState.Pose.Position;
			this.transform.localPosition = Mouse_Position;
//			float new_x = Mathf.Clamp(rbState.Pose.Position.x, x_bound.x, x_bound.y);
//			float new_y = Mathf.Clamp(rbState.Pose.Position.y, y_bound.x, y_bound.y);
//			float new_z = Mathf.Clamp(rbState.Pose.Position.z, z_bound.x, z_bound.y);
			//			Debug.Log (new_y);
//			this.transform.localPosition = new Vector3 (new_x, new_y, new_z);
			this.transform.localRotation = rbState.Pose.Orientation;
			Mouse_Orientation = this.transform.eulerAngles;
			Time_stamp = rbState.DeliveryTimestamp.m_ticks;
			//Debug.Log(this.transform.localPosition);
			//			Debug.Log(Time_stamp);
		}

//		List<OptitrackMarkerState> markerStates = StreamingClient.GetLatestMarkerStates();

//		foreach (OptitrackMarkerState marker in markerStates) {
//			if (marker.Labeled == false) {
//				//				Debug.Log (marker.Position);
//				Cricket = marker.Position;
//			} else {
//				Cricket = new Vector3 (10.0f, 10.0f, 10.0f);
//			}
//		}

		Cricket = CricketObj.transform.position;

		StreamWriter writer = new StreamWriter(string.Concat("C:/Users/drguggiana/Documents/Motive_test1/etc/",string_datetime,".txt"), true);

		writer.WriteLine(string.Concat(Time_stamp.ToString(), ',', Mouse_Position.x.ToString(), ',', Mouse_Position.y.ToString(), ',', Mouse_Position.z.ToString(), ',',
			Mouse_Orientation.x.ToString(), ',', Mouse_Orientation.y.ToString(), ',', Mouse_Orientation.z.ToString(), ',',
			Cricket.x.ToString(), ',', Cricket.y.ToString(), ',', Cricket.z.ToString()));
		writer.Close();

	}

	void OnApplicationQuit(){
		//writer.Close();
	}	
}
