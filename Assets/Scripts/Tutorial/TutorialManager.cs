using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tutorialMessage1;
    [SerializeField]
    private GameObject tutorialMessage2;
    [SerializeField]
    private GameObject innerWalls;
    [SerializeField]
    private GameObject tutPlayer1;
    [SerializeField]
    private GameObject tutPlayer2;
    [SerializeField]
    private GameObject tutHUD;

    // Start is called before the first frame update
    void Start()
    {
        tutorialMessage1.SetActive(true);
        innerWalls.SetActive(false);
        tutPlayer1.SetActive(false);
        tutPlayer2.SetActive(false);
        tutHUD.SetActive(false);
    }

    public void NextClicked(){
        tutorialMessage1.SetActive(false);
        tutorialMessage2.SetActive(true);

    }

    public void ContinueClicked(){
        tutorialMessage2.SetActive(false);
        innerWalls.SetActive(true);
        tutPlayer1.SetActive(true);
        tutPlayer2.SetActive(true);
        tutHUD.SetActive(true);
    }
}
