using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Script that manages the exhibition of weapons status in the game UI
public class WeaponUIManager : MonoBehaviour
{
    
    public static WeaponUIManager Instance { get; private set; }
    
    public TextMeshProUGUI weaponName;
    public Weapon currentWeapon;
    public Image currentWeaponImage;

    private Transform bulletContainer;
    private void Awake()
    {
        // Ensure there is only one instance of the WeaponController
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scene loads
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        bulletContainer = transform.Find("Panel").transform;
        InitializeWeaponDisplay();
        
        // ---------- TEMPORARILY LOGIC --- LATER MAKE A UNIQUE ACQUIRE WEAPON 
        foreach (Weapon weapon in PlayerWeaponController.Instance.weaponsOwned)
        {
            weapon.OnAmmoChanged += UpdateBulletDisplay; // Ensure that the class is delegated
        }
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
    public void InitializeWeaponDisplay()
    {
        // Gets the current weapon from the PlayerWeaponController 
        currentWeapon = PlayerWeaponController.Instance.currentWeapon;
        
        Debug.Log(currentWeapon);
        
        // Write Weapon Name
        weaponName.text = currentWeapon.gunName;
        
        // Display gun image
        currentWeaponImage.sprite = currentWeapon.weaponSprite;
        
        // Clear any existing bullets
        foreach (Transform child in bulletContainer)
        {
            Destroy(child.gameObject);
        }
        
        Debug.Log("Child count segundo" +bulletContainer.childCount);

        // Create bullets based on maxBullets
        for (int i = 0; i < currentWeapon.ammoCapacity; i++)
        {
            Instantiate(currentWeapon.weaponAmmoSprite, bulletContainer);
        }
        
        Debug.Log(currentWeapon.currentAmmo);
        // Update the display to reflect the current ammo count
        UpdateBulletDisplay(currentWeapon.currentAmmo);
    }

    private void UpdateBulletDisplay(int currentBullets)
    {
        StartCoroutine(InitializeBulletsAfterClear(currentBullets));
    }
    private IEnumerator InitializeBulletsAfterClear(int currentBullets)
    {
        yield return null;
        Debug.Log("Child count segundo" +bulletContainer.childCount);
        
        // Enable/disable bullets based on the current ammo count
        for (int i = 0; i < currentWeapon.ammoCapacity; i++)
        {
            Debug.Log(i < currentBullets);
            bulletContainer.GetChild(i).GetComponent<Image>().enabled = i < currentBullets;
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
