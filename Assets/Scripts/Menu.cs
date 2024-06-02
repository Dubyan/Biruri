using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    private string[] NameOfLvls = { "LVL1", "LVL2", "LVL3", "LVL4" };
    public Image[] lvl;
    public Image[] closed;
    public Image[] buttons;
    public GameObject[] LVLCanvas;
    public TextMeshProUGUI[] numbers;
    private ProgressController ProgControl;
    private int windows;


    void Start()
    {
        ProgControl = GameObject.FindGameObjectWithTag("progresscontroller").GetComponent<ProgressController>();
        LVLCanvas[0].SetActive(true);
        LVLCanvas[1].SetActive(true);
        LVLCanvas[2].SetActive(false);
        LVLCanvas[3].SetActive(false);
    }


    void Update()
    {
        SelectLVL();
        OpenLVLs();
        ChangeWindow();
    }
    
    void ChangeWindow()
    {
        Button buttonleft = buttons[1].GetComponent<Button>();
        Button buttonright = buttons[0].GetComponent<Button>();
        if (buttonleft != null)
        {
            buttonleft.onClick.RemoveAllListeners();
            buttonleft.onClick.AddListener(() =>
            {
                LVLCanvas[0].SetActive(true);
                LVLCanvas[1].SetActive(true);
                LVLCanvas[2].SetActive(false);
                LVLCanvas[3].SetActive(false);
            });
        }
        if (buttonright != null)
        {
            buttonright.onClick.RemoveAllListeners();
            buttonright.onClick.AddListener(() =>
            {
                LVLCanvas[2].SetActive(true);
                LVLCanvas[3].SetActive(true);
                LVLCanvas[0].SetActive(false);
                LVLCanvas[1].SetActive(false);
            });
        }
    }


    void OpenLVLs()
    {
        foreach(Image image in closed)
        {
            image.enabled = !(LvlIsOpened(System.Array.IndexOf(closed, image)+1));
        }
        foreach (TextMeshProUGUI text in numbers)
        {
            text.enabled = !(LvlIsOpened(System.Array.IndexOf(numbers, text)+2));
        }
    }

    bool LvlIsOpened(int lvl)
    {
        return !(lvl > ProgControl.GetProgress());
    }

    void SelectLVL()
    {
        foreach (Image image in lvl)
        {

            Button button = image.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if(LvlIsOpened(System.Array.IndexOf(lvl, image) + 1)) SceneManager.LoadScene(NameOfLvls[System.Array.IndexOf(lvl, image)]);
                });
            }
        }
    }

}
