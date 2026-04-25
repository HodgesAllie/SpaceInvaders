using FlyingWormConsole3;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int score = 0;

    public void GameOver()
    {
        Debug.Log($"Game Over: {score}");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Score(int amount)
    {
        score += amount;
        //ConsoleProDebug.Watch("Player Score", score.ToString());
    }
}
