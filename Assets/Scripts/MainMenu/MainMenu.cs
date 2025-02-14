using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private GameObject controls;
    
    public void Start(){
        controls.SetActive(false);
        canvas.SetActive(true);
    }

    public void StartGame(){
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenControls(){
        canvas.SetActive(false);
        controls.SetActive(true);
    }

    public void BackToMain(){
        controls.SetActive(false);
        canvas.SetActive(true);
    }
}
