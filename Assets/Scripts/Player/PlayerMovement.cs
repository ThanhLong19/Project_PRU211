using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody2D rb;

    public Joystick joystick;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<Joystick>();
    }

    private void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float moveHorizontal = joystick.GetJoystickPosition().x;
        float moveVertical = joystick.GetJoystickPosition().y;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        rb.velocity = movement * moveSpeed;
    }
}
