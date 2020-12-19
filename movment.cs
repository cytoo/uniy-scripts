using UnityEngine;

public class movment : MonoBehaviour
{
    public Rigidbody rb;
    public AudioSource WalkSound;
    private const int maxDoubleJump = 2;
    public int currentJump = 0;
    public float gravity = -10.28f;
    public float jumpSpeed = -10f;
    public float speed = 10f;
    CharacterController chrctrl;
    Vector3 velocity;
    public Transform ori;
    public Transform Camera;
    public LayerMask whatIsWall;
    public float wallRunForce, maxWalltime, maxWallspeed;
    bool isWallRight,isWallLeft;
    bool isWallRunning;
    public float maxWallRunCameratilt, WallRunCameraTilt;
    delegate void de();
    bool playerMoving = false;
    void Start()
    {
        chrctrl = GetComponent<CharacterController>();
    }


    void wallRunInput()
    {
        if(Input.GetKeyDown(KeyCode.A) && isWallLeft) startWallRun();
        if(Input.GetKeyDown(KeyCode.D) && isWallRight) startWallRun();
    }
    void startWallRun()
    {
        gravity = 0;
        isWallRunning = true;
        if(rb.velocity.magnitude <= maxWallspeed)
        {
            rb.AddForce(ori.forward * wallRunForce * Time.deltaTime);
            if(isWallRight) rb.AddForce(ori.right * wallRunForce / 5 * Time.deltaTime);
            else rb.AddForce(-ori.right * wallRunForce / 5 * Time.deltaTime);
        }
    }
    void stopWallRunning()
    {
        gravity = -16.215f;
        isWallRunning = false;
    }
    void checkForWall()
    {
        isWallRight = Physics.Raycast(transform.position, ori.right, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -ori.right, 1f, whatIsWall);
        if(!isWallRight && !isWallRight)stopWallRunning();
    }

    void FixedUpdate()
    {
        playerMoving = false;
        if(chrctrl.isGrounded)currentJump = 0;
        checkForWall();
        if((chrctrl.isGrounded || currentJump < maxDoubleJump) && Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        Vector3 move = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;
        de moveChar = () => 
        {
        playerMoving = true;
        chrctrl.Move(move * speed * Time.deltaTime);
        chrctrl.Move(velocity * Time.deltaTime);
        };
        if(playerMoving)WalkSound.Play();
        moveChar();
        if(chrctrl.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if(Mathf.Abs(WallRunCameraTilt) < maxWallRunCameratilt && isWallRunning && isWallRight)
        {
            WallRunCameraTilt += Time.deltaTime * maxWallRunCameratilt * 2;
        }
        if(Mathf.Abs(WallRunCameraTilt) < maxWallRunCameratilt && isWallRunning && isWallLeft)
        {
            WallRunCameraTilt -= Time.deltaTime * maxWallRunCameratilt * 2;
        }
    } 
    private void jump()
    {
        velocity.y = Mathf.Sqrt(jumpSpeed * -2 - gravity);
        ++currentJump;
    }
}