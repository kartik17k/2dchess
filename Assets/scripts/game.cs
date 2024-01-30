using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game : MonoBehaviour
{
    public GameObject chesspiece;

    private GameObject[,] postion = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";

    private bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[]{  Create("whiteElephant",0,0), Create("whiteHorse",1,0),
           Create("whiteCamel",2,0), Create("whiteQueen",3,0),Create("whiteKing",4,0),
           Create("whiteCamel",5,0), Create("whiteHorse",6,0),Create("whiteElephant",7,0),
           Create("whitePawn",0,1), Create("whitePawn",1,1), Create("whitePawn",2,1),
            Create("whitePawn",3,1),Create("whitePawn",4,1),
           Create("whitePawn",5,1), Create("whitePawn",6,1), Create("whitePawn",7,1)
       };

        playerBlack = new GameObject[]{  Create("blackElephant",0,7), Create("blackHorse",1,7),
           Create("blackCamel",2,7), Create("blackQueen",3,7),Create("blackKing",4,7),
           Create("blackCamel",5,7), Create("blackHorse",6,7),Create("blackElephant",7,7),
           Create("blackPawn",0,6), Create("blackPawn",1,6), Create("blackPawn",2,6),
            Create("blackPawn",3,6),  Create("blackPawn",4,6),  Create("blackPawn",5,6),
            Create("blackPawn",6,6), Create("blackPawn",7,6)
       };

        //add all pieces on board
        for (int i = 0; i < playerWhite.Length; i++)
        {
            setPosition(playerBlack[i]);
            setPosition(playerWhite[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        chessmen cm = obj.GetComponent<chessmen>();
        cm.name = name;
        cm.setxBoard(x);
        cm.setyBoard(y);
        cm.activate();
        return obj;
    }

    public void setPosition(GameObject obj)
    {
        chessmen cm = obj.GetComponent<chessmen>();

        postion[cm.getxBoard(), cm.getxBoard()] = obj;
    }

    public void setPositionEmpty(int x, int y)
    {
        postion[x, y] = null;
    }

    public GameObject getPositon(int x, int y)
    {
        return postion[x, y];
    }

    public bool positionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x>=postion.GetLength(0) || y>=postion.GetLength(1)) return false; 
        return true;
    }

    public string getCurrentPlayer()
    {
        return currentPlayer;
    }


    public bool isGameOver()
    {
        return gameOver;
    }

    public void nextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public void Update()
    {
        if(gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("game");
        }
    }

    public void winner(string playerwinner)
    {
        gameOver = true;

        GameObject.FindGameObjectWithTag("winnerTag").GetComponent<Text>().enabled = true;

        GameObject.FindGameObjectWithTag("winnerTag").GetComponent<Text>().text = playerwinner+"is the winner";

        GameObject.FindGameObjectWithTag("restartTag").GetComponent<Text>().enabled = true;




    }

}
