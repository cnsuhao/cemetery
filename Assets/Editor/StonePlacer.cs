using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

public class StonePlacer : EditorWindow {
	UnityEngine.Object[] selected_stones = new UnityEngine.Object[0];
	Terrain terrain = null;
	System.Random random_number_generator = new System.Random();
	String vertical_shift = "0";
	Vector3 rotate_limits = Vector3.zero;

	[MenuItem("Window/Stone Placer")]
	static void Init() {
		StonePlacer.GetWindow(typeof(StonePlacer));
	}

	void Update() {
		selected_stones = Selection.GetFiltered(
			typeof(GameObject),
			SelectionMode.TopLevel
				| SelectionMode.ExcludePrefab
				| SelectionMode.Editable
		);

		GameObject terrain = GameObject.Find("ground");
		if (terrain != null) {
			this.terrain = terrain.GetComponent<Terrain>();
		}
	}

	void OnGUI() {
		if (terrain == null) {
			GUILayout.Label(
				"Terrain not found!", EditorStyles.boldLabel
			);
		}

		GUILayout.Label(
			String.Format(
				"Selected stones: {0}.", selected_stones.Length
			),
			EditorStyles.boldLabel
		);

		vertical_shift = EditorGUILayout.TextField("Vertical shift:", vertical_shift);

		rotate_limits = EditorGUILayout.Vector3Field("Rotate limits:", rotate_limits);

		GUI.enabled = selected_stones.Length > 0 && terrain != null;
		bool pressed = GUILayout.Button("Process");
		if (pressed) {
			process();
		}
		GUI.enabled = true;
	}

	void process() {
		foreach(UnityEngine.Object stone in selected_stones) {
			GameObject game_object = (GameObject)stone;
			Undo.RecordObject(game_object.transform, "Place stone");

			float vertical_shift_value = 0.0f;
			try {
				vertical_shift_value = float.Parse(vertical_shift);
			} catch(FormatException) {}

			Vector3 game_object_position = game_object.transform.position;
			game_object_position.y = terrain.SampleHeight(game_object_position) + vertical_shift_value;
			game_object.transform.position = game_object_position;

			Vector3 random_angles = new Vector3(
				getRandomNumber(rotate_limits.x),
				getRandomNumber(rotate_limits.y),
				getRandomNumber(rotate_limits.z)
			);
			game_object.transform.Rotate(random_angles);
		}
	}

	float getRandomNumber(float base_value) {
		return (float)random_number_generator.NextDouble() * 2 * base_value - base_value;
	}
}
