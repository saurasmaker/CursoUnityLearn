using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate;
    public int lifes = 3;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifesText;
    public GameObject gameOverPanel;
    public GameObject menuPanel;

    [HideInInspector] public int score = 0;
    public bool isGameOver = false;
    private bool isGamePaused = false;
    private int difficulty = 1;

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        menuPanel.SetActive(false);
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver && Input.GetKeyDown(KeyCode.Escape))
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
    }

    IEnumerator SpawnTarget()
    {
        while(!isGameOver)
        {
            yield return new WaitForSeconds(spawnRate/difficulty);
            int i = Random.Range(0, targets.Count);
            Instantiate(targets[i]);
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        lifesText.text = lifes  + " lifes";
        if (score < 0 || lifes <= 0)        
            GameOver();    
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        isGameOver = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
        isGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        menuPanel.SetActive(false);
        isGamePaused = false;
    }

    public void SetEasyDifficulty()
    {
        difficulty = 1;
        ResumeGame();
    }

    public void SetMediumDifficulty()
    {
        difficulty = 2;
        ResumeGame();
    }

    public void SetHardDifficulty()
    {
        difficulty = 3;
        ResumeGame();
    }
}
