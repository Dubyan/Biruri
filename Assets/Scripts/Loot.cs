using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public GameObject spikes;
    public GameObject objectToDisappear;
    public GameObject objectToAppear;
    public Siren siren;

    void Start()
    {
        if(spikes != null) spikes.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (CompareTag("Food"))
            {
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeKitiCat();
                    Destroy(gameObject);
                }
            }
            else if (CompareTag("jBooster"))
            {
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    if (siren != null) siren.StartSiren();
                    player.SetDoubleJump(true);
                    Destroy(gameObject);
                    if(spikes != null) spikes.SetActive(true);
                    SwitchObjects();
                }
            }
            else if (CompareTag("sBooster"))
            {
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.speed = 4.5f;
                    Destroy(gameObject);
                }
            }
        }
    }
    void SwitchObjects()
    {
        if (objectToDisappear != null && objectToAppear != null)
        {
            objectToDisappear.SetActive(false);
            objectToAppear.SetActive(true);
            Invoke("ResetSwitching", 1.0f); // Reset switching after 1 second
        }
    }

}