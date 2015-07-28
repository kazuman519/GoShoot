using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour {
	 
	public Player player;
	public Monster monster;

	// Camera
	public Camera MainCamera;
	public Camera SubCamera;
	public Camera playerCamera;

	// Vamvas
	public Canvas rollCanvas;
	public Canvas battleCanvas;

	// component
	public Text headerTitle;
	public Text rollNum;
	public Text centerTitle;
	public Button replayButton;
	 
	// battle canvas
	public Text hpValue;
	public Text spValue;
	 
	private float rollReadyTimeCount;
	private float rollTimeCount;
	private float battleReadyTimeCount;
	private float battleTimeCount;
	private int rollCount;

	// game flag
	private bool isRollReadyTurn;
	private bool isRollTurn;
	private bool isBattleReadyTurn;
	private bool isBattleStart;
	private bool isBattleTurn;
	private bool isBatteleEnd;
	private bool isResultTurn;

	// template color
	private Color defaultCentetTitleColor;

	public LineRenderer lineRenderer;
	List<Vector3> drawRopePositionList = new List<Vector3>();

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
		isBatteleEnd = false;
		isResultTurn = false;

		defaultCentetTitleColor = centerTitle.color;

		lineRenderer.enabled = true;

		print("RollReady Turn");
	}
	
	// Update is called once per frame
	void Update () {
		// ---------------------------------------------
		// ターン制御 
		// ---------------------------------------------
		if (isRollReadyTurn) {
			/*
			 * ロールスタートのカウントターン
			 */
			rollReadyTimeCount -= Time.deltaTime;
			if (rollReadyTimeCount <= 0) {
				centerTitle.text = "Start!!";
				
				// change flag
				isRollReadyTurn = false;
				isRollTurn = true;
				print ("Roll Turn");
			} else if (rollReadyTimeCount <= 0) {
				centerTitle.text = "Roll Ready?";
			}
		} else if (isRollTurn) {
			/*
			 * ロールターン
			 */
			rollTimeCount -= Time.deltaTime;
			rollNum.text = "Roll:" + rollCount;

			if (rollTimeCount <= -2.5f) {
				centerTitle.text = "";

				// change flag
				isRollTurn = false;
				isBattleReadyTurn = true;
				print ("ButtleReady Turn");
			} else if (rollTimeCount <= -1.0f) {
				headerTitle.text = "合図と共に紐を引け!!";
				centerTitle.text = "next";
				centerTitle.color = Color.cyan;
				changeCmera (SubCamera);
			} else if (rollTimeCount <= 0f) {
				centerTitle.text = "End!!";
			} else if (rollTimeCount <= 2.75f) {
				centerTitle.text = "";
			}
		} else if (isBattleReadyTurn) {
			/*
			 * バトルスタートのカウントターン
			 */
			battleReadyTimeCount -= Time.deltaTime;
			if (battleReadyTimeCount <= 0f) {
				centerTitle.text = "Shoot!!!";
				
				// change flag
				isBattleReadyTurn = false;
				isBattleStart = true;
				print ("Buttle Turn");
			} else if (battleReadyTimeCount <= 3.0f) {
				centerTitle.text = "Battle Ready?";
				centerTitle.color = defaultCentetTitleColor;
			}
		} else if (isBattleTurn) {
			/*
			 * バトルターン
			 */
			battleTimeCount += Time.deltaTime; // バトルタイムカウントは増えてく

			// プレイヤーとモンスターを回す
			if (player.turmPower > 0) {
				player.transform.Rotate (Vector3.up, player.turmPower * Time.deltaTime);
			}
			if (monster.turmPower > 0) { 
				monster.transform.Rotate (Vector3.up, monster.turmPower * Time.deltaTime);
			}

			// Battle開始数秒後にフィールドのカメラに変える
			if (battleTimeCount >= 0.5f) {
				// change canvas&camera
				changeCanvas (battleCanvas);
				changeCmera (MainCamera);
			}

			// バトルが終わったかの管理
			if (isBatteleEnd) {
				print ("Buttle End");
				replayButton.gameObject.SetActive (true);

				// change flag
				isBatteleEnd = false;
				isBattleTurn = false;
				isResultTurn = true;
			}
		} else if (isResultTurn) {

		}

		// ---------------------------------------------
		// ステータス表示制御
		// ---------------------------------------------
		if (player.turmPower > 0) {
			hpValue.text = player.turmPower.ToString ("F0");
			spValue.text = player.magicPower.ToString ("F0");
		} else {
			hpValue.text = "0";

			// change flag
			isBatteleEnd = true;
		}


		// ---------------------------------------------
		// コマンド制御
		// ---------------------------------------------
		if (Input.GetKeyDown (KeyCode.Space)) {
			releaseFreezeRotationY (player.gameObject);
			releaseFreezeRotationY (monster.gameObject);
			
			// change flag
			isBattleStart = false;
			isBattleTurn = true;
			changeCanvas (battleCanvas);
			changeCmera (MainCamera);
		}

		if (Input.GetMouseButtonDown(0)
		    || Input.touchCount > 0) {
			print ("downsitayo");


//			Touch touch = Input.GetTouch(0);
//			Vector2 touchPosition = new Vector2(touch.position.x, Screen.height - touch.position.y);
//			print ("touch pos x:"+touchPosition.x+", y:"+touchPosition.y);
//			headerTitle.text = "touch pos x:"+touchPosition.x+", y:"+touchPosition.y;

			if (isRollTurn) {
//				Vector3 vec =  new Vector3(touchPosition.x,touchPosition.y,player.ropeBox.transform.position.z);
				//player.ropeBox.transform.position = playerCamera.ScreenToWorldPoint(vec);;


			} else if (isBattleStart) {
				player.ropePowerParticle.SetActive(false);
				player.ropeBox.SetActive(false);

				releaseFreezeRotationY (player.gameObject);
				releaseFreezeRotationY (monster.gameObject);

				// change flag
				isBattleStart = false;
				isBattleTurn = true;
			}
		}

		if (Input.GetMouseButton (0)) {
			DrawRope ();
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		print ("hoge");
		print (other.gameObject.tag);
	}
	
	void DrawRope() {
		Vector3 pos = Input.mousePosition;
		pos.z = 25;
//		player.transform.position = playerCamera.ScreenToWorldPoint(pos);
		Vector3 mousePosition = playerCamera.ScreenToWorldPoint(pos);
		player.ropeBox.transform.position = mousePosition;
//		print ("Camera   pos -> (x:"+mousePosition.x+", y:"+mousePosition.y+", z:"+mousePosition.z+")");
//		print ("drawRope pos -> (x:"+Input.mousePosition.x+", y:"+Input.mousePosition.y+", z:"+Input.mousePosition.z+")");

		// drawRopePositionListは後にロープの長さの計算を行うため作成している
		// 線を描画したいだけであれば直接、LineRendererに座標を格納するので良い
		// ただし、余計な描画は避ける為に、最後と同じ位置であれば座標を格納しないようにしている
		if (drawRopePositionList.Count == 0 || (mousePosition != drawRopePositionList.Last())) {
			drawRopePositionList.Add(mousePosition);
		}
		if (drawRopePositionList.Count > 0) {
			// 座標の格納可能数を更新する
//			lineRenderer.SetVertexCount(drawRopePositionList.Count);

			// 座標を格納する 
			// SetPosition(index, Vector3)
			// 既にindexに座標が格納されている場合、更新される
			// これを利用して毎フレーム毎に伸び縮みする線の描画等も可能
//			lineRenderer.SetPosition(drawRopePositionList.Count - 1, mousePosition);
		}
	}
	
	
	void changeCmera(Camera camera) {
		MainCamera.enabled = false;
		SubCamera.enabled = false;
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

	public void replayGame() {
		Application.LoadLevel("GameScene");
	}
}
