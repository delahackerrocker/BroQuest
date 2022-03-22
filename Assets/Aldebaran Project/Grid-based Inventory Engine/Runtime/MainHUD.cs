using UnityEngine;

public class MainHUD : MonoBehaviour
{
    [SerializeField]
    CharacterScreen characterScreen;

    private void Awake()
    {
        ItemList.GenerateList();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.I))
        {
            characterScreen.ToggleOpen();
        }
    }
}
