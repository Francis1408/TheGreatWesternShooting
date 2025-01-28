using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Script that manages the exhibition of weapons status in the game UI
public class WeaponUIManager : MonoBehaviour
{
    
    public static WeaponUIManager Instance { get; private set; }
    
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponTotalAmmo;
    public Weapon currentWeapon;
    public Image currentWeaponImage;
    public GameObject weaponPanel;

    private Transform panelContainer;
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
        panelContainer = transform.Find("PanelList").transform;
        
        // ---------- TEMPORARILY LOGIC --- LATER MAKE A UNIQUE ACQUIRE WEAPON 
        for (int i = 0; i < PlayerWeaponController.Instance.weaponsOwned.Count; i++)
        {
            PlayerWeaponController.Instance.weaponsOwned[i].OnAmmoChanged += UpdateBulletDisplay; // Register all weapon Listeners
            RegisterBulletsSprites(i, PlayerWeaponController.Instance.weaponsOwned[i]); // Create panel for all owned weapons
            
        }
        
        InitializeWeaponDisplay();
     
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
    
    
    // Function to update the bullet display for a new selected weapon
    public void InitializeWeaponDisplay()
    {
        // Gets the current weapon from the PlayerWeaponController 
        currentWeapon = PlayerWeaponController.Instance.currentWeapon;
        
        // Write Weapon Name
        weaponName.text = currentWeapon.gunName;
        
        // Display gun image
        currentWeaponImage.sprite = currentWeapon.weaponSprite;
        
        // Write the amount of total ammo
        weaponTotalAmmo.text = currentWeapon.totalAmmo < 0 ? "\u221E" : currentWeapon.totalAmmo.ToString(); // Check if gu  has infinity ammo

        // Enable/Disable bullet panels
        for (int i = 0; i < PlayerWeaponController.Instance.weaponsOwned.Count; i++)
        {
            Transform selectedPanel = panelContainer.transform.GetChild(i);
            if (i != PlayerWeaponController.Instance.currentIndex)
            {
                selectedPanel.gameObject.SetActive(false);
            }
            else
            {
                selectedPanel.gameObject.SetActive(true);
            }
        }
    }
    
    private void UpdateBulletDisplay(int currentBullets)
    {
        // Update the total ammo
        weaponTotalAmmo.text = currentWeapon.totalAmmo < 0 ? "\u221E" : currentWeapon.totalAmmo.ToString(); // Check if gu  has infinity ammo
        
        Transform selectedPanel = panelContainer.transform.GetChild(PlayerWeaponController.Instance.currentIndex);
        
        // Enable/disable bullets based on the current ammo count
        for (int i = 0; i < currentWeapon.ammoCapacity; i++)
        {
            selectedPanel.GetChild(i).GetComponent<Image>().enabled = i < currentBullets;
        }
    }
    
    // Method called once to instantiate the amount of bullets in the weapon panel
    private void RegisterBulletsSprites(int index, Weapon weapon) {
        
        Instantiate(weaponPanel, panelContainer);
        Transform selectedPanel = panelContainer.transform.GetChild(index);
        for (int i = 0; i < weapon.ammoCapacity; i++)
        {
            Instantiate(weapon.weaponAmmoSprite, selectedPanel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
