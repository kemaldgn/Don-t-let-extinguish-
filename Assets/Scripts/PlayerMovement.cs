using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;

    public Joystick movementJoystick;
    public Joystick aimingJoystick;
    public float aimX;
    public float aimY;
    private float fireRate =0.6f;
    private float nextFire;

    private Vector2 moveDirection;
    private Vector2 aimDirection;

    private bool isMoving;
    public Animator anim;

    public float x,y;

    public GameObject crosshair;
    public float CROSSHAIR_DISTANCE = 1.0f;

    public bool endOfAiming;  

    public GameObject fireBallPrefab;
    public float FIREBALL_SPEED;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        //transform.position = new Vector3((float)10.5,(float)5.5,-1);
    }

    // Update is called once per frame
     void Update()
    {
        

        Aim();

        Shoot();


        

    }

    void FixedUpdate(){// Physics calculations, called every fixed framerate frame

    ProcessInputs();

    Move();

    }

    void ProcessInputs()
    {

        // moving codes
        x = movementJoystick.Horizontal;
        y = movementJoystick.Vertical;

        if (movementJoystick.Horizontal >= 0.3f)
        {
            x = moveSpeed;
        }
        else if (movementJoystick.Horizontal <= -0.3f)
        {
            x = -moveSpeed;
        }
        else
        {
            x = 0;
        }
        if (movementJoystick.Vertical >= 0.3f)
        {
            y = moveSpeed;

        }
        else if (movementJoystick.Vertical <= -0.3f)
        {
            y = -moveSpeed;
        }
        else
        {
            y = 0;
        }

        // Idle aiming

        aimX = aimingJoystick.Horizontal;
        aimY = aimingJoystick.Vertical;

        if ((x == 0 && y == 0) && (aimX!=0 || aimY !=0))
        {

            anim.SetFloat("X", aimX);
            anim.SetFloat("Y", aimY);
            aimDirection = new Vector2(aimX,aimY).normalized;
            endOfAiming = true;

        }


        if (x != 0 || y != 00)
        {
            anim.SetFloat("X", x);
            anim.SetFloat("Y", y);
            if (!isMoving)
            {
                isMoving = true;
                anim.SetBool("isMoving", isMoving);
            }
            if(aimX !=0 || aimY !=0){
                endOfAiming = true;
            }
        }
        else
        {
            if (isMoving)
            {
                isMoving = false;
                anim.SetBool("isMoving", isMoving);
                stopMoving();
            }

        }

       
        moveDirection = new Vector2(x, y).normalized; // it will normalize the speed.
        

        // if (x != 0 || y != 0){ // while running it will throw fireball
        //     if(aimX != 0 || aimY != 0){
            
        //     }
        // }
        

    }


    void lockPosition(){
        moveSpeed = 0;
        isMoving = false;
        anim.SetBool("isMoving",isMoving);
    }
    void Move(){        
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }


    void stopMoving(){
        rb.velocity = Vector2.zero;
    }

    void Aim(){
        if(aimDirection != Vector2.zero){
            if(aimDirection.y < 5){
                crosshair.transform.localPosition = aimDirection* CROSSHAIR_DISTANCE;                
            }
        crosshair.transform.localPosition = aimDirection* CROSSHAIR_DISTANCE;

        // then throw a fireball
        
        }
        if(moveDirection != Vector2.zero){
            if(moveDirection.y < 5){
                crosshair.transform.localPosition = moveDirection* CROSSHAIR_DISTANCE;
                
            }
        crosshair.transform.localPosition = moveDirection* CROSSHAIR_DISTANCE;
        }
    }

    void Shoot(){
        

        Vector2 shootingDirection = crosshair.transform.localPosition;

        shootingDirection.Normalize();

        if(endOfAiming && Time.time > nextFire){
            
            nextFire = Time.time + fireRate;
            GameObject fireball = Instantiate(fireBallPrefab, transform.position, Quaternion.identity);

            fireball.GetComponent<Rigidbody2D>().velocity = shootingDirection * FIREBALL_SPEED;
            fireball.transform.Rotate(0,0,Mathf.Atan2(shootingDirection.y, shootingDirection.x) * Mathf.Rad2Deg);
            Destroy(fireball,2.0f);
            
        }
        endOfAiming = false;

    }  

    

}
