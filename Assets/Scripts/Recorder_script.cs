using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Recorder_script : MonoBehaviour {

	public Transform playerObj;

	//string writePath = "C:/Users/drguggiana/Documents/Motive_test1/etc/test.txt";
	//StreamWriter writer = new StreamWriter("C:/Users/drguggiana/Documents/Motive_test1/etc/test.txt", true);

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		StreamWriter writer = new StreamWriter("C:/Users/drguggiana/Documents/Motive_test1/etc/test.txt", true);

		writer.WriteLine(playerObj.position.ToString());
		writer.Close();
		
	}

	void OnApplicationQuit(){
		//writer.Close();
	}	
}
