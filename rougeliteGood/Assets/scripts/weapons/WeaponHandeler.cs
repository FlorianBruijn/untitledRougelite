using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandeler : MonoBehaviour
{
    [SerializeField] GameObject impactPrefab;
    [SerializeField] ParticleSystem particleSystem_;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Vector3 aimPos;
    [SerializeField] Vector3 regPos;
    [SerializeField] GameObject gun;
    IWeapon pistol;
    void Start()
    {
        pistol = new Pistol();
    }
    void Update()
    {
        if (Input.GetMouseButton(1)) pistol.aim(gun, aimPos);
        else
        {
            Camera.main.fieldOfView = 60;
            gun.transform.localPosition = regPos;
        }
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        // pistol = new TestDecorator(pistol);
        // }
        if (Input.GetMouseButtonDown(0)) pistol.shoot(transform.position, transform.forward, layerMask, particleSystem_, impactPrefab);
    }
}
