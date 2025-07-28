using Unity.XR.Oculus.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float restartDelay = 2f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    AudioSource thisAudioPlayer;

    // Simple State Implementation
    bool isControllable;

    void Start()
    {
        isControllable = true;
        thisAudioPlayer = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (isControllable)
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
        else
        {
            return;
        }

    }

    void StartWinSequence()
    {
        DisablePlayer();
        thisAudioPlayer.Stop(); // Stop all current playing audio
        thisAudioPlayer.PlayOneShot(successSFX);
        successParticles.Play();

        Invoke("LoadNextLevel", restartDelay);
    }

    void StartCrashSequence()
    {
        DisablePlayer();
        thisAudioPlayer.Stop(); // Stop all current playing audio
        thisAudioPlayer.PlayOneShot(crashSFX);
        crashParticles.Play();

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
        isControllable = false;
        // Get the Movement Script compoinent and set it to diabled
        GetComponent<Movement>().enabled = false;
    }

}
