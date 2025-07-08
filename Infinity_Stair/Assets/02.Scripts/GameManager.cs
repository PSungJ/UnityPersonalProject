using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameInstance;
    
    private const string HIGHSCORE_KEY = "HighScore";   // PlayerPrefs�� �ְ������� ����

    public UIScript hpBar;
    public Text scoreText;
    public Text textMaxScore;
    public Text textCurScore;
    public GameObject gameOverUI;
    public GameObject upButton;
    public GameObject turnButton;
    public bool isGameOver = false;
    private int curScore = 0;
    [Header("Audio")]
    private AudioSource sound;
    public AudioClip BgmClip;

    void Start()
    {
        if (gameInstance == null)
            gameInstance = this;
        else if (gameInstance != this)
        {
            Destroy(gameObject);
        }
        Initialized();
    }

    private void Initialized()
    {
        sound = GetComponent<AudioSource>();
        curScore = 0;
        if (textCurScore != null)
            scoreText.text = curScore.ToString();
        LoadHighScore();
        gameOverUI.SetActive(false);
        upButton.SetActive(true);
        turnButton.SetActive(true);
        sound.PlayOneShot(BgmClip);
        sound.loop = true;
        sound.volume = 0.5f;
    }

    public void GameOver()
    {
        Die();
        SaveGame();
        DisplayScores();
    }

    public void Die()
    {
        sound.loop = false;
        sound.Stop();
        upButton.SetActive(false);
        turnButton.SetActive(false);
        hpBar.hpSlider.value = hpBar.curHp;
    }

    private void SaveGame()
    {
        int savedHighScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);  // ������ ����� �ְ����� �ҷ�����
        if (curScore > savedHighScore)  // ���� ������ ������ ����� �ְ��������� ���ٸ� ���������� ����
        {
            PlayerPrefs.SetInt(HIGHSCORE_KEY, curScore);
            PlayerPrefs.Save();
        }
    }

    private void DisplayScores()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
        if (textCurScore != null)
        {
            textCurScore.text = $"<color=#000000>Score : {curScore.ToString()}</color>";
        }
        if (textMaxScore != null)
        {
            int finalHighScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
            textMaxScore.text = $"<color=#00FF00>Best Score : {finalHighScore.ToString()}</color>";
        }
    }
    private void LoadHighScore()
    {
        int loadedHighScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);         // �ʱ⿡ �ε�Ǵ� �ְ�����
        if (textMaxScore != null)
        {
            textMaxScore.text = "�ְ� ����: " + loadedHighScore.ToString();
        }
    }
    public void AddScore()
    {
        if (isGameOver)
            return;
        curScore++;
        scoreText.text = curScore.ToString();
        hpBar.RecoverHp();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        isGameOver = false;
    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("����");
    }
}
