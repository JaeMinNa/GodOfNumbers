using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _calculationFormulaes;
    private ObjectPoolController _objectPool;

    [Header("GameData")]
    public int MaxNumber;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _scoreText;
    private int _hp;
    private int _score;

    [Header("Block")]
    public Dictionary<float, GameObject> Blocks;
    public float BlockCreateTime;

    private void Awake()
    {
        Blocks = new Dictionary<float, GameObject>();
        _objectPool = GameObject.FindWithTag("ObjectPool").GetComponent<ObjectPoolController>();
    }

    private void Start()
    {
        SetactiveCalculationFormula();
        StartCoroutine(COCreateBlock());
        _score = 0;
        _hp = 3;
        GetScore(0);
        LoseHp(0);
    }

    private void Update()
    {
        if (_score < 4000) MaxNumber = 10;
        else MaxNumber = 50;
    }


    #region CalculationFormula
    public void SetactiveCalculationFormula()
    {
        for (int i = 0; i < _calculationFormulaes.transform.childCount; i++)
        {
            _calculationFormulaes.transform.GetChild(i).gameObject.SetActive(false);
        }

        int index = 0;
        if(_score <= 5000) index = Random.Range(0, 1);

        _calculationFormulaes.transform.GetChild(index).gameObject.SetActive(true);
    }
    #endregion

    #region Block
    IEnumerator COCreateBlock()
    {
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            CreateBlock(MaxNumber);

            yield return new WaitForSeconds(BlockCreateTime);
        }
    }

    private void CreateBlock(int range) // 0 ~ range Áß ·£´ý »ý¼º
    {
        while (true)
        {
            int num = Random.Range(0, range + 1);
            if (Blocks.ContainsKey(num)) continue;
            else
            {
                float value = Random.Range(-2f, 2f);
                GameObject obj = _objectPool.CreateBlock(RandomBlockName(), new Vector3(value, 5.2f, 0));
                obj.GetComponent<Block>().SetBlockNumber(num);
                Blocks.Add(num, obj);
                break;
            }
        }
    }

    private string RandomBlockName()
    {
        int value = Random.Range(1, 101);

        if (value >= 81 && value <= 85) return "Block1";
        else if (value >= 86 && value <= 90) return "Block0_Rotation";
        else if (value >= 91 && value <= 95) return "Block1_Rotation";
        else if (value >= 96 && value <= 100) return "BlockBoss";
        else return "Block0";
    }
    #endregion

    #region GameData
    public void GetScore(int value)
    {
        _score += value;
        _scoreText.text = "Score : " + _score;
    }

    public void LoseHp(int value)
    {
        _hp -= value;
        _hpText.text = "HP : " + _hp + " / 3";
    }
    #endregion
}
