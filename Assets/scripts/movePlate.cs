using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlate : MonoBehaviour
{
    public GameObject controller;

    GameObject reference = null;

    //board position 
    int matrixX;
    int matrixY;

    public bool attack = false;
    //here false means moment and true is attacking

    public void Start()
    {
        if (attack)
        {   // here gameobject is script component
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f,1.0f);
        }
    }


    public void OnMouseUp()
    {
        //its just like tapping
        controller = GameObject.FindGameObjectWithTag("GameController");

        if (attack)
        {
            GameObject cp = controller.GetComponent<game>().getPositon(matrixX, matrixY);

            if (cp.name == "whiteKing") controller.GetComponent<game>().winner("black");
            if (cp.name == "blackKing") controller.GetComponent<game>().winner("white");
            Destroy(cp);
        }

        controller.GetComponent<game>().setPositionEmpty(reference.GetComponent<chessmen>().getxBoard(), 
            reference.GetComponent<chessmen>().getyBoard());

        reference.GetComponent<chessmen>().setxBoard(matrixX);
        reference.GetComponent<chessmen>().setyBoard(matrixY);
        reference.GetComponent<chessmen>().setCoords();


        controller.GetComponent<game>().setPosition(reference);
        controller.GetComponent<game>().nextTurn();

        reference.GetComponent<chessmen>().destroyMoviePlates();
    }

    public void setcoords(int  x, int y)
    {
        matrixX = x;    
        matrixY = y;
    }

    public void setReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject getReference()
    {
        return reference;
    }
}
