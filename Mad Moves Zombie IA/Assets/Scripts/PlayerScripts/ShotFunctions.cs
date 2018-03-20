using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFunctions : MonoBehaviour
{
    public GameObject weapons;
    public WeaponsManager weaponsManager;
    private GameObject muzzle;
    private GameObject cartridge;
    
    

    // Use this for initialization
    void Start()
    {
        weaponsManager = weapons.GetComponent<WeaponsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        muzzle = GameObject.FindGameObjectWithTag("MuzzleEffect");
        cartridge = GameObject.FindGameObjectWithTag("CartridgeEffect");

    }
    public void OnStartShot()
    {
        GameObject Fire = GameObject.FindGameObjectWithTag("Fire");//Cerco il fire dell'arma 
        RaycastHit hit; //Parametro che contiene info sull'oggetto colpito
        
        weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot--;//Diminuzione proiettili
        muzzle.GetComponent<ParticleSystem>().Play();
        cartridge.GetComponent<ParticleSystem>().Play();

        Debug.DrawRay(Fire.transform.position, Fire.transform.forward * 300, Color.black);
        if (Physics.Raycast(Fire.transform.position, Fire.transform.forward, out hit, 300))
        {//Se il raggio colpisce qualcosa!
            if (hit.transform.gameObject.CompareTag("Zombie"))//Ed è uno zombie 
            {
             //Logica sugli zombie
            }

        }
    }

    public void OnStartRecharge()
    {
        if (weaponsManager.bulletsShotted <= weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsStored)
        {
            weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsStored -= weaponsManager.bulletsShotted;//Diminuzione proiettili totali
            weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot += weaponsManager.bulletsShotted;//Incremento caricatore
        }
        else if (weaponsManager.bulletsShotted > weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsStored)
        {
            weaponsManager.bulletsShotted = weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsStored;
            weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsStored -= weaponsManager.bulletsShotted;//Diminuzione proiettili totali
            weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot += weaponsManager.bulletsShotted;//Incremento caricatore
        }
        weaponsManager.animator.SetBool("IsReloading", false);// Setto a false il bool dell'animazione
    }
}
     
    
	


