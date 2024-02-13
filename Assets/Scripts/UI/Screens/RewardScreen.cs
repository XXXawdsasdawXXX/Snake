using UI.Components;
using UnityEngine;

namespace UI.Screens
{
    public class RewardScreen : Screen
    {
        [SerializeField] private CoinTable _coinTable;

        public void SetReward(string value)
        {
            _coinTable.SetValue(value);
        }
    }
}