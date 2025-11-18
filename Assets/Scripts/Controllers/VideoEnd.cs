using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        // Ensure the VideoPlayer is assigned
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("VideoPlayer not assigned to the script!");
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Optional: Unsubscribe from the event when the GameObject is destroyed
    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
