using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    public Image[] heartImages;
    public Image[] kitiCatImages;
    public TextMeshProUGUI[] tasks;
    private PlayerMovement playerMovement;
    public GameObject canvasObject;
    public PlayerMovement Biruri;
    private bool opened;
    int currentKitiCat;
    int currentHealth;
    public Image exit;
    public string sceneToLoad;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < playerMovement.GetCurrentHealth();
            kitiCatImages[i].enabled = false;
        }
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i].enabled = (i == playerMovement.GetCurrentTask());
        }
        canvasObject.SetActive(false);
        opened = false;
        currentKitiCat = playerMovement.GetCurrentKitiCat();
        currentHealth = playerMovement.GetCurrentHealth();
    }

    void Update()
    {
        HealthBar();
        KitiCatBar();
        OpenClose();
        UseKitiCat();
        UseExit();
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i].enabled = (i == playerMovement.GetCurrentTask());
        }
    }

    void HealthBar()
    {
        currentHealth = playerMovement.GetCurrentHealth();
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentHealth;
        }
    }

    void KitiCatBar()
    {
        currentKitiCat = playerMovement.GetCurrentKitiCat();
        if (opened)
        {
            for (int i = 0; i < kitiCatImages.Length; i++)
            {
                kitiCatImages[i].enabled = i < currentKitiCat;
            }
        }
    }

    void OpenClose()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (opened)
            {
                canvasObject.SetActive(false);
                opened = false;
            }
            else
            {
                canvasObject.SetActive(true);
                opened = true;
            }
        }
    }

    void UseKitiCat()
    {
        currentKitiCat = playerMovement.GetCurrentKitiCat();
        if (opened)
        {
            foreach (Image image in kitiCatImages)
            {
                Button button = image.GetComponent<Button>();
                if (button != null)
                {

                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() =>
                    {
                        if (currentHealth < 3 && currentKitiCat > 0)
                        {
                            Debug.Log("Player touched");
                            playerMovement.Heal();
                            currentKitiCat -= 1;
                        }
                    });
                }
            }
        }
    }

    void UseExit()
    {
        Button button = exit.GetComponent<Button>();
        if (button != null)
        {

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(sceneToLoad);
            });
        }
    }

}