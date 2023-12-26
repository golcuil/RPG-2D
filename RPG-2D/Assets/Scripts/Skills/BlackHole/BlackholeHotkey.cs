using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BlackholeHotkey : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private KeyCode myHotKey;
    private TextMeshProUGUI myText;

    private Transform myEnemy;
    private BlackholeSkillController blackHole;

    public void SetupHotKey(KeyCode _myHotKey, Transform _myEnemy, BlackholeSkillController _myBlackHole)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshProUGUI>();

        myEnemy = _myEnemy;
        blackHole = _myBlackHole;

        myHotKey = _myHotKey;
        myText.text = _myHotKey.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(myHotKey))
        {
            blackHole.AddEnemyToList(myEnemy);

            myText.color = Color.clear;
            spriteRenderer.color = Color.clear;
        }
    }

}
