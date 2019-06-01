using UnityEngine;
using System.Collections;

public class WanderingAI : MonoBehaviour {

	public float wanderRadius;
	public float wanderTimer;
	public Animator anim;

	private Transform target;
	private UnityEngine.AI.NavMeshAgent agent;
	private float timer;
	private float new_speed = 0;
	private float old_forwardPos = 0;
	private float delta_angle = 0;
	private float old_angle = 0;

	// Use this for initialization
	void OnEnable () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		timer = wanderTimer;
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= wanderTimer) {
			Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
//			Debug.Log (newPos);
			agent.SetDestination(newPos);
			timer = 0;

		}
		new_speed = Mathf.Abs((transform.position.x-old_forwardPos)/Time.deltaTime);
		old_forwardPos = transform.position.x;

		delta_angle = Mathf.Abs(Mathf.Rad2Deg*(transform.localRotation.eulerAngles.y - old_angle));
		// update the angle
		old_angle = transform.localRotation.eulerAngles.y;



		Debug.Log (new_speed);



		anim.SetFloat ("Speed", new_speed);

		anim.SetInteger("State_selector", Random.Range(1,3));

		anim.SetFloat ("Angle", delta_angle);



	}		

	public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
		Vector3 randDirection = Random.insideUnitSphere * dist;

		randDirection += origin;

		UnityEngine.AI.NavMeshHit navHit;

		UnityEngine.AI.NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);

		return navHit.position;
	}
}
