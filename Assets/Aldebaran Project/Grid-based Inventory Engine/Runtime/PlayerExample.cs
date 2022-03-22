using UnityEngine;

public class PlayerExample : MonoBehaviour
{
    Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.AddItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.AddItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.AddItem(2);
        }
    }
}
