using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public Animator fader;

    public void LoadScene(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
        Time.timeScale = 1f;
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex > SceneManager.sceneCount)
        {
            nextSceneIndex = 2;
        }

        StartCoroutine(Finish());

        IEnumerator Finish()
        {
            fader.Play("FadeIn");

            yield return new WaitForSeconds(2f);

            SceneManager.LoadScene(nextSceneIndex);
            Time.timeScale = 1f;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
