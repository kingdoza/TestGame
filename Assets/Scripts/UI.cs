using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    private void Awake() {
        if(SceneManager.GetActiveScene().name == "main") {
            gameObject.SetActive(false);
        }
    }
    public void Show() {
        gameObject.SetActive(true);
    }

    public void Retry() {
        SceneManager.LoadScene("main"); 
    }

    public void Quit() {
        Application.Quit(); //빌드해서 응용프로그램으로 실행하면 작동함
    }

    public void SceneChange() {
        SceneManager.LoadScene("main");
    } 
}