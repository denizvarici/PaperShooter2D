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


    //Shoot System
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletWayTransform;
    [SerializeField] private float bulletSpeed;
    

    


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
        shootInput.performed += Shoot;
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
    }
   
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        LookMouse();
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

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Shoot(InputAction.CallbackContext context)
    {
        GameObject bulletObject = Instantiate(bulletPrefab, bulletWayTransform.position, Quaternion.identity);
        bulletObject.GetComponent<Rigidbody2D>().AddForce(bulletWayTransform.right * bulletSpeed,ForceMode2D.Impulse);
        gunAnimator.SetTrigger("Shoot");
    }
    #endregion
}
