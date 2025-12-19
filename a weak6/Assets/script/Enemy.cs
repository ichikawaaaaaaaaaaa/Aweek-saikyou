using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 



public class Enemy : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private TextMeshProUGUI tkoText1;
    [SerializeField] private TextMeshProUGUI tkoText2;
    [SerializeField] private TextMeshProUGUI tkoText3;

    [SerializeField] private AudioSource audioSource;   // AudioSourceをInspectorで割り当て
    [SerializeField] private AudioClip tkoVoice;        // 再生するボイス
    [SerializeField] private AudioClip tVoice;
    [SerializeField] private AudioClip kVoice;
    [SerializeField] private AudioClip oVoice;
    [SerializeField] private AudioClip kouka;

    [SerializeField] private AudioSource bgmSource;　// とめるよう

    [SerializeField] private GameObject itaidesu;
    [SerializeField] private GameObject itaidesu2;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        tkoText1.gameObject.SetActive(false);

    }

    void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.CompareTag("bottle"))

        {
            audioSource.PlayOneShot(kouka);
            Debug.Log("いたい！");
            itaidesu.SetActive(true);
            itaidesu2.SetActive(false); 
            // ボトルを消す
            Destroy(collision.gameObject);



            StartCoroutine(TKO());
           

        }
    }
    IEnumerator TKO()
    {
       
        // BGMの音を消す
        bgmSource.Stop();


        // ボイス入れる
        // ボイス再生
        audioSource.PlayOneShot(tkoVoice);

        yield return new WaitForSeconds(4.0f);

        // TKO表示
        audioSource.PlayOneShot(tVoice);
        tkoText1.gameObject.SetActive(true);
        tkoText1.text = "T";

        yield return new WaitForSeconds(1.0f);

        audioSource.PlayOneShot(kVoice);
        tkoText2.gameObject.SetActive(true);
        tkoText2.text = "K";

        yield return new WaitForSeconds(1.0f);

        audioSource.PlayOneShot(oVoice);
        tkoText3.gameObject.SetActive(true);
        tkoText3.text = "O"; 

        yield return new WaitForSeconds(2.0f);

        // Scene 移動
        SceneManager.LoadScene("Clear");

    }

}
