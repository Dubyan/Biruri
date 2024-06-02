using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerLoop : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {

        videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.Play();
        videoPlayer.isLooping = true;
    }
}