using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    public float speed = 5;
    public float jumpStrength = 5f;

    private float height;

    public float mouseSensitivity = 1;
    public float mouseSmoothing = 2;

    Vector2 velocity;

    Vector2 currentMouseLook;
    Vector2 appliedMouseDelta;

    [SerializeField] private GameObject playerCamera;
    // Start is called before the first frame update
    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        height = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update() {
        Move();
        Look();
    }

    bool isGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, height + 0.1f);
    }

    void Move() {
        velocity.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        velocity.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        transform.Translate(velocity.x, 0, velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded()) {
            GetComponent<Rigidbody>().AddForce(Vector3.up * 100 * jumpStrength);
        }
    }

    void Look() {
        Vector2 smoothMouseDelta = Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * mouseSensitivity * mouseSmoothing);

        appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / mouseSmoothing);
        currentMouseLook += appliedMouseDelta;
        currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

        //rotate cam and controller
        playerCamera.transform.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
        transform.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
    }
}
