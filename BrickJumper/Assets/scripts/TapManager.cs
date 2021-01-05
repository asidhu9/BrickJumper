using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapManager : MonoBehaviour {

	public delegate void PlayerDelegate();
	public static event PlayerDelegate OnPlayerDied;
	public static event PlayerDelegate OnPlayerScored;

	public float tapForce = 10;
	public float tiltSmooth = 5;
	public Vector3 startPos;
	public AudioSource tapSound;
	public AudioSource scoreSound;
	public AudioSource dieSound;

    Rigidbody2D rigidBody;
	Quaternion downRotation;
	Quaternion forwardRotation;

	GameController game;
	TrailRenderer trail;

	void Start() {
		rigidBody = GetComponent<Rigidbody2D>();
		game = GameController.Instance;
		rigidBody.simulated = false;
    }

	void OnEnable() {
        GameController.OnGameStarted += OnGameStarted;
        GameController.OnGameOverConfirmed += OnGameOverConfirmed;
	}

	void OnDisable() {
        GameController.OnGameStarted -= OnGameStarted;
        GameController.OnGameOverConfirmed -= OnGameOverConfirmed;
	}

	void OnGameStarted() {
		rigidBody.velocity = Vector3.zero;
		rigidBody.simulated = true;
	}

	void OnGameOverConfirmed() {
		transform.localPosition = startPos;
		transform.rotation = Quaternion.identity;
	}

	void Update() {
		if (game.GameOver) return;

		if (Input.GetMouseButtonDown(0)) {
            rigidBody.velocity = Vector2.zero;
			rigidBody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
			tapSound.Play();
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "ScoreZone") {
			OnPlayerScored();
			scoreSound.Play();
		}
		if (col.gameObject.tag == "DeadZone") {
			rigidBody.simulated = false;
			OnPlayerDied();
			dieSound.Play();
		}
	}

}
