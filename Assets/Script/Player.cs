using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public string name = "homura";
	public float  turmPower = 3000;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		print("col_enter:"+col.gameObject.tag); 

		if (col.gameObject.tag.Equals("monster")) {
			print ("モンスターです");
			col.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(100,5000,1500));
		}

	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		print ("hit");
		if(hit.gameObject.tag == "Enemy"){
			print("hit");
		}
		
		//物体を押す処理（Enamy2タグのついてるオブジェクトは押せる）
		if(hit.gameObject.tag == "Enemy2"){
			Rigidbody body = hit.collider.attachedRigidbody;
			
			if(body == null | body.isKinematic){return;}    //rigidBodyがない、もしくは物理演算の影響を受けない設定をされている 
			if(hit.moveDirection.y<-0.3){return;}            //押す力が弱い 
			
			Vector3 pushDir = new Vector3(hit.moveDirection.x,0,hit.moveDirection.z);    //y成分を０に 
			
			float pushPower = 2.0f;
			body.velocity = pushDir * pushPower;    //押す力を加える 
		}
		
	}
}
