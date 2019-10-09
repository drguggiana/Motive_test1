using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Optitrack_cricket : MonoBehaviour {

	public OptitrackStreamingClient StreamingClient;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		List<OptitrackMarkerState> markerStates = StreamingClient.GetLatestMarkerStates();

		foreach (OptitrackMarkerState marker in markerStates) {
			if (marker.Labeled == false) {
//				Debug.Log (marker.Position);
				this.transform.localPosition = marker.Position;
			} else {
				this.transform.localPosition = new Vector3 (10.0f, 10.0f, 10.0f);
			}
		}
	}
}
