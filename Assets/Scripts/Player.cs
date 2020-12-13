using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController gameController;
    private UIManager uiManager;

    public static int playerSteps = 0;
    void Start()
    {
        uiManager = gameController.GetComponent<UIManager>();
    }
    void Update()
    {
        //1 0 | -1 0 | 0 1 | |0 -1 |
        Vector2Int moveDirection;
       
        if(Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = new Vector2Int(0, 1);
            Move(moveDirection);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = new Vector2Int(0, -1);
            Move(moveDirection);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = new Vector2Int(-1, 0);
            Move(moveDirection);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = new Vector2Int(1, 0);
            Move(moveDirection);
        }
    }
    private void Move(Vector2Int moveDirection)
    {
        if (gameController.TryMovePlayer(moveDirection))
        {
            playerSteps += 1;
            uiManager.StepsUpdate();
        }
    }
}

