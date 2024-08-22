using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public List<GameObject> allCardlist = new List<GameObject>();
    private List<GameObject> cardlist = new List<GameObject>(); //in game card list
    public GameObject cardContainer;
    public int colum = 0, row = 0;


    // Start is called before the first frame update
    void Start()
    {
        if (cardContainer != null)
        {
            cardContainer.GetComponent<CardContainer>().modifyContainer(colum, row);
            int totalItem = colum * row;
            //add card to cardlist
            for (int a = 0; a < totalItem / 2; a++)
            {

            }
            for (int i =0; i < totalItem; i++)
            {
                GameObject card = Instantiate(cardlist[Random.Range(0, cardlist.Count)], cardlist[0].transform.position, Quaternion.identity, cardContainer.transform);
            }
        }
        else
            Debug.LogError("No Card Container please");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
