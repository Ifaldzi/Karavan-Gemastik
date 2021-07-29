using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void onBalapanButtonClick()
    {
        DontDestroyOnLoad(FindObjectOfType<PlayerData>().gameObject);
        SceneManager.LoadScene("RaceScene");
    }

    public void OnOkBtnClick()
    {
        PlayerData playerData = FindObjectOfType<PlayerData>();
        playerData.addExp(50);
        playerData.updateProgress = true;
        //DontDestroyOnLoad(playerData.gameObject);
        SceneManager.LoadScene("HomeScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
