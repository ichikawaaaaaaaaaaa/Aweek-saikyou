using UnityEngine;
using System.Collections;
using TMPro;

public class www : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(akeome());
    }

    // Update is called once per frame
    void Update()
    {
       


    }

    IEnumerator akeome()
    {
        yield return new WaitForSeconds(2.0f);

        //もじがうきでてくる
        messageText.gameObject.SetActive(true);
        messageText.text = "あけおめ！";

        // フェードイン演出
        Color c = messageText.color;
        c.a = 0;
        messageText.color = c;

        float duration = 2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / duration);
            messageText.color = c;
            yield return null;
        }


    }
}
