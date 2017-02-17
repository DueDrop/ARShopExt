using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ApplicationModeManager : MonoBehaviour {

    public void GoToReadMode() {

        if (SceneManager.GetActiveScene().name == "BarcodeScene") return;
      
        SceneManager.LoadScene("BarcodeScene");
        SceneManager.UnloadScene("StartMenuScene");

    }

    public void GoToSettingsMode() {

        if (SceneManager.GetActiveScene().name == "StartMenuScene") return;


        SceneManager.LoadScene("StartMenuScene");
        SceneManager.UnloadScene("BarcodeScene");
    }

    public void ExitApplication() {
        Application.Quit();
    }

}
