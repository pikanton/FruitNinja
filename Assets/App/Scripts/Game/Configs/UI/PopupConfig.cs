using UnityEngine;

namespace App.Scripts.Game.Configs.UI
{
    [CreateAssetMenu(fileName = "PopupConfig", menuName = "Configs/UI/Popup")]
    public class PopupConfig : ScriptableObject
    {
        [SerializeField] public float animationDuration = 0.5f;
        
        [SerializeField] public string continueInfoText = "ПАУЗА";
        [SerializeField] public string continueButtonText = "Продолжить";
        [SerializeField] public float continueButtonFontSize = 25f;
        [SerializeField] public string restartInfoText = "ВЫ ПРОИГРАЛИ!";
        [SerializeField] public string restartButtonText = "Рестарт";
        [SerializeField] public float restartButtonFontSize = 35f;

        [SerializeField] public float startPopupAlpha = 0f;
        [SerializeField] public float endPopupAlpha = 0.9f;
    }
}