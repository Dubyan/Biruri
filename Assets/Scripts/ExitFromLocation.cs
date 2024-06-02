using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public int NumberOfLvlToLoad;
    private ProgressController ProgControl;


    void Start()
    {
        ProgControl = GameObject.FindGameObjectWithTag("progresscontroller").GetComponent<ProgressController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TransitionEffect());
        }
    }

    IEnumerator TransitionEffect()
    {
        // Затемнение экрана
        float duration = 1f; // длительность затемнения
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            // Производим затемнение экрана
            // Например, изменяем прозрачность черного изображения на экране
            // Или используем другие способы затемнения экрана
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        ProgControl.SetProgress(NumberOfLvlToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}