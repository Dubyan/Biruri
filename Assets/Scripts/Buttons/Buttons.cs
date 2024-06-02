using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public GameObject objectToMove;
    public float moveDistance = 1.0f;
    public float moveSpeed = 1.0f; // Скорость движения объекта
    private bool inTrigger = false;
    private bool isMoving = false;
    private int isMoved = 0;
    private Vector3 targetPosition;
    public AudioSource Sound;

    void Update()
    {
        if (isMoving)
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (objectToMove.transform.position == targetPosition)
            {
                isMoving = false;
                isMoved += 1;
                if (Sound.isPlaying) Sound.Stop();
            }
        }

        if (inTrigger && Input.GetKeyDown(KeyCode.F) && !isMoving&& isMoved % 2 == 0)
        {
            targetPosition = objectToMove.transform.position - Vector3.up * moveDistance;
            isMoving = true;
            if (!Sound.isPlaying) Sound.Play();
        }
        if(inTrigger && isMoved%2==1 && Input.GetKeyDown(KeyCode.F) && !isMoving)
        {
            if (!Sound.isPlaying) Sound.Play();
            targetPosition = objectToMove.transform.position + Vector3.up * moveDistance;
            isMoving = true;
        }
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