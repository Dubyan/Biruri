using UnityEngine;

public class AutoBridge : MonoBehaviour
{
    private bool started;
    public GameObject objectToMove;
    public float moveDistance = 1.0f;
    public float moveSpeed = 1.0f; // �������� �������� �������
    private bool inTrigger = false;
    private bool isMoving = false;
    private bool isGoingUp = false; // ����, ������������ ����������� ��������
    private Vector3 targetPosition;
    public AudioSource Sound;

    void Start()
    {
        started = false;
    }

    void Update()
    {
        // ���� ���� ��� ��������
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
                isGoingUp = !isGoingUp; // ������ ����������� ��������
                started = true;
            }
        }

        // �������������� � ������ (������� "F")
        if (inTrigger && Input.GetKeyDown(KeyCode.F) && !isMoving)
        {
            // �������� �������� �����, ���� ���� ��� �� ��������
            isGoingUp = true;
            targetPosition = objectToMove.transform.position + Vector3.up * moveDistance;
            isMoving = true;
        }

        // ���� ���� �� �������� � ��� ��� �����������
        if (!isMoving && started)
        {
            // ���������� ����������� ��������
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