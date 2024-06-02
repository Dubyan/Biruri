using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressController : MonoBehaviour
{

    public int progress;
    private int Health;
    private int KitiCat;
    private static ProgressController instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        progress = 1;
        Health = 3;
    }

    void Update()
    {

    }

    public void SetProgress(int a)
    {
        progress = a;
    }

    public int GetProgress()
    {
        return progress;
    }

    public void SetHealth(int a)
    {
        Health = a;
    }

    public int GetHealth()
    {
        return Health;
    }

    public void SetKitiCat(int a)
    {
        KitiCat = a;
    }

    public int GetKitiCat()
    {
        return KitiCat;
    }
}