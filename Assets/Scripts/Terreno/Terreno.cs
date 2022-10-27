using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Enumeradores;
using Assets.Scripts.Interacao;
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

    public void AcaoEntrada(GameObject tObject)
    {
        if (tObject.CompareTag(GameObjectsTags.PlayerTag.Value))
        {
            tObject.GetComponent<IPlayer>().estaNoChao = true;
        }
    }

    public void AcaoSaida(GameObject tObject)
    {
        if (tObject.CompareTag(GameObjectsTags.PlayerTag.Value))
        {
            tObject.GetComponent<IPlayer>().estaNoChao = false;
        }
    }
}
