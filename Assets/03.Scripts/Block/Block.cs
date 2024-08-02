using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public bool UseHp;
    public int Number;
    public int Hp;
    public float VerticalSpeed;
    public float HorizontalSpeed;
    private TextMeshPro _numberText;
    private bool _isRight;
    private GameController _gameController;

    private void Awake()
    {
        _numberText = transform.GetChild(1).GetComponent<TextMeshPro>();
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void OnEnable()
    {
        if (UseHp)
        {
            Hp = 3;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else Hp = 1;
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

        if (Hp == 0) gameObject.SetActive(false);
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
            transform.localScale = new Vector3(0.8f, 0.8f, 1);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Wall"))
        {
            if (_isRight) _isRight = false;
            else _isRight = true;
        }
        else if (collision.CompareTag("DestroyLine"))
        {
            GameManager.I.SoundManager.StartSFX("DestroyLine");
            _gameController.Blocks.Remove(Number);
            _gameController.LoseHp(1);
            gameObject.SetActive(false);
        }
    }
}
