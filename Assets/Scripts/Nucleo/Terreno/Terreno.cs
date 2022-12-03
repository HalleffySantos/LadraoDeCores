using Assets.Scripts.Interacao;
using Assets.Scripts.Player;
using UnityEngine;

public class Terreno : MonoBehaviour, IInteracao
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public void AcaoEntrada(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            player.estaNoChao = true;
        }
    }

    // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
    public void AcaoSaida(GameObject tObject)
    {
        var player = tObject.GetComponent<IPlayer>();
        if (player != null)
        {
            player.estaNoChao = false;
        }
    }

    // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
    public void AcaoStay(GameObject tObject)
    {
        
    }
}
