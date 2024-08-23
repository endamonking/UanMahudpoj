using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Burst.Intrinsics;

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
    public TextMeshProUGUI HPTMP, stageTMP, highScoreTMP, comboTMP;
    public int scoreMultiplier = 10, hp = 100, stage = 1, score = 0, combo = 0;

    [Header("Game Over and save")]
    public GameObject gameOverScreen;
    public SaveAndLoadSystem saveScript;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
        scoreTMP.text = "Score : " + score.ToString(); 
        HPTMP.text = "HP : " + hp.ToString(); 
        stageTMP.text = "Stage : " + stage.ToString();
        comboTMP.text = "Combo : " + combo.ToString();

        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreTMP.text = "Highest Score : " + highScore.ToString();

        createCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //use to load save when come into game
    public void loadSave()
    {
        saveStructure save = saveScript.loadSaveStructure();
        if (save != null)
        {
            score = save.Score;
            stage = save.Stage;
            hp = save.HP;
            row = save.Row;
            colum = save.Column;
            combo = save.Combo;
            Start();
        }
        else
        {
            Debug.Log("No save found!");
        }

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
    //For combo if player can do more than 10 it will increase bonus point by third of combo score
    private void addScore (int number)
    {
        combo++;
        updateCombo(combo);
        int scoreNum = number;

        if (combo >= 5)
            scoreNum += combo / 3;

        score += scoreNum;
        scoreTMP.text = "Score : " + score.ToString();

        cardCounter += 2;
        AudioManager.Instance.playSFX(AudioManager.Instance.scored);
        //If has no card remaining go to enxt stage
        if (cardCounter >= totalItem)
        {
            cardCounter = 0;
            StartCoroutine(nextStage());
        }
    }
    IEnumerator nextStage()
    {
        yield return new WaitForSeconds(2f);
        stage++;
        stageTMP.text = "Stage : " + stage.ToString();

        if (stage % 5 == 0)
        {
            //The total item cant be odd number so for easy just plus 2 for column and row
            colum += 2;
            row += 2;
        }
        createCard();
        //save progress
        saveScript.save();
    }

    private void minusHP(int number)
    {
        updateCombo(0);
         hp -= number;
        HPTMP.text = "HP : " + hp.ToString();
        AudioManager.Instance.playSFX(AudioManager.Instance.hurt);
        if (hp <= 0)
        {
            AudioManager.Instance.playSFX(AudioManager.Instance.gameOver);
            showGameOver();
            Debug.Log("Game Over");
        }
    }

    private void updateCombo(int number)
    {
        combo = number;
        comboTMP.text = "Combo : " + combo.ToString();
    }

    //Game over

    public void showGameOver()
    {
        gameOverScreen.SetActive(true);
        saveScript.deleteSaveFile();
        //save high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public void reloadScreen()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currentScene.name);
    }


        
}
