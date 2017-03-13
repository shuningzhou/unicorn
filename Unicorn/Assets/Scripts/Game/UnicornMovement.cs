using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornMovement : MonoBehaviour {

    Animator anim;
    Rigidbody rb;
    int jumpHash = Animator.StringToHash("Jump");
    float move = 0.0f;
    int walkStateHash = Animator.StringToHash("Base Layer.Walk");
    int runStateHash = Animator.StringToHash("Base Layer.Run");
    int jumpStateHash = Animator.StringToHash("Base Layer.Jump");
    float runSecounds = 0.0f;
    float unicornAcc = 1.0f;
    float maxBonusSpeedRate = 1.0f;
    float normalTopSpeed = 4.0f;
    float maxBonusSpeed = 0;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        maxBonusSpeed = maxBonusSpeedRate * normalTopSpeed;
    }

    void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update () {
        move = Input.GetAxis("Vertical");

        if (move == 1)
        {
            runSecounds = runSecounds + Time.deltaTime;
        }
        else
        {
            runSecounds = 0;
        }

        anim.SetFloat("Speed", move);
        float bonusSpeed = (runSecounds / unicornAcc);
        float walkSpeed = 0.25f * normalTopSpeed;
        float runSpeed = move * 0.75f * normalTopSpeed;
        
        if (bonusSpeed > maxBonusSpeed)
        {
            bonusSpeed = maxBonusSpeed;
        }

        float currentSpeed = walkSpeed + runSpeed + bonusSpeed;

        anim.SetFloat("RunSpeed", 1 + bonusSpeed / normalTopSpeed);

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + currentSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.fullPathHash != jumpStateHash )
            {
                anim.SetTrigger(jumpHash);
                Vector3 jumpSpeed = new Vector3(0, 6f, currentSpeed);
                rb.velocity = rb.velocity + jumpSpeed;
            }
        }
    }
}
