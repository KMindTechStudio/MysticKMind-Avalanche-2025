using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;

    [Header("Persitent Objects")]
    public GameObject[] persistentObjects;

    public LocationHistoryTracker LocationHistoryTracker { get; private set; }
    public DialogueHistoryTracker DialogueHistoryTracker { get; private set; }


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }

        LocationHistoryTracker = new LocationHistoryTracker();
        DialogueHistoryTracker = new DialogueHistoryTracker();
    }

    private void MarkPersistentObjects()
    {
        foreach(GameObject obj in persistentObjects)
        {
            if(obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }
    }

    private void CleanUpAndDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj); 
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fadeAnim.Play("FadeToWhite");
            StartCoroutine(DelayFade());
        }
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
