using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public string level;
   
    public int currentGold = 0;
    public string letters = "";
    public bool status;
    //string Characters = new string[26]; 

   // List<string> characters = new List<string>();

    public Text goldText;
    public Text boardText;
    public Text lettertext;
    public Text Statustext;
    public Text ReloadMessageText;
    public string theText = "Collect all the letters.";
    //public bool show = true;
    public GameObject messageUI;

    public GameObject levelclearUI;


    public GameObject gameOverUI;

    public Image black;
    public Animator anim;


    void Start()
    {
        //show = false;
        messageUI.SetActive(false);
        gameOverUI.SetActive(false);
        levelclearUI.SetActive(false);
        status = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddGold(int goldToAdd)
    {
        currentGold += goldToAdd;
        goldText.text = "Coins: " + currentGold + "!";
    }

    public void AddLetter(string letter)
    {
        letters += " "+letter;
        //characters.Add(letter);
        lettertext.text = letters;
        boardText.text = letters;
    }
    public void Board() {
        if (letters == " false ? : 5")
        {
            status = true;
            boardText.text = "Correct";
        }

        if (letters == " true ? : 6")
        {
            status = true;
            boardText.text = "Correct";
        }

        if (!status)
        {
            Statustext.text = "";
            Statustext.color = new Color(10f, 0f, 0f);
        }
        else
        {
            Statustext.text = "Complete";
            ReloadMessageText.text = "Loading Next Level";
            Statustext.color = new Color(0f, 10f, 0f);

            //Time.timeScale = 0;
            //SceneManager.LoadScene(level);
            StartCoroutine(Fading());

        }
        //show = true;
        messageUI.SetActive(true);

    }

    public void NoBoard()
    {
        //show = false;
        messageUI.SetActive(false);
    }


    public void Die()
    {
        //gameOverUI.SetActive(true);
        StartCoroutine(Fading_Restart());
        //Time.timeScale = 0;
    }

/*    void OnGUI()
    {
        if (show) { GUI.Label(new Rect(10, 10, Screen.width - 20, 30), theText);

            //messageUI.SetActive(!messageUI.activeSelf);

        }
    }
*/

    IEnumerator Fading()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        //SceneManager.LoadScene(level);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator Fading_Restart()
    {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
