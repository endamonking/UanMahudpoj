using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CardClicked : MonoBehaviour
{

    [SerializeField]
    private GameObject backCard;
    [SerializeField]
    private GameObject frontCard;

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
            startFlip();
    }

    private void startFlip()
    {
        Debug.Log("sFlip");
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
                Debug.Log("FLIP");
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
                Debug.Log("FLIP");
            }
        }
        timer = 0;
        isFlipping = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
