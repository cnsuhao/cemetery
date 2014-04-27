using UnityEngine;

public class CrossbowBehaviourScript : MonoBehaviour {
	public GameObject arrow;
	public float speed = 1.0f;
	public float shot_delay_in_ms = 1000.0f;

	Transform arrow_origin;
	System.DateTime last_timestamp;

	void Start() {
		arrow_origin = transform.Find("arrow_origin");
		last_timestamp = System.DateTime.Now;
	}

	void Update () {
		System.DateTime current_timestamp = System.DateTime.Now;
		if (
			arrow != null
			&& arrow_origin != null
			&& Input.GetButtonDown("Fire1")
			&& current_timestamp.Subtract(last_timestamp).TotalMilliseconds >= shot_delay_in_ms
		) {
			GameObject arrow_clone = (GameObject)Instantiate(arrow, arrow_origin.position, arrow_origin.rotation);
			arrow_clone.transform.parent = arrow_origin;
			arrow_clone.GetComponent<Rigidbody>().velocity = speed * arrow_origin.up;

			last_timestamp = current_timestamp;
		}
	}
}
