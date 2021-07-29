using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    int currentLevel;
    int currentExp;
    int maxExpToNextLevel;
    int totalScore;

    public bool updateProgress = false;

    LevelProgress levelProgress;

    public static PlayerData instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        levelProgress = FindObjectOfType<LevelProgress>();

        currentExp = 0;
        currentLevel = 0;
        maxExpToNextLevel = 100;
        totalScore = 0;

        levelProgress.setMaxExp(maxExpToNextLevel);
        levelProgress.setExpValue(currentExp);
        levelProgress.setLevel(currentLevel);
        levelProgress.SetTotalScore(totalScore);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateProgress)
        {
            if(currentExp > maxExpToNextLevel)
            {
                currentExp = currentExp - maxExpToNextLevel;
                currentLevel++;
                maxExpToNextLevel += 50;
            }

            levelProgress = FindObjectOfType<LevelProgress>();
            if (levelProgress)
            {
                Debug.Log("Lv Update");
                levelProgress.setMaxExp(maxExpToNextLevel);
                levelProgress.setExpValue(currentExp);
                levelProgress.setLevel(currentLevel);
                levelProgress.SetTotalScore(totalScore);
                
                updateProgress = false;
            }

        }
    }

    public void addExp(int exp)
    {
        currentExp += exp + 5 * currentLevel;
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }
}
