using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Script that manages the exhibition of weapons status in the game UI
public class WeaponUIManager : MonoBehaviour
{
    
    public TextMeshProUGUI weaponName;
    public Weapon currentWeapon;

    private Transform bulletContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletContainer = transform.Find("Panel").transform;
        InitializeBulletDisplay();
        currentWeapon.OnAmmoChanged += UpdateBulletDisplay; // Ensure that the class is delegated
    }
    // It subscribes to the currentWeapon.OnAmmoChanged event so that it can
    // update the UI when the ammo count changes.
    private void OnEnable()
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnAmmoChanged += UpdateBulletDisplay;
        }
    }
    // It unsubscribes from the currentWeapon.OnAmmoChanged event
    // to prevent the UI from being updated unnecessarily while the BulletDisplay is inactive.
    private void OnDisable()
    {
        if (currentWeapon != null)
        {
            currentWeapon.OnAmmoChanged -= UpdateBulletDisplay;
        }
    }
    
    
    // Function to update the bullet display for a new weapon
    public void InitializeBulletDisplay()
    {
        // Gets the current weapon from the PlayerWeaponController 
        currentWeapon = PlayerWeaponController.Instance.currentWeapon;
        
        // Clear any existing bullets
        foreach (Transform child in bulletContainer)
        {
            Destroy(child.gameObject);
        }

        // Create bullets based on maxBullets
        for (int i = 0; i < currentWeapon.ammoCapacity; i++)
        {
            Instantiate(currentWeapon.weaponAmmoSprite, bulletContainer);
        }

        // Update the display to reflect the current ammo count
        UpdateBulletDisplay(currentWeapon.currentAmmo);
    }
    
    public void UpdateBulletDisplay(int currentBullets)
    {
        
        // Enable/disable bullets based on the current ammo count
        for (int i = 0; i < bulletContainer.childCount; i++)
        {
            bulletContainer.GetChild(i).GetComponent<Image>().enabled = i < currentBullets;
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
