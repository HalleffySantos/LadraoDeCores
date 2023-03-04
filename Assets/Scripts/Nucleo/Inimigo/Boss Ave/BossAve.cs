using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class BossAve : MonoBehaviour, IInteracao, IBossAve, IInimigo
{
    public float tempoDeAguardo;

    private float tempoInicioBusca;

    private bool podePerseguir;

    private bool momentoAnterior;

    private Vector3 direcaoInicial;

    private IList<Vector3> posParaSeguir;

    private IPlayer player;

    private Rigidbody2D enemyRigidbody;

    private IGameManager gameManager;

    private ICamera cameraGame;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value) != null)
        {
            gameManager = GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value).GetComponent<IGameManager>();
        }

        if (GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value) != null)
        {
            player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        }

        if (GameObject.FindGameObjectWithTag(GameObjectsTags.CameraTag.Value) != null)
        {
            cameraGame = GameObject.FindGameObjectWithTag(GameObjectsTags.CameraTag.Value).GetComponent<ICamera>();
        }

        enemyRigidbody = GetComponent<Rigidbody2D>();

        enemyRigidbody.gravityScale = 0;
        momentoAnterior = false;
        podePerseguir = false;
        posParaSeguir = new List<Vector3>();

        LoadBoss();
    }

    // Update is called once per frame
    void Update()
    {
        EstaVivo();
        FollowPlayer();
    }

    public void AcaoEntrada(GameObject tObject)
    {
        if (tObject.GetComponent<IPlayer>() != null)
        {
            tObject.GetComponent<IPlayer>().Morte();
            return;
        }
    }

    public void AcaoSaida(GameObject tObject)
    {

    }

    public void AcaoStay(GameObject tObject)
    {

    }

    public void ComecaAPerseguir()
    {
        podePerseguir = true;
    }

    public void SaveBossState()
    {
        int persegue = podePerseguir ? 1 : 0;
        PlayerPrefs.SetInt("podePerseguir", persegue);
    }

    private void FollowPlayer()
    {
        if (!podePerseguir || !player.movimentoHabilitado)
        {
            return;
        }

        posParaSeguir.Add(player.GetPosicao());

        if (!momentoAnterior)
        {
            SaveBossState();
            momentoAnterior = true;
            tempoInicioBusca = Time.time;
            direcaoInicial = gameObject.transform.position - posParaSeguir[0];
        }

        if (Time.time >= tempoInicioBusca + tempoDeAguardo && player.movimentoHabilitado)
        {
            gameObject.transform.position = posParaSeguir[0];
            posParaSeguir.Remove(posParaSeguir[0]);
        }

        else 
        {
            Vector3 direcao = gameObject.transform.position - posParaSeguir[0];
            gameObject.transform.position -= direcaoInicial * Time.deltaTime * 1/tempoDeAguardo;
        }
    }

    private void EstaVivo()
    {
        if (gameObject.transform.position.x < 70 && gameObject.transform.position.y < 7)
        {
            podePerseguir = false;
            SaveBossState();

            Destroy(gameObject);
        }
    }

    private void LoadBoss()
    {
        bool persegue = false;

        if (PlayerPrefs.HasKey("podePerseguir"))
        {
            var aux = PlayerPrefs.GetInt("podePerseguir");
            persegue = (aux == 1) ? true : false;
        }

        if (persegue)
        {
            podePerseguir = true;
        }
    }

}
