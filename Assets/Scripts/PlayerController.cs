using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    private int desiredlane = 1;  //0:left 1:mid 2:rigth
    public float laneDistance = 4; 
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float jumpForce;
    public float Gravity = -20;


    void Start()
    {
        controller = GetComponent<CharacterController>();
         //Time.timeScale = 1.2f;
    }
  /*  private void FixedUpdate()
    {
        if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
            return;
            controller.Move(direction*Time.deltaTime);
    }
*/
       private void FixedUpdate()
    {
        if(!PlayerManager.isGameStarted)
        return;
        controller.Move(direction*Time.deltaTime);
    }
    void Update()
    {
        if (!PlayerManager.isGameStarted || PlayerManager.gameOver)
            return;
        direction.z = forwardSpeed;

        
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundLayer);
        Debug.Log(isGrounded+ "asd");
        if(controller.isGrounded)
        {
           // direction.y = -2;
            if(SwipeManager.swipeUp)
            {
                jump();
            }
        }
        else
        {
            direction.y += Gravity*Time.deltaTime;
        }

        if(SwipeManager.swipeRight)
        {
            desiredlane++;
            if(desiredlane == 3)
            desiredlane = 2;
        }
       if(SwipeManager.swipeLeft)
        {
            desiredlane--;
            if(desiredlane == -1)
            desiredlane = 0;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredlane == 0)
        {
            targetPosition += Vector3.left * laneDistance;

        }
        else if (desiredlane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        if (transform.position == targetPosition)
        
            return;
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 *Time.deltaTime;
            if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
            else
            controller.Move(diff);
        
    }




    private void jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
        }
    }
    




}
