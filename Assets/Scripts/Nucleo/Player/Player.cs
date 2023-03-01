using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Enumeradores.Animacoes;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

// Script referente as reponsábilidade do player.
public class Player : MonoBehaviour, IPlayer
{
    // Efeito sonoro referente a morte.
    public AudioClip somMortePlayer;

    // Recebe a direção que o player está se movendo.
    public Vector3 direcaoMovimento { get; private set; }
    
    // Recebe e insere se o player pode ou não se mover.
    public bool movimentoHabilitado { get; set; }

    // Recebe e insere se o player está no chão ou não.
    public bool estaNoChao { get; set; }

    // Recebe o id do ultimo objeto que o player esteve em contato.
    public int ultimoObjetoEmContato { get; private set; }

    private Animator animatorPlayer;

    private Rigidbody2D playerRigidbody;

    private TrailRenderer playerTr;

    private float velocidadeMaxPlayer;

    private float forcaPulo;
    
    private bool podePular;

    private float limiteInferior;

    private IList<Collider2D> objetosEmContato;

    private IList<Collision2D> objetosEmColisao;

    private bool pulou;

    private bool podeDash;
    private bool estaNoDash;
    private float forcaDash;
    private float tempoDash;
    private float cooldownDash;

    private bool podeEscalar;
    private Vector3 dirInicialEscalada;

    private IGameManager gameManager;

    private AudioSource audioSourcePlayer;

    private int corAtualPlayer;

    // Start is called before the first frame update
    void Start()
    {

        movimentoHabilitado = true;
        estaNoChao = false;

        velocidadeMaxPlayer = 4.5f;
        forcaPulo = 7f;
        limiteInferior = -5f;
        direcaoMovimento = new Vector3(0, 0, 0);

        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        animatorPlayer = gameObject.GetComponent<Animator>();
        playerTr = gameObject.GetComponent<TrailRenderer>();
        audioSourcePlayer = gameObject.GetComponent<AudioSource>();

        objetosEmContato = new List<Collider2D>();
        objetosEmColisao = new List<Collision2D>();
        pulou = false;
        podePular = true;

        podeDash = true;
        estaNoDash = false;
        forcaDash = 12f;
        tempoDash = 0.2f;
        cooldownDash = 1.5f;

        podeEscalar = true;

        gameManager = GameObject.FindGameObjectWithTag(GameObjectsTags.GameManagerTag.Value).GetComponent<IGameManager>();

        LoadGamePosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (estaNoDash || !movimentoHabilitado)
        {
            return;
        }

        MovimentoPular();
        MovimentoDash();
        VerificaAcaoStay();
        EscalaParede();
        TrocaCor();
    }

    // FixedUpdate is called a predefined number of times per second
    void FixedUpdate()
    {
        if (estaNoDash || !movimentoHabilitado)
        {
            return;
        }

        Movimento();
        //ForaDosLimites();

        ConfiguracaoAnimacaoPlayer();
    }

    // Mata o player.
    public void Morte()
    {
        movimentoHabilitado = false;
        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.gravityScale = 0;

        audioSourcePlayer.PlayOneShot(somMortePlayer);

        animatorPlayer.SetBool(TriggersAnimacaoPlayer.Morte.Value, true);
    }

    // Recebe a posição do player.
    public Vector3 GetPosicao()
    {
        return gameObject.transform.position;
    }

    // Inicia a animação de ataque do player.
    public void ComecaAnimacaoDeAtaque()
    {
        animatorPlayer.SetBool(TriggersAnimacaoPlayer.Ataque.Value, true);
    }

    // Termina a animação de ataque do player.
    public void TerminaAnimacaoDeAtaque()
    {
        animatorPlayer.SetBool(TriggersAnimacaoPlayer.Ataque.Value, false);
    }

    // Configurações para o player se prender a uma parede.
    public void ComecaEscalarParede()
    {
        animatorPlayer.SetBool(TriggersAnimacaoPlayer.Escalar.Value, true);

        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.angularVelocity = 0;

        playerRigidbody.gravityScale = 0.1f;

        dirInicialEscalada = new Vector3(direcaoMovimento.x, direcaoMovimento.y, 0);
    }
  
