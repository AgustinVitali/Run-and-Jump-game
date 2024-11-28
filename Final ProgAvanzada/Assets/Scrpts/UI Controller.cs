using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Asegúrate de incluir esto

public class UIController : MonoBehaviour
{
    public static UIController instance;

    public Image Heart1, Heart2, Heart3;
    public Sprite heartFull, heartEmpty;

    public Text gemText;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool shouldFadeToBlack, shouldFadeFromBlack;

    public GameObject levelCompleteText;


    private void Awake()
    {
        instance = this;

        FadeFromBlack();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateGemCount();   
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 
            Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if(shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, 
            Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if(fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
    }

    public void UpdateHealthDisplay()
    {
        switch(PlayerHealthController.instance.currentHealth)
        {
            case 3:
                Heart1.sprite = heartFull;
                Heart2.sprite = heartFull;
                Heart3.sprite = heartFull;
                break;

            case 2:
                Heart1.sprite = heartFull;
                Heart2.sprite = heartFull;
                Heart3.sprite = heartEmpty;
                break;

            case 1:
                Heart1.sprite = heartFull;
                Heart2.sprite = heartEmpty;
                Heart3.sprite = heartEmpty;
                break;

            case 0:
                Heart1.sprite = heartEmpty;
                Heart2.sprite = heartEmpty;
                Heart3.sprite = heartEmpty;
                break;
            default:
                Heart1.sprite = heartEmpty;
                Heart2.sprite = heartEmpty;
                Heart3.sprite = heartEmpty;
                break;
        }
    }

    public void UpdateGemCount()
    {
        gemText.text = LavelManager.instance.gemsCollected.ToString();
    }


    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()        
    {
        shouldFadeFromBlack = true;
        shouldFadeToBlack = false;
    }

}
