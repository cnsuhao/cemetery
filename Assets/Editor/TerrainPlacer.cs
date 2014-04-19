using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

enum TerrainPlacerError {
	TERRAIN_OBJECT_NOT_SELECTED,
	TERRAIN_OBJECT_IS_PERSISTENT,
	TERRAIN_OBJECT_NOT_CONTAINS_TERRAIN,
	OBJECTS_NOT_SELECTED
}

public class TerrainPlacer : EditorWindow {
	GameObject terrain_object;
	float vertical_shift = 0.0f;
	Vector3 rotate_limits = Vector3.zero;

	Terrain terrain;
	Object[] selected_objects = new Object[0];

	List<TerrainPlacerError> errors = new List<TerrainPlacerError>();

	System.Random random_number_generator = new System.Random();

	[MenuItem("Window/Terrain Placer")]
	static void Init() {
		EditorWindow window = TerrainPlacer.GetWindow(typeof(TerrainPlacer));
		window.title = "Terrain Placer";
		window.Show();
	}

	void Update() {
		errors.Clear();
		updateTerrain();
		updateSelectedObjects();
	}

	void updateTerrain() {
		if (terrain_object == null) {
			errors.Add(TerrainPlacerError.TERRAIN_OBJECT_NOT_SELECTED);
		} else if (EditorUtility.IsPersistent(terrain_object)) {
			errors.Add(TerrainPlacerError.TERRAIN_OBJECT_IS_PERSISTENT);
		} else {
			terrain = terrain_object.GetComponent<Terrain>();
			if (terrain == null) {
				errors.Add(TerrainPlacerError.TERRAIN_OBJECT_NOT_CONTAINS_TERRAIN);
			}
		}
	}

	void updateSelectedObjects() {
		selected_objects = Selection.GetFiltered(
			typeof(GameObject),
			SelectionMode.TopLevel
				| SelectionMode.ExcludePrefab
				| SelectionMode.Editable
		);
		if (selected_objects.Length == 0) {
			errors.Add(TerrainPlacerError.OBJECTS_NOT_SELECTED);
		}
	}

	void OnGUI() {
		outputErrorMessages();
		outputUtilInfo();
		outputParametersFields();
		outputProcessButton();
	}

	void outputErrorMessages() {
		if (errors.Contains(TerrainPlacerError.TERRAIN_OBJECT_NOT_SELECTED)) {
			EditorGUILayout.HelpBox(
				"Terrain object not selected.", MessageType.Error
			);
		}
		if (errors.Contains(TerrainPlacerError.TERRAIN_OBJECT_IS_PERSISTENT)) {
			EditorGUILayout.HelpBox(
				"Terrain object is persistent. Selects its from scene.", MessageType.Error
			);
		}
		if (errors.Contains(TerrainPlacerError.TERRAIN_OBJECT_NOT_CONTAINS_TERRAIN)) {
			EditorGUILayout.HelpBox(
				"Terrain object not contains terrain.", MessageType.Error
			);
		}
		if (errors.Contains(TerrainPlacerError.OBJECTS_NOT_SELECTED)) {
			EditorGUILayout.HelpBox(
				"Objects for placing not selected.", MessageType.Warning
			);
		}
		if (errors.Count > 0) {
			EditorGUILayout.Space();
		}
	}

	void outputUtilInfo() {
		EditorGUILayout.LabelField(
			"Selected objects:",
			selected_objects.Length.ToString()
		);
		EditorGUILayout.Space();
	}

	void outputParametersFields() {
		terrain_object = (GameObject)EditorGUILayout.ObjectField(
			"Terrain object:",
			terrain_object,
			typeof(GameObject),
			true
		);
		vertical_shift = EditorGUILayout.FloatField("Vertical shift:", vertical_shift);
		rotate_limits = EditorGUILayout.Vector3Field("Rotate limits:", rotate_limits);
		EditorGUILayout.Space();
	}

	void outputProcessButton() {
		GUI.enabled = errors.Count == 0;
		bool pressed = GUILayout.Button("Process");
		if (pressed) {
			processObjects();
		}
		GUI.enabled = true;
	}

	void processObjects() {
		foreach(Object selected_object in selected_objects) {
			saveObjectState((GameObject)selected_object);
			changeObjectPosition((GameObject)selected_object);
			changeObjectRotation((GameObject)selected_object);
		}
	}

	void saveObjectState(GameObject game_object) {
		Undo.RecordObject(game_object.transform, "Place object on terrain");
	}

	void changeObjectPosition(GameObject game_object) {
		game_object.transform.position = new Vector3(
			game_object.transform.position.x,
			terrain.SampleHeight(game_object.transform.position) + vertical_shift,
			game_object.transform.position.z
		);
	}

	void changeObjectRotation(GameObject game_object) {
		game_object.transform.Rotate(
			new Vector3(
				getRandomAngle(rotate_limits.x),
				getRandomAngle(rotate_limits.y),
				getRandomAngle(rotate_limits.z)
			)
		);
	}

	float getRandomAngle(float limit) {
		return (float)random_number_generator.NextDouble() * 2 * limit - limit;
	}
}
