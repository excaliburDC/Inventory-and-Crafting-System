
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryObj;
    [SerializeField] private KeyCode[] toggleInventory;


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < toggleInventory.Length; i++)
        {
            if(Input.GetKeyDown(toggleInventory[i]))
            {
                inventoryObj.SetActive(!inventoryObj.activeSelf);

                break;
            }
        }
    }
}
