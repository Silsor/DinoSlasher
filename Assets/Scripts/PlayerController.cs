using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    //Timer time = new Timer();
    public Action actionMoveStart;
    public Action actionMoveEnd;

    private void Awake()
    {
        playerControls = new PlayerControls(); 
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Start()
    {
        transform.position =  FindObjectOfType<MapGenerator>().spawn;
    }

    private void Update()
    {
        PlayerInput();
        if(Vector3.Distance(FindObjectOfType<MapGenerator>().spawn,transform.position) < 1.00f || Vector3.Distance(FindObjectOfType<MapGenerator>().spawn, transform.position) > 17.00f)
        {
            actionMoveEnd?.Invoke();
        }
        else actionMoveStart?.Invoke(); 
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerInput() { 
    movement = playerControls.Movement.Move.ReadValue<Vector2>();

        //Debug.Log(movement.x);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
        Debug.Log("position: " + rb.position + ", dystans :" + Vector3.Distance(FindObjectOfType<MapGenerator>().spawn, transform.position) + "czas : " + GetComponent<Timer>().ShowTime());

        //if (rb.position.x < 1)
        //{
        //    time
        //        }
    }


}
