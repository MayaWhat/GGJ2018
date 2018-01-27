using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    Collider2D collisions;

    [SerializeField]
    Collider2D floorCollisions;

    [SerializeField]
    float horizontalAcceleration;

    [SerializeField]
    float horizontalMaxSpeed;

    [SerializeField]
    float jumpVelocityBoost;

    [SerializeField]
    float damageVelocityBoost;

    [SerializeField]
    int iFrames;

    [SerializeField]
    int hp;

    int currentIFrames = 0;

    bool canJump = true;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void HandleHorizontalMovement()
    {
        var inputX = Input.GetAxis("Horizontal");
        if (inputX > 0)
        {
            var maxVelocityIncrease = Mathf.Max(horizontalMaxSpeed - body.velocity.x, 0);
            body.AddForce(new Vector2(maxVelocityIncrease, 0), ForceMode2D.Impulse);
        }
        else if (inputX < 0)
        {
            var maxVelocityIncrease = Mathf.Max(horizontalMaxSpeed - -body.velocity.x, 0);
            body.AddForce(new Vector2(-maxVelocityIncrease, 0), ForceMode2D.Impulse);
        }
        else if(body.velocity.x != 0)
        {
            body.AddForce(new Vector2(-body.velocity.x, 0), ForceMode2D.Impulse);
        }
    }

    void HandleJumping()
    {
        var inputJump = Input.GetAxis("Jump"); 

        if(!canJump) {
            if(inputJump == 0 && TestPlatformBelow()) {
                canJump = true;
            }
            else {
                return;
            }
        }

        if (inputJump != 0)
        {
            if (TestPlatformBelow())
            {
                body.AddForce(new Vector2(0, jumpVelocityBoost), ForceMode2D.Impulse);
                canJump = false;
            }
        }     
    }

    bool TestPlatformBelow()
    {
        return floorCollisions.GetContacts(new ContactPoint2D[1]) > 0;
    }

    void FixedUpdate()
    {
        HandleHorizontalMovement();
        HandleJumping();

        if(currentIFrames > 0) {
            currentIFrames--;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(currentIFrames == 0) {
		    var spike = other.GetComponent(typeof(SpikeScript));
            if(spike != null && body.velocity.y < 0) {
                currentIFrames = iFrames;
                hp--;
                body.AddForce(new Vector2(0, damageVelocityBoost), ForceMode2D.Impulse);
            }
        }
	}
}
