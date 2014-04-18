using UnityEngine;
using UnityEditor;

public class TerrainPlacer : EditorWindow {
	string terrain_name = "terrain";
	float vertical_shift = 0.0f;
	Vector3 rotate_limits = Vector3.zero;
	Terrain terrain = null;
	UnityEngine.Object[] selected_stones = new UnityEngine.Object[0];
	System.Random random_number_generator = new System.Random();

	[MenuItem("Window/Terrain Placer")]
	static void Init() {
		TerrainPlacer.GetWindow(typeof(TerrainPlacer));
	}

	void Update() {
		GameObject terrain = GameObject.Find(terrain_name);
		this.terrain =
			terrain != null
				? terrain.GetComponent<Terrain>()
				: null;

		selected_stones = Selection.GetFiltered(
			typeof(GameObject),
			SelectionMode.TopLevel
				| SelectionMode.ExcludePrefab
				| SelectionMode.Editable
		);
	}

	void OnGUI() {
		if (terrain == null) {
			EditorGUILayout.HelpBox(
				"Terrain not found!", MessageType.Error
			);
		}
		if (selected_stones.Length == 0) {
			EditorGUILayout.HelpBox(
				"Objects for placing not selected!", MessageType.Warning
			);
		}
		GUILayout.Label(
			string.Format(
				"Selected objects: {0}.", selected_stones.Length
			),
			EditorStyles.boldLabel
		);

		terrain_name = EditorGUILayout.TextField("Terrain name:", terrain_name);
		vertical_shift = EditorGUILayout.FloatField("Vertical shift:", vertical_shift);
		rotate_limits = EditorGUILayout.Vector3Field("Rotate limits:", rotate_limits);

		GUI.enabled = terrain != null && selected_stones.Length > 0;
		bool pressed = GUILayout.Button("Process");
		if (pressed) {
			process();
		}
		GUI.enabled = true;
	}

	void process() {
		foreach(UnityEngine.Object stone in selected_stones) {
			GameObject game_object = (GameObject)stone;
			Undo.RecordObject(game_object.transform, "Place object on terrain");

			Vector3 game_object_position = game_object.transform.position;
			game_object_position.y = terrain.SampleHeight(game_object_position) + vertical_shift;
			game_object.transform.position = game_object_position;

			game_object.transform.Rotate(
				new Vector3(
					getRandomNumber(rotate_limits.x),
					getRandomNumber(rotate_limits.y),
					getRandomNumber(rotate_limits.z)
				)
			);
		}
	}

	float getRandomNumber(float base_value) {
		return (float)random_number_generator.NextDouble() * 2 * base_value - base_value;
	}
}
