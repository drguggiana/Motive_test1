using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class Cam_move : MonoBehaviour
{

	public float movementSpeed = 5.0f;
	public float rotationSpeed = 5.0f;
	public float mouseSensitivity = 3.0f;
	public Transform camObj;
	public Camera camCam;
	public Text winText;
	public float udRange = 180.0f;
	public float rot_scale = 360f;

	[Range(-1.0f, 1.0f)]
	public float x_for;

	[Range(-1.0f, 1.0f)]
	public float y_for;

	[Range(-1.0f, 1.0f)]
	public float z_for;

	[Range(-1.0f, 1.0f)]
	public float x_up;

	[Range(-1.0f, 1.0f)]
	public float y_up;

	[Range(-1.0f, 1.0f)]
	public float z_up;

	string[] temp_data;
	Vector3 posvec;
	Vector3 fwd_vector;
	Vector3 up_vector;
	Vector2 center_vec;


	float rotUD = 0;

	// Use this for initialization
	void Start()
	{

		string path = "C:\\Users\\drguggiana\\Desktop\\projector_calib.txt";

		StreamReader reader = new StreamReader (path);

		temp_data = reader.ReadToEnd().Split("\n"[0]);
		reader.Close ();



		posvec = new Vector3 (-float.Parse (temp_data [0]), float.Parse (temp_data [1]), float.Parse (temp_data [2]));

        // after flipping the obj_points and such, also flip z, y and invert y on both vectors. invert x and z on both vectors, BEST
//		fwd_vector = new Vector3 (float.Parse (temp_data [9]),-float.Parse (temp_data [11]),float.Parse (temp_data [10]));
//		up_vector = new Vector3 (float.Parse (temp_data [6]),-float.Parse (temp_data [8]),float.Parse (temp_data [7]));
		fwd_vector = new Vector3 (float.Parse (temp_data [9]),-float.Parse (temp_data [11]),float.Parse (temp_data [10]));
		up_vector = new Vector3 (float.Parse (temp_data [6]),-float.Parse (temp_data [8]),float.Parse (temp_data [7]));

		center_vec = new Vector2 (float.Parse (temp_data [14]),float.Parse (temp_data [17]));



//		Debug.Log (posvec[2]);


	}

	// Update is called once per frame
	void Update()
	{

		float x;
		float y;
		float z;
		float step_size = 0.0001f;
		float angle_step = 0.001f;
		x = transform.position.x;
		y = transform.position.y;
		z = transform.position.z;

		if (Input.GetKey ("w")) {
			x = x + step_size;
		}
		if (Input.GetKey ("s")) {
			x = x - step_size;
		}
		if (Input.GetKey ("q")) {
			y = y + step_size;
		}
		if (Input.GetKey ("e")) {
			y = y - step_size;
		}
		if (Input.GetKey ("a")) {
			z = z + step_size;
		}
		if (Input.GetKey ("d")) {
			z = z - step_size;
		}


		if (Input.GetKey ("i")) {
			x_for = x_for - angle_step;
		}
		if (Input.GetKey ("o")) {
			x_for = x_for + angle_step;
		}
		if (Input.GetKey ("k")) {
			y_for = y_for - angle_step;
		}
		if (Input.GetKey ("l")) {
			y_for = y_for + angle_step;
		}
		if (Input.GetKey ("m")) {
			z_for = z_for - angle_step;
		}
		if (Input.GetKey (",")) {
			z_for = z_for + angle_step;
		}
		if (Input.GetKey ("p")) {
			x_up = x_up - angle_step;
		}
		if (Input.GetKey ("[")) {
			x_up = x_up + angle_step;
		}
		if (Input.GetKey (";")) {
			y_up = y_up - angle_step;
		}
		if (Input.GetKey ("'")) {
			y_up = y_up + angle_step;
		}
		if (Input.GetKey (".")) {
			z_up = z_up - angle_step;
		}
		if (Input.GetKey ("/")) {
			z_up = z_up + angle_step;
		}




		transform.position = new Vector3 (x, y, z);

		// Rotation
		// Taken from nm8shun's comment here: https://forum.unity.com/threads/keyboard-to-rotate-objects-around-objects-axis.53712/

//		if (Input.GetKey("o"))
//		{
//			transform.Rotate (Vector3.right * Time.deltaTime * rotationSpeed, Space.World);
//			//float _x = transform.eulerAngles.x + rot_scale * Time.deltaTime;
//			//transform.eulerAngles = new Vector3 (_x, transform.eulerAngles.y, transform.eulerAngles.z);
//		}
//
//		if (Input.GetKey("p"))
//		{
//			transform.Rotate(-Vector3.right * Time.deltaTime * rotationSpeed, Space.World);
//			//float _x = transform.eulerAngles.x -  rot_scale * Time.deltaTime;
//			//transform.eulerAngles = new Vector3 (_x, transform.eulerAngles.y, transform.eulerAngles.z);
//		}
//
//		if (Input.GetKey("l"))
//		{
//			transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
//		}
//
//		if (Input.GetKey(";"))
//		{
//			transform.Rotate(-Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
//		}
//
//		if (Input.GetKey("."))
//		{
//			transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed, Space.World);
//		}
//
//		if (Input.GetKey("/"))
//		{
//			transform.Rotate(-Vector3.forward * Time.deltaTime * rotationSpeed, Space.World);
//		}

		if (Input.GetKey ("y") & camCam.orthographic == true) {
			camCam.orthographicSize = camCam.orthographicSize + 0.001f;
		} else if (Input.GetKey ("y") & camCam.orthographic == false) {
			camCam.fieldOfView = camCam.fieldOfView + 0.01f;
		}

		if (Input.GetKey ("u") & camCam.orthographic == true) {
			camCam.orthographicSize = camCam.orthographicSize - 0.001f;
		} else if (Input.GetKey ("u") & camCam.orthographic == false) {
			camCam.fieldOfView = camCam.fieldOfView - 0.01f;
		}

//		if (Input.GetKey ("g") & camCam.orthographic == true) 
//		{
//			camCam.orthographic = false;
//		}
//			else if (Input.GetKey("g") & camCam.orthographic == false)
//		{
//			camCam.orthographic = true;
//		}

		//float rotLR = Input.GetAxis("Mouse X") * mouseSensitivity;

		//transform.Rotate(0, rotLR, 0);

		//rotUD -= Input.GetAxis("Mouse Y");
		//rotUD = Mathf.Clamp(rotUD, -udRange, udRange);

		//float rotPitch = Input.GetAxis("Pitch") * rot_scale * Time.deltaTime * 100;

		//camObj.localRotation = Quaternion.Euler(0, 0, rotPitch);
		//// Debug.Log(rotPitch);

		//camObj_obs.localRotation = Quaternion.Euler(rotUD, 0, 0);

		if (Input.GetKey ("b")) 
		{
			winText.text = "";

			// STEP 1 : fetch position from OpenCV + basic transformation
			Vector3 pos; // from OpenCV
//			pos = new Vector3(0.216f, 1.456f, 0.084f); // right-handed coordinates system (OpenCV) to left-handed one (Unity) OLD CALIB
//			pos = new Vector3(-0.0456f, 1.9799f, -0.0332f); // original from posVec, flipping y and z
//			pos = new Vector3(0.0456f, 1.9799f, 0.0332f); // inverted z and x
//			pos = new Vector3(0.0456f, 1.9799f, -0.0332f); // inverted x, BEST
//			pos = new Vector3(0.0456f, -0.0332f, 1.9799f); // direct from flipping coord, same as above, BEST

			pos = posvec; // loaded from file

			// STEP 2 : set virtual camera's frustrum (Unity) to match physical camera's parameters
//			Vector2 fparams = new Vector2 (0.786f, 0.789f); // from OpenCV (calibration parameters Fx and Fy = focal lengths in pixels)
			Vector2 fparams = new Vector2 (posvec[2], posvec[2]); // assuming focus at the bottom of the arena
			Vector2 resolution = new Vector2 (2.551f, 1.435f); // image resolution measured from the arena in [m]
			float vfov =  2.0f * Mathf.Atan(0.5f * resolution.y / fparams.y)* Mathf.Rad2Deg; // virtual camera (pinhole type) vertical field of view
//			Debug.Log(vfov);

//			Camera cam; // TODO get reference one way or another
			camCam.fieldOfView = vfov;
			camCam.aspect = resolution.x / resolution.y; // you could set a viewport rect with proper aspect as well... I would prefer the viewport approach

			// STEP 3 : shift position to compensate for physical camera's optical axis not going exactly through image center
//			Vector2 cparams = new Vector2 (0.00361f, -0.0266f); // from OpenCV (calibration parameters Cx and Cy = optical center shifts from image center in pixels)
//			Vector2 cparams = new Vector2 (0.00361f, 0.0266f); // from coord flipping (only sign changes from above)

			Vector2 cparams = center_vec; // loaded from file
			Vector3 imageCenter = new Vector3(0.5f, 0.5f, pos.z); // in viewport coordinates
			Vector3 opticalCenter = new Vector3(0.5f + cparams.x / resolution.x, 0.5f + cparams.y / resolution.y, pos.z); // in viewport coordinates
			pos += camCam.ViewportToWorldPoint(imageCenter) - camCam.ViewportToWorldPoint(opticalCenter); // position is set as if physical camera's optical axis went exactly through image center

			// switch the z and y axes after correction
			Vector3 pos_temp;
			pos_temp = pos;
			pos_temp.y = pos.z;
			pos_temp.z = pos.y;
			pos = pos_temp;
//			pos.x = 0.018f;
//			Debug.Log (pos);
			transform.position = pos;

//			Vector3 rotation = new Vector3 (-90.296f, -4.084f, 90.91f);
//			transform.Rotate (rotation, Space.World);

//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.9998f, -0.0163f, -0.005f), new Vector3(-0.006f, 0.071f, 0.997f)); //1 from rodriguez
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.071f, -0.997f, -0.005f), new Vector3(-0.997f, 0.071f, 0.0163f)); // 2 from model matrix (i.e. post inversion)
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.071f, -0.005f, -0.997f), new Vector3(-0.997f, 0.0163f, 0.071f)); // 3flippin y and z
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.071f, -0.997f, -0.005f), new Vector3(-0.997f, -0.071f, 0.0163f)); // 4flipping only sign of middle axis
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.997f, 0.071f, 0.0163f), new Vector3(-0.071f, -0.997f, -0.005f)); // 5flip forward and up
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.071f, -0.997f, -0.005f), new Vector3(0.997f, 0.071f, 0.0163f));// 6switched sign on x component of the up vector from 2, BEST, OLD CALIB



