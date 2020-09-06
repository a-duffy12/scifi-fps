using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float mouseSensX = 50f; // x sens TODO
    [SerializeField]
    private float mouseSensY = 50f; // y sens TODO
    [SerializeField]
    private float defaultSpeed = 10f; // default movement speed

    private Rigidbody rb; // player's rigidbody
    private float _sensMod = 2f; // sensitivity modifier
    private float _xRot = 0f; // used to control camera rotation
    private float _speed; // final player speed

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // get player's rigidbody
        Cursor.lockState = CursorLockMode.Locked; // lock cursor in center of screen
    }

    // Update is called once per frame
    void Update()
    {
        Look(); // look around
        Move(); // move player
    }

    // function to look around
    void Look()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensX * _sensMod * Time.deltaTime; // get mouse movement along x axis
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensY * _sensMod * Time.deltaTime; // get mouse movement along y axis

        _xRot -= mouseY; // get vertical rotatino
        // for clamp, first is up, second is down
        _xRot = Mathf.Clamp(_xRot, -90f, 90f); // restrict head movement

        transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f); // apply this vertical mouse movement as a rotation to player head
        transform.Rotate(Vector3.up * mouseX); // rotate entire body with lateral mouse movement
    }

    // function to calculate movement direction
    void Move()
    {
        // collect movement and create movement vector 
        float xMove = Input.GetAxisRaw("Horizontal"); // get horizontal movement
        float zMove = Input.GetAxisRaw("Vertical"); // get vertical movement
        Vector3 playerMove = transform.right * xMove + transform.forward * zMove; // add WASD movement to player
    
        // get speed modifier TODO
        _speed = defaultSpeed; // TODO

        // move player
        if (playerMove != Vector3.zero)
        {
            rb.MovePosition(rb.position + (playerMove * _speed * Time.deltaTime)); // apply movement
        }
    }
}
