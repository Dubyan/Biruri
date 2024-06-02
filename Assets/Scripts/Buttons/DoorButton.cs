using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject objectToDisappear;
    public GameObject objectToAppear;
    private bool inTrigger = false;
    private bool isSwitching = false;
    public AudioSource Sound;


    void Start()
    {
        objectToAppear.SetActive(false);
    }

    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.F) && !isSwitching)
        {
            SwitchObjects();
        }
    }

    void SwitchObjects()
    {
        Sound.Play();
        objectToDisappear.SetActive(false);
        objectToAppear.SetActive(true);
        isSwitching = true;
        Invoke("ResetSwitching", 1.0f); // Reset switching after 1 second
    }

    void ResetSwitching()
    {
        isSwitching = false;
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