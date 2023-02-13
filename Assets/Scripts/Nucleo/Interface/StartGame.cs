using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
  void OnEnable()
  {
        float posYPlayer = 0;
        float posXPlayer = 0;

        if (PlayerPrefs.HasKey("posYPlayer"))
        {
            posYPlayer = PlayerPrefs.GetFloat("posYPlayer");
        }

        if (PlayerPrefs.HasKey("posXPlayer"))
        {
            posXPlayer = PlayerPrefs.GetFloat("posXPlayer");
        }

        SceneManager.LoadScene("Global");
        SceneManager.LoadSceneAsync("Vila", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 8", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 9", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 10", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 11", LoadSceneMode.Additive);
  }

}