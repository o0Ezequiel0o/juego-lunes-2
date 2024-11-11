using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateWeaponCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private TextMeshProUGUI maxAmmoText;
    [SerializeField] private TextMeshProUGUI totalAmmoText;

    private GunController gunController;

    void Start()
    {
        gunController = FindObjectOfType<Player>().GetComponent<GunController>();
    }

    void Update()
    {
        if (gunController.HasGun)
        {
            currentAmmoText.text = gunController.EquippedGun.Ammo.ToString();
            maxAmmoText.text = gunController.EquippedGun.MaxAmmo.ToString();
            totalAmmoText.text = gunController.EquippedGun.TotalAmmo.ToString();
        }
        else
        {
            currentAmmoText.text = "0";
            maxAmmoText.text = "0";
            totalAmmoText.text = "0";
        }
    }
}