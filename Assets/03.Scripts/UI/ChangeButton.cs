using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeButton : MonoBehaviour
{
    private GameController _gameController;

    private void Awake()
    {
        _gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void ChangeCalculationFormulaButton()
    {
        if(_gameController.ChangeCount > 0)
        {
            _gameController.ChangeCount--;
            _gameController.SetactiveCalculationFormula();
        }
    }
}
