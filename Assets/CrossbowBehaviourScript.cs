using UnityEngine;
using System.Collections;

public class CrossbowBehaviourScript : MonoBehaviour {
	public GameObject arrow;
	public float speed = 1.0f;

	Transform arrow_origin;

	void Start() {
		arrow_origin = transform.Find("arrow_origin");
	}

	void Update () {
		if (arrow != null && arrow_origin != null && Input.GetButtonDown("Fire1")) {
			GameObject arrow_clone = (GameObject)Instantiate(arrow, arrow_origin.position, arrow_origin.rotation);
			arrow_clone.GetComponent<Rigidbody>().velocity = speed * arrow_origin.up;
		}
	}
}
