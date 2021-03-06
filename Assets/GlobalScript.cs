using UnityEngine;

public class GlobalScript : MonoBehaviour {
	public GameObject skull;
	public float skull_creating_delay_in_ms = 1000.0f;
	public Vector2 position_limits = new Vector2(25.0f, 25.0f);
	public string player_object_name = "player";

	System.DateTime last_timestamp;
	PlayerBehaviourScript player_script;
	System.Random random_number_generator = new System.Random();

	void Start() {
		last_timestamp = System.DateTime.Now;

		GameObject player = GameObject.Find(player_object_name);
		player_script = player.GetComponent<PlayerBehaviourScript>();

		Screen.showCursor = false;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			player_script.SaveResults();
			Application.LoadLevel("menu_scene");
		}

		System.DateTime current_timestamp = System.DateTime.Now;
		if (
			skull != null
			&& current_timestamp.Subtract(last_timestamp).TotalMilliseconds >= skull_creating_delay_in_ms
		) {
			GameObject new_sull = (GameObject)Instantiate(skull);
			new_sull.transform.position =  new Vector3(
				getRandomPosition(position_limits.x),
				0.0f,
				getRandomPosition(position_limits.y)
			);

			last_timestamp = current_timestamp;
		}
	}

	float getRandomPosition(float limit) {
		return (float)random_number_generator.NextDouble() * 2 * limit - limit;
	}
}
