using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
//using System.Collections;

public class Recorder_script_both : MonoBehaviour {

//	public Transform playerObj;
//	public Transform cricketObj;
	public OptitrackStreamingClient StreamingClient;
	public Int32 RigidBodyId;

	private Vector3 Mouse_Position;
	private Vector3 Mouse_Orientation;
	private Vector3 Cricket;
	private Int64 Time_stamp;
//	private Int64 Time_ref;

	//string writePath = "C:/Users/drguggiana/Documents/Motive_test1/etc/test.txt";
	//StreamWriter writer = new StreamWriter("C:/Users/drguggiana/Documents/Motive_test1/etc/test.txt", true);

	// Use this for initialization
	void Start () {
//		Time_ref = OptitrackHiResTimer.Timestamp.Now ();
		
	}

	// Update is called once per frame
	void Update () {

		OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId);
		if (rbState != null)
		{
			Mouse_Position = rbState.Pose.Position;
			this.transform.localPosition = Mouse_Position;
			this.transform.localRotation = rbState.Pose.Orientation;
			Mouse_Orientation = this.transform.eulerAngles;
			Time_stamp = rbState.DeliveryTimestamp.m_ticks;
			//Debug.Log(this.transform.localPosition);
//			Debug.Log(Time_stamp);
		}

		List<OptitrackMarkerState> markerStates = StreamingClient.GetLatestMarkerStates();

		foreach (OptitrackMarkerState marker in markerStates) {
			if (marker.Labeled == false) {
				//				Debug.Log (marker.Position);
				Cricket = marker.Position;
			} else {
				Cricket = new Vector3 (10.0f, 10.0f, 10.0f);
			}
		}

		StreamWriter writer = new StreamWriter("C:/Users/drguggiana/Documents/Motive_test1/etc/recording.txt", true);

		writer.WriteLine(string.Concat(Time_stamp.ToString(), ',', Mouse_Position.x.ToString(), ',', Mouse_Position.y.ToString(), ',', Mouse_Position.z.ToString(), ',',
			Mouse_Orientation.x.ToString(), ',', Mouse_Orientation.y.ToString(), ',', Mouse_Orientation.z.ToString(), ',',
			Cricket.x.ToString(), ',', Cricket.y.ToString(), ',', Cricket.z.ToString()));
		writer.Close();
		
	}

	void OnApplicationQuit(){
		//writer.Close();
	}	
}
