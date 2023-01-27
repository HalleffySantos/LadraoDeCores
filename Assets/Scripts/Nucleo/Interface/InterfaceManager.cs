using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization;
using System.IO;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;


public class InterfaceManager : MonoBehaviour
{
    private IPlayer player;
    //[SerializeField] private string faseJogo;
     void Start()
    {
        var player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
    }

    public void NovoJogo(){
        //SceneManager.LoadScene(faseJogo);
        
        SceneManager.LoadScene("Fase 0");
        SceneManager.LoadSceneAsync("sala 1", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("sala 2", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("sala 3", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("sala 4", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Global", LoadSceneMode.Additive);
    }

    //  public void LoadGame()
    // { 
    //     //SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");
    //     Debug.Log("Testar Salvar:" +  SaveData.current.profile.ultimaSala);
    //      Debug.Log("Testar Salvar:" +  SaveData.current.profile.vermelho);
    // }

    public void LoadGame()
    {
        //player.LoadPosition();
        string filePath = Application.persistentDataPath + "/gameSave.txt";
        if (File.Exists(filePath))
        {
            string saveData = File.ReadAllText(filePath);
            string[] data = saveData.Split(',');

            float x = float.Parse(data[0]);
            float y = float.Parse(data[1]);
            float z = float.Parse(data[2]);

            Debug.Log("===> "+x+" - "+y+" - "+z);
            //transform.position = new Vector3(x, y + 2, -10);
    
        }
        SceneManager.LoadScene("Fase 0");
        SceneManager.LoadSceneAsync("sala 1", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Global", LoadSceneMode.Additive);
    }

    public void SairJogo(){
        Debug.Log("Sair do Jogo");

        //Editor Unity
        UnityEditor.EditorApplication.isPlaying = false;

        //Jogo Compilado
        //Application.Quit();
    }
}
