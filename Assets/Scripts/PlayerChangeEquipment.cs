using UnityEngine;

public class PlayerChangeEquipment : MonoBehaviour
{
    public PlayerCombat combat;
    public PlayerBow bow;

    void Update()
    {
        if (Input.GetButtonDown("ChangeEquipment"))
        {
            bow.enabled = !bow.enabled;
        }
    }
}
