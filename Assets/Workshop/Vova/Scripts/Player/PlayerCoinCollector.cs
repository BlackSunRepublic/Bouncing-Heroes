using UnityEngine;

namespace Workshop
{
    public class PlayerCoinCollector : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D colCoin)
        {
            Coin _coin = colCoin.GetComponent<Coin>();
            if (_coin != null)
            {
                GameManager.instance.AddCoins();
                Destroy(colCoin.gameObject);
            }
        }
    }
}