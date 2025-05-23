using System.Collections;
using UnityEngine;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public static CountdownManager Instance;

    [SerializeField] private TextMeshProUGUI countdownText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (countdownText != null)
            countdownText.gameObject.SetActive(false);
    }

    public void ShowCountdown(int seconds)
    {
        StartCoroutine(CountdownCoroutine(seconds));
    }

    private IEnumerator CountdownCoroutine(int seconds)
    {
        countdownText.gameObject.SetActive(true);

        for (int i = seconds; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1f); // uses unscaled time
        }

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(1f);

        countdownText.gameObject.SetActive(false);
    }
}
