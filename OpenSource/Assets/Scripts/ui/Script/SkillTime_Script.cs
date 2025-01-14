using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTime_Script : MonoBehaviour
{
    Player PLAYER; //�߰���
    private Image img_skill;
    bool checkc = false;
    // Start is called before the first frame update
    void Start()
    {
        PLAYER = GameObject.Find("PLAYER").GetComponent<Player>(); //�߰���
        this.img_skill = GameObject.Find("img_fill").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && checkc == false && !PLAYER.isJump) //�����߰�
        {
            StartCoroutine(CoolTime(5f));
            checkc = true;
        }
        if (this.img_skill.fillAmount == 1.0f)
        {
            checkc = false;
        }
    }
    IEnumerator CoolTime(float cool)
    {
        while (cool > 0.0f)
        {
            cool -= Time.deltaTime;
            this.img_skill.fillAmount = 1.0f - cool / 5f;
            yield return new WaitForFixedUpdate();
        }
    }
}
