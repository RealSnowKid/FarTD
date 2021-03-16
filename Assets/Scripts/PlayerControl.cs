using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    //player speed
    private float speed;

    //speed variables
    [Tooltip("The walking speed is the normal speed. While the sprint speed is this x1.5 and the crouched speed is this /2")]
    public float walkingSpeed = 5f;
    private float runningSpeed;
    private float crouchedSpeed;

    public float jumpStrength = 5f;

    //player height
    private float height;

    //crouch variables
    public float crouchHeight = 1f;
    private float originalHeight;

    private bool isCrouched = false;
    private bool isSprinting = false;

    public float mouseSensitivity = 1;
    public float mouseSmoothing = 2;

    CapsuleCollider playerCapColl;

    Vector2 velocity;

    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;

    [SerializeField] private GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerCapColl = GetComponent<CapsuleCollider>();
        originalHeight = GetComponent<CapsuleCollider>().height;
        height = GetComponent<Collider>().bounds.extents.y;
        speed = walkingSpeed;
        runningSpeed = walkingSpeed * 1.5f;
        crouchedSpeed = walkingSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Look();
        Crouch();
        Sprint();
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, height + 0.1f);
    }

    void Move()
    {
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded() && !isCrouched)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 100 * jumpStrength);
        }
    }

    void Look()
    {
        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * mouseSensitivity * mouseSmoothing);

        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / mouseSmoothing);
        currentMouseLook += appliedMouseDelta;
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

        //rotate cam and controller
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
    }

    void Crouch()
    {
        if (Input.GetButtonDown("Crouch") && isGrounded() && !isSprinting)
        {
            playerCapColl.height = crouchHeight;
            speed = crouchedSpeed;
            isCrouched = true;
        }
        else if (Input.GetButtonUp("Crouch") && isGrounded() && !isSprinting)
        {
            playerCapColl.height = originalHeight;
            speed = walkingSpeed;
            isCrouched = false;
        }
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint") && isGrounded() && !isCrouched)
        {
            speed = runningSpeed;
            isSprinting = true;
        }
        else if (Input.GetButtonUp("Sprint") && isGrounded() && !isCrouched)
        {
            speed = walkingSpeed;
            isSprinting = false;
        }
    }
}
