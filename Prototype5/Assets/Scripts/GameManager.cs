using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate;
    [HideInInspector] public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    private bool isGameActive = true;
     

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        StartCoroutine(SpawnTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int i = Random.Range(0, targets.Count);
            Instantiate(targets[i]);
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        if(score < 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
