using UnityEngine;

public class StarterMiniGame : MonoBehaviour
{
    public GameObject objectToMove;
    public float moveDistance = 1.0f;
    public float moveSpeed = 1.0f; // Скорость движения объекта
    private bool inTrigger = false;
    private bool isMoving = false;
    private Vector3 targetPosition;
    public GameObject canvasObject;
    public GameObject destination;
    public PlayerMovementMiniGame LabCat;
    public FlappyBird FlappyCat;
    public Camera mainCamera;
    public Camera miniGameCamera;
    private PlayerMovement Player;
    private Rigidbody2D rb;
    public AudioSource Sound;
    public AudioSource MusicForTheGame;

    void Start()
    {

        if (FlappyCat != null)
        {
            FlappyCat.enabled = false;
            rb = FlappyCat.GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
        }

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>(); ;
        canvasObject.SetActive(false);
        mainCamera = Camera.main;
        mainCamera.enabled = true;
        miniGameCamera = GameObject.Find("MiniGameCamera").GetComponent<Camera>();
        miniGameCamera.enabled = false;
        canvasObject.SetActive(false);
    }

    void Update()
    {
        if(inTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Player.enabled = false;
            canvasObject.SetActive(true);
            mainCamera.enabled = false;
            miniGameCamera.enabled = true;
            FlappyCat.enabled = true;
            if(MusicForTheGame != null) MusicForTheGame.Play();
            rb.isKinematic = false;
        }
        if (isMoving)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (objectToMove.transform.position == targetPosition)
            {
                isMoving = false;
                Sound.Stop();
            }
        }

        if (SetReached() && objectToMove.transform.position != targetPosition && !isMoving)
        {
            targetPosition = objectToMove.transform.position - Vector3.up * moveDistance;
            isMoving = true;
            Sound.Play();
            if (MusicForTheGame != null) MusicForTheGame.Stop();
        }

    }

    public bool SetReached()
    {
        if (FlappyCat != null)
        {
            if (FlappyCat.CheckDestination())
            {
                canvasObject.SetActive(false);
                mainCamera.enabled = true;
                miniGameCamera.enabled = false;
                Player.enabled = true;
                return true;
            }
            else return false;
        }
        if(LabCat != null)
        {
            if (LabCat.CheckDestination())
            {
                canvasObject.SetActive(false);
                mainCamera.enabled = true;
                miniGameCamera.enabled = false;
                Player.enabled = true;
                return true;
            }
            else return false;
        }
        else return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }


}