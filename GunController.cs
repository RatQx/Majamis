using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyCharacter;

public class GunController : MonoBehaviour
{
    //Gun settings
    public float autofireRate = 0.6f;
    public float fireRate = 0.2f;
    public int clipSize = 12;
    public bool autofireing = false;
    public int reservedAmmoCapacity = 200;

    private float timer;
    private AudioSource shootingSound;
    //Ammo capacity
    bool canShoot;
    int currentAmmoInClip;
    int ammoInReserve;

    public PlayerController playerController;
    public LayerMask layermask;
    public Camera camera;
    //Aiming
    public Vector3 normalLocalPositionl;
    public Vector3 aimingLocalPosition;
    public float aimSmoothing = 10;

    public Character character;
    //Mouse settings
    public float mouseSensitivity = 1;
    Vector2 currentRotation;
    public float weaponSwayAmount = 10;

    //Weapon recoil
    public bool randomizeRecoil;
    public Vector2 randomRecoilConstraints;
    public Vector2 recoilPattern;

    private void Start()
    {
        currentAmmoInClip = clipSize;
        ammoInReserve = reservedAmmoCapacity;
        canShoot = true;
        shootingSound = GetComponent<AudioSource>();
        timer = fireRate;
    }
    private void Update()
    {
        DetermineAim();
        if (Input.GetMouseButton(1))
        {
            playerController.offset = new Vector3(0.5f,0,0);
            DetermineRotation();
            if (Input.GetMouseButtonDown(0) && canShoot && currentAmmoInClip > 0)
            {
                Debug.Log("1");
                canShoot = false;
                currentAmmoInClip--;
                StartCoroutine(ShootGun(fireRate));
                shootingSound.Play();
                autofireing = false;
                timer = autofireRate;
            }
            else if (Input.GetMouseButton(0) && canShoot && currentAmmoInClip > 0)
            {
                Debug.Log("2");
                if (autofireing==false)
                {
                    timer -= Time.deltaTime;
                    if(timer <= 0)
                    {
                        autofireing = true;
                    }
                }
                else
                {
                    canShoot = false;
                    currentAmmoInClip--;
                    StartCoroutine(ShootGun(autofireRate));
                    shootingSound.Play();
                }
            }
            else if (Input.GetKeyDown(KeyCode.R) && currentAmmoInClip < clipSize && ammoInReserve > 0)
            {
                int amountNeeded = clipSize - currentAmmoInClip;
                if (amountNeeded >= ammoInReserve)
                {
                    currentAmmoInClip += ammoInReserve;
                    ammoInReserve -= amountNeeded;
                }
                else
                {
                    currentAmmoInClip = clipSize;
                    ammoInReserve -= amountNeeded;
                }
            }
        }
        else
        {
            playerController.offset = new Vector3(0, 0, 0);
            character.RotationSettings._useControlRotation = false;
            character.RotationSettings._orientRotationToMovement = true;

            if (Input.GetKeyDown(KeyCode.R) && currentAmmoInClip < clipSize && ammoInReserve > 0)
            {
                int amountNeeded = clipSize - currentAmmoInClip;
                if (amountNeeded >= ammoInReserve)
                {
                    currentAmmoInClip += ammoInReserve;
                    ammoInReserve -= amountNeeded;
                }
                else
                {
                    currentAmmoInClip = clipSize;
                    ammoInReserve -= amountNeeded;
                }
            }
        }
        
    }

    void DetermineRotation()
    {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseAxis *= mouseSensitivity;
        currentRotation += mouseAxis;
        character.RotationSettings._useControlRotation = true;
        character.RotationSettings._orientRotationToMovement = false;

        //currentRotation.y = Mathf.Clamp(currentRotation.y, -90, 90);

        //transform.localPosition += (Vector3)mouseAxis * weaponSwayAmount / 1000;

        //transform.root.localRotation = Quaternion.AngleAxis(currentRotation.x, Vector3.up);
        //transform.parent.localRotation = Quaternion.AngleAxis(-currentRotation.y, Vector3.right);

    }

    void DetermineAim()
    {
        Vector3 target = normalLocalPositionl;
        if (Input.GetMouseButton(1)) target = aimingLocalPosition;

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
        transform.localPosition = desiredPosition;
    }

    void DetermineRecoil()
    {
        transform.localPosition -= Vector3.forward * 0.1f;

        if (randomizeRecoil)
        {
            float xRecoil = Random.Range(-randomRecoilConstraints.x, randomRecoilConstraints.x);
            float yRecoil = Random.Range(-randomRecoilConstraints.y, randomRecoilConstraints.y);

            Vector2 recoil = new Vector2(xRecoil, yRecoil);
            currentRotation += recoil;

        }

    }

    IEnumerator ShootGun(float fireRate)
    {
        DetermineRecoil();
        RayCastForEnemy();
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }


    IEnumerator Delay(float fireRate)
    {
        yield return new WaitForSeconds(fireRate);
        
        autofireing = true;
    }
    void RayCastForEnemy()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1 << LayerMask.NameToLayer("Enemy")))
        if (Physics.Raycast(camera.transform.position,camera.transform.forward, out hit , 1 << LayerMask.NameToLayer("Enemy")))
        {
            try
            {
                Debug.Log("Hit an enemy");
                Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(transform.parent.transform.forward * 500);

            }
            catch
            {

            }

        }
    }

}
