using UnityEngine;

namespace Assets.Scripts.Interacao
{
    public interface IInteracao
    {        
        //Acao de entrada gerada pelo OnCollisionEnter.
        void AcaoEntrada(GameObject tObject);

        //Acao de saida gerada pelo OnCollisionExit.
        void AcaoSaida(GameObject tObject);

        //Acao de stay gerada pelo OnCollisionStay.
        void AcaoStay(GameObject tObject);
    }
}
