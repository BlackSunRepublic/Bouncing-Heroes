using UnityEngine;

namespace Workshop
{
    public class PlayerCoinCollector : MonoBehaviour
    {
        [SerializeField]
        private CoinCounter _coinCounter;

        private void OnTriggerEnter2D(Collider2D colCoin)
        {
            Coin _coin = colCoin.GetComponent<Coin>();
            if (_coin != null)
            {
                _coinCounter.AddCoin();
                Destroy(colCoin.gameObject, 0.1f);
            }
        }
    }
}