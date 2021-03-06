//======================================================================================================
// Copyright 2016, NaturalPoint Inc.
//======================================================================================================

using System;
using UnityEngine;


public class Optitrack_mod_marker : MonoBehaviour
{
    public OptitrackStreamingClient StreamingClient;
    public Int32 RigidBodyId;
	public Vector2 x_bound; 
	public Vector2 y_bound;
	public Vector2 z_bound;


    void Start()
    {
        // If the user didn't explicitly associate a client, find a suitable default.
        if (this.StreamingClient == null)
        {
            this.StreamingClient = OptitrackStreamingClient.FindDefaultClient();

            // If we still couldn't find one, disable this component.
            if (this.StreamingClient == null)
            {
                Debug.LogError(GetType().FullName + ": Streaming client not set, and no " + typeof(OptitrackStreamingClient).FullName + " components found in scene; disabling this component.", this);
                this.enabled = false;
                return;
            }
        }
    }


    void Update()
    {
        OptitrackRigidBodyState rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId);
        if (rbState != null)
        {
			
			float new_x = Mathf.Clamp(rbState.Pose.Position.x, x_bound.x, x_bound.y);
			float new_y = Mathf.Clamp(rbState.Pose.Position.y, y_bound.x, y_bound.y);
			float new_z = Mathf.Clamp(rbState.Pose.Position.z, z_bound.x, z_bound.y);
//			Debug.Log (new_y);
			this.transform.localPosition = new Vector3 (new_x, new_y, new_z);
//			this.transform.localPosition = rbState.Pose.Position;
            this.transform.localRotation = rbState.Pose.Orientation;
            //Debug.Log(this.transform.localPosition);
        }
    }
}
