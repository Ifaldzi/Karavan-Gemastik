using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCounter : MonoBehaviour
{
    private int lapCount;

    public LapTrigger first;
    private LapTrigger next;

    public int maxLap = 3;

    public Text lapCountText;

    public float timeElapse;

    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        lapCount = 0;
        SetNextTrigger(first);
        if(lapCountText != null)
            lapCountText.text = string.Format("Lap {0}/{1}", 1, maxLap);
        timeElapse = 0;
        startTime = Time.time;
    }

    private void SetNextTrigger(LapTrigger next)
    {
        this.next = next;
    }

    public void OnLapTrigger(LapTrigger lap)
    {
        if(next == lap)
        {
            if(lap == first)
            {
                timeElapse += Time.time - startTime;
                startTime = Time.time;
                Debug.Log("Time elapase: " + timeElapse);
                lapCount++;
                if (lapCount <= maxLap)
                {
                    UpdateLapText();
                }
                else
                    if(CompareTag("Player"))
                        GameManager.state = State.FINISH;
                    else
                    {
                        GetComponent<KarapanMovement>().enabled = false;
                        Debug.Log("Time race: " + GetComponent<KarapanMovement>().timeElapsed);
                    }
                //Debug.Log("Time Race: " + GetComponent<RacingPlayer>().timeElapsed);
            }
            SetNextTrigger(lap.next);
        }
        Debug.Log("Lap: " + lapCount);
    }

    public int getLapCount()
    {
        return lapCount;
    }

    private void UpdateLapText()
    {
        if(lapCountText)
            lapCountText.text = string.Format("Lap {0}/{1}", lapCount, maxLap);
    }
}