//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.0313f, -0.999f, -0.013f), new Vector3(0.999f, 0.0311f, 0.008f)); // NEW, 
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.0313f, -0.013f, 0.999f), new Vector3(-0.999f, 0.008f, -0.0312f)); //test axis inversion as in pos= looks to the side
			// after flipping the obj_points and such, also flip z, y and invert y. invert x on the up vector
//			Quaternion rot = Quaternion.LookRotation (new Vector3(0.0313f, -0.999f, 0.013f), new Vector3(0.999f, -0.0311f, 0.008f)); 
			// after flipping the obj_points and such, also flip z, y and invert y. invert the whole up vector
//			Quaternion rot = Quaternion.LookRotation (new Vector3(0.0313f, -0.999f, 0.013f), new Vector3(0.999f, 0.0311f, -0.008f));
			// after flipping the obj_points and such, also flip z, y and invert y. invert x on both vectors
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.0313f, -0.999f, 0.013f), new Vector3(0.999f, 0.0311f, -0.008f)); 

			// after flipping the obj_points and such, also flip z, y and invert y. invert x and z on both vectors, GREAT
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.0313f, -0.999f, -0.013f), new Vector3(0.999f, 0.0311f, 0.008f)); 
//			// after flipping the obj_points and such, also flip z, y and invert y on both vectors. invert x and z on both vectors, BEST
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.0313f, -0.999f, -0.013f), new Vector3(0.999f, -0.0311f, 0.008f)); 
			// after flipping the obj_points and such, also flip z, y and invert y on both vectors. invert x on both vectors and z only on forward
//			Quaternion rot = Quaternion.LookRotation (new Vector3(-0.0313f, -0.999f, -0.013f), new Vector3(0.999f, -0.0311f, -0.008f)); 



			Quaternion rot = Quaternion.LookRotation (fwd_vector, up_vector);// loaded from file. see transformations applied when loading

			x_for = fwd_vector.x;
			y_for = fwd_vector.y;
			z_for = fwd_vector.z;
			x_up = up_vector.x;
			y_up = up_vector.y;
			z_up = up_vector.z;

			transform.rotation = rot;

		}

		Quaternion rot_rt = Quaternion.LookRotation (new Vector3(x_for,y_for,z_for), new Vector3(x_up,y_up,z_up) );
		transform.rotation = rot_rt;



		winText.text = "" + transform.position.x.ToString()+"/"+transform.position.y.ToString()+"/" +transform.position.z.ToString() + "\n\r" +
			transform.eulerAngles.ToString() + "\n\r" +
			camCam.fieldOfView.ToString(); 


	}
}
