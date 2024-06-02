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

    private void OnTriggerEnter2D(Collider2D other) // Название встроенной функции
    {
        if (other.CompareTag("Player")) // если это игрок
        {
            canvasObject.SetActive(true); // включить табличку
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