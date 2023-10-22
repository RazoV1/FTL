using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : MonoBehaviour
{
    public int coolDown;
    public int damage;
    public bool CanGoThroughShield;
    public int Rounds;
    public int MaxEnergy;
    public int UsingEnergy;
    public BasicPart target;
    public SpaceshipMainframe enemy;
    public bool isOnCooldown;

    public bool is_selecting;

    public GameObject[] powers;

    private IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(coolDown);
        isOnCooldown = false;
    }

    public void Cycle()
    {
        if (UsingEnergy == MaxEnergy)
        {
            if (!isOnCooldown && target != null)
            {
                enemy.TakeDamage(target);   
                StartCoroutine(Cooldown());
            }
        }
    }

    public void SetTarget()
    {
        if (is_selecting)
        {
            is_selecting = false;
        }
        else
        {
            is_selecting = true;
        }
    }
    public void TrySettingTarget(BasicPart part)
    {
        if (is_selecting && (part != target || target == null))
        {
            target = part;
            is_selecting=false;
        }
    }

    private void Update()
    {
        Cycle();
    }
}
