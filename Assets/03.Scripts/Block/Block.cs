using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Block : MonoBehaviour
{
    public int Number;
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
    }

    public void SetBlockNumber(int num)
    {
        Number = num;
        _numberText.text = Number.ToString();
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
            _gameController.Blocks.Remove(Number);
            _gameController.LoseHp(1);
            Destroy(gameObject);
        }
    }
}
