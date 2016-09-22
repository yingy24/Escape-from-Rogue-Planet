using UnityEngine;
using System.Collections;

public class EquipmentEnabler : MonoBehaviour {

    public GameObject armor;
    public GameObject sword;
    public bool hasArmor;
    public bool hasSword;


    // Update is called once per frame
    void Update () {
	    if (hasArmor)
        {
            armor.SetActive(true);
        }
        if (hasSword)
        {
            armor.SetActive(true);
        }
    }
}
