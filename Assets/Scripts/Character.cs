using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    protected float health;
    protected float speed;

    protected int dmg;
    protected int exp;
    protected int expLvl;
    protected Attack[] attackList;

    //health attributes
    public Image healthBar;
    private float healthAmount;
    protected float healRate = 5.0f;
    protected float nextHeal = 0.0f;

    /* public float speed;

     private int dmg;
     private int exp;
     private int expLvl;
     private Attack[] attackList;*/

    //onMove() fields
    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 6f;
    public float gravity = 15;
    public float lookSpeed = 2f;
    public float lookXLimit = 45f;
    Vector3 moveDirection = Vector3.zero;
    public float rotationX = 0;
    public bool canMove = true;
    public float lastDash = -10f;


    public Character(Camera playerCam, int health, float speed, int dmg, int exp, int expLvl, Attack[] aList)
    {
        this.health = health;
        this.speed = speed;
        this.dmg = dmg;
        this.exp = exp;
        this.expLvl = expLvl;
        this.attackList = aList;
    }

    public float Health { get { return health; } set { health = value; } }
    public float Speed { get { return speed; } set { speed = value; } }
    public int Dmg { get { return dmg; } set { dmg = value; } }
    public int Exp { get { return exp; } set { exp = value; } }
    public int ExpLvl { get { return expLvl; } set { expLvl = value; } }
    public Attack[] AttackList { get { return attackList; } set { attackList = value; } }
    public Camera PlayerCamera { get { return playerCamera; } set { playerCamera = value; } }
    
    CharacterController characterController;

    public void onAttack()
    {
    }
    public void onMove()
    {
        #region Handles Movment
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.KeypadMinus);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);


        #endregion

        #region Handles Jumping
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        #endregion

        #region Handles Rotation
        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        #endregion

        #region Handles Dash
        float dashSpeed = 20f;
        float dashTime = 0.25f;
        float dashCD = 1f;
        if (Input.GetKeyDown(KeyCode.LeftShift) && canMove && Time.time - lastDash >= dashCD)
        {
            lastDash = Time.time;
            StartCoroutine(DashMove());
        }

        IEnumerator DashMove()
        {
            float startTime = Time.time;

            while (Time.time < startTime + dashTime)
            {
                characterController.Move(forward * dashSpeed * Time.deltaTime);

                yield return null;
            }
        }
        #endregion
    }
    
    public void takeDamage(float damage)
    {
        health -= damage;
        healthBar.fillAmount = health/ 100;
    }
    public void heal(float heal)
    {
        if (health < 100)
        {
            health += heal;
            health = Mathf.Clamp(health, 0, 100);
        }

        healthBar.fillAmount = health / 100;
    }
    public void GameIsOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        health = 100;
        healRate = 5f;
    }

    void Update()
    {


       if (health < 100 && Time.time > nextHeal) //passive healing
        {
            nextHeal = Time.time + healRate;
            heal(20);
        }


        if (Input.GetKeyUp(KeyCode.G)) //testing health
        {
            takeDamage(20);
        }

        if (health <= 0)
        {
            GameIsOver();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    
}