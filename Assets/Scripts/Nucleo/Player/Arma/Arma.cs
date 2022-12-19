using Assets.Scripts.Enumeradores;
using Assets.Scripts.Enumeradores.Animacoes;
using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

// Script referente as reponsábilidades da arma.
public class Arma : MonoBehaviour, IArma
{
    private SpriteRenderer spriteRendArma;

    private Collider2D colliderArma;

    private Vector3 distArmaPlayer;

    private IPlayer player;

    private Animator animatorArma;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        colliderArma = GetComponent<Collider2D>();
        spriteRendArma = GetComponent<SpriteRenderer>();
        animatorArma = GetComponent<Animator>();
        DefinicoesArma();
    }

    // Update is called once per frame
    void Update()
    {
        ConfiguracaoArma();
        Slash();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<IInteracao>() != null)
        {
            other.gameObject.GetComponent<IInteracao>().AcaoEntrada(gameObject);
        }
    }

    private void DefinicoesArma()
    {
        distArmaPlayer = new Vector3(0.3f, -0.05f, 0);
        DesabilitaArma();
    }

    private void Slash()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            HabilitaArma();

            animatorArma.SetBool(TriggersAnimacaoArma.Ataque.Value, true);
            player.ComecaAnimacaoDeAtaque();
        }
    }

    private void ConfiguracaoArma()
    {
        PosicaoArma();
        DirecaoArma();
    }

    private void TerminaSlash()
    {
        DesabilitaArma();

        animatorArma.SetBool(TriggersAnimacaoArma.Ataque.Value, false);
        player.TerminaAnimacaoDeAtaque();
    }

    private void PosicaoArma()
    {
        gameObject.transform.position = player.GetPosicao();
    }

    private void DirecaoArma()
    {
        var dirX = player.direcaoMovimento.x;
        var dirY = player.direcaoMovimento.y;

        var anguloZ = 0;
        var anguloY = 0;
        if (dirX > 0 && dirY > 0)
        {
            anguloZ = 45;
        }

        if (dirX > 0 && dirY < 0)
        {
            anguloZ = 315;
        }

        if (dirX == 0 && dirY > 0)
        {
            anguloZ = 90;
        }

        if (dirX == 0 && dirY < 0)
        {
            anguloZ = 270;
        }

        if (dirX < 0 && dirY > 0)
        {
            anguloZ = 45;
            anguloY = 180;
        }

        if (dirX < 0 && dirY == 0)
        {
            anguloY = 180;
        }

        if (dirX < 0 && dirY < 0)
        {
            anguloZ = 315;
            anguloY = 180;
        }

        gameObject.transform.rotation = Quaternion.Euler(0, anguloY, anguloZ);
    }

    private void HabilitaArma()
    {
        colliderArma.isTrigger = false;
        spriteRendArma.color = new Color(spriteRendArma.color.r, spriteRendArma.color.g, spriteRendArma.color.b, 1);
    }

    private void DesabilitaArma()
    {
        colliderArma.isTrigger = true;
        spriteRendArma.color = new Color(spriteRendArma.color.r, spriteRendArma.color.g, spriteRendArma.color.b, 0);
    }
}
