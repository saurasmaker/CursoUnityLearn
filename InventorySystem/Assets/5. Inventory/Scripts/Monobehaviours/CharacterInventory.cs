using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    #region Variable Declarations
    public static CharacterInventory instance;

    public CharacterStats charStats;
    GameObject foundStats;

    public Image[] hotBarDisplayHolders = new Image[4];
    public GameObject InventoryDisplayHolder;
    public Image[] inventoryDisplaySlots = new Image[30];

    int inventoryItemCap = 20;
    int idCount = 1;
    bool addedItem = true;

    public Dictionary<int, InventoryEntry> itemsInInventory = new Dictionary<int, InventoryEntry>();
    public InventoryEntry itemEntry;
    #endregion

    #region Initializations
    void Start()
    {
        instance = this;
        itemEntry = new InventoryEntry(0, null, null);
        itemsInInventory.Clear();

        inventoryDisplaySlots = InventoryDisplayHolder.GetComponentsInChildren<Image>();

        foundStats = GameObject.FindGameObjectWithTag("Player");
        charStats = foundStats.GetComponent<CharacterStats>();
    }
    #endregion

    void Update()
    {
        #region Watch for Hotbar Keypresses - Called by Character Controller Later
        //Checking for a hotbar key to be pressed
        // TODO: Add some keypresses
        #endregion

        //Check to see if the item has already been added - Prevent duplicate adds for 1 item
        if (!addedItem)
        {
            TryPickUp();
        }
    }


    // TODO: Add functions

    
    public void StoreItem(ItemPickUp itemToStore)
    {

        addedItem = false;

        if((charStats.characterDefinition.currentDamage + 
            itemToStore.itemDefinition.itemWeight) <= charStats.characterDefinition.maxEncumbrance)
        {
            itemEntry.invEntry = itemToStore;
            itemEntry.stackSize = 1;
            itemEntry.hbSprite = itemToStore.itemDefinition.itemIcon;

            itemToStore.gameObject.SetActive(false);
        }

    }

    void TryPickUp()
    {
        bool itsInInv = true;

        if (itemEntry.invEntry)
        {
            if(itemsInInventory.Count == 0)
            {
                addedItem = AddItemToInv(addedItem);
            }
            else
            {
                if (itemEntry.invEntry.itemDefinition.isStackable)
                {
                    foreach(KeyValuePair<int, InventoryEntry> ie in itemsInInventory)
                    {
                        if(itemEntry.invEntry.itemDefinition == ie.Value.invEntry.itemDefinition)
                        {
                            ie.Value.stackSize += 1;
                            AddItemToHotBar(ie.Value);
                            itsInInv = true;
                            DestroyObject(itemEntry.invEntry.gameObject);
                            break;
                        }
                        else
                        {
                            itsInInv = false;
                        }
                    }
                }
                else
                {
                    itsInInv = false;
                    if(itemsInInventory.Count == inventoryItemCap)
                    {
                        itemEntry.invEntry.gameObject.SetActive(true);
                        Debug.Log("Inventory is Full");
                    }
                }

                if (!itsInInv)
                {
                    addedItem = AddItemToInv(addedItem);
                    itsInInv = true;
                }
            }
        }
    }

    bool AddItemToInv(bool finishedAdding)
    {

        itemsInInventory.Add(
            idCount,
            new InventoryEntry(
                itemEntry.stackSize,
                Instantiate(itemEntry.invEntry),
                itemEntry.hbSprite
            )
        );

        DestroyObject(itemEntry.invEntry.gameObject);

        FillInventoryDisplay();
        AddItemToHotBar(itemsInInventory[idCount]);

        idCount = IncreaseID(idCount);

        #region Reset itemEntry

        itemEntry.invEntry = null;
        itemEntry.stackSize = 0;
        itemEntry.hbSprite = null;

        #endregion

        finishedAdding = true;

        return finishedAdding;
    }

    int IncreaseID(int currentID)
    {
        int newID = 1;

        return 0;
    }

    private void AddItemToHotBar(InventoryEntry itemForHotBar)
    {

    }

    void DisplayInventory()
    {

    }

    void FillInventoryDisplay()
    {

    }

    public void TriggerItemUse(int itemToUseID)
    {

    }
}