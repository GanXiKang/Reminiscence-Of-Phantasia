using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StorySkillControl_Prince : MonoBehaviour
{
    [Header("Musia")]
    public AudioSource BGM;
    public AudioClip rot, cli, ope, suc, fai;
    private Coroutine current;
    bool isPlayOnce = true;

    [Header("ClockUI")]
    public GameObject clockUI;
    public GameObject pointer;
    public Button stopTime;
    public Button skillClock;
    public Button back;
    public static bool isClockActice = false;
    public static bool isDisabledClock = false;
    bool isCheckZone = false;
    bool isRotating = false;
    bool isIncreasing = false;
    bool isReducing = false;
    bool isChange = false;
    float _currentTime = 0f;
    float _duration = 3f;
    float _maxRotationSpeed = 270f;

    [Header("EnergyUI")]
    public Image energyBar;
    public Image bg;
    public Sprite bgEnergy, bgNoEnergy, bgNowOnly, bgFutureOnly;
    public static int _zoneNum;
    float _energyValue = 0.7f;
    float _rotation = 0.008f;
    float _smallArea = 0.03f;
    float _largeArea = 0.05f;
    float _nowArea = 0.01f;
    bool isEnergyConsume = false;

    [Header("Effects")]
    public GameObject timeEF;

    //Plot
    bool isRecoverEnergy;
    public static bool isFirstUse = true;
    public static bool isGainEnegry = false;
    public static bool isFirstBackNow = false;
    public static float _gainEnegryValue = 0;
    public static int _goPast = 0;

    void Update()
    {
        ObjectActive();
        KeyButton();
        ClockRotating();
        CheckCurrentZone();
        Energy();
        FirstUseRecoverEnergy();
        GainEnegry();
    }

    void ObjectActive()
    {
        clockUI.SetActive(isClockActice);
        stopTime.interactable = ButtonTimeInteractable();
        back.interactable = ButtonTimeInteractable() && !isRotating;
        skillClock.interactable = !isDisabledClock;
    }
    bool ButtonTimeInteractable()
    {
        return !isIncreasing &&
               !isReducing &&
               !isRecoverEnergy &&
               !isChange &&
               !StoryLoadingScene_Prince.isOpen &&
               energyBar.fillAmount > 0;
    }

    void KeyButton()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isDisabledClock && StoryUIControl_Prince.isSkillActive)
        {
            if (!isClockActice)
            {
                Button_ClockActive();
            }
            else
            {
                Button_Back();
            }
        }
    }
    void ClockRotating()
    {
        if (!isRotating) return;

        if (isIncreasing)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _duration)
            {
                _currentTime = _duration;
                isIncreasing = false;
            }
        }
        float _rotationSpeed = Mathf.Lerp(0f, _maxRotationSpeed, _currentTime / _duration);
        pointer.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        _energyValue -= _rotation * Time.deltaTime;
    }
    void CheckCurrentZone()
    {
        if (!isCheckZone) return;

        if (isReducing)
        {
            _currentTime += Time.deltaTime;
            if (_currentTime >= _duration)
            {
                _currentTime = _duration;
                isReducing = false;
            }
        }
        float _rotationSpeed = Mathf.Lerp(_maxRotationSpeed, 0f, _currentTime / _duration);
        pointer.transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);

        if (_rotationSpeed <= 0)
        {
            float zRotation = pointer.transform.eulerAngles.z % 360;
            int zone = Mathf.FloorToInt(zRotation / 30f) + 1;
            isCheckZone = false;
            isEnergyConsume = true;
            _zoneNum = zone;
        }
    }
    void Energy()
    {
        energyBar.fillAmount = _energyValue;

        if (energyBar.fillAmount <= 0)
        {
            bg.sprite = bgNoEnergy;
            if (isRotating)
            {
                isRotating = false;
                isReducing = false;
                isCheckZone = false;
                StopCoroutine(current);
                BlackScreenControl.isOpenBlackScreen = true;
                Invoke("NoEnergyGameOver", 1f);
            }
        }
        else
        {
            if (isFirstUse)
                bg.sprite = bgFutureOnly;
            else if (isFirstBackNow)
                bg.sprite = bgNowOnly;
            else
                bg.sprite = bgEnergy;
        }

        if (!isEnergyConsume) return;

        switch (_zoneNum)
        {
            case 1:
            case 2:
            case 3:
            case 12:
                if (!StoryLoadingScene_Prince.isNowScene)
                {
                    if (_zoneNum != 3)
                        _energyValue -= _largeArea * Time.deltaTime;
                    else
                        _energyValue -= _smallArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = true;
                    StoryLoadingScene_Prince.isPastScene = false;
                    StoryLoadingScene_Prince.isFutureScene = false;
                    isChange = true;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 4:
            case 5:
            case 6:
            case 7:
                if (!StoryLoadingScene_Prince.isFutureScene)
                {
                    if (_zoneNum != 7)
                        _energyValue -= _largeArea * Time.deltaTime;
                    else
                        _energyValue -= _smallArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = false;
                    StoryLoadingScene_Prince.isPastScene = false;
                    StoryLoadingScene_Prince.isFutureScene = true;
                    isChange = true;
                    if (StoryInteractableControl_Prince.isReallyGoFutureGood)
                    {
                        isDisabledClock = true;
                        StoryGameControl_Prince.isFutureGood = true;
                        StoryInteractableControl_Prince.isGoFutureGood = true;
                        StoryInteractableControl_Prince.isPrinceInNow = false;
                        StoryInteractableControl_Prince.isReallyGoFutureGood = false;
                    }
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;

            case 8:
            case 9:
            case 10:
            case 11:
                if (!StoryLoadingScene_Prince.isPastScene && !isFirstUse && !isFirstBackNow)
                {
                    if (_zoneNum != 11)
                        _energyValue -= _largeArea * Time.deltaTime;
                    else
                        _energyValue -= _smallArea * Time.deltaTime;
                    StoryLoadingScene_Prince.isNowScene = false;
                    StoryLoadingScene_Prince.isPastScene = true;
                    StoryLoadingScene_Prince.isFutureScene = false;
                    _goPast++;
                    isChange = true;
                }
                else
                {
                    _energyValue -= _nowArea * Time.deltaTime;
                }
                break;
        }
        if (isPlayOnce)
        {
            isPlayOnce = false;
            StopCoroutine(current);
            current = null;
            if (isChange)
                BGM.PlayOneShot(suc);
            else
                BGM.PlayOneShot(fai); 
        }
        Invoke("FalseByisCheckConsume", 1f);
    }
    void NoEnergyGameOver()
    {
        isIncreasing = false;
        if (isFirstUse)
            _energyValue = 0.7f;
        else
            _energyValue = 0.15f;
    }
    void FalseByisCheckConsume()
    {
        isPlayOnce = true;
        isEnergyConsume = false;
        if (isChange)
        {
            isChange = false;
            timeEF.SetActive(true);
            StoryLoadingScene_Prince.isOpen = true;
            if (isFirstUse)
            {
                isRecoverEnergy = true;
                StoryUIControl_Prince.isDialogue = true;
                StoryDialogueControl_Prince._isAboveWho1 = 1;
                StoryDialogueControl_Prince._textCount = 5;
                isDisabledClock = true;
            }
        }
    }
    void FirstUseRecoverEnergy()
    {
        if (!isRecoverEnergy) return;

        _energyValue = Mathf.Lerp(_energyValue, 0.71f, Time.deltaTime * 0.5f);
        if (_energyValue >= 0.7f)
            isRecoverEnergy = false;
    }
    void GainEnegry()
    {
        if (!isGainEnegry) return;

        _energyValue += _gainEnegryValue;
        isGainEnegry = false;
    }

    public void Button_Time()
    {
        if (isIncreasing || isReducing) return;

        BGM.PlayOneShot(cli);
        isRotating = !isRotating;
        _currentTime = 0f;
        if (isRotating)
        {
            isIncreasing = true;
            if (current == null)
                current = StartCoroutine(PlaySoundRot());
        }
        else
        {
            isCheckZone = true;
            isReducing = true;
        }
    }
    public void Button_ClockActive()
    {
        if (StoryUIControl_Prince.isDialogue) return;
        if (StoryGhostControl_Prince.isWarp) return;
        if (StoryGhostControl_Prince.isDisappear) return;

        BGM.PlayOneShot(ope);
        if (isFirstUse || StoryInteractableControl_Prince.isReallyGoFutureGood)
        {
            isClockActice = true;
        }
        else
        {
            StoryGhostControl_Prince.isWarp = true;
            StoryGhostControl_Prince.isWatchSkill = true;
        }
    }
    public void Button_Back()
    {
        if (StoryGhostControl_Prince.isWarp) return;
        if (StoryGhostControl_Prince.isDisappear) return;
        if (isRotating) return;

        BGM.PlayOneShot(ope);
        if (isFirstUse)
        {
            isClockActice = false;
        }
        else
        {
            isClockActice = false;
            StoryGhostControl_Prince.isDisappear = true;
        }
    }

    IEnumerator PlaySoundRot()
    {
        while (true) 
        {
            BGM.PlayOneShot(rot);
            yield return new WaitForSeconds(rot.length);
        }
    }
}