    // Configurações para o player parar de se prender a uma parede.
    public void TerminaEscalarParede()
    {
        animatorPlayer.SetBool(TriggersAnimacaoPlayer.Escalar.Value, false);
        playerRigidbody.gravityScale = 1.0f;
    }

    // Recuo vertical infringido ao player após acertar um inimigo/objeto com a arma.
    public void RecuoVertical()
    {
        var dir = new Vector3();
        //dir.y = direcaoMovimento.y == 0 ? 0 : (direcaoMovimento.y > 0 ? 1 : -1);
        //dir.y = direcaoMovimento.y == 0 ? 0 : (direcaoMovimento.y > 0 ? 1 : -1);
        dir.y = -1;

        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.angularVelocity = 0;

        playerRigidbody.AddForce(dir * (-1) * forcaPulo, ForceMode2D.Impulse);
    }

    // Instance id do terreno que o player está em contato.
    public int TerrenoEmContato()
    {
        return objetosEmColisao.Where(x => x.gameObject.GetComponent<ITerreno>() != null).FirstOrDefault().gameObject.GetInstanceID();
    }

    // A cor atual do player.
    public Color CorDoPlayer()
    {
        return gameObject.GetComponent<SpriteRenderer>().color;
    }

    // Salva a posição do jogador.
    public void SaveGame()
    {
        var posXPlayer = GetPosicao().x;
        var posYPlayer = GetPosicao().y;

        PlayerPrefs.SetFloat("posXPlayer", posXPlayer);
        PlayerPrefs.SetFloat("posYPlayer", posYPlayer);
        PlayerPrefs.SetInt("corAtualPlayer", corAtualPlayer);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        objetosEmColisao.Add(col);
        VerificaInteracaoEntrada(col.gameObject.GetComponent<IInteracao>());
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        objetosEmColisao.Remove(col);
        ultimoObjetoEmContato = col.gameObject.GetInstanceID();
        VerificaInteracaoSaida(col.gameObject.GetComponent<IInteracao>());
    }

