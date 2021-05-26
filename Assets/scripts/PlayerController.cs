using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    KeyCode rMove = KeyCode.D;
    [SerializeField]
    KeyCode lMove = KeyCode.A;
    [SerializeField]
    KeyCode jamp = KeyCode.W;

    [SerializeField]
    WeaponKey[] weaponKeys;

    [SerializeField]
    Unit unit;

    private void Update()
    {
        if (Input.GetKey(rMove))
            unit.Move(1f);
        if (Input.GetKey(lMove))
            unit.Move(-1f);
        if (Input.GetKeyDown(jamp))
            unit.Jump();

        for (int i = 0; i < weaponKeys.Length; i++)
        {
            if (Input.GetKeyDown(weaponKeys[i].keyCode))
                weaponKeys[i].weapon.StartFire();
            if (Input.GetKeyUp(weaponKeys[i].keyCode))
                weaponKeys[i].weapon.StopFire();
        }
    }

    [System.Serializable]
    public class WeaponKey
    {
        public KeyCode keyCode;
        public Weapon weapon;
    }
}
