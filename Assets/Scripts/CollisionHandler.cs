using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    AudioSource thisAudioPlayer;
    [SerializeField] float restartDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    void Start()
    {
        thisAudioPlayer = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                StartWinSequence();
                break;
            case "Friendly":
                Debug.Log("We pals");
                break;
            default:
                StartCrashSequence();
                break;
        }

    }

    void StartWinSequence()
    {
        DisablePlayer();
        thisAudioPlayer.PlayOneShot(successSFX);

        Invoke("LoadNextLevel", restartDelay);
    }

    void StartCrashSequence()
    {
        DisablePlayer();
        thisAudioPlayer.PlayOneShot(crashSFX);

        // Invoke() helps us call methods after a delay
        Invoke("ReloadLevel", restartDelay);
    }

    void LoadNextLevel()
    {

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = sceneIndex + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
            Debug.Log("Scene does not exist");

        }
        else
        {
            SceneManager.LoadScene(nextScene);

        }
    }

    void ReloadLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(sceneIndex);
    }

    
    void DisablePlayer()
    {
        // Get the Movement Script compoinent and set it to diabled
        GetComponent<Movement>().enabled = false;
    }

}
