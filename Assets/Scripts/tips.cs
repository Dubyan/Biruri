using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public GameObject canvasObject;

    void Start()
    {
        canvasObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) // �������� ���������� �������
    {
        if (other.CompareTag("Player")) // ���� ��� �����
        {
            canvasObject.SetActive(true); // �������� ��������
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canvasObject != null)
        {
            canvasObject.SetActive(false);
        }
    }
}