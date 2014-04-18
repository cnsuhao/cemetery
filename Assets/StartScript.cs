using UnityEngine;

public class StartScript : MonoBehaviour {
	void Start() {
		Screen.showCursor = false;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			UnityEditor.EditorApplication.isPlaying = false;
			Application.Quit();
		}
	}
}
