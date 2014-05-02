using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour {
	public float maximal_health = 100.0f;
	public Vector2 gui_size = new Vector2(100, 100);
	public Texture skull_icon;
	public Texture heart_icon;

	public void DecreaseHealth(float value) {
		if (health > value) {
			health -= value;
		} else {
			SaveResults();
			Application.LoadLevel("menu_scene");
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
	GUIStyle text_style = new GUIStyle();

	void Start() {
		health = maximal_health;

		text_style.fontSize = 64;
		text_style.normal.textColor = Color.grey;
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
