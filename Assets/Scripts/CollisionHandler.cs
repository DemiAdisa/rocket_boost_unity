using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Finish":
                LoadNextLevel();
                break;
            case "Friendly":
                Debug.Log("We pals");
                break;
            default:
                ReloadLevel();
                break;
        }

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

}
