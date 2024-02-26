using TMPro;
using UnityEngine;
using EventBus;
using EventBus.Events;

namespace UI
{
    public class FinishTextController : MonoBehaviour
    {
        [SerializeField] private TMP_Text finishText;
        [SerializeField] [TextArea] private string winText;
        [SerializeField] [TextArea] private string loseText;
        [SerializeField] private GameObject restartButton;
        
        private void OnEnable()
        {
            EventBus<DisplayFinishTextEvent>.Subscribe(DisplayFinishText);
        }
        
        private void OnDisable()
        {
            EventBus<DisplayFinishTextEvent>.Unsubscribe(DisplayFinishText);
        }

        private void DisplayFinishText(DisplayFinishTextEvent @event)
        {
            finishText.text = @event.IsWin ? winText : loseText;
            finishText.enabled = true;
            restartButton.SetActive(true);
        }
    }
}