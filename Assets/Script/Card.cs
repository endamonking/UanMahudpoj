using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{

    [SerializeField]
    private GameObject backCard;
    [SerializeField]
    private GameObject frontCard;

    public int cardNo = 0; //use to check, is it the same card when matched

    public int timer = 0;
    public bool isFlipping = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(starting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clicked()
    {
        if (!isFlipping)
        {
            startFlip();
            Game_Manager.Instance.clickedCard(this);
            AudioManager.Instance.playSFX(AudioManager.Instance.fliping);
        }
    }

    private void startFlip()
    {
        
        isFlipping = true;
        StartCoroutine(flipping());
    }

    IEnumerator flipping()
    {
        //Rotating
        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(new Vector3(0, -3, 0));
            timer++;

        }
        //Start shwoing front card
        if (backCard.activeSelf)
            backCard.SetActive(false);
        else
            backCard.SetActive(true);

        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Rotate(new Vector3(0, -3, 0));
            timer++;

            if (timer >= 60)
            {
                //Debug.Log("FLIP");
            }
        }
        timer = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }    

    public void resetFlip()
    {
        StartCoroutine(flipping());
        isFlipping = false;
    }


    public void matchedCard()
    {
        Button but = GetComponent<Button>();
        but.interactable = false;
        Destroy(this.gameObject, 1f);
    }


    //Use to show front card at start then flipping it back and start playing
    IEnumerator starting()
    {
        backCard.SetActive(false);
        isFlipping = true;
        yield return new WaitForSeconds(3f);
        StartCoroutine(flipping());
        isFlipping = false;
    }


}
