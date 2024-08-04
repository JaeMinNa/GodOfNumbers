using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public enum Item
    {
        None,
        Hp,
        Time,
        Destroy,
    }

    public Item ItemType;
    public bool UseHp;
    public int Number;
    public int Hp;
    public float VerticalSpeed;
    public float HorizontalSpeed;
    private TextMeshPro _numberText;
    private bool _isRight;
    private bool _isDestroy;
    private GameController _gameController;
    private Item[] _itemTypes;
    private GameObject _frame;
    private GameObject _itemFrame;

    private void Awake()
    {
        _numberText = transform.GetChild(2).GetComponent<TextMeshPro>();
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        _itemTypes = (Item[])System.Enum.GetValues(typeof(Item));
        _frame = transform.GetChild(0).gameObject;
        _itemFrame = transform.GetChild(1).gameObject;
    }

    private void OnEnable()
    {
        if (UseHp)
        {
            Hp = 3;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else Hp = 1;

        SetItemType();
        _isDestroy = false;
    }

    private void Start()
    {
        int value = Random.Range(0, 2);
        if (value == 0) _isRight = true;
        else _isRight = false;
    }

    private void Update()
    {
        transform.position += Vector3.down * VerticalSpeed * Time.deltaTime;

        if(_isRight) transform.position += Vector3.right * HorizontalSpeed * Time.deltaTime;
        else transform.position -= Vector3.right * HorizontalSpeed * Time.deltaTime;

        if (Hp == 0 && !_isDestroy)
        {
            _isDestroy = true;
            DestroySuccess();
        }
    }

    #region Number
    public void SetBlockNumber(int num)
    {
        Number = num;
        _numberText.text = Number.ToString();
    }

    private void ChangeNumber()
    {
        while (true)
        {
            int num = Random.Range(0, _gameController.MaxNumber + 1);
            if (_gameController.Blocks.ContainsKey(num)) continue;
            else
            {
                SetBlockNumber(num);
                _gameController.Blocks.Add(num, transform.gameObject);
                break;
            }
        }
    }
    #endregion

    #region Destroy
    private void DestroySuccess()
    {
        _gameController.Blocks.Remove(Number);
        ItemAbility();

        if (ItemType == Item.Time)
        {
            transform.position = new Vector3(10, 0, 0);
            StartCoroutine(COInActiveObject(5f));
        }
        else gameObject.SetActive(false);
    }

    IEnumerator COInActiveObject(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        gameObject.SetActive(false);
    }


    public void ReduceHp()
    {
        Hp--;

        if (Hp == 2)
        {
            transform.localScale = new Vector3(0.75f, 0.75f, 1);
            ChangeNumber();
        }
        else if (Hp == 1)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1);
            ChangeNumber();
        }
    }
    #endregion

    #region Item
    private void SetItemType()
    {
        int value = Random.Range(1, 101);
        int value2 = Random.Range(1, _itemTypes.Length);

        if (value <= 95) ItemType = Item.None;
        else ItemType = _itemTypes[value2];

        if (ItemType == Item.None)
        {
            _frame.SetActive(true);
            _itemFrame.SetActive(false);
        }
        else
        {
            _frame.SetActive(false);
            _itemFrame.SetActive(true);

            for (int i = 0; i < _itemFrame.transform.childCount; i++)
            {
                _itemFrame.transform.GetChild(i).gameObject.SetActive(false);
            }
            _itemFrame.transform.GetChild(value2 - 1).gameObject.SetActive(true);
        }
    }

    private void ItemAbility()
    {
        if(ItemType == Item.Hp)
        {
            if (_gameController.Hp < 3) _gameController.GetHp(1);
        }
        else if(ItemType == Item.Time)
        {
            Time.timeScale = 0.1f;
            StartCoroutine(_gameController.CORestoreTime());
        }
        else if (ItemType == Item.Destroy)
        {
            if (_gameController.Blocks.Count <= 1) return;

            while(true)
            {
                int num = Random.Range(0, _gameController.MaxNumber + 1);

                if (_gameController.Blocks.ContainsKey(num))
                {
                    _gameController.Blocks[num].GetComponent<Block>().ReduceHp();
                    break;
                }
                else continue;
            }
        }
    }
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall") || collision.CompareTag("Block"))
        {
            Debug.Log("Ãæµ¹!");
            if (_isRight) _isRight = false;
            else _isRight = true;
        }
        else if (collision.CompareTag("DestroyLine"))
        {
            GameManager.I.SoundManager.StartSFX("DestroyLine");
            _gameController.Blocks.Remove(Number);
            _gameController.GetHp(-1);
            gameObject.SetActive(false);
        }
    }
}
