using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMarker : MonoBehaviour
{
    private GameObject[] beeObjects;
    public AudioSource Sound;

    private void Start()
    {

        beeObjects = GameObject.FindGameObjectsWithTag("Bee");
        foreach (GameObject beeObject in beeObjects)
        {
            beeObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            Sound.Play();
            foreach (GameObject beeObject in beeObjects)
            {
                beeObject.SetActive(true);

            }
        }
    }
    public void StopMusic()
    {
        Sound.Stop();
    }
}