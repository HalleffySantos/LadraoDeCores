using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public void NovoJogo()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("CutsceneInicio");
    }

    public void LoadGame()
    {
        GameManager.FirstLoadGame();
    }

    public void SairJogo(){ 
        //Editor Unity
        //UnityEditor.EditorApplication.isPlaying = false;

        //Jogo Compilado
        Application.Quit();
    }
}
