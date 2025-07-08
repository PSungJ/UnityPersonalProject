using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameInstance;

    public Text score;
    public Text textMaxScore;
    public Text textCurScore;
    public GameObject gameOverUI;
    public bool isGameOver = false;
    private int curScore = 0;
    private int maxScore = 0;
    [Header("Audio")]
    private AudioSource source;
    public AudioClip BgmClip;
    public AudioClip DieClip;

    void Awake()
    {
        if (gameInstance == null)
            gameInstance = this;
        else if (gameInstance != this)
        {
            Debug.LogWarning("씬에 두개 이상의 게임매니저가 존재합니다.");
            Destroy(gameObject);
        }
        source = GetComponent<AudioSource>();
        curScore = 0;
        score.text = curScore.ToString();
        gameOverUI.SetActive(false);
        source.clip = BgmClip;
        source.Play();
        source.loop = true;
    }
    public void GameOver()
    {
        source.loop = false;
        source.Stop();
        source.clip = DieClip;
        source.Play();
        StartCoroutine(ShowGameOver());
    }
    IEnumerator ShowGameOver()
    {
        isGameOver = true;
        yield return new WaitForSeconds(0.3f);
        gameOverUI.SetActive(true);
        if (curScore > maxScore)
        {
            maxScore = curScore;
        }
        textMaxScore.text = $"<color=#00FF00>Best Score : {maxScore.ToString()}</color>";
        textCurScore.text = $"<color=#000000>Score : {curScore.ToString()}</color>";
    }
    public void AddScore()
    {
        curScore++;
        score.text = curScore.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameOver = false;
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("종료");
    }
}
