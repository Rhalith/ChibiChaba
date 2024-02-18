using UnityEngine;

namespace Booster
{
    public class BoosterController : MonoBehaviour
    {
        [SerializeField] private BoosterType boosterType;
        public BoosterType Type => boosterType;
    }
}