using UnityEngine;

public class DoorStarter : MonoBehaviour
{
    public GameObject objectToDisappear;
    public GameObject objectToAppear;
    private bool inTrigger = false;
    private bool isSwitching = false;
    public AudioSource Sound;
    public GameObject destination;
    public PlayerMovementMiniGame LabCat;
    public Camera mainCamera;
    public Camera miniGameCamera;
    public GameObject canvasObject;
    public PlayerMovement Player;

    void Start()
    {
        objectToAppear.SetActive(false);
        canvasObject.SetActive(false);
        miniGameCamera.enabled = false;
    }

    void Update()
    {
        if (SetReached())
        {
            SwitchObjects();
        }
        StartGame();
    }

    public bool SetReached()
    {
        if (LabCat != null)
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

    void StartGame()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.F))
        {
            Player.enabled = false;
            canvasObject.SetActive(true);
            mainCamera.enabled = false;
            miniGameCamera.enabled = true;
        }
    }

    void SwitchObjects()
    {
        Sound.Play();
        objectToDisappear.SetActive(false);
        objectToAppear.SetActive(true);
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