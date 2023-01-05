using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    //[SerializeField] private string faseJogo;
    public void NovoJogo(){
        //SceneManager.LoadScene(faseJogo);
        SceneManager.LoadScene("Global");
        SceneManager.LoadSceneAsync("Test", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Fase 0", LoadSceneMode.Additive);
    }

    public void Salvar(){

    }

    public void SairJogo(){
        Debug.Log("Sair do Jogo");

        //Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;

        //Jogo Compilado
        //Application.Quit();
    }
}
