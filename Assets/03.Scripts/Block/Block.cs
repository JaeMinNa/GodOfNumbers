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
    private GameController _gameController;
    private Item[] _itemTypes;

    private void Awake()
    {
        _numberText = transform.GetChild(1).GetComponent<TextMeshPro>();
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        _itemTypes = (Item[])System.Enum.GetValues(typeof(Item));
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

        if (Hp == 0) DestroySucces();
    }

    public void SetBlockNumber(int num)
    {
        Number = num;
        _numberText.text = Number.ToString();
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

    private void DestroySucces()
    {
        ItemAbility();
        gameObject.SetActive(false);
    }

    #region Item
    private void SetItemType()
    {
        int value = Random.Range(1, 101);

        if (value <= 50) ItemType = Item.None;
        else
        {
            int value2 = Random.Range(1, _itemTypes.Length);
            ItemType = _itemTypes[value2];
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
            Time.timeScale = 0.5f;
            StartCoroutine(CORestoreTime());
        }
        else if (ItemType == Item.Destroy)
        { 
            while(true)
            {
                int num = Random.Range(0, _gameController.MaxNumber + 1);

                if (_gameController.Blocks.ContainsKey(num))
                {
                    _gameController.Blocks[num].SetActive(false);
                    _gameController.Blocks.Remove(num);
                    break;
                }
                else continue;
            }
        }
    }

    IEnumerator CORestoreTime()
    {
        yield return new WaitForSecondsRealtime(3f);

        Time.timeScale = 1f;
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
