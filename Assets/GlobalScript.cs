using UnityEngine;

public class GlobalScript : MonoBehaviour {
	public GameObject skull;
	public float skull_creating_delay_in_ms = 1000.0f;

	System.DateTime last_timestamp;
	System.Random random_number_generator = new System.Random();

	void Start() {
		last_timestamp = System.DateTime.Now;
		Screen.showCursor = false;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			UnityEditor.EditorApplication.isPlaying = false;
			Application.Quit();
		}

		System.DateTime current_timestamp = System.DateTime.Now;
		if (
			skull != null
			&& current_timestamp.Subtract(last_timestamp).TotalMilliseconds >= skull_creating_delay_in_ms
		) {
			GameObject new_sull = (GameObject)Instantiate(skull);
			new_sull.transform.position =  new Vector3(
				getRandomPosition(25),
				2.5f,
				getRandomPosition(25)
			);

			last_timestamp = current_timestamp;
		}
	}

	float getRandomPosition(float limit) {
		return (float)random_number_generator.NextDouble() * 2 * limit - limit;
	}
}
