/* Joseph Breslin 2018
* Rigidbody and transform movement script for 2D player */

using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 move;
    private float playerSpeed;  

    //Flag to use Rigidbody movement. Default to true for optimal performance, 
    //If the flag is false this script will update the transform directly.
    public bool isRigidBody = true;

    //Scaler for player Speed
    [Range(0, 10)] 
    public float startSpeed;

    //strings for user input axis    
    public string   xAxis,
                    yAxis;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerSpeed = startSpeed;
    }

    public void TogglePlayerMovement(bool isMoving)
    {
        if (isMoving)
        {
            playerSpeed = startSpeed;
        }
        else
        {
            playerSpeed = 0;
        }
    }

    private void FixedUpdate()
    {
        if (Manager.Instance.gameState == Types.EGameState.FREE)
        {
            //INPUT
            move = new Vector3(Input.GetAxisRaw(xAxis), Input.GetAxisRaw(yAxis));
            move = (move.magnitude > 1.0f) ? move = move.normalized : move;
          
            //MOVEMENT
            if (!isRigidBody)
            {
                transform.position += move * Time.deltaTime * playerSpeed;
            }
            else
            {
                if (!isMoving)
                {
                    rb.velocity = Vector3.zero;
                }
                else
                {
                    rb.velocity = move * playerSpeed;
                }
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}
