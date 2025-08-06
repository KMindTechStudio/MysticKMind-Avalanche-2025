using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectGameManager : MonoBehaviour
{
    public Animator fadeAnim;
    public float fadeTime = .5f;

    private IEnumerator LoadSceneWithFade(string sceneName)
    {
        fadeAnim.Play("FadeToWhite");
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneName);
    }

    public void OptionsGame()
    {
        // Optional
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMap1()
    {
        StartCoroutine(LoadSceneWithFade("Map 1"));
    }

    public void LoadMap2()
    {
        StartCoroutine(LoadSceneWithFade("Map 2"));
    }
}
