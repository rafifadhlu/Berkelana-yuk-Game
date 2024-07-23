using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Variable of player
    //using Method animator from unity
    public Animator playerAnim;
    //using Method rigidbody from unity
	public Rigidbody playerRigid;
	public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
	public bool walking;
    //using Method transform from unity
	public Transform playerTrans;
	
	
	void FixedUpdate(){
		
		if(Input.GetKey(KeyCode.W)){
			playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
		}
		if(Input.GetKey(KeyCode.S)){
			playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
		}

		if(Input.GetKey(KeyCode.A)){
			playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
		}
		if(Input.GetKey(KeyCode.D)){

			playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
		}
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.W)){
			playerAnim.SetTrigger("Jog Forward");
			playerAnim.ResetTrigger("Standing");
			walking = true;
			//steps1.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.W)){
			playerAnim.ResetTrigger("Jog Forward");
			playerAnim.SetTrigger("Standing");
			walking = false;
			//steps1.SetActive(false);
		}
		if(Input.GetKeyDown(KeyCode.S)){
			playerAnim.SetTrigger("Jog Backward");
			playerAnim.ResetTrigger("Standing");
			//steps1.SetActive(true);
		}
		if(Input.GetKeyUp(KeyCode.S)){
			playerAnim.ResetTrigger("Jog Backward");
			playerAnim.SetTrigger("Standing");
			//steps1.SetActive(false);
		}
		if(walking == true){				
			if(Input.GetKeyDown(KeyCode.LeftShift)){
				//steps1.SetActive(false);
				//steps2.SetActive(true);
				w_speed = w_speed + rn_speed;
				playerAnim.SetTrigger("Running");
				playerAnim.ResetTrigger("Jog Forward");
			}
			if(Input.GetKeyUp(KeyCode.LeftShift)){
				//steps1.SetActive(true);
				//steps2.SetActive(false);
				w_speed = olw_speed;
				playerAnim.ResetTrigger("Running");
				playerAnim.SetTrigger("Jog Forward");
			}
		}
		
	}
}
