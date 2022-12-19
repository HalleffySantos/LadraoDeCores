using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores.Animacoes;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script referente as reponsábilidade do player.
public class Player : MonoBehaviour, IPlayer
{
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

    private float limiteInferior;

    private IList<Collider2D> objetosEmContato;

    private bool pulou;

    private bool estaEscalando;

    private bool podeDash;
    private bool estaNoDash;
    private float forcaDash;
    private float tempoDash;
    private float cooldownDash;

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

        objetosEmContato = new List<Collider2D>();
        pulou = false;
        estaEscalando = false;

        podeDash = true;
        estaNoDash = false;
        forcaDash = 5f;
        tempoDash = 0.2f;
        cooldownDash = 1.5f;
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
    }

    void FixedUpdate()
    {
        if (estaNoDash || !movimentoHabilitado)
        {
            return;
        }

        MovimentoHorizontal();
        ForaDosLimites();

        ConfiguracaoAnimacaoPlayer();
    }

    // Mata o player.
    public void Morte()
    {
        movimentoHabilitado = false;
        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.gravityScale = 0;

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
        estaEscalando = true;
        animatorPlayer.SetBool(TriggersAnimacaoPlayer.Escalar.Value, true);
        playerRigidbody.gravityScale = 0.1f;
    }

    
    // Configurações para o player parar de se prender a uma parede.
    public void TerminaEscalarParede()
    {
        estaEscalando = false;
        animatorPlayer.SetBool(TriggersAnimacaoPlayer.Escalar.Value, false);
        playerRigidbody.gravityScale = 1.0f;
    }

    // Recuo vertical infringido ao player após acertar um inimigo/objeto com a arma.
    public void RecuoVertical()
    {
        var dir = new Vector3();
        //dir.y = direcaoMovimento.y == 0 ? 0 : (direcaoMovimento.y > 0 ? 1 : -1);
        dir.y = direcaoMovimento.y == 0 ? 0 : (direcaoMovimento.y > 0 ? 1 : -1);

        playerRigidbody.velocity = new Vector2(0, 0);
        playerRigidbody.angularVelocity = 0;

        Debug.Log(dir);
        playerRigidbody.AddForce(dir * (-1) * forcaPulo, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        VerificaInteracaoEntrada(col.gameObject.GetComponent<IInteracao>());
    }

    private void OnCollisionExit2D(Collision2D col)
    {
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

    private void MovimentoHorizontal()
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
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            pulou = true;
            estaNoChao = false;
            playerRigidbody.velocity = new Vector2(0, 0);
            playerRigidbody.angularVelocity = 0;
            playerRigidbody.AddForce(Vector3.up * forcaPulo, ForceMode2D.Impulse);
        }
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
        SceneManager.LoadScene("Global");
        SceneManager.LoadSceneAsync("Test", LoadSceneMode.Additive);
    }

}
