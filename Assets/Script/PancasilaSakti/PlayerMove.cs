using System.Collections;
using System.Collections.Generic;
using Sunbox.Avatars;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Properti Karakter
    [Header("Player Attribute")]
    public CharacterController controller;
    public float speed;
    public float wSpeed;
    public float runSpeed;
    public AvatarCustomization playerAnim;
    public bool isWalking;

    float gravity = 10f;
    float FallSpeed = 1f;  

    
    // Joystick untuk bergerak sesuai yang diinginkan player
    [Header("Joystick Player")]
     public FixedJoystick FdJoystick;  
    float horizontal;
    float vertical;

    // Camera player untuk mengambil arah player
    [Header("Player Camera")]
    public Transform cam;
    
    // Properti untuk mengatur gravitasi
    float z;
    bool Grounded;

    //properti sound effect player
    public AudioManagerLB audioManager;

    //method awake dipanggil sebelum frame mulai
    void Awake(){
        Application.targetFrameRate = 60;
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManagerLB>();
    }

    //method ini dipanggil setiap frame berjalan e.g 30 fps (frame per second), 
    //maka method ini berjalan 30 kali setiap detik
    void Update()
    {

        horizontal = FdJoystick.Horizontal;     
        vertical = FdJoystick.Vertical;  
    }

    //method fixedUpdate dipanggil lebih dulu daripada update
    //method ini berjalan setiap 0,02 detik. 
    //Sehingga apabila ada perubahan dapat lebih cepat merespon    
    void FixedUpdate(){
        //mengatur gravity disetiap method ini berjalan
        GravityAdjustment();
        //bergerak jika terdapat input dari player
        moveCharacter();  
    }

    // method untuk pergerakan karakter
    // private void moveCharacter(){

    //     //variabel direction untuk menampung arah (X,Y,Z) dari input player
    //     Vector3 direction = new Vector3(horizontal,0f,vertical);

    //     //Mengatur arah fisik karakter di dalam game. 
    //     //targetAngle mengambil informasi dari joystick dan kamera player
    //     // float targetAngle = Mathf.Atan2(direction.x,direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
    //     // transform.rotation = Quaternion.Euler(0f,targetAngle,0f);

    //     //variabel untuk mendeteksi pergerakan karakter 
    //     bool isMoving = direction.magnitude >= 0.1f;

    //     //Kondisi untuk melihat apakah karakter sedang jalan(Walking), berlari(Running), atau sedang diam(Idle)
    //      if(isMoving && direction.magnitude < 0.5f){
    //         Debug.Log("walking");
    //         speed = wSpeed;
            
    //         if (!isWalking) {
    //         audioManager.PlayWalkEffect(audioManager.CHARACTERWALK);
    //         audioManager.StopRunEffect();
    //         isWalking = true;
    //         }


    //         playerAnim.Animator.SetBool("Walking", true);
    //         playerAnim.Animator.SetBool("Running",false);
	// 		playerAnim.Animator.SetBool("Idle",false);
	// 		isWalking = true;
    //     }else if(isMoving && direction.magnitude >= 0.5f){
    //         Debug.Log("running"); 
    //         speed = runSpeed;  
     
    //         if (isWalking) {
    //             audioManager.StopWalkEffect();
    //             audioManager.PlayRunEffect(audioManager.CHARACTERRUN);
    //             isWalking = false;
    //         }

    //         playerAnim.Animator.SetBool("Walking",false); 
    //         playerAnim.Animator.SetBool("Idle",false);
    //         playerAnim.Animator.SetBool("Running",true);
            
    //     }else{
    //         speed = 0f; 
    //         audioManager.StopWalkEffect();
    //         audioManager.StopRunEffect();

    //         playerAnim.Animator.SetBool("Running",false);
    //         playerAnim.Animator.SetBool("Walking",false);
	// 		playerAnim.Animator.SetBool("Idle",true);

            
	// 		isWalking = false;
    //     }
    //         // Calculate movement direction
    //     Vector3 moveDir = direction.normalized;

    //     // Rotate the character to face the movement direction
    //     if (isMoving)
    //     {
            
    //         float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    //         transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
    //     }

    //     // Move the character
    //     controller.Move(moveDir * speed * Time.deltaTime);
    // } 
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


    //method untuk mengecek apakah karakter sedang menapak tanah(object ground) atau tidak
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
