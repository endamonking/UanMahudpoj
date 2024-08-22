using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardContainer : MonoBehaviour
{
    public GridLayoutGroup GLG;
    

    // Start is called before the first frame update
    void Start()
    {
        GLG = GetComponent<GridLayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //it use to modify cell size and colum base on card that got instantiate
    //TotalCard - all card (usally it come from column * row)
    //Size of box are 1000, 1000 and spacing 20
    public void modifyContainer(int columns, int rows)
    {
        GLG.constraintCount = columns;
        //cell size
        int width = 1000;
        int height = 1000;
        int xCell = (width - ((columns - 1) * 20)) / columns;
        int yCell = (height - ((rows - 1) * 20)) / rows;
        GLG.cellSize = new Vector2(xCell, yCell);


    }

}
