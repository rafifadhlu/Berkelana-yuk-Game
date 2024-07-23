using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Sunbox.Avatars;

public class TPlayerMove : MonoBehaviour
{
    
    [Header("Player Attribute")]
    public CharacterController controller;
    public float speed;
    public float wSpeed;
    public float runSpeed;
    public AvatarCustomization playerAnim;
    public bool isWalking;
    // public Rigidbody rb;

    float gravity = 10f;
    float FallSpeed = 1f;  

    
    [Header("Joystick Player")]
     public FixedJoystick FdJoystick;  

    [Header("Player Camera")]
    public Transform cam;
    
    float horizontal;
    float vertical;
    float z;
    bool Grounded;
    public AudioManagerTP audioManager;
    

    void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerTP>();
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = FdJoystick.Horizontal;     
        vertical = FdJoystick.Vertical;  
    }

    void FixedUpdate(){
        GravityAdjustment();
        moveCharacter();  
    }


    private void moveCharacter(){

         Vector3 direction = new Vector3(horizontal,0f,vertical);
        //  Debug.Log(direction.magnitude);
        
        float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0f,targetAngle,0f);

        bool isMoving = direction.magnitude >= 0.1f;

         //Kondisi untuk melihat apakah karakter sedang jalan(Walking), berlari(Running), atau sedang diam(Idle)
         if(isMoving && direction.magnitude < 0.5f){
            Debug.Log("walking");
            speed = wSpeed;
            
            if (!isWalking) {
            audioManager.PlayWalkEffect(audioManager.CHARACTERWALK);
            audioManager.StopRunEffect();
            isWalking = true;
            }


            playerAnim.Animator.SetBool("Walking", true);
            playerAnim.Animator.SetBool("Running",false);
			playerAnim.Animator.SetBool("Idle",false);
			isWalking = true;
        }else if(isMoving && direction.magnitude >= 0.5f){
            Debug.Log("running"); 
            speed = runSpeed;  
     
            if (isWalking) {
                audioManager.StopWalkEffect();
                audioManager.PlayRunEffect(audioManager.CHARACTERRUN);
                isWalking = false;
            }

            playerAnim.Animator.SetBool("Walking",false); 
            playerAnim.Animator.SetBool("Idle",false);
            playerAnim.Animator.SetBool("Running",true);
            
        }else{
            speed = 0f; 
            audioManager.StopWalkEffect();
            audioManager.StopRunEffect();

            playerAnim.Animator.SetBool("Running",false);
            playerAnim.Animator.SetBool("Walking",false);
			playerAnim.Animator.SetBool("Idle",true);

            
			isWalking = false;
        }
            //Mengambil arah dari targetAngle yang sudah di dapat lalu buat karakter maju
            Vector3 moveDir = Quaternion.Euler(0f,targetAngle,0f) * Vector3.forward;
            controller.Move(moveDir.normalized *speed*Time.deltaTime);

    } 


    public void GravityAdjustment(){
        Grounded = controller.isGrounded;

         if(Grounded){
            z = Mathf.Clamp(z, -0.1f, Mathf.Infinity);
         }else{
            z = Mathf.Clamp(z - gravity * Time.deltaTime, -FallSpeed, Mathf.Infinity);
         }

         Vector3 gravityDir = new Vector3(0f,z,0f).normalized;
         controller.Move(gravityDir);
    }



}
