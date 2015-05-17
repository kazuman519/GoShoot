using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
	 
	public Player player;
	public Monster monster;

	public Camera mainCamera;
	public Camera subCamera;
	public Camera playerCamera;

	public Canvas rollCanvas;
	public Canvas battleCanvas;

	// roll canvas
	public Text headerTitle;
	public Text rollNum;
	public Text centerTitle;
	 
	// battle canvas
	public Text hpValue;
	public Text spValue;
	 
	float rollReadyTimeCount;
	float rollTimeCount;
	float battleReadyTimeCount;
	float battleTimeCount;
	int rollCount;
	bool isRollReadyTurn;
	bool isRollTurn;
	bool isBattleReadyTurn;
	bool isBattleStart;
	bool isBattleTurn;
	Color defaultCentetTitleColor;
	
	// Use this for initialization
	void Start () {
		print ("name:"+player.name);
		freezeRotationY (player.gameObject);
		freezeRotationY (monster.gameObject);

		// public init
		changeCmera (playerCamera);
		changeCanvas(rollCanvas);
		centerTitle.text = "Roll Ready?";

		// init
		rollReadyTimeCount = 3.0f;
		rollTimeCount = 5.0f;
		battleReadyTimeCount = 3.0f;
		rollCount = 0;
		isRollReadyTurn = true;
		isRollTurn = false;
		isBattleReadyTurn = false;
		isBattleTurn = false;
		isBattleStart = false;
		defaultCentetTitleColor = centerTitle.color;
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
			rollNum.text = "Roll:"+rollCount;

			if (rollTimeCount <= -2.5f) {
				centerTitle.text = "";

				// change flag
				isRollTurn = false;
				isBattleReadyTurn = true;
			} else if (rollTimeCount <= -1.0f) {
				headerTitle.text = "合図と共に紐を引け!!";
				centerTitle.text = "next";
				centerTitle.color = Color.cyan;
				changeCmera (subCamera);
			} else if (rollTimeCount <= 0f) {
				centerTitle.text = "End!!";
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
				centerTitle.color = defaultCentetTitleColor;
			}
		}

		// HP & SP
		if (player.turmPower > 0) {
			hpValue.text = player.turmPower.ToString ("F0");
			spValue.text = player.magicPower.ToString ("F0");
		} else {
			hpValue.text = "0";
		}


		// KeyDown
		if (Input.GetKeyDown (KeyCode.Space)) {
			releaseFreezeRotationY (player.gameObject);
			releaseFreezeRotationY (monster.gameObject);
			
			// change flag
			isBattleStart = false;
			isBattleTurn = true;
			changeCanvas(battleCanvas);
			changeCmera (mainCamera);
		}
		if (Input.GetMouseButtonDown(0)
		    || Input.touchCount > 0) {
			print ("downsitayo");
//			Touch touch = Input.GetTouch(0);
//			Vector2 touchPosition = new Vector2(touch.position.x, Screen.height - touch.position.y);
//			print ("touch pos x:"+touchPosition.x+", y:"+touchPosition.y);

			if (isRollTurn) {

			} else if (isBattleStart) {
				releaseFreezeRotationY (player.gameObject);
				releaseFreezeRotationY (monster.gameObject);

				// change flag
				isBattleStart = false;
				isBattleTurn = true;
			}
		}

		// Action
		if (isBattleTurn) {
			// バトルタイムカウントは増えてく
			battleTimeCount += Time.deltaTime;
			if (player.turmPower > 0) {
				player.transform.Rotate (Vector3.up, player.turmPower * Time.deltaTime);
			}
			if (monster.turmPower > 0) {
				monster.transform.Rotate (Vector3.up, monster.turmPower * Time.deltaTime);
			}

			if (battleTimeCount >= 0.5f) {
				// change canvas&camera
				changeCanvas(battleCanvas);
				changeCmera (mainCamera);
			}
		}
	}
	

	void changeCmera(Camera camera) {
		mainCamera.enabled = false;
		subCamera.enabled = false;
		playerCamera.enabled = false;
		
		camera.enabled = true;
	}

	void changeCanvas(Canvas canvas) {
		rollCanvas.enabled = false;
		battleCanvas.enabled = false;

		canvas.enabled = true;
	}

	void freezeRotationY(GameObject gameObject) {
		gameObject.GetComponent<Rigidbody>().constraints =
			RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationZ |
				RigidbodyConstraints.FreezePositionY;
	}

	void releaseFreezeRotationY(GameObject gameObject) {
		gameObject.GetComponent<Rigidbody>().constraints =
			RigidbodyConstraints.FreezeRotationX |
				RigidbodyConstraints.FreezeRotationZ;
	}
}
