using Assets.Scripts.Enumeradores;
using Assets.Scripts.Nucleo.Terreno.ItemJump;
using Assets.Scripts.Player;
using UnityEngine;

// Script referente ao ItemJump.
public class ItemJump : Terreno
{
    private IItemJumpManager itemManager;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag(GameObjectsTags.ItemJumpManagerTag.Value) != null)
        {
            itemManager = GameObject.FindGameObjectWithTag(GameObjectsTags.ItemJumpManagerTag.Value).GetComponent<IItemJumpManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoEntrada(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            player.estaNoChao = true;
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public override void AcaoSaida(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {                
            itemManager.ItemDestruido(gameObject.transform.position.x, gameObject.transform.position.y);
            Destroy(gameObject);
        }
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public override void AcaoStay(GameObject tObject)
    {
        
    }
}
