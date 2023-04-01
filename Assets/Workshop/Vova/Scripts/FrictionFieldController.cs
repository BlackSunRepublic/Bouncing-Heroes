using UnityEngine;
using Workshop;

public class FrictionFieldController : MonoBehaviour
{
    [Header("Используем тысячные. Отрицательное значение = замедление, положительное ускорение")]
    public float multiFriction = -0.5f;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Enter");
        var player = col.GetComponent<PlayerMover>();
        if (player != null)
        {
            player.CorrectCurrentVelocityMultiplier(multiFriction);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Exit");
        var player = other.GetComponent<PlayerMover>();
        if (player != null)
        {
            player.CorrectCurrentVelocityMultiplier(multiFriction * -1);
        }
    }
}
