using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Text countdownText;
    private float timeLeft = 3.0f;

    [SerializeField]
    private GameObject completePanel;

    public static State state = State.COUNTING;

    public int totalPositionScore = 1000;
    public int scorePerItem = 10;

    // Start is called before the first frame update
    void Start()
    {
        completePanel.SetActive(false);
        state = State.COUNTING;

        GameObject controllerButton = GameObject.Find("Button Input");
        if (controllerButton)
        {
            controllerButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.COUNTING:
                timeLeft -= Time.deltaTime;
                //Debug.Log(timeLeft);
                countdownText.text = timeLeft.ToString("0");
                if (timeLeft < 1)
                {
                    countdownText.text = "GO !";
                    if (timeLeft < 0)
                        state = State.RACE;
                }
                break;
            case State.FINISH:
                if(!completePanel.activeInHierarchy)
                    showCompleteRacePanel();
                break;
            case State.RACE:
                countdownText.gameObject.SetActive(false);
                break;
            case State.HOME:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Application.Quit();
                }
                break;
        }
    }

    public void showCompleteRacePanel()
    {
        GameObject controllerButton = GameObject.Find("Button Input");
        if (controllerButton)
        {
            controllerButton.SetActive(false);
        }

        LapCounter[] players = FindObjectsOfType<LapCounter>();
        players = players.OrderByDescending(lap => lap.getLapCount()).ThenBy(player => player.timeElapse).ToArray();
        Debug.Log("Winner: " + players.First().name);
        int position = 0;
        for(int i=0; i<players.Length; i++)
        {
            Debug.Log("Pos: " + players[i].name + ", with lap: " + players[i].getLapCount() + ", and time: " + players[i].timeElapse);
            if (players[i].CompareTag("Player"))
                position = i + 1;
        }
        int positionScore = totalPositionScore / position;
        int itemCollect = players[position - 1].GetComponent<KarapanMovement>().getCoinCollected();
        int itemScore = scorePerItem * itemCollect;
        int raceTime = (int)players[position - 1].timeElapse;
        int timeScore = positionScore / raceTime;
        int totalScore = positionScore + itemScore + timeScore;

        PlayerData playerData = FindObjectOfType<PlayerData>();
        playerData.AddScore(totalScore);
        playerData.addExp(25);
        playerData.updateProgress = true;

        completePanel.SetActive(true);
        GameObject.Find("Position").GetComponent<Text>().text = string.Format("Posisi Akhir: Ke-{0}", position);
        GameObject.Find("Position Score").GetComponent<Text>().text = positionScore.ToString();
        GameObject.Find("Item Collect").GetComponent<Text>().text = string.Format("Jumlah Koin: {0}", itemCollect);
        GameObject.Find("Item Score").GetComponent<Text>().text = itemScore.ToString();
        GameObject.Find("Race Time").GetComponent<Text>().text = string.Format("Waktu: {0}", raceTime);
        GameObject.Find("Time Score").GetComponent<Text>().text = timeScore.ToString();
        GameObject.Find("Total Score").GetComponent<Text>().text = string.Format("Total Score: {0}", totalScore);
    }
}
