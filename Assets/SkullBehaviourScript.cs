using UnityEngine;

public class SkullBehaviourScript : MonoBehaviour {
	public string player_camera_name = "camera";
	public float speed = 1.0f;
	public float minimal_distance = 2.5f;

	GameObject player_camera;

	void Start() {
		player_camera = GameObject.Find(player_camera_name);
	}

	void Update () {
		if (player_camera != null) {
			transform.LookAt(player_camera.transform);

			float speed = this.speed;
			if (Mathf.Abs(Vector3.Distance(transform.position, player_camera.transform.position)) < minimal_distance) {
				speed = -speed;
			}
			transform.position += transform.forward * speed;
		}
	}
}
