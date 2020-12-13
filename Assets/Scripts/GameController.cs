using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject wall;
    public GameObject floor;
    public GameObject target;
    public GameObject chest;
    public GameObject player;

    List<Level> levels;
    public int currentLevel = 0;

    private GameObject[,] boxes;

    private Vector2Int playerPos;
    private Player playerObject;

    Level lev;

    void Start()
    {
        levels = new List<Level>();
        levels.Add(new Level());
        levels.Add(new Level(
@"#######
#@.#..#            
#.$...#
#...#_#
#######"
            
            ));
        LoadLevel(0);
    }
    private void LoadLevel(int lvlIdx)
    {
        currentLevel = lvlIdx;

        lev = levels[currentLevel];

        boxes = new GameObject[lev.height, lev.width];

        for (int row = 0 ; row < lev.height; row++)
        {
            for(int col = 0; col < lev.levelLayout[row].Count; col++)
            {
                InstantiateFiled(col, row, lev.levelLayout[row][col]);
            }
        }

        playerObject = FindObjectOfType<Player>();
        playerObject.gameController = this;
    }
    private void CenterCameraOn(float x, float y)
    {
       // CenterCameraOn(lev.width / 2, lev.height / 2);
        //TODO wycentrować Camera.main na (x, y) i ustawic orthographicSize tak aby cały poziom był widoczny
    }
    private void InstantiateFiled(int x, int y, Field field)
    {
        switch(field.fieldType)
        {
            case FieldType.Wall:
                Instantiate(wall, new Vector3 (x, y, 0),Quaternion.identity);
                break;
            case FieldType.Floor:
                Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity);
                break;
            case FieldType.Target:
                Instantiate(target, new Vector3(x, y, 0), Quaternion.identity);
                break;
        }
        if(field.entityType == EntityType.Player)
        {
            Instantiate(player, new Vector3(x, y, 0), Quaternion.identity);
            playerPos = new Vector2Int(x, y);
        }
        if (field.entityType == EntityType.Chest)
        {
            boxes[y,x] = Instantiate(chest, new Vector3(x, y, 0), Quaternion.identity);
        }

    }

    internal bool TryMovePlayer(Vector2Int moveDirection)
    {
        Vector2Int pos = playerPos + moveDirection;
        
        if(lev.levelLayout[pos.y][pos.x].fieldType == FieldType.Wall)
        {
            return false;
        }
        else if (lev.levelLayout[pos.y][pos.x].entityType == EntityType.Chest)
        {
            return TryMoveBox( pos, moveDirection);
        }
        else if (lev.levelLayout[pos.y][pos.x].fieldType == FieldType.Floor)
        {
            playerObject.transform.position = new Vector3(pos.x, pos.y, 0);
            playerPos = pos;

            return true;
        }
        return false;
    }
    private bool TryMoveBox(Vector2Int pos, Vector2Int directrion)
    {
        Vector2Int boxPos = pos + directrion;

        if (lev.levelLayout[boxPos.y][boxPos.x].fieldType == FieldType.Wall)
        {
            return false;
        }
        else if (lev.levelLayout[boxPos.y][boxPos.x].entityType == EntityType.Chest)
        {
            return false;
        }
        else if(lev.levelLayout[boxPos.y][boxPos.x].fieldType == FieldType.Target)
        {
            playerObject.transform.position = new Vector3(pos.x, pos.y, 0);
            playerPos = pos;

            boxes[boxPos.y, boxPos.x] = boxes[pos.y, pos.x];
            boxes[pos.y, pos.x] = null;

            boxes[boxPos.y, boxPos.x].transform.position = new Vector3(boxPos.x, boxPos.y, 0);
            lev.levelLayout[pos.y][pos.x].entityType = EntityType.None;
            lev.levelLayout[boxPos.y][boxPos.x].entityType = EntityType.Chest;

            Win();
            return true;

        }
        else if (lev.levelLayout[boxPos.y][boxPos.x].fieldType == FieldType.Floor)
        {
            playerObject.transform.position = new Vector3(pos.x, pos.y, 0);
            playerPos = pos;

            //box

            boxes[boxPos.y, boxPos.x] = boxes[pos.y, pos.x];
            boxes[pos.y, pos.x] = null;

            boxes[boxPos.y, boxPos.x].transform.position = new Vector3(boxPos.x, boxPos.y, 0);
            lev.levelLayout[pos.y][pos.x].entityType = EntityType.None;
            lev.levelLayout[boxPos.y][boxPos.x].entityType = EntityType.Chest;

            return true;
        }
        return false;
    }
    public void Clear()
    {
        //Player
        
        Player.playerSteps = 0;
        playerPos = new Vector2Int();
        playerObject = null;

        //Level & UI

        GameObject[] allObj = UnityEngine.Object.FindObjectsOfType<GameObject>();

        foreach( GameObject obj in allObj)
        {
            if(obj.CompareTag("Untagged"))
            {
                Destroy(obj);
            }
        }  
        boxes = null;
    }
    public void Reset()
    {
        Clear();
        LoadLevel(currentLevel);
    }
    public void Win()
    {
        Clear();
        LoadLevel(1);
    }
}
