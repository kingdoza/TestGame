using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {
    private void Awake() {
        transform.gameObject.SetActive(false); 
    }

    public void Show() {
        transform.gameObject.SetActive(true);
    }

    private void Retry() {
        SceneManager.LoadScene("sunguk"); 
    }

    private void Quit() {
        Application.Quit(); //빌드해서 응용프로그램으로 실행하면 작동함
    }

}