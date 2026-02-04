using UnityEngine;

public class DropItem : MonoBehaviour
{
    public enum Item
    {
        Heal,
    }

    [SerializeField] private Item item;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.CompareTag("Player"))
        {
            Debug.Log(item);
            if (item == Item.Heal)
            {
                Player.instance.OnHeal(1);
                Destroy(gameObject);
            }
        }
    }
}
