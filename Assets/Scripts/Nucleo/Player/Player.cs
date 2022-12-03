using System.Collections.Generic;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IPlayer
{
    public Vector3 direcaoMovimento { get; private set; }
    
    public bool movimentoHabilitado { get; set; }

    public bool estaNoChao { get; set; }

    private Animator animatorPlayer;

    private Rigidbody2D playerRigidbody;

    private float velocidadeMaxPlayer;

    private float forcaPulo;

    private float limiteInferior;

    private IList<Collider2D> objetosEmContato;

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

        objetosEmContato = new List<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimentoPular();
        VerificaAcaoStay();
    }

    void FixedUpdate()
    {
        MovimentoHorizontal();
        ForaDosLimites();

        ConfiguracaoAnimacaoPlayer();
    }

    public void Morte()
    {
        SceneManager.LoadScene("Fase 0", LoadSceneMode.Single);
    }

    public Vector3 GetPosicao()
    {
        return gameObject.transform.position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        VerificaInteracaoEntrada(col.gameObject.GetComponent<IInteracao>());
    }

    private void OnCollisionExit2D(Collision2D col)
    {
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

        direcaoMovimento = new Vector3(Input.GetAxis("Horizontal"), 0 , 0);
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {   
            gameObject.transform.rotation = new Quaternion(0, direcaoMovimento.x < 0 ? 180 : 0, 0, 0);

            gameObject.transform.position += direcaoMovimento * Time.fixedDeltaTime * velocidadeMaxPlayer;
        }
    }

    private void MovimentoPular()
    {
        if (!movimentoHabilitado)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            playerRigidbody.AddForce(Vector3.up * forcaPulo, ForceMode2D.Impulse);
        }
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
        AnimacaoParada();
        
        //AnimacaoPular();
        //AnimacaoFala();
        
    }

    private void AnimacaoAndar()
    {
        if (EstaAndando())
        {
            animatorPlayer.SetBool("EstaAndando", true);
        }
    }

    private void AnimacaoParada()
    {
        if (!EstaAndando() && animatorPlayer.GetBool("EstaAndando"))
        {
            animatorPlayer.SetBool("EstaAndando", false);
        }
    }

    private bool EstaAndando()
    {
        return direcaoMovimento.x > 0 || direcaoMovimento.x < 0 ? true : false;
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

}
