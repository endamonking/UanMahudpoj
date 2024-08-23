using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;
    [Header("Card")]
    public List<GameObject> allCardlist = new List<GameObject>();
    private List<GameObject> cardlist = new List<GameObject>(); //in game card list
    public GameObject cardContainer;
    public int colum = 0, row = 0, totalItem = 0;
    private int cardCounter;
    private Card firstCard = null, secondCard = null;

    [Header("Text and score")]
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI HPTMP, stageTMP;
    public int scoreMultiplier = 10, hp = 100, stage = 1, score = 0;


    // Start is called before the first frame update
    void Start()
    {
        scoreTMP.text = "Score : " + score.ToString(); 
        HPTMP.text = "HP : " + hp.ToString(); 
        stageTMP.text = "Stage : " + stage.ToString(); 

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
            totalItem = colum * row;
            cardlist.Clear();
            //add card to cardlist
            for (int a = 0; a < totalItem / 2; a++)
            {
                int randomIndex = Random.Range(0, allCardlist.Count);
                cardlist.Add(allCardlist[randomIndex]);
                cardlist.Add(allCardlist[randomIndex]);
            }
            ShuffleList(cardlist);
            CC.createCard(cardlist, colum,row);
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
                addScore(stage * scoreMultiplier);
            }
            else
            {
                StartCoroutine(flippingBack(firstCard, secondCard));
                minusHP(5);
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

    //update score and update text
    //and formular for my design is Score multiplier * Stage
    private void addScore (int number)
    {
        score += number;
        scoreTMP.text = "Score : " + score.ToString();

        cardCounter += 2;
        //If has no card remaining go to enxt stage
        if (cardCounter >= totalItem)
        {
            cardCounter = 0;
            StartCoroutine(nextStage());
        }
    }
    IEnumerator nextStage()
    {
        yield return new WaitForSeconds(1f);
        stage++;
        stageTMP.text = "Stage : " + stage.ToString();

        if (stage % 5 == 0)
        {
            //The total item cant be odd number so for easy just plus 2 for column and row
            colum += 2;
            row += 2;
        }
        createCard();
    }

    private void minusHP(int number)
    {
        hp -= number;
        HPTMP.text = "HP : " + hp.ToString();
        if (hp <= 0)
        {
            Debug.Log("Game Over");
        }
    }

}
