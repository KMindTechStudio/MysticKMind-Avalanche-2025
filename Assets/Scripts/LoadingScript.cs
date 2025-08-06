using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScript : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI tapToContinueText;
    public RectTransform loadingIcon;
    public float rotateSpeed = 200f;

    private float currentProgress = 0f;
    private bool isLoading = true;

    void Start()
    {
        tapToContinueText.gameObject.SetActive(false);
        StartCoroutine(SimulateLoading());
    }

    void Update()
    {
        if (isLoading && loadingIcon != null)
            loadingIcon.Rotate(Vector3.forward, -rotateSpeed * Time.deltaTime);
    }

    IEnumerator SimulateLoading()
    {
        float loadingDuration = 5.5f;
        float targetProgress = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < loadingDuration)
        {
            currentProgress = Mathf.Lerp(0f, targetProgress, elapsedTime / loadingDuration);
            loadingText.text = Mathf.Round(currentProgress * 100f) + "%";
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentProgress = 1f;
        loadingText.text = "100%";
        yield return new WaitForSeconds(0.5f);

        if (loadingIcon != null)
            loadingIcon.gameObject.SetActive(false);

        Destroy(loadingText.gameObject); // Xoá hẳn loading %
        tapToContinueText.gameObject.SetActive(true);
        StartCoroutine(ContinuousZoomText(tapToContinueText.transform));
        isLoading = false;

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        SceneManager.LoadScene("LoginScreen");
    }

    IEnumerator ContinuousZoomText(Transform target)
    {
        Vector3 originalScale = target.localScale;
        Vector3 targetScale = originalScale * 1.2f;
        float zoomDuration = 1f;
        float elapsedTime = 0f;

        while (true)
        {
            if (elapsedTime < zoomDuration)
            {
                target.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / zoomDuration);
                elapsedTime += Time.deltaTime;
            }
            else if (elapsedTime < zoomDuration * 2)
            {
                target.localScale = Vector3.Lerp(targetScale, originalScale, (elapsedTime - zoomDuration) / zoomDuration);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                elapsedTime = 0f;
            }
            yield return null;
        }
    }
}
