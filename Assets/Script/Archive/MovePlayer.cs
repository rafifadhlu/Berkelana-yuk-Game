using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;

    public Transform orientation;

    public float groundDrag;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool isGrounded;

    float horizontal;
    float vertical;

    Vector3 moveDirection;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight*0.5f+0.2f,whatIsGround);
        inputKey();

        if(isGrounded) { rb.drag = groundDrag;}else{  rb.drag = 0;};
    }

    void FixedUpdate(){
        MoveCharacter();
    }

    private void inputKey(){
        // wKeyInput = Input.GetKey(KeyCode.W);
        // sKeyInput = Input.GetKey(KeyCode.S);

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

    }

    private void MoveCharacter(){
	
    
        moveDirection = orientation.forward * vertical + orientation.right * horizontal;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    

    
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;
// using System.Linq;

// public class TryGetDataTXT 
// {
//     // the structure of data dialogue is 
//     // String name and Array of group sentence
//     // Start is called before the first frame update

//        List<string> temporarySentence = new List<string>();

//         string nameCharacterDialog1 = "";
//         string nameCharacterDialog2 = "";

//         string[] sentencesDialog1 = {};
//         string[] sentencesDialog2 = {};
//     void Start()
//     {

//         string readFromTxtFile = Application.dataPath + "/Content/" + "D-Npc-Ebi.txt";
//         string[] sentences = File.ReadAllLines(readFromTxtFile).ToArray();
//         string temporaryText = "";

//         foreach (string line in sentences)
//         {
//             if (line.StartsWith("1+"))
//             {
//                 temporaryText = line.Substring(2).Trim() + "\n"; // Remove "1+" and trim any leading spaces
//                 nameCharacterDialog1 = temporaryText;
//                 temporaryText = "";
//             }
//             else if (line.StartsWith("2+"))
//             {
//                 temporaryText = line.Substring(2).Trim() + "\n"; // Remove "2+" and trim any leading spaces
//                 nameCharacterDialog2 = temporaryText;
//                 temporaryText = "";
//             }
//             else if (line.StartsWith("1-"))
//             {
//                 temporaryText = line.Substring(2).Trim() + "\n"; // Remove "1-" and trim any leading spaces
//                 temporarySentence.Add(temporaryText);
//                 temporaryText = "";
//                 sentencesDialog1 = temporarySentence.ToArray();
//             }
//             else if (line.StartsWith("2-"))
//             {
//                 temporaryText += line.Substring(2).Trim() + "\n"; // Remove "2-" and trim any leading spaces
                
//                 temporarySentence.Add(temporaryText);
//                 temporaryText = "";
//                 sentencesDialog2 = temporarySentence.ToArray();
//             }
            
//         }

//         foreach(string line in sentences){
//             Debug.Log(line);
//         }

//     void Update()
//     {
        
//     }
// }



        
		