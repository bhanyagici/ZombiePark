using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float coolDown = 0.1f;
    float lastFireTime = 0;
    public int defaultAmmo = 120;
    public int magSize = 30;
    public int currentAmmo;
    public int currentMagAmmo;
    public Camera camera;
    public int range;
    [Header("Gun Damage On Hit")]
    public int damage;
    public GameObject bloodPrefab;
    public GameObject decalPrefab;
    public GameObject magObject;
    public ParticleSystem muzzleParticle;
    

    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = defaultAmmo - magSize;
        currentMagAmmo = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reloaad();
        }


        if (Input.GetMouseButton(0))
        {
            if (CanFire())
            {
                Fire();
            }
            
        }
    }

    private void Reloaad()
    {
        if (currentAmmo == 0)
        {
            return;
        }

        if (currentAmmo==0 || currentMagAmmo == magSize)
        {
            return;
        }
       
        if (currentAmmo < magSize)
        {
            currentMagAmmo = currentMagAmmo + currentAmmo;
            currentAmmo = 0;
           
        }
        else
        {
            currentAmmo -= magSize - currentAmmo;
            currentMagAmmo = magSize;

        }
        GameObject newMagobject = Instantiate(magObject);
        newMagobject.transform.position = magObject.transform.position;
        newMagobject.AddComponent<Rigidbody>();

    }

    private bool CanFire()
    {
   
        if (currentMagAmmo > 0 && lastFireTime + coolDown < Time.time)
        {
            lastFireTime = Time.time + coolDown;
            return true;
        }
            return false;
    }

    private void Fire()
    {
        //Fire() fonksiyonu kameram�n merkezinden cameramin ilerisine do�ru 10 metrelik bir raycast yap�yor.
        muzzleParticle.Play(true);
        currentMagAmmo -= 1;
        Debug.Log("Kalan Mermim: " + currentMagAmmo);
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward,out hit,10))
        {
            if (hit.transform.tag == "Zombie")//E�er vurulan obje Zombie tagli bir obje ise
            {
                hit.transform.GetComponent<ZombieHealth>().Hit(damage);//Objenin transformundan ZombieHealth scriptini bul ve ona
                //damage kadar hasar ver.
                GenerateBloodEffect(hit);//Zombiyi vurdu�umuzda kan efektinin olu�mas�n� sa�layan metod.
                //Bu fonksiyonu kullanan da Raycast de�i�keninde olan hit fonksiyonu.
            }
            else
            {
                GenerateHitEffect(hit);
            }
        }
    }

    private void GenerateHitEffect(RaycastHit hit)
    {
        //TODO: Mermi izi olu�tur.
        GameObject hitObject = Instantiate(decalPrefab, hit.point, Quaternion.identity);
        hitObject.transform.rotation = Quaternion.FromToRotation(decalPrefab.transform.forward*-1,hit.normal);
        //Objenin ilerisi zemin ile 90 derece yap�yor olsun.
    }

    private void GenerateBloodEffect(RaycastHit hit)
    {
        //GameObject de��ken tipindeki bloodObject de�i�keni = ate� etti�im yerde kan efektini Insantiate(Yaratmak,olu�turmak) et.
        GameObject bloodObject = Instantiate(bloodPrefab, hit.point, hit.transform.rotation);
    }
}
