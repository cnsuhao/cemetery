using UnityEngine;

public class ArrowBehaviourScript : MonoBehaviour {
	void Update() {
		if (transform.position.y < 0) {
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision) {
		Destroy(gameObject);
	}
}
