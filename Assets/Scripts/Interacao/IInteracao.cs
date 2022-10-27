using UnityEngine;

namespace Assets.Scripts.Interacao
{
    public interface IInteracao
    {        
        void AcaoEntrada(GameObject tObject);

        void AcaoSaida(GameObject tObject);
    }
}
