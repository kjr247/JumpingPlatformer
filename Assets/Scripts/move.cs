using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    public float jumpHeight = 0.01F; /* defaulted from unity at another value */
    public float moveSpeed = 0.01F; /* defaulted from unity at another value */
    private Rigidbody2D rb2d;
    public float jumpLagCount = 0.0F;
    public float jumpLagStat = 0.25F;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if( Input.GetKeyDown(KeyCode.Space) && jumpLagCount <= 0.0F)
        {
            rb2d.velocity = new Vector2(0, jumpHeight + Time.deltaTime);
            jumpLagCount = jumpLagStat;
        } else {
            jumpLagCount -= Time.deltaTime;
        }

        //Store the current horizontal/vertical inputs in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2 (moveHorizontal, moveVertical);

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce (movement * (moveSpeed + Time.deltaTime));
    }

    float smooth = 5.0f;
    float tiltAngle = 60.0f;

    void Update()
    {
        // Smoothly tilts a transform towards a target rotation.
        float tiltAroundZ = Input.GetAxis("Horizontal") * tiltAngle;
        float tiltAroundX = Input.GetAxis("Vertical") * tiltAngle;

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * smooth);
    }
}
