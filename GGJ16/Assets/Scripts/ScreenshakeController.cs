using UnityEngine;
using System.Collections;

public class ScreenshakeController : MonoBehaviour {

	private Vector3 cameraStartPosition;
	private float shakeAmountX;
	private float shakeAmountY;
	private float time;
	bool reset=true;

	public void Start () {
		cameraStartPosition=Camera.main.gameObject.transform.position;
		InvokeRepeating("CameraShake", 0, 0.01f);
	}

	public void ScreenShake(float amountX, float amountY, float _time){
		if(amountX>shakeAmountX){
			shakeAmountX = amountX;
		}

		if(amountY>shakeAmountY){
			shakeAmountY = amountY;
		}
		if(_time>time){
			time=_time;
		}
		reset=false;    
	}

	public void ScreenShake(float amount, float _time){
		if(amount>shakeAmountX && amount>shakeAmountY){
			shakeAmountX = amount;
			shakeAmountY = amount;
		}
		if(_time>time){
			time=_time;
		}
		reset=false;
	}

	void CameraShake(){
		if(shakeAmountX>0 && shakeAmountY>0 && time>0) {
			time-=.01f;
			float quakeAmtX = UnityEngine.Random.value*Mathf.Sin( shakeAmountX)*2 - shakeAmountX;
			float quakeAmtY = UnityEngine.Random.value*Mathf.Sin(shakeAmountY)*2 - shakeAmountY;
			Vector3 pp = cameraStartPosition;
			pp.y+= quakeAmtY; // can also add to x and/or z
			pp.x+= quakeAmtX;
			Camera.main.transform.position = pp;
		}else if(!reset){
			Camera.main.transform.position=cameraStartPosition;
			reset=true;
		}
	}
}
