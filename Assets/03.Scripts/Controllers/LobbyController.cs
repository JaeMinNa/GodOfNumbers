using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private GameObject _inventory;

    #region Inventory
    public void InventroyActive()
    {
        _inventory.SetActive(true);
    }

    public void InventroyInActive()
    {
        _inventory.SetActive(false);
    }
    #endregion
}
