using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] itemSlots;
    public UseItem useItem;
    public int gold;
    public TextMeshProUGUI goldTxt;

    private void Start()
    {
        foreach(var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }

    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;
    }

    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;
    }

    public void AddItem(ItemSO itemSO, int quantity)
    {
        if (itemSO.isGold)
        {
            gold += quantity;
            goldTxt.text = gold.ToString();
            return;
        }
        else
        {
            foreach(var slot in itemSlots)
            {
                if(slot.itemSO == null)
                {
                    slot.itemSO = itemSO;
                    slot.quantity = quantity;
                    slot.UpdateUI();
                    return;
                }
            }
        }
    }

    public void UseItem(InventorySlot slot)
    {
        if(slot.itemSO != null && slot.quantity >= 0)
        {
            useItem.ApplyItemEffects(slot.itemSO);

            slot.quantity--;
            if(slot.quantity <= 0)
            {
                slot.itemSO = null;
            }
            slot.UpdateUI();
        }
    }
}
