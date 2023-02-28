using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
    private IPlayer player;

    public bool AmareloEncontrado { get; set; }

    public bool AmareloPrimeiroEncontro { get; set; }

    public Color Amarelo { get; private set; }

    public Color Cinza { get; private set; }

    //Primeiro carregamento do jogo, seja pelo 'novo jogo' ou 'carregar jogo'.
    public static void FirstLoadGame()
    {
        SceneManager.LoadScene("Camera");
        SceneManager.LoadSceneAsync("Global", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Vila", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 8", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 9", LoadSceneMode.Additive);
    }


    //Carregamento do jogo após a primeira instancia de carregamento, usado quando o player morre.
    public static void LoadGame()
    {
        SceneManager.UnloadSceneAsync("Global");
        SceneManager.UnloadSceneAsync("Vila");
        SceneManager.UnloadSceneAsync("Sala 8");
        SceneManager.UnloadSceneAsync("Sala 9");

        SceneManager.LoadSceneAsync("Global", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Vila", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 8", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 9", LoadSceneMode.Additive);
    }

    void Awake()
    {
        Cinza = Color.white;
        Amarelo = new Color(1, 0.9571f, 0.7311f, 1);
        AmareloEncontrado = false;
        if (PlayerPrefs.HasKey("AmareloEncontrado") && PlayerPrefs.GetInt("AmareloEncontrado") == 1)
        {
            AmareloEncontrado = true;
        }
    }

    // Chamado quando o objeto é instaciado.
    void Start()
    {
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>() != null)
        {
            player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("AmareloEncontrado", AmareloEncontrado ? 1 : 0);
    }
}
