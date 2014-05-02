using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour {
	public float maximal_health = 100.0f;
	public Vector2 gui_size = new Vector2(100, 100);
	public Texture skull_icon;
	public Texture heart_icon;

	public void DecreaseHealth(float value) {
		if (health > value) {
			health -= value;
			if (
				player_pain_source != null
				&& player_pain_source.audio != null
				&& !player_pain_source.audio.isPlaying
			) {
				player_pain_source.audio.Play();
			}
		} else {
			if (
				!dying
				&& player_death_source != null
				&& player_death_source.audio != null
			) {
				player_death_source.audio.Play();
				dying = true;
			}
		}
	}

	public void IncreaseSkulls() {
		skulls++;
	}

	public void SaveResults() {
		string scores = PlayerPrefs.GetString("scores");
		if (scores.Length != 0) {
			scores += ";";
		}
		scores += skulls.ToString();

		PlayerPrefs.SetString("scores", scores);
	}

	float health = 1.0f;
	float skulls = 0;
	CharacterController character_controller;
	bool player_was_on_ground = false;
	GameObject player_steps_source;
	GameObject player_jump_start_source;
	GameObject player_jump_end_source;
	GameObject player_pain_source;
	GameObject player_death_source;
	bool dying = false;
	GUIStyle text_style = new GUIStyle();

	void Start() {
		health = maximal_health;

		character_controller = GetComponent<CharacterController>();
		if (character_controller != null) {
			player_was_on_ground = character_controller.isGrounded;
		}

		player_steps_source = GameObject.Find("player_steps_source");
		player_jump_start_source = GameObject.Find("player_jump_start_source");
		player_jump_end_source = GameObject.Find("player_jump_end_source");
		player_pain_source = GameObject.Find("player_pain_source");
		player_death_source = GameObject.Find("player_death_source");

		text_style.fontSize = 64;
		text_style.normal.textColor = Color.grey;
	}

	void Update() {
		if (character_controller != null && !dying) {
			if (
				character_controller.isGrounded
				&& character_controller.velocity.magnitude > 0.0f
				&& player_steps_source != null
				&& player_steps_source.audio != null
			) {
				if (!player_steps_source.audio.isPlaying) {
					player_steps_source.audio.Play();
				}
			} else {
				player_steps_source.audio.Pause();
			}

			if (
				player_was_on_ground
				&& !character_controller.isGrounded
				&& player_jump_start_source != null
				&& player_jump_start_source.audio != null
			) {
				player_jump_start_source.audio.Play();
			}
			if (
				!player_was_on_ground
				&& character_controller.isGrounded
				&& player_jump_end_source != null
				&& player_jump_end_source.audio != null
			) {
				player_jump_end_source.audio.Play();
			}

			player_was_on_ground = character_controller.isGrounded;
		}

		if (
			dying
			&& player_death_source != null
			&& player_death_source.audio != null
			&& !player_death_source.audio.isPlaying
		) {
			SaveResults();
			Application.LoadLevel("menu_scene");
		}
	}

	void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width - gui_size.x, 0, gui_size.x, gui_size.y));
		GUILayout.BeginVertical();

		GUILayout.BeginHorizontal();
		GUILayout.Label(skull_icon);
		GUILayout.Label(skulls.ToString(), text_style);
		GUILayout.EndHorizontal();

		GUILayout.BeginHorizontal();
		GUILayout.Label(heart_icon);
		GUILayout.Label(health.ToString(), text_style);
		GUILayout.EndHorizontal();

		GUILayout.EndVertical();
		GUILayout.EndArea();
	}
}
