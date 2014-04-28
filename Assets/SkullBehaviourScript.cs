using UnityEngine;

public class SkullBehaviourScript : MonoBehaviour {
	public float speed = 1.0f;

	GameObject player;

	void Start() {
		player = GameObject.Find("player");
	}

	void Update () {
		if (player != null) {
			float speed = this.speed;
			float distance = Mathf.Abs(Vector3.Distance(transform.position, player.transform.position));
			if (distance == 0.0f) {
				return;
			} else if (distance < 2.5f) {
				speed *= -1.0f;
			}

			//transform.LookAt(player.transform);
			float angle = Mathf.Atan2(
				player.transform.position.z - transform.position.z,
				player.transform.position.x - transform.position.x
			);
			Debug.Log(angle);
			transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
			transform.position += transform.forward * speed;
		}
	}
}
