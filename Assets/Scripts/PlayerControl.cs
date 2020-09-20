using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class PlayerControl : MonoBehaviour
{
    // walk left and right
    private const float walkSpeed = 8.0f;
    private const float accelerateTime = 0.09f;
    private const float decelerateTime = 0.09f;
    private float velocityX = 0;

    // jump
    [Header("jump")] public float jumpingSpeed;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    private bool isJumping;
    private bool isOnGround;
    private bool gravitySwitch;

    // isGround
    [Header("isGround")] public Vector2 pointOffset;
    public Vector2 size;
    public LayerMask groundLayerMask;

    private Rigidbody2D rig;

    // Start is called before the first frame update
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isOnGround = OnGround();

        // check left and right
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            var vectorX = Mathf.SmoothDamp(rig.velocity.x, walkSpeed * Time.fixedDeltaTime * 60, ref velocityX,
                accelerateTime);
            rig.velocity = new Vector2(vectorX, rig.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            var vectorX = Mathf.SmoothDamp(rig.velocity.x, walkSpeed * Time.fixedDeltaTime * 60 * -1, ref velocityX,
                accelerateTime);
            rig.velocity = new Vector2(vectorX, rig.velocity.y);
        }
        else
        {
            var vectorX = Mathf.SmoothDamp(rig.velocity.x, 0, ref velocityX, decelerateTime);
            rig.velocity = new Vector2(vectorX, rig.velocity.y);
        }

        // check jump
        if ((int) Input.GetAxis("Jump") == 1 && !isJumping)
        {
            rig.velocity = new Vector2(rig.velocity.x, jumpingSpeed);
            isJumping = true;
        }

        if (isOnGround && Input.GetAxis("Jump") == 0)
        {
            isJumping = false;
        }

        if (rig.velocity.y < 0) // when player is falling down
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime; // speed up to fall down
        }
        else if (rig.velocity.y > 0 && (int)Input.GetAxis("Jump") != 1) // when play is moving up without pressing the jump key
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime; // slow down to move up
        }
    }

    bool OnGround()
    {
        var coll =
            Physics2D.OverlapBox((Vector2) transform.position + pointOffset, size, 0, groundLayerMask);
        if (coll != null)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((Vector2) transform.position + pointOffset, size);
    }
}