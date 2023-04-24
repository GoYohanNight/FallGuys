using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTrigger : MonoBehaviour
{
    public GameObject gameManager;
    public GameTimer gameTimer;

    // Start is called before the first frame update
    void Start()
    {
        gameTimer = gameManager.GetComponent<GameTimer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        gameTimer.isGameOver = true;
        gameManager.GetComponent<GameManager>().isGameOver = true;
    }
}