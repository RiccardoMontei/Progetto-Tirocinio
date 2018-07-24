using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotFunctions : MonoBehaviour
{
    public GameObject weapons;
    public WeaponsManager weaponsManager;
    private GameObject muzzle;
    private GameObject cartridge;
    
	public GameObject bulletTrail; //effetto scia del proiettile

	public AudioSource reload ;
	public AudioSource ak47Shot;
	public AudioSource m4a1Shot;
	public AudioSource ump45Shot;
	public AudioSource knifeHit;
    

    // Use this for initialization
    void Start()
    {
        weaponsManager = weapons.GetComponent<WeaponsManager>();
    }

    // Update is called once per frame
    void Update()
    {

        

    }
    public void OnStartShot()
    {
		muzzle = GameObject.FindGameObjectWithTag("MuzzleEffect");
		cartridge = GameObject.FindGameObjectWithTag("CartridgeEffect");

        GameObject Fire = GameObject.FindGameObjectWithTag("Fire");//Cerco il fire dell'arma 
        RaycastHit hit; //Parametro che contiene info sull'oggetto colpito
        
        weaponsManager.YourWeapon().GetComponent<WeaponsDettails>().bulletsToShot--;//Diminuzione proiettili
		switch (weaponsManager.YourWeapon().gameObject.name)
		{
		case "Ak-47":
			ak47Shot.Play();
			break;
		case "M4A1_Sopmod":
			m4a1Shot.Play();
			break;
		case "UMP-45":
			ump45Shot.Play();
			break;

		}
        muzzle.GetComponent<ParticleSystem>().Play();
		bulletTrail.GetComponent<ParticleSystem> ().Play();
        cartridge.GetComponent<ParticleSystem>().Play();

        Debug.DrawRay(Fire.transform.position, Fire.transform.forward * 300, Color.black);
        if (Physics.Raycast(Fire.transform.position, Fire.transform.forward, out hit, 300))
        {//Se il raggio colpisce qualcosa!
			switch (hit.transform.tag) {
			case "block":
				hit.transform.GetComponent<ZombieScript> ().TakeDamage (GetComponentInChildren<WeaponsDettails> ().damageForBullets);
				break;
			case "fence":
				hit.transform.GetComponent<FenceLife> ().DecreaseLife (GetComponentInChildren<WeaponsDettails> ().damageForBullets / 2);
				break;
			}
        }
    }

	public void OnStartKnifeHit(){
		knifeHit.Play ();
	}

	public void OnMiddleOfChangeWeapon(){
		if (weaponsManager.changeWeapon) {
			if (weaponsManager.weaponChanged) {
				weaponsManager.yourWeapons [1].SetActive (true);
			}
			if (!weaponsManager.weaponChanged) {
				weaponsManager.yourWeapons [0].SetActive (true);
			}
			weaponsManager.changeWeapon = false;
		}
	}
    public void OnStartRecharge()
    {
		reload.Play ();
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
     
    
	


