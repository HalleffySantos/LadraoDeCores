using UnityEngine;

namespace Assets.Scripts.PauseInterface
{
    // Interface referente ao Player.
    public interface IPauseInterface
    { 
        bool isPaused { get; }
    }
}