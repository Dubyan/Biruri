using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class VideoPlayerController : MonoBehaviour
{
    public GameObject canvas;
    public VideoPlayer videoPlayer;
    public string sceneToLoad;

    void Start()
    {
        canvas.SetActive(false);
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Play();
            canvas.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            videoPlayer.Stop();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}