using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private RectTransform optionBG;
    [SerializeField] private RectTransform optionMenu;
    [SerializeField] private RectTransform soundMenu;
    [SerializeField] private GameObject player;
    [SerializeField] private Toggle toggle;
    public bool isPause = false;

    public void OnClickOptionBtn()
    {
        isPause = true;
        if (!optionBG.gameObject.activeInHierarchy)
        {
            if (!optionMenu.gameObject.activeInHierarchy)
            {
                optionMenu.gameObject.SetActive(true);
                soundMenu.gameObject.SetActive(false);
            }
            optionBG.gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    
    public void OnClickResumeBtn()
    {
        isPause = false;
        optionMenu.gameObject.SetActive(false);
        soundMenu.gameObject.SetActive(false);
        optionBG.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickSoundBtn(bool isOpen)
    {
        if (isOpen)
        {
            optionMenu.gameObject.SetActive(false);
            soundMenu.gameObject.SetActive(true);
        }
        else
        {
            optionMenu.gameObject.SetActive(true);
            soundMenu.gameObject.SetActive(false);
        }
    }

    public void OnClickExitBtn()
    {
        Application.Quit();
        Debug.Log("게임종료");
    }
}
