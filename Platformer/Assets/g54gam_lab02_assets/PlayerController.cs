using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isIdle;
    bool isLeft;
    int isIdleKey = Animator.StringToHash("isIdle");
    public int speed;
    public int jumpHeight;
    bool canJump = true;
    int groundMask = 1<<8;
    int lavaMask = 1<<9;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Animator a = GetComponent<Animator>();
        a.SetBool(isIdleKey, isIdle);

        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.flipX = isLeft;
    }

    void FixedUpdate(){
        //the new velocity to apply to the character
        isIdle = true;
        Vector2 physicsVelocity = Vector2.zero;
        Rigidbody2D r = GetComponent<Rigidbody2D>();

        //move to the left
        if (Input.GetKey(KeyCode.A)){
            isIdle = false;
            isLeft = true;
            physicsVelocity.x -= speed;
        }

        // implement moving to the right for the D Key
        if (Input.GetKey(KeyCode.D)){
            isIdle = false;
            isLeft = false;
            physicsVelocity.x += speed;
        }

        //jump, but only once
        if (Input.GetKey(KeyCode.W)){
            if(canJump){
                r.velocity = new Vector2(physicsVelocity.x, jumpHeight);
                canJump = false;
            }
        }

        //test ground below player
        // if tagged as Ground layer, allow the player to jump again
        if (Physics2D.Raycast(new Vector2(
                                        transform.position.x,
                                        transform.position.y),
                                        -Vector2.up, 1.0f, groundMask))
        {
            canJump = true;
        }

        //apply updated velocity to the rigid body
        r.velocity = new Vector2(physicsVelocity.x, r.velocity.y);


        //check if the player is on lava and should die
        if (Physics2D.Raycast(new Vector2(
                                        transform.position.x,
                                        transform.position.y),
                                        -Vector2.up, 1.0f, lavaMask))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }
}