    private void VerificaAcaoStay()
    {
        foreach (var col in objetosEmContato)
        {
            var interacao = col.gameObject.GetComponent<IInteracao>();
            if (interacao != null)
            {
                interacao.AcaoStay(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        objetosEmContato.Add(col);
        VerificaInteracaoEntrada(col.gameObject.GetComponent<IInteracao>());
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        ultimoObjetoEmContato = col.gameObject.GetInstanceID();
        objetosEmContato.Remove(col);
        VerificaInteracaoSaida(col.gameObject.GetComponent<IInteracao>());
    }

    private void Movimento()
    {
        if (!movimentoHabilitado)
        {
            direcaoMovimento = new Vector3(0, 0 , 0);
            return;
        }

        direcaoMovimento = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {   
            var direcao = new Vector3(direcaoMovimento.x, 0, 0);
            gameObject.transform.rotation = new Quaternion(0, direcao.x < 0 ? 180 : 0, 0, 0);

            gameObject.transform.position += direcao * Time.fixedDeltaTime * velocidadeMaxPlayer;
        }
    }

    private void MovimentoPular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao && podePular)
        {
            StartCoroutine(Pulo());
        }
    }

    private IEnumerator Pulo()
    {
        podePular = false;
        pulou = true;
        estaNoChao = false;
        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.angularVelocity = 0;
        playerRigidbody.AddForce(Vector3.up * forcaPulo, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.5f);
        podePular = true;
    }

    private void MovimentoDash()
    {
        if (Input.GetKeyDown(KeyCode.K) && podeDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        podeDash = false;
        estaNoDash = true;
        float originalGravity = playerRigidbody.gravityScale; 
        playerRigidbody.gravityScale = 0f;
        playerRigidbody.velocity = new Vector2(transform.localScale.x * forcaDash * ((direcaoMovimento.x < 0) ? -1 : 1), 0f);
        playerTr.emitting = true;

        yield return new WaitForSeconds(tempoDash);

        playerTr.emitting = false;
        playerRigidbody.gravityScale = originalGravity;
        playerRigidbody.velocity = new Vector2(0, 0);
        estaNoDash = false;
        
        yield return new WaitForSeconds(cooldownDash);
        podeDash = true;
    }

    private void ForaDosLimites()
    {
        if (gameObject.transform.position.y < limiteInferior)
        {
            Morte();
        }
    }

    private void ConfiguracaoAnimacaoPlayer()
    {
        AnimacaoAndar();
        AnimacaoPular();
    }

    private void AnimacaoAndar()
    {
        if (EstaAndando())
        {
            animatorPlayer.SetBool(TriggersAnimacaoPlayer.Andar.Value, true);
        }

        if (!EstaAndando() && animatorPlayer.GetBool(TriggersAnimacaoPlayer.Andar.Value))
        {
            animatorPlayer.SetBool(TriggersAnimacaoPlayer.Andar.Value, false);
        }
    }

    private void AnimacaoPular()
    {
        if (EstaPulando())
        {
            animatorPlayer.SetBool(TriggersAnimacaoPlayer.Pular.Value, true);
        }

        if (!EstaPulando() && animatorPlayer.GetBool(TriggersAnimacaoPlayer.Pular.Value))
        {
            animatorPlayer.SetBool(TriggersAnimacaoPlayer.Pular.Value, false);
        }
    }

    private bool EstaAndando()
    {
        return direcaoMovimento.x > 0 || direcaoMovimento.x < 0 ? true : false;
    }

    private bool EstaPulando()
    {
        if (estaNoChao && pulou)
        {
            pulou = false;
        }

        return pulou && !estaNoChao;
    }

    private bool EstaEscalando()
    {
        return animatorPlayer.GetBool(TriggersAnimacaoPlayer.Escalar.Value);
    }

    private void VerificaInteracaoEntrada(IInteracao interacao)
    {
        if (interacao != null)
        {
            interacao.AcaoEntrada(gameObject);
        }
    }

    private void VerificaInteracaoSaida(IInteracao interacao)
    {
        if (interacao != null)
        {
            interacao.AcaoSaida(gameObject);
        }
    }

    private void ConfiguracoesMorte()
    {
        animatorPlayer.speed = 0;
        GameObject.FindGameObjectWithTag(GameObjectsTags.CameraTag.Value).GetComponent<ICamera>().PlayerMorreu();
        GameManager.LoadGame();
    }

    private void EncerraPulo()
    {
        pulou = false;
    }

    private void EscalaParede()
    {
        if (podeEscalar)
        {
            StartCoroutine(ConfEscalaParede());
        }
    }

    private IEnumerator ConfEscalaParede()
    {
        if (animatorPlayer.GetBool(TriggersAnimacaoPlayer.Escalar.Value))
        {
            podeEscalar = false;
            yield return new WaitForSeconds(0.2f);

            bool mesmaDir = (direcaoMovimento.x > 0 && dirInicialEscalada.x > 0) ? true : (direcaoMovimento.x < 0 && dirInicialEscalada.x < 0) ? true : (direcaoMovimento.x == 0 && direcaoMovimento.y == 0) ? true : false;

            if (mesmaDir)
            {
                playerRigidbody.velocity = new Vector2(0, 0);
                playerRigidbody.angularVelocity = 0;
                estaNoChao = true;
            }
        }
        
        podeEscalar = true;
    }

    private void LoadGamePosition()
    {
        float posY = 0;
        float posX = 0;
        int qtd = 0;

        if (PlayerPrefs.HasKey("posYPlayer"))
        {
            posY = PlayerPrefs.GetFloat("posYPlayer");
            qtd++;
        }

        if (PlayerPrefs.HasKey("posXPlayer"))
        {
            posX = PlayerPrefs.GetFloat("posXPlayer");
            qtd++;
        }

        if (qtd == 2)
        {
            gameObject.transform.position = new Vector3(posX, posY, 0);
        }

        corAtualPlayer = 0;
        if (PlayerPrefs.HasKey("corAtualPlayer"))
        {
            corAtualPlayer = PlayerPrefs.GetInt("corAtualPlayer");
            gameObject.GetComponent<SpriteRenderer>().color = corAtualPlayer == 0 ? gameManager.Cinza : gameManager.Amarelo;
        }
    }

    private void TrocaCor()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            gameObject.GetComponent<SpriteRenderer>().color = gameManager.Cinza;
            corAtualPlayer = 0;
        }

        else if (Input.GetKeyDown(KeyCode.I) && gameManager.AmareloEncontrado)
        {
            gameObject.GetComponent<SpriteRenderer>().color = gameManager.Amarelo;
            corAtualPlayer = 1;
        }
    }

}
