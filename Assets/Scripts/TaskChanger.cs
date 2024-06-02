using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskChanger : MonoBehaviour
{
    public PlayerMovement Player;
    public int SetTask;
    // Start is called before the first frame update
    void Start()
    {
        Player.SetTask(0);
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Player.SetTask(SetTask);
        }
    }
}
