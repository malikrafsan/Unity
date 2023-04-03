using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;

    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        //Access Object Component
        anim = GetComponent<Animator>();

        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //Get Input from User

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Moving(horizontal, vertical);
        Turning();
        Animating(horizontal, vertical);

    }

    private void Moving(float horizontal, float vertical)
    {
        movement.Set(horizontal, 0f, vertical);

        movement = movement.normalized * speed * Time.deltaTime;

        playerRigidBody.MovePosition(transform.position + movement);
    }

    private void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

            playerRigidBody.MoveRotation(newRotation);
        }
    }

    private void Animating(float horizontal, float vertical)
    {
        bool IsMoving = horizontal != 0f || vertical != 0f;
        anim.SetBool("IsMoving", IsMoving);
    }
}
