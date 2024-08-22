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
        if (backCard.activeSelf != true)
            backCard.SetActive(true);
        
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

            if (timer >= 30)
            {
                //Debug.Log("FLIP");
            }
        }
        //Start shwoing front card
        backCard.SetActive(false);
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
        isFlipping = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void matchedCard()
    {
        Button but = GetComponent<Button>();
        but.interactable = false;
        Destroy(this.gameObject, 1f);
    }

}
