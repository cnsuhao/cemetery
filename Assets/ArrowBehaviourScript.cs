using UnityEngine;

public class ArrowBehaviourScript : MonoBehaviour {
	public float free_distance = 1.0f;

	void Update() {
		if (
			transform.parent != null
			&& Mathf.Abs(Vector3.Distance(transform.parent.position, transform.position)) > free_distance
		) {
			transform.parent = null;
		}

		if (transform.position.y < 0) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "skull") {
			Destroy(collision.gameObject);
		}
		Destroy(gameObject);
	}
}
