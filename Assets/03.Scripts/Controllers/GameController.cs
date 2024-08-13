using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject _calculationFormulaes;
    private ObjectPoolController _objectPool;
    private float _playTime;

    [Header("GameData")]
    public int MaxNumber;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text[] _changeCountText;
    [HideInInspector] public int Hp;
    [HideInInspector] public int ChangeCount;
    private int _score;

    [Header("Block")]
    public Dictionary<float, GameObject> Blocks;
    public float BlockCreateTime;

    [Header("Complete")]
    [SerializeField] private GameObject _completePanel;
    [SerializeField] private TMP_Text _completeScoreText;
    [SerializeField] private GameObject _bestLabel;
    [SerializeField] private TMP_Text _completeTime;
    [SerializeField] private TMP_Text _completeCoinText;
    private bool _finishGame;

    [Header("Audio")]
    [SerializeField] private AudioMixer _audioMixer;

    private void Awake()
    {
        Blocks = new Dictionary<float, GameObject>();
        _objectPool = GameObject.FindWithTag("ObjectPool").GetComponent<ObjectPoolController>();
        ChangeCount = 2;
    }

    private void Start()
    {
        GameManager.I.SoundManager.StartBGM("GameBGM");
        SetactiveCalculationFormula();
        StartCoroutine(COCreateBlock());
        _score = 0;
        Hp = 3;
        _playTime = 0f;
        _finishGame = false;
        GetScore(0);
        GetHp(0);
        SetAudio();
    }

    private void Update()
    {
        if (_score < 4000) MaxNumber = 10;
        else MaxNumber = 15;

        if (Hp < 1 && !_finishGame)
        {
            _finishGame = true;
            FinishGame();
        }

        PlayTime();
    }

    #region CalculationFormula
    public void SetactiveCalculationFormula()
    {
        for (int i = 0; i < _calculationFormulaes.transform.childCount; i++)
        {
            _calculationFormulaes.transform.GetChild(i).gameObject.SetActive(false);
        }

        int index = 0;
        if(_score <= 100000) index = Random.Range(0, 1);

        _calculationFormulaes.transform.GetChild(index).gameObject.SetActive(true);
    }
    #endregion

    #region Block
    IEnumerator COCreateBlock()
    {
        yield return new WaitForSeconds(0.5f);
        

        while (true)
        {
            if (Blocks.Count >= MaxNumber + 1) continue;
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

    public void GetHp(int value)
    {
        Hp += value;
        _hpText.text = "HP : " + Hp + " / 3";
    }

    public void GetChangeCount(int value)
    {
        ChangeCount += value;

        for (int i = 0; i < _changeCountText.Length; i++)
        {
            _changeCountText[i].text = ChangeCount + "/2";
        }
    }
    #endregion

    #region GameTime
    public IEnumerator CORestoreTime()
    {
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
    }

    private void PlayTime()
    {
        _playTime += Time.unscaledDeltaTime;
    }
    #endregion

    #region Audio
    private void SetAudio()
    {
        float sfx = PlayerPrefs.GetFloat("SFX");
        float bgm = PlayerPrefs.GetFloat("BGM");

        if (sfx == -40f)
        {
            _audioMixer.SetFloat("SFX", -80f);
        }
        else
        {
            _audioMixer.SetFloat("SFX", sfx);
        }

        if (bgm == -40f)
        {
            _audioMixer.SetFloat("BGM", -80f);
        }
        else
        {
            _audioMixer.SetFloat("BGM", bgm);
        }
    }
    #endregion

    #region Complete
    public void HomeButton()
    {
        GameManager.I.SoundManager.StartSFX("ClickButton");
        GameManager.I.ScenesManager.LoadScene("LobbyScene");
    }

    private void FinishGame()
    {
        GameManager.I.SoundManager.StopBGM();
        Time.timeScale = 0f;
        _bestLabel.SetActive(false);
        int coin = _score + ((int)(_playTime / 60) * 1000);
        _completeScoreText.text = _score.ToString();
        _completeTime.text = ConvertSecondsToTimeString(_playTime);
        _completeCoinText.text = coin.ToString();
        
        if(_score >= GameManager.I.DataManager.GameData.BestScore)
        {
            _bestLabel.SetActive(true);
            GameManager.I.DataManager.GameData.BestScore = _score;
        }
        GameManager.I.DataManager.GameData.Coin += coin;

        _completePanel.SetActive(true);
        GameManager.I.DataManager.DataSave();
    }

    private string ConvertSecondsToTimeString(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60);
        int remainingSeconds = Mathf.RoundToInt(seconds % 60);

        return string.Format("{0:00}:{1:00}", minutes, remainingSeconds);
    }
    #endregion
}
