using UnityEngine;

public class PlayerBehaviourScript : MonoBehaviour {
	public float maximal_health = 10.0f;

	public void DecreaseHealth(float value) {
		if (health > value) {
			health -= value;
		} else {
			Application.LoadLevel("menu_scene");
		}
	}

	float health = 1.0f;

	void Start() {
		health = maximal_health;
	}
}
