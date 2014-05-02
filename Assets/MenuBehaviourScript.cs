using UnityEngine;
using System.Linq;

enum MenuPage {
	MAIN,
	SCORES,
	AUTHORS
}

public class MenuBehaviourScript : MonoBehaviour {
	public Vector2 major_page_size = new Vector2(100, 104);
	public Vector2 minor_page_size = new Vector2(250, 250);
	public float back_batton_width_factor = 0.33f;
	public TextAsset authors_text;
	public Color background_color = new Color(21.0f / 255.0f, 37.0f / 255.0f, 46.0f / 255.0f, 0.5f);

	MenuPage current_page = MenuPage.MAIN;
	GUIStyle background_style = new GUIStyle();
	string[] scores = new string[0];
	Vector2 authors_page_scroll_position = Vector2.zero;

	void Start() {
		Screen.showCursor = true;

		background_style.normal.background = new Texture2D(1, 1);
		background_style.normal.background.SetPixel(0, 0, background_color);
		background_style.normal.background.Apply();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			switch (current_page) {
				case MenuPage.MAIN:
					quit();
					break;
				case MenuPage.SCORES:
				case MenuPage.AUTHORS:
					current_page = MenuPage.MAIN;
					break;
			}
		}

		if (current_page == MenuPage.SCORES) {
			scores =
				PlayerPrefs
				.GetString("scores")
				.Split(
					new char[] {';'},
					System.StringSplitOptions.RemoveEmptyEntries
				)
				.Where(score => score != "0")
				.Distinct()
				.ToArray();
			System.Array.Sort(scores, (score1, score2) => int.Parse(score2) - int.Parse(score1));
		}
	}

	void OnGUI() {
		switch (current_page) {
			case MenuPage.MAIN:
				showMainPage();
				break;
			case MenuPage.SCORES:
				showScoresPage();
				break;
			case MenuPage.AUTHORS:
				showAuthorsPage();
				break;
		}
	}

	void showMainPage() {
		GUILayout.BeginArea(
			new Rect(
				(Screen.width - major_page_size.x) / 2,
				(Screen.height - major_page_size.y) / 2,
				major_page_size.x,
				major_page_size.y
			),
			background_style
		);

		bool pressed = GUILayout.Button("Играть");
		if (pressed) {
			Application.LoadLevel("game_scene");
		}

		pressed = GUILayout.Button("Результаты");
		if (pressed) {
			current_page = MenuPage.SCORES;
		}

		pressed = GUILayout.Button("Авторы");
		if (pressed) {
			current_page = MenuPage.AUTHORS;
		}

		pressed = GUILayout.Button("Выход");
		if (pressed) {
			quit();
		}

		GUILayout.EndArea();
	}

	void showScoresPage() {
		GUILayout.BeginArea(
			new Rect(
				(Screen.width - minor_page_size.x) / 2,
				(Screen.height - minor_page_size.y) / 2,
				minor_page_size.x,
				minor_page_size.y
			),
			background_style
		);

		authors_page_scroll_position = GUILayout.BeginScrollView(authors_page_scroll_position);
		GUILayout.SelectionGrid(-1, scores, 1);
		GUILayout.EndScrollView();

		bool pressed = GUILayout.Button("Назад", GUILayout.Width(back_batton_width_factor * minor_page_size.x));
		if (pressed) {
			current_page = MenuPage.MAIN;
		}

		GUILayout.EndArea();
	}

	void showAuthorsPage() {
		GUILayout.BeginArea(
			new Rect(
				(Screen.width - minor_page_size.x) / 2,
				(Screen.height - minor_page_size.y) / 2,
				minor_page_size.x,
				minor_page_size.y
			),
			background_style
		);

		authors_page_scroll_position = GUILayout.BeginScrollView(authors_page_scroll_position);
		if (authors_text != null) {
			GUILayout.Label(authors_text.text);
		}
		GUILayout.EndScrollView();

		bool pressed = GUILayout.Button("Назад", GUILayout.Width(back_batton_width_factor * minor_page_size.x));
		if (pressed) {
			current_page = MenuPage.MAIN;
		}

		GUILayout.EndArea();
	}

	void quit() {
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#elif !UNITY_WEBPLAYER
			Application.Quit();
		#endif
	}
}
