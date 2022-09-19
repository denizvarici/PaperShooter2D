using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//https:.//www.youtube.com/watch?v=wZUPCSuqklM
//https:.//www.youtube.com/watch?v=UsGuN69g2NI&t=184s
public class PlayerManager : MonoBehaviour
{
    //Input System
    InputManager inputManager;
    InputAction moveInput;
    InputAction shootInput;


    //Player Components
    private Rigidbody2D playerRigidbody;
    private Animator gunAnimator;

    //Player features
    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerNormalSpeed;
    [SerializeField] private float playerSlowSpeed;
    [SerializeField] private float playerFastSpeed;

    //SHOOOOOOTTTTT
    //Shoot System
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletWayTransform;
    [SerializeField] private float bulletSpeed;

    //Hold to shoot System
    [SerializeField] private GameObject longBulletPrefab;
    [SerializeField] private float longBulletSpeed;
    [SerializeField] private float holdTimer;
    [SerializeField] private float holdBaseTimer;

    //WeaponSystem
    [SerializeField] private GameObject[] weapons;
    private int currentWeapon;


    //PLAYER ANGLE
    [HideInInspector]
    public float playerAngle;

    //COÝN SYSTEM

    [SerializeField]
    private int coinAmount;





    private void Awake()
    {
        inputManager = new InputManager();
    }
    private void OnEnable()
    {
        moveInput = inputManager.Player.Move;
        moveInput.Enable();
        shootInput = inputManager.Player.Shoot;
        shootInput.Enable();
        

    }
    private void OnDisable()
    {
        moveInput.Disable();
        shootInput.Disable();

    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gunAnimator = GetComponentInChildren<Animator>();
        holdTimer = holdBaseTimer;
        ChangeWeapon();
    }

    void Update()
    {
        SwitchWeapon();
        PlayerShooting();
        LookMouse();
    }

    private void FixedUpdate()
    {
        Move();
        
    }
    bool holdMode = false;
    bool oneMode = false;

    void PlayerShooting()
    {
        
        if (shootInput.WasPressedThisFrame())
        {

            if (currentWeapon == 0)
            {
                holdMode = false;
                oneMode = true;
                playerSpeed = 1f;
                GameObject bulletObject = Instantiate(bulletPrefab, bulletWayTransform.position, Quaternion.identity);
                bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletWayTransform.right * bulletSpeed, ForceMode2D.Impulse);

            }
        }
        if (shootInput.IsPressed())
        {
            
            if (currentWeapon == 1)
            {
                holdMode = true;
                oneMode = false;
                Debug.Log("Ateþ ediliyor.");
                playerSpeed = playerSlowSpeed;
                holdTimer -= Time.deltaTime;
                if (holdTimer <= 0)
                {

                    GameObject bulletObject = Instantiate(longBulletPrefab, bulletWayTransform.position, Quaternion.Euler(0, 0, playerAngle));
                    bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletWayTransform.right * longBulletSpeed, ForceMode2D.Impulse);
                    holdTimer = holdBaseTimer;

                }
            }
            //if (currentWeapon == 0)
            //{
            //    holdMode = false;
            //    oneMode = true;
            //    playerSpeed = 1f;
            //    GameObject bulletObject = Instantiate(bulletPrefab, bulletWayTransform.position, Quaternion.identity);
            //    bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletWayTransform.right * bulletSpeed, ForceMode2D.Impulse);

            //}
        }
        if (shootInput.WasReleasedThisFrame())
        {
            if (holdMode)
            {
                
                playerSpeed = playerNormalSpeed;
                holdMode = false;
            }
            if (oneMode)
            {

                playerSpeed = playerNormalSpeed;
                oneMode = false;
            }
            
        }



    }

    #region Move,LookMouse,Shoot
    void Move()
    {
        Vector2 direction = moveInput.ReadValue<Vector2>();
        playerRigidbody.velocity = direction * playerSpeed;
    }



    void LookMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 objectPosition = Camera.main.WorldToScreenPoint(transform.position);

        mousePosition.x -= objectPosition.x;
        mousePosition.y -= objectPosition.y;

        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        playerAngle = angle;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    #endregion

    void SwitchWeapon()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{            
        //    ChangeWeapon(0);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{            
        //    ChangeWeapon(1);
        //}
        if (playerRigidbody.velocity != Vector2.zero)
        {
            ChangeWeapon(1);
        }
        else
        {
            ChangeWeapon(0);
        }
    }

    void ChangeWeapon(int currentWeaponIndex = 0)
    {
        currentWeapon = currentWeaponIndex;
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == currentWeapon)
            {
                weapons[i].SetActive(true);
            }
            else
            {
                weapons[i].SetActive(false);
            }
        }
    }

    public void AddCoin(int coinNumberToAdd)
    {
        coinAmount += coinNumberToAdd;
        Debug.Log("Current Coin:" + coinAmount);
    }
}