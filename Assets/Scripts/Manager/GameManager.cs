using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    #region Variables
    [SerializeField] private Text scoreText;
    [SerializeField] private DSpawner spawner;
    [SerializeField] private Image endImage;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text endText;

    [SerializeField] private float crossFadeAmount;
    [SerializeField] private Player player;
    [SerializeField] private AudioClip gameOverClip;

    private AudioSource audioSource;

    private float time;
    private float highScoreTime;

    #endregion Variables

    #region Unity Methods
    void Awake()
    {
        Screen.SetResolution(Screen.height / 16 * 9, Screen.height, true);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        scoreText.text = "Time : ";
        InitData();
    }

    void Update()
    {
        if (!player.isDead)
        {
            time += Time.deltaTime;

            scoreText.text = $"Time : {time}";
        }

        if (highScoreText.IsActive() && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }
    }

    #endregion Unity Methods

    #region Helper Methods
    public void GameOver()
    {
        if (player != null)
        {
            if (player.isDead)
            {
                StopAllCoroutines();
                spawner.gameObject.SetActive(false);

                StartCoroutine(GameOverRoutine(2f, 1f));
            }
        }
    }

    public void InitData()
    {
        SaveData saveData = new SaveData();
        string path = $"{Application.dataPath}/Save/Save.json";
        if (File.ReadAllText(path).Length == 0)
        {
            saveData.highScore = 0f;
            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(path, json);
        }
    }

    public void LoadData()
    {
        string path = $"{Application.dataPath}/Save/Save.json";
        string json = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(json);
        saveData.highScore = (saveData.highScore < time) ? time : saveData.highScore;
        highScoreTime = saveData.highScore;
        highScoreText.text = "High Score : " + highScoreTime;
        json = JsonUtility.ToJson(saveData);
        File.WriteAllText(path, json);
    }

    private IEnumerator GameOverRoutine(float startTime, float time)
    {
        yield return new WaitForSeconds(startTime);

        float timer = 0f;

        while ((timer += Time.deltaTime) < time)
        {
            Color tempColor = endImage.color;
            tempColor.a += Time.deltaTime * crossFadeAmount;
            endImage.color = tempColor;
            yield return null;
        }

        audioSource.PlayOneShot(gameOverClip);
        endText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
    }

    #endregion Helper Methods
}
