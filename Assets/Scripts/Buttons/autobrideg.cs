using UnityEngine;

public class AutoBridge : MonoBehaviour
{
    private bool started;
    public GameObject objectToMove;
    public float moveDistance = 1.0f;
    public float moveSpeed = 1.0f; // Скорость движения объекта
    private bool inTrigger = false;
    private bool isMoving = false;
    private bool isGoingUp = false; // Флаг, определяющий направление движения
    private Vector3 targetPosition;
    public AudioSource Sound;

    void Start()
    {
        started = false;
    }

    void Update()
    {
        // Если мост уже движется
        if (isMoving)
        {
            if (!Sound.isPlaying)
            {
                Sound.Play();
            }
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (objectToMove.transform.position == targetPosition)
            {
                isMoving = false;
                isGoingUp = !isGoingUp; // Меняем направление движения
                started = true;
            }
        }

        // Взаимодействие с мостом (нажатие "F")
        if (inTrigger && Input.GetKeyDown(KeyCode.F) && !isMoving)
        {
            // Включаем движение вверх, если мост еще не двигался
            isGoingUp = true;
            targetPosition = objectToMove.transform.position + Vector3.up * moveDistance;
            isMoving = true;
        }

        // Если мост не движется и уже был активирован
        if (!isMoving && started)
        {
            // Определяем направление движения
            if (isGoingUp)
            {
                targetPosition = objectToMove.transform.position + Vector3.up * moveDistance;
            }
            else
            {
                targetPosition = objectToMove.transform.position - Vector3.up * moveDistance;
            }
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