using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _UIScore;
    private int _score = 0;
    private Text _textScore;

    [SerializeField] 
    private GameObject[] _UIHealth;
    [SerializeField]
    private Sprite _looseHealth;
    private int _activeHealth;

    [SerializeField]
    private GameObject _gameOver;
    void Start()
    {
        _textScore = _UIScore.GetComponent<Text>();
        _textScore.text = "SCORE: " + 0;
        _activeHealth = _UIHealth.Length-1;

    }

    private void OnEnable()
    {
        PlayerScripts.die += PlayerScripts_Die;
        PlayerScripts.damage += PlayerScripts_Damage;
        EnamyScript.destroyEnamy += EnamyScript_DestroyEnamy;
    }
    private void OnDisable()
    {
        PlayerScripts.die -= PlayerScripts_Die;
        PlayerScripts.damage -= PlayerScripts_Damage;
        EnamyScript.destroyEnamy -= EnamyScript_DestroyEnamy;
    }
    private void PlayerScripts_Die()
    {
        _gameOver.SetActive(true);
    }
    private void EnamyScript_DestroyEnamy()
    {
        _score++;
        _textScore.text = "SCORE: "+_score;

    }
    private void PlayerScripts_Damage()
    {
        _UIHealth[_activeHealth].GetComponent<Image>().overrideSprite = _looseHealth;
       _activeHealth--;
    }
}
