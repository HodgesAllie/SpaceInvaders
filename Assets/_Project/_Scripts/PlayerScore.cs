using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    Player player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void Score(int amount)
    {
        player?.Score(amount);
    }
}
