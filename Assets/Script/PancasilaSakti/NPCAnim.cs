using System.Collections;
using System.Collections.Generic;
using Sunbox.Avatars;
using UnityEngine;

public class NPCAnim : MonoBehaviour
{
    public AvatarCustomization _avatarAction;
    
    public void startTalk(){
        _avatarAction.Animator.SetTrigger("Talking");
    }

    public void idle(){
        _avatarAction.Animator.SetTrigger("Idle");
    }
}
