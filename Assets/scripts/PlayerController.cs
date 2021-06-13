using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    KeyCode rMove = KeyCode.D;
    [SerializeField]
    KeyCode lMove = KeyCode.A;
    [SerializeField]
    GameTrigger enamyContactTrigger;
    [SerializeField]
    float MoveCorfitientOnContactEnemy = 0.3f;

    [SerializeField]
    KeyCode jump = KeyCode.W;
    [SerializeField]
    KeyCode fall = KeyCode.S;

    [SerializeField]
    WeaponKey[] weaponKeys;

    [SerializeField]
    Unit unit;

    Weapon ActiveWeapon = null;

    private void Update()
    {

        if ((!Input.GetKey(rMove) && !Input.GetKey(lMove)) || (Input.GetKey(rMove) && Input.GetKey(lMove)))
        {

        }
        else
        {
            if (Input.GetKey(rMove))
                unit.Move(Vector2.right);
            if (Input.GetKey(lMove))
                unit.Move(Vector2.left);
        }


        //if ((!Input.GetKey(rMove) && !Input.GetKey(lMove)) || (Input.GetKey(rMove) && Input.GetKey(lMove)))
        //    unit.Move(0);
        //else
        //{
        //    if (Input.GetKey(rMove))
        //        unit.Move(enamyContactTrigger.Enter ? MoveCorfitientOnContactEnemy : 1f);
        //    if (Input.GetKey(lMove))
        //        unit.Move(enamyContactTrigger.Enter ? -MoveCorfitientOnContactEnemy : -1f);
        //}
        if (Input.GetKeyDown(jump))
            unit.Jump();
        if (Input.GetKeyDown(fall))
            unit.Fall();


        for (int i = 0; i < weaponKeys.Length; i++)
        {
            if (Input.GetKeyUp(weaponKeys[i].keyCode) && ActiveWeapon == weaponKeys[i].weapon)
            {
                ActiveWeapon = null;
                weaponKeys[i].weapon.StopFire();
            }
            if (Input.GetKeyDown(weaponKeys[i].keyCode) && ActiveWeapon == null)
            {
                weaponKeys[i].weapon.StartFire();
                ActiveWeapon = weaponKeys[i].weapon;
            }
        }
    }

    [System.Serializable]
    public class WeaponKey
    {
        public KeyCode keyCode;
        public Weapon weapon;
    }
}
