using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    public List<GameObject> allCardlist = new List<GameObject>();
    private List<GameObject> cardlist = new List<GameObject>(); //in game card list
    public GameObject cardContainer;
    public int colum = 0, row = 0;

    private Card firstCard = null, secondCard = null;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        createCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //using Fisher-Yates shuffle algorithm to shuffle
    private void ShuffleList<T>(List<T> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public void createCard()
    {
        if (cardContainer != null)
        {
            CardContainer CC = cardContainer.GetComponent<CardContainer>();
            CC.modifyContainer(colum, row);
            int totalItem = colum * row;
            //add card to cardlist
            for (int a = 0; a < totalItem / 2; a++)
            {
                int randomIndex = Random.Range(0, allCardlist.Count);
                cardlist.Add(allCardlist[randomIndex]);
                cardlist.Add(allCardlist[randomIndex]);
            }
            ShuffleList(cardlist);
            CC.createCard(cardlist, totalItem);
        }
        else
            Debug.LogError("No Card Container please");
    }

    public void clickedCard(Card cardNumber)
    {
        if (firstCard == null)
        {
            firstCard = cardNumber;
        }
        else if (secondCard == null)
        {
            if (firstCard == cardNumber)
                return;

            secondCard = cardNumber;
            if (firstCard.cardNo == secondCard.cardNo)
            {
                firstCard.matchedCard();
                secondCard.matchedCard();
            }
            else
            {
                StartCoroutine(flippingBack(firstCard, secondCard));
            }
            firstCard = null;
            secondCard = null;
    
        }
    }

    IEnumerator flippingBack(Card firstCard, Card secondCard)
    {
        yield return new WaitForSeconds(1f);
        firstCard.resetFlip();
        secondCard.resetFlip();
    }



}
