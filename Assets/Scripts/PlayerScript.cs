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
    float spikeVelocityBoost;

    [SerializeField]
    float damageVelocityBoost;

    [SerializeField]
    int iFrames;

    [SerializeField]
    int stunnedFrames;

    [SerializeField]
    int hp;

    int currentIFrames = 0;

    int currentStunnedFrames = 0;

    bool canJump = true;

    bool canToggle = true;

    Animator animator;
    SpriteRenderer sprite;

    [SerializeField]
    AudioSource fluteVoice;

    [SerializeField]
    AudioSource footstepSound;

    [SerializeField]
    AudioClip[] ouchClips;

    [SerializeField]
    AudioClip fallClip;

    [SerializeField]
    AudioClip[] footstepClips;

    public bool WeDied {
        get
        {
            return hp <= 0;
        }
    }


	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentIFrames > 0) {
            sprite.color = new Color(1f, currentStunnedFrames > 0 ? 0.5f : 0f, 0f, 0.5f);
        }
        else {
            sprite.color = new Color(1f, currentStunnedFrames > 0 ? 0.5f : 0f, 0f, 1f);
        }
	}

    void HandleHorizontalMovement()
    {
        var inputX = Input.GetAxis("Horizontal");
        var isWalking = inputX != 0 && TestPlatformBelow();

        animator.SetBool("IsWalking", isWalking);
        if(isWalking) {

        }

        if (inputX > 0)
        {
            var maxVelocityIncrease = Mathf.Max(horizontalMaxSpeed - body.velocity.x, 0);
            body.AddForce(new Vector2(maxVelocityIncrease, 0), ForceMode2D.Impulse);
            sprite.flipX = false;
        }
        else if (inputX < 0)
        {
            var maxVelocityIncrease = Mathf.Max(horizontalMaxSpeed - -body.velocity.x, 0);
            body.AddForce(new Vector2(-maxVelocityIncrease, 0), ForceMode2D.Impulse);
            sprite.flipX = true;
        }
        else if(body.velocity.x != 0)
        {
            body.AddForce(new Vector2(-body.velocity.x, 0), ForceMode2D.Impulse);
        }
    }

    void Footstep() {
        footstepSound.clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepSound.Play();
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

    void HandleToggle() {
		var inputToggle = Input.GetAxis("ToggleState"); 

        if(!canToggle) {
            if(inputToggle == 0) {
                canToggle = true;
            }
            else {
                return;
            }
        }

        if (inputToggle != 0)
        {
            GameMasterScript.TheMaster.CurrentState = (GameMasterScript.TheMaster.CurrentState + 1) % 2;
			canToggle = false;
        }  
	}

    bool TestPlatformBelow()
    {
        return floorCollisions.GetContacts(new ContactPoint2D[1]) > 0;
    }

    void FixedUpdate()
    {
        if(WeDied) {
            return;
        }

        if(transform.position.y < GameMasterScript.TheMaster.LowerLevelBound) {
            TakeDamage(999, false);
        }

        if(currentStunnedFrames <= 0) {
            HandleHorizontalMovement();
            HandleJumping();
            HandleToggle();
        }
        else {
            currentStunnedFrames--;
        }

        if(currentIFrames > 0) {
            currentIFrames--;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(WeDied) {
            return;
        }

        OnHitSpike(other);
        OnHitDamage(other);
        OnDealDamage(other);
	}

    void TakeDamage(int damage, bool stun) {
        if(currentIFrames == 0 || damage > 900) {
            hp -= damage;

            if(damage > 900) {
                fluteVoice.clip = fallClip;
            }
            else {
                fluteVoice.clip = ouchClips[Random.Range(0, ouchClips.Length)];
            }

            fluteVoice.Play();

            if(WeDied) {
                //body.simulated = false;
                GameMasterScript.TheMaster.WeDied();
            }
            else {
                currentIFrames = iFrames;
                if(stun) {
                    currentStunnedFrames = stunnedFrames;
                }

            }
        }
    }

    void OnHitSpike(Collider2D other) {
        var spike = other.GetComponent<SpikeScript>();
        if(spike != null && ((spike.Inverted && body.velocity.y > 0) || (!spike.Inverted && body.velocity.y < 0))) {
            TakeDamage(10, true);

            var percentOfMax = Mathf.Clamp(Mathf.Abs(body.velocity.y) / Physics2D.gravity.magnitude, 0f, 1f);
            var velocityIncrease = (spikeVelocityBoost * percentOfMax) + Mathf.Abs(body.velocity.y);
            
            body.AddForce(new Vector2(0, velocityIncrease * (spike.Inverted ? -1 : 1)), ForceMode2D.Impulse);
        }  
    }

    void OnHitDamage(Collider2D other) {
        var damageDealer = other.GetComponent<DamageDealerScript>();
        if(damageDealer != null && currentIFrames <= 0) {
            TakeDamage(damageDealer.DamageAmount, true);
            var velocityChange = (((transform.position - damageDealer.transform.position).normalized) * damageVelocityBoost) - new Vector3(body.velocity.x, body.velocity.y, 0f);
            body.AddForce(velocityChange, ForceMode2D.Impulse);
        }
    }

    void OnDealDamage(Collider2D other) {
        var damageReceiver = other.GetComponent<DamageReceiverScript>();
        if(damageReceiver != null) {
            damageReceiver.ReceiveDamage();
            var velocityChange = (((transform.position - damageReceiver.transform.position).normalized) * damageVelocityBoost) - new Vector3(body.velocity.x, body.velocity.y, 0f);
            body.AddForce(velocityChange, ForceMode2D.Impulse);
        }
    }
}
