using UnityEngine;

namespace Assets.Scripts.Interacao
{
    // Responsável por delagar a reponsábilidade de uma colisão ou trigger.
    public interface IInteracao
    {        
        // Método para realização da ação chamada pelo OnTriggerEnter ou OnCollisionEnter.
        void AcaoEntrada(GameObject tObject);

        // Método para realização da ação chamada pelo OnTriggerExit ou OnCollisionExit.
        void AcaoSaida(GameObject tObject);

        // Método para realização da ação chamada pelo OnTriggerStay ou OnCollisionStay.
        void AcaoStay(GameObject tObject);
    }
}
