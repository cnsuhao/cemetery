using UnityEngine;

public class ArrowBehaviourScript : MonoBehaviour {
	public float free_distance = 1.0f;
	public string player_object_name = "player";

	PlayerBehaviourScript player_script;

	void Start() {
		GameObject player = GameObject.Find(player_object_name);
		player_script = player.GetComponent<PlayerBehaviourScript>();
	}

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
			if (player_script != null) {
				player_script.IncreaseSkulls();
			}
		}
		Destroy(gameObject);
	}
}
