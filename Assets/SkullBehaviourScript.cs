using UnityEngine;

public class SkullBehaviourScript : MonoBehaviour {
	public string player_camera_name = "player_camera";
	public float vertical_shift = -1.0f;
	public float minimal_distance = 2.5f;
	public float speed = 1.0f;
	public string player_object_name = "player";
	public float attack_value = 1.0f;

	GameObject player_camera;
	PlayerBehaviourScript player_script;

	void Start() {
		player_camera = GameObject.Find(player_camera_name);

		GameObject player = GameObject.Find(player_object_name);
		player_script = player.GetComponent<PlayerBehaviourScript>();
	}

	void Update () {
		if (player_camera != null) {
			transform.LookAt(
				player_camera.transform.position
				+ new Vector3(0.0f, vertical_shift, 0.0f)
			);

			if (Mathf.Abs(Vector3.Distance(transform.position, player_camera.transform.position)) > minimal_distance) {
				transform.position += transform.forward * speed;
			} else {
				Destroy(gameObject);
				if (player_script != null) {
					player_script.DecreaseHealth(attack_value);
				}
			}
		}
	}
}
