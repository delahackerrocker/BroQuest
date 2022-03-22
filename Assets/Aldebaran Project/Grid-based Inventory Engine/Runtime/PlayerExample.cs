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

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.AddItem(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventory.AddItem(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            inventory.AddItem(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            inventory.AddItem(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            inventory.AddItem(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            inventory.AddItem(8);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            inventory.AddItem(9);
        }
    }
}
