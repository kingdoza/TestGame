using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    void Awake() {
        if(SceneManager.GetActiveScene().name == "sunguk") {
            gameObject.SetActive(false);
        }
    }
    public void Show() {
        gameObject.SetActive(true);
    }

    public void Retry() {
        SceneManager.LoadScene("sunguk"); 
    }

    public void Quit() {
        Application.Quit(); //빌드해서 응용프로그램으로 실행하면 작동함
    }

    public void SceneChange() {
        SceneManager.LoadScene("sunguk");
    } 
}