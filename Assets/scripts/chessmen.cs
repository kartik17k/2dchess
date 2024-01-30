using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chessmen : MonoBehaviour
{
    public GameObject controller;
    public GameObject movePlate;

    private int xBoard = -1;
    private int yBoard = -1;

    private string player; // to keep track of player

    public Sprite blackKing, blackQueen, blackHorse, blackCamel, blackElephant, blackPawn;
    public Sprite whiteKing, whiteQueen, whiteHorse, whiteCamel, whiteElephant, whitePawn;

    public void activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        setCoords(); // used to institate location and adjust to transform 

        switch (this.name)
        {
            case "blackKing": this.GetComponent<SpriteRenderer>().sprite = blackKing; player = "black"; break;
            case "blackQueen": this.GetComponent<SpriteRenderer>().sprite = blackQueen; player = "black"; break;
            case "blackHorse": this.GetComponent<SpriteRenderer>().sprite = blackHorse; player = "black"; break;
            case "blackCamel": this.GetComponent<SpriteRenderer>().sprite = blackCamel; player = "black"; break;
            case "blackElephant": this.GetComponent<SpriteRenderer>().sprite = blackElephant; player = "black"; break;
            case "blackPawn": this.GetComponent<SpriteRenderer>().sprite = blackPawn; player = "black"; break;


            case "whiteKing": this.GetComponent<SpriteRenderer>().sprite = whiteKing; player = "white"; break;
            case "whiteQueen": this.GetComponent<SpriteRenderer>().sprite = whiteQueen; player = "white"; break;
            case "whiteHorse": this.GetComponent<SpriteRenderer>().sprite = whiteHorse; player = "white"; break;
            case "whiteCamel": this.GetComponent<SpriteRenderer>().sprite = whiteCamel; player = "white"; break;
            case "whiteElephant": this.GetComponent<SpriteRenderer>().sprite = whiteElephant; player = "white"; break;
            case "whitePawn": this.GetComponent<SpriteRenderer>().sprite = whitePawn; player = "white"; break;

        }


    }

    public void setCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 1.5f;
        y *= 1.5f;

        x += -5.19f;
        y += -5.25f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }
    public int getxBoard()
    {
        return xBoard;
    }

    public int getyBoard()
    {
        return yBoard;
    }

    public void setxBoard(int x) {
        xBoard = x;    
    }

    public void setyBoard(int y) {
        yBoard = y;
    }


    private void OnMouseUp()
    {
        if (!controller.GetComponent<game>().isGameOver() && controller.GetComponent<game>().getCurrentPlayer() == player)
        {
            destroyMoviePlates();

            initiateMovePlates();
        }
        
    }

    public void destroyMoviePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("movePlates");

        for(int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void initiateMovePlates()
    {
        switch (this.name)
        {
            case "blackQueen":
            case "whiteQueen":
                linemovePlate(1, 0);
                linemovePlate(0, 1);
                linemovePlate(1, 1);
                linemovePlate(-1, 0);
                linemovePlate(0, -1);
                linemovePlate(-1, -1);
                linemovePlate(-1, 1);
                linemovePlate(1, -1);
                break;

            case "blackHorse":
            case "whiteHorse":
                LmovePlate();
                break;

            case"blackCamel":
            case "whiteCamel":
                linemovePlate(1,1);
                linemovePlate(1, -1);
                linemovePlate(-1, 1);
                linemovePlate(-1, -1);
                break;

            case "blackKing":
            case "whiteKing":
                surroundmovePlate();
                break;

            case "blackElephant":
            case "whiteElephant":
                linemovePlate(1, 0);
                linemovePlate(0, 1);
                linemovePlate(-1, 0);
                linemovePlate(0, -1);
                break;
            case "blackPawn":
                pawnmovePlate(xBoard, yBoard - 1);
                break;

            case "whitePawn":
                pawnmovePlate(xBoard, yBoard + 1);
                break;
        }
    }

    public void linemovePlate(int xIncrement,  int yIncrement)
    {
        game sc = controller.GetComponent<game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while( sc.positionOnBoard(x, y) && sc.getPositon(x,y ) == null) {

            movePlateSpwan(x, y);
             
            x += xIncrement;
            y += yIncrement;

        }

        if (sc.positionOnBoard(x,y) && sc.getPositon(x,y).GetComponent<chessmen>().player  != player)
        {
            movePlateAttackSpwan(x,y);
        }
    }


    public void LmovePlate()
    {
        piontMovePlate(xBoard + 1, yBoard + 2);
        piontMovePlate(xBoard - 1, yBoard + 2);
        piontMovePlate(xBoard + 2, yBoard + 1);
        piontMovePlate(xBoard + 2, yBoard - 1);
        piontMovePlate(xBoard + 1, yBoard - 2);
        piontMovePlate(xBoard - 1, yBoard - 2);
        piontMovePlate(xBoard - 2, yBoard + 1);
        piontMovePlate(xBoard - 2, yBoard - 1);
    }

    public void surroundmovePlate()
    {
        piontMovePlate(xBoard , yBoard + 1);
        piontMovePlate(xBoard , yBoard - 1);
        piontMovePlate(xBoard - 1, yBoard - 1);
        piontMovePlate(xBoard - 1, yBoard - 0);
        piontMovePlate(xBoard - 1, yBoard + 1);
        piontMovePlate(xBoard + 1, yBoard - 1);
        piontMovePlate(xBoard + 1, yBoard - 0);
        piontMovePlate(xBoard + 1, yBoard + 1);
    }


    public void piontMovePlate(int x, int y)
    {
        game sc = controller.GetComponent<game>();
        if (sc.positionOnBoard(x, y))
        {
            GameObject cp = sc.getPositon(x, y);

            if (cp == null)
            {
                movePlateSpwan(x, y);
            }
            else if (cp.GetComponent<chessmen>().player != player) 
            {
                movePlateAttackSpwan(x, y);
            }
        }
    }

    public void pawnmovePlate(int x, int y)
    {
        game sc = controller.GetComponent<game>();

        if (sc.positionOnBoard(x, y))
        {
            if (sc.getPositon(x, y) == null)
            {
                movePlateSpwan(x, y);
            }
            if (sc.positionOnBoard(x + 1, y) && sc.getPositon(x + 1, y) != null &&
                sc.getPositon(x + 1, y).GetComponent<chessmen>().player != player)
            {
                movePlateAttackSpwan(x + 1, y);
            }

            if (sc.positionOnBoard(x - 1, y) && sc.getPositon(x - 1, y) != null &&
                sc.getPositon(x - 1, y).GetComponent<chessmen>().player != player)
            {
                movePlateAttackSpwan(x - 1, y);
            }
        }

    }

    public void movePlateSpwan(int matrixX , int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.5f;
        y *= 1.5f;

        x += -5.19f;
        y += -5.25f;
         
        // mp moveplate
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity); //set on screen 

        movePlate mpScripts = mp.GetComponent<movePlate>();
        mpScripts.setReference(gameObject);
        mpScripts.setcoords(matrixX, matrixY); // to keep trick 
    }

    public void movePlateAttackSpwan(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.5f;
        y *= 1.5f;

        x += -5.19f;
        y += -5.25f;

        // mp moveplate
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity); //set on screen 

        movePlate mpScripts = mp.GetComponent<movePlate>();
        mpScripts.attack = true;
        mpScripts.setReference(gameObject);
        mpScripts.setcoords(matrixX, matrixY); // to keep trick 
    }





}
