using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : MonoBehaviour
{
    public AudioSource Sound;
    private bool inTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }



    void Update()
    {
        if(inTrigger && Input.GetKeyDown(KeyCode.F))
        {
            StopSiren();
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

    public void StartSiren()
    {
        Sound.Play();
    }

    public void StopSiren()
    {
        Sound.Stop();
    }
}
