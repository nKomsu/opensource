using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GunNum_Scripit : MonoBehaviour
{
    private TextMeshProUGUI gun_num;
    private TextMeshProUGUI max_num;

    WeaponManager gunStat;
    public Player player;//

    private int maxAmmo;
    private int curAmmo;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PLAYER").GetComponent<Player>();//
        gunStat = player.CurWeapon;//
        this.gun_num = GameObject.Find("Current_Num").GetComponent<TextMeshProUGUI>();
        this.max_num = GameObject.Find("Max_Num").GetComponent<TextMeshProUGUI>();
        maxAmmo = gunStat.MaxAmmo;
        curAmmo = gunStat.CurAmmo;
        this.max_num.text = maxAmmo.ToString();
        this.gun_num.text = curAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmo();
        //SwapWeapon();
    }

    void UpdateAmmo()
    {
        gunStat = player.CurWeapon;//
        maxAmmo = gunStat.MaxAmmo;
        curAmmo = gunStat.CurAmmo;
        this.max_num.text = maxAmmo.ToString();
        this.gun_num.text = curAmmo.ToString();
    }

    void SwapWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
        {

        }
    }

}
