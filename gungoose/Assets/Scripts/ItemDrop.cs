using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] GameObject itemToDrop;

    public void DropItem()
    {
        int ammountOfItems = Random.Range(1, 5);

        for (int i = 1; i < ammountOfItems; i++)
        {
            Instantiate(itemToDrop);
        }
    }

}
