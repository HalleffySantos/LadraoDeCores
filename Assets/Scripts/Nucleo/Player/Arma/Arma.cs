using Assets.Scripts.Enumeradores;
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag(GameObjectsTags.PlayerTag.Value).GetComponent<IPlayer>();
        colliderArma = GetComponent<Collider2D>();
        spriteRendArma = GetComponent<SpriteRenderer>();
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
        colliderArma.isTrigger = true;
        spriteRendArma.color = new Color(spriteRendArma.color.r, spriteRendArma.color.g, spriteRendArma.color.b, 0);
    }

    private void Slash()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            colliderArma.isTrigger = false;
            spriteRendArma.color = new Color(spriteRendArma.color.r, spriteRendArma.color.g, spriteRendArma.color.b, 1);
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            colliderArma.isTrigger = true;
            spriteRendArma.color = new Color(spriteRendArma.color.r, spriteRendArma.color.g, spriteRendArma.color.b, 0);
        }
    }

    private void ConfiguracaoArma()
    {
        PosicaoArma();
        DirecaoArma();
    }

    private void PosicaoArma()
    {
        var posArma = new Vector3((player.direcaoMovimento.x < 0 ? -1 : 1) * distArmaPlayer.x, distArmaPlayer.y, 0);
        gameObject.transform.position = player.GetPosicao() + posArma;
    }

    private void DirecaoArma()
    {
        gameObject.transform.rotation = new Quaternion(0, player.direcaoMovimento.x < 0 ? 180 : 0, 0, 0);
    }
}
