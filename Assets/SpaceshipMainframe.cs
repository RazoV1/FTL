using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipMainframe : MonoBehaviour
{
    [Header("Basa")]
    public int Hull;
    private int Sum;
    public BasicPart[] parts;
    public EnergyStorage energy;
    [Header("Weaponary")]
    public BasicPart Weaponary;
    [Header("Shield")]
    public BasicPart Shields;
    public int ProtLayers;
    private int LayersInCooldown;
    public bool Is_protected;
    public int ShieldTimeOut;
    [Header("Engine")]
    public BasicPart Engine;
    [Header("Controll")]
    public BasicPart ControllRoom;

    private IEnumerator Restore_Shield()
    {
        yield return new WaitForSeconds(4);
        ProtLayers++;
        LayersInCooldown--;
    }

    private void UpdateHull()
    {
        Sum = 0;
        foreach (BasicPart part in parts)
        {
            Sum += part.HP;
        }
        Hull = Sum;
    }
    
    private void UpdateShields()
    {
        
        if (Shields.UsingEnergy % 2 == 0 || Shields.HP<=0)
        {
            ProtLayers = (Shields.UsingEnergy / 2) - LayersInCooldown;
        }
    }

    public void TakeDamage(BasicPart part)
    {
        if (ProtLayers > 0)
        {
            LayersInCooldown++;
            ProtLayers--;
            StartCoroutine(Restore_Shield());
        }
        else
        {
            if (part.gameObject.name == "Shields")
            {
                part.HP -= 2;
                if (part.MaxEnergy >= 2)
                {
                    part.MaxEnergy -= 2;
                    
                    if (part.UsingEnergy >= 2)
                    {
                        part.UsingEnergy -= 2;
                        energy.FreePoints += 2;
                    }
                }
            }
            else
            {
                part.HP -= 2;
            }
        }
    }
    public void Update()
    {
        UpdateHull();
        UpdateShields();
    }
}
