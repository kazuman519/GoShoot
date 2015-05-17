using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	 
	public Player player;
	public Monster monster;

	public Canvas rollCanvas;
	public Canvas battleCanvas;

	// roll canvas
	public Text rollNum;
	// battle canvas
	public Text centerTitle;
	 
	float rollReadyTimeCount;
	float rollTimeCount;
	float battleReadyTimeCount;
	float battleTimeCount;
	bool isRollReadyTurn;
	bool isRollTurn;
	bool isBattleReadyTurn;
	bool isBattleStart;
	bool isBattleTurn;

	// Use this for initialization
	void Start () {
		print ("name:"+player.name);
		freezeRotationY (player);

		// public init
		changeCanvas(rollCanvas);
		centerTitle.text = "Roll Ready?";

		// init
		rollReadyTimeCount = 3.0f;
		rollTimeCount = 5.0f;
		battleReadyTimeCount = 3.0f;
		isRollReadyTurn = true;
		isRollTurn = false;
		isBattleReadyTurn = false;
		isBattleTurn = false;
		isBattleStart = false;
	}
	
	// Update is called once per frame
	void Update () {
		// count
		if (isRollReadyTurn) {
			rollReadyTimeCount -= Time.deltaTime;
			if (rollReadyTimeCount <= 0) {
				centerTitle.text = "Start!!";
				
				// change flag
				isRollReadyTurn = false;
				isRollTurn = true;
			} else if (rollReadyTimeCount <= 0) {
				centerTitle.text = "Roll Ready?";
			}
		} else if (isRollTurn) {
			rollTimeCount -= Time.deltaTime;

			if (rollTimeCount <= -1.5f) {
				centerTitle.text = "";

				// change flag
				isRollTurn = false;
				isBattleReadyTurn = true;
			} else if (rollTimeCount <= 0f) {
				// 巻くのが終わった時のアクション
				centerTitle.text = "next";
				centerTitle.color = Color.cyan;

			} else if (rollTimeCount <= 2.75f) {
				centerTitle.text = "";
			}
		} else if (isBattleReadyTurn) {
			battleReadyTimeCount -= Time.deltaTime;
			if (battleReadyTimeCount <= 0f) {
				centerTitle.text = "Shoot!!!";
				
				// change flag
				isBattleReadyTurn = false;
				isBattleStart = true;
			} else if (battleReadyTimeCount <= 3.0f) {
				centerTitle.text = "Battle Ready?";
			}
		}


		// Turn check

		// KeyDown
		if (Input.GetKeyDown (KeyCode.Space)) {
			if (isBattleStart) {
				playerShoot();

				// change flag
				isBattleStart = false;
				isBattleTurn = true;
			}
		}

		// Action
		if (isBattleTurn) {
			// バトルタイムカウントは増えてく
			battleTimeCount += Time.deltaTime;

			player.transform.Rotate (Vector3.up, player.turmPower * Time.deltaTime);
			monster.transform.Rotate (Vector3.up, monster.turmPower * Time.deltaTime);

			if (battleTimeCount >= 1.0f) {
				changeCanvas(battleCanvas);
			}
		}
	}

	void changeCanvas(Canvas canvas) {
		rollCanvas.enabled = false;
		battleCanvas.enabled = false;

		canvas.enabled = true;
	}

	void playerShoot() {
		releaseFreezeRotationY (player);
	}

	void freezeRotationY(Player gameObject) {
		gameObject.GetComponent<Rigidbody>().constraints =
			RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationZ |
				RigidbodyConstraints.FreezePositionY;
	}

	void releaseFreezeRotationY(Player gameObject) {
		gameObject.GetComponent<Rigidbody>().constraints =
			RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationZ;
	}
}
