using UnityEngine;

public class FlappyCatStarter : MonoBehaviour
{
    public FlappyBird flappyBird;
    public Camera miniGameCamera;
    public Camera mainCamera;
    public GameObject spikes;

    private Rigidbody2D rb;

    void Start()
    {
        spikes.SetActive(false);
        rb = flappyBird.GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Disable Rigidbody at start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartMiniGame();
        }
    }

    void StartMiniGame()
    {
        rb.isKinematic = false; // Enable Rigidbody
        flappyBird.enabled = true;
        mainCamera.enabled = false;
        miniGameCamera.enabled = true;
        spikes.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartMiniGame();
        }
    }
}