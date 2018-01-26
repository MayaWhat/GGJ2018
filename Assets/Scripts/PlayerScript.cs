using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    [SerializeField]
    Rigidbody2D body;

    [SerializeField]
    Collider2D collisions;

    [SerializeField]
    float horizontalAcceleration;

    [SerializeField]
    float horizontalMaxSpeed;

    [SerializeField]
    float jumpVelocityBoost;

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
        return collisions.Raycast(Vector2.down, new RaycastHit2D[1], collisions.bounds.extents.y + 0.1f) > 0;
    }

    void FixedUpdate()
    {
        HandleHorizontalMovement();
        HandleJumping();
    }
}
