using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IGameManager
{
    private IPlayer player;

    private IList<Color> coresColetadas;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>() != null)
        {
            player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        }

        coresColetadas = new List<Color>();
        coresColetadas.Add(Color.black);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Global");
        SceneManager.LoadSceneAsync("Vila", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 8", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 9", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 10", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Sala 11", LoadSceneMode.Additive);
    }

    public void NovaCorColetada(Color cor)
    {
        coresColetadas.Add(cor);
        ResetarCoresObjetos();
    }

    public void ResetaInimigos()
    {
        foreach(var inimigo in GameObject.FindGameObjectsWithTag(GameObjectsTags.InimigoTag.Value))
        {
            Destroy(inimigo);
        }
    }

    private void ResetarCoresObjetos()
    {
        var terrenoPlayer = player.TerrenoEmContato();
        var listaTerreno = GameObject.FindGameObjectsWithTag(GameObjectsTags.TerrenoTag.Value);
        Debug.Log(listaTerreno);
        foreach (var terreno in listaTerreno)
        {
            if (terreno.GetInstanceID() == terrenoPlayer)
            {
                continue;
            }
            
            terreno.GetComponent<ITerreno>().ChangeColor(coresColetadas[(int)Random.Range(0, coresColetadas.Count)]);
        }
    }
}
