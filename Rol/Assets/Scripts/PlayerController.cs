using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float gravity = -9.81f;
    public bool isPlayableChara = false;
    public float IconHeight = 2.0f;

    [Header("Referencias")]
    public int CharacterID;
    public GameObject Gamemanager;
    public Transform cameraTransform; // Arrastra aqu� tu c�mara fija
    private CharacterController controller;
    private Animator animator;
    private Vector3 velocity;
    private Vector3 initPosition;
    private Quaternion initRotation;
    public LayerMask interactableLayer;          // Capa de objetos interactivos
    public SphereCollider InteractCollider;

    [HideInInspector]
    public GameObject InteractChara;
    public bool OnDialog = false;





    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        initPosition = GetComponent<Transform>().position;
        initRotation = GetComponent<Transform>().rotation;

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        if (isPlayableChara)
        {
            if (!OnDialog)
            {
                HandleMovement();
            }

            HandleInteraction();
        }

    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Direcci�n de entrada
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (inputDirection.magnitude >= 0.1f)
        {
            // Orienta el movimiento respecto a la c�mara
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;
            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDirection = camForward * vertical + camRight * horizontal;

            // Movimiento
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            animator.SetBool("Moving", true);

            // Rotaci�n del jugador en direcci�n al movimiento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        // Gravedad
        if (!controller.isGrounded)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleInteraction()
    {

        if (Input.GetKeyDown(KeyCode.Space) && InteractChara != null)
        {
            Interact();
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !OnDialog)
        {
            Gamemanager.GetComponent<Gamemanagerbehaviour>().OpenMenuFromScene();
        }

    }

    public void Interact()
    {
        if (!OnDialog)
        {
            Gamemanager.GetComponent<Gamemanagerbehaviour>().OpenDialog(CharacterID, InteractChara.GetComponent<PlayerController>().CharacterID);
            animator.SetBool("Moving", false);
            OnDialog = true;
        }
        else
        {
            Gamemanager.GetComponent<Gamemanagerbehaviour>().ChangeDialog();
        }
    }


    public void ResetPosition()
    {
        transform.position = initPosition;
        transform.rotation = initRotation;
    }


    void OnTriggerEnter(Collider col)
    {
        if (isPlayableChara)
        {
            if (col.gameObject.layer == 6 && col.gameObject != this.gameObject && col.gameObject != InteractChara)
            {
                InteractChara = col.gameObject;
                col.gameObject.GetComponent<AudioSource>().Play();
                Gamemanager.GetComponent<Gamemanagerbehaviour>().SetIconOnChara(InteractChara);
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (isPlayableChara)
        {
            if (col.gameObject == InteractChara)
            {
                InteractChara = null;
                Gamemanager.GetComponent<Gamemanagerbehaviour>().HideIconOnChara();
            }
        }
    }
}
