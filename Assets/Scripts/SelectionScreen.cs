using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.ComponentModel;
using static SelectionScreen;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using Unity.VisualScripting;

public class SelectionScreen : MonoBehaviour
{
    #region GUI variables
    [EditorFoldout("Return/Journal GUI objects")]
    public ReturnJournal ReturnJournalGUIObjects;
    [System.Serializable]
    public class ReturnJournal
    {
        public GameObject ReturnBtn;
        public GameObject ReturnSelectedBtn;
        public GameObject JournalBtn;
        public GameObject JournalSelectedBtn;
        public GameObject BackspaceTxt;
        public GameObject AD;
        public GameObject ReturnJournalInactive;
    }

    [EditorFoldout("Play/LevelSelect GUI objects")]
    public PlayLevelSelect PlayLevelselectGUIObjects;
    [System.Serializable]
    public class PlayLevelSelect
    {
        public GameObject PlayLevelselect;
        public GameObject ReturnBtn;
        public GameObject ReturnSelectedBtn;
        public GameObject UpArrowBtn;
        public GameObject UpArrowSelectedBtn;
        public GameObject PlayBtn;
        public GameObject PlaySelectedBtn;
        public GameObject LevelselectBtn;
        public GameObject LevelselectSelectedBtn;
        public GameObject DownArrowBtn;
        public GameObject DownArrowSelectedBtn;
        public GameObject LeftRightBackspaceSpaceEnter;
        public GameObject LevelselectInactive;
        public GameObject PlayLevelselectInactive;
    }

    [EditorFoldout("LevelSelection GUI objects")]
    public LevelSelection LevelselectionGUIObjects;
    [System.Serializable]
    public class LevelSelection
    {
        public GameObject Levelselection;
        public GameObject ReturnBtn;
        public GameObject ReturnSelectedBtn;
        public GameObject UpArrowBtn;
        public GameObject UpArrowSelectedBtn;
        public GameObject IntroBtn;
        public GameObject IntroSelectedBtn;
        public GameObject MinigameBtn;
        public GameObject MinigameSelectedBtn;
        public GameObject QuizTextBtn;
        public GameObject QuizTextSelectedBtn;
        public GameObject QuizSoundBtn;
        public GameObject QuizSoundSelectedBtn;
        public GameObject DownArrowBtn;
        public GameObject DownArrowSelectedBtn;
        public GameObject LeftRightBackspaceSpaceEnter;
        public GameObject LevelselectionInactive;
    }

    [EditorFoldout("AnimalSelection GUI objects")]
    public AnimalSelection AnimalselectionGUIObjects;
    [System.Serializable]
    public class AnimalSelection
    {
        public GameObject LeftArrowBtn;
        public GameObject LeftArrowSelectedBtn;
        public GameObject HummingbirdBtn;
        public GameObject HummingbirdSelectedBtn;
        public GameObject HummingbirdSilhoutteBtn;
        public GameObject HummingbirdSilhoutteSelectedBtn;
        public GameObject SurgeonfishBtn;
        public GameObject SurgeonfishSelectedBtn;
        public GameObject SurgeonfishSilhoutteBtn;
        public GameObject SurgeonfishSilhoutteSelectedBtn;
        public GameObject ChameleonBtn;
        public GameObject ChameleonSelectedBtn;
        public GameObject ChameleonSilhoutteBtn;
        public GameObject ChameleonSilhoutteSelectedBtn;
        public GameObject RightArrowBtn;
        public GameObject RightArrowSelectedBtn;
        public GameObject ADSpaceEnter;
        public GameObject AnimalselectionInactive;
    }

    [EditorFoldout("AnimalPictures GUI objects")]
    public AnimalPictures AnimalpicturesGUIObjects;
    [System.Serializable]
    public class AnimalPictures
    {
        public GameObject HummingbirdImg;
        public GameObject HummingbirdSilhoutteImg;
        public GameObject SurgeonfishImg;
        public GameObject SurgeonfishSilhoutteImg;
        public GameObject ChameleonImg;
        public GameObject ChameleonSilhoutteImg;
    }
    #endregion

    public Inputs Inputs;
    public bool IsNewGame /*{ get; set; }*/ = true;
    public bool IsHummingbirdActive /*{ get; set; }*/ = false;
    public bool IsSurgeonfishActive /*{ get; set; }*/ = false;
    public bool IsChameleonActive /*{ get; set; }*/ = false;

    #region AnimalSelection
    private GameObject[] _animalBtns;
    private GameObject[] _animalSelectedBtns;
    private GameObject[] _animalSilhoutteBtns;
    private GameObject[] _animalSilhoutteSelectedBtns;
    private int _currentAnimalButtonIndex = 0;
    private bool _canSelectAnimal = true;
    private bool _isAnimalSelectionHorizontalMovePressed = false;
    #endregion

    #region PlayLevelSelect
    private GameObject[] _playLevelSelectBtns;
    private GameObject[] _playLevelSelectSelectedBtns;
    private GameObject[] _playReturnBtns;
    private GameObject[] _playReturnSelectedBtns;
    private int _currentPlayLevelSelectIndex = 0;
    private int _currentPlayReturnIndex = 0;
    private bool _isPlayLevelSelectVerticalMovePressed = false;
    private bool _isPlayReturnHorizontalMovePressed = false;
    private bool _canSelectPlayLevelSelect = false;
    private bool _isActionComfirmButtonReleased = false;


    private bool actionTriggered = false;
    #endregion

    void Awake()
    {
        SetGameGui();

        if(IsNewGame)
        {
            IsHummingbirdActive = false;
            IsSurgeonfishActive = false;
            IsChameleonActive = false;
        }

        FillAnimalBtnArrays();
        FillPlayLevelSelectReturnBtnArrays();
    }

    private void OnEnable()
    {
        InputSystem.onBeforeUpdate += OnBeforeUpdateHandler;
    }

    private void OnDisable()
    {
        InputSystem.onBeforeUpdate -= OnBeforeUpdateHandler;
    }

    private void SetGameGui()
    {
        ReturnJournalGUIObjects.ReturnBtn.SetActive(true);
        ReturnJournalGUIObjects.ReturnSelectedBtn.SetActive(false);
        ReturnJournalGUIObjects.JournalBtn.SetActive(true);
        ReturnJournalGUIObjects.JournalSelectedBtn.SetActive(false);
        ReturnJournalGUIObjects.BackspaceTxt.SetActive(true);
        ReturnJournalGUIObjects.AD.SetActive(false);
        ReturnJournalGUIObjects.ReturnJournalInactive.SetActive(false);

        PlayLevelselectGUIObjects.PlayLevelselect.SetActive(true);
        PlayLevelselectGUIObjects.ReturnBtn.SetActive(true);
        PlayLevelselectGUIObjects.ReturnSelectedBtn.SetActive(false);
        PlayLevelselectGUIObjects.UpArrowBtn.SetActive(true);
        PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(false);
        PlayLevelselectGUIObjects.PlayBtn.SetActive(true);
        PlayLevelselectGUIObjects.PlaySelectedBtn.SetActive(false);
        PlayLevelselectGUIObjects.LevelselectBtn.SetActive(true);
        PlayLevelselectGUIObjects.LevelselectSelectedBtn.SetActive(false);
        PlayLevelselectGUIObjects.DownArrowBtn.SetActive(true);
        PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(false);
        PlayLevelselectGUIObjects.LeftRightBackspaceSpaceEnter.SetActive(false);
        PlayLevelselectGUIObjects.LevelselectInactive.SetActive(false);
        PlayLevelselectGUIObjects.PlayLevelselectInactive.SetActive(true);

        LevelselectionGUIObjects.Levelselection.SetActive(false);
        LevelselectionGUIObjects.ReturnBtn.SetActive(false);
        LevelselectionGUIObjects.ReturnSelectedBtn.SetActive(false);
        LevelselectionGUIObjects.UpArrowBtn.SetActive(false);
        LevelselectionGUIObjects.UpArrowSelectedBtn.SetActive(false);
        LevelselectionGUIObjects.IntroBtn.SetActive(false);
        LevelselectionGUIObjects.IntroSelectedBtn.SetActive(false);
        LevelselectionGUIObjects.MinigameBtn.SetActive(false);
        LevelselectionGUIObjects.MinigameSelectedBtn.SetActive(false);
        LevelselectionGUIObjects.QuizTextBtn.SetActive(false);
        LevelselectionGUIObjects.QuizTextSelectedBtn.SetActive(false);
        LevelselectionGUIObjects.QuizSoundBtn.SetActive(false);
        LevelselectionGUIObjects.QuizSoundSelectedBtn.SetActive(false);
        LevelselectionGUIObjects.DownArrowBtn.SetActive(false);
        LevelselectionGUIObjects.DownArrowSelectedBtn.SetActive(false);
        LevelselectionGUIObjects.LeftRightBackspaceSpaceEnter.SetActive(false);
        LevelselectionGUIObjects.LevelselectionInactive.SetActive(false);

        AnimalselectionGUIObjects.LeftArrowBtn.SetActive(true);
        AnimalselectionGUIObjects.LeftArrowSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.HummingbirdBtn.SetActive(false);
        AnimalselectionGUIObjects.HummingbirdSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.HummingbirdSilhoutteBtn.SetActive(true);
        AnimalselectionGUIObjects.HummingbirdSilhoutteSelectedBtn.SetActive(true);
        AnimalselectionGUIObjects.SurgeonfishBtn.SetActive(false);
        AnimalselectionGUIObjects.SurgeonfishSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.SurgeonfishSilhoutteBtn.SetActive(true);
        AnimalselectionGUIObjects.SurgeonfishSilhoutteSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.ChameleonBtn.SetActive(false);
        AnimalselectionGUIObjects.ChameleonSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.ChameleonSilhoutteBtn.SetActive(true);
        AnimalselectionGUIObjects.ChameleonSilhoutteSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.RightArrowBtn.SetActive(true);
        AnimalselectionGUIObjects.RightArrowSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.ADSpaceEnter.SetActive(true);
        AnimalselectionGUIObjects.AnimalselectionInactive.SetActive(false);

        AnimalpicturesGUIObjects.HummingbirdImg.SetActive(false);
        AnimalpicturesGUIObjects.HummingbirdSilhoutteImg.SetActive(true);
        AnimalpicturesGUIObjects.SurgeonfishImg.SetActive(false);
        AnimalpicturesGUIObjects.SurgeonfishSilhoutteImg.SetActive(false);
        AnimalpicturesGUIObjects.ChameleonImg.SetActive(false);
        AnimalpicturesGUIObjects.ChameleonSilhoutteImg.SetActive(false);

        _canSelectAnimal = true;
        _canSelectPlayLevelSelect = false;
    }

    private void OnBeforeUpdateHandler()
    {
        if (_canSelectAnimal)
        {
            OnAnimalSelectionHorizontalMove();
            SetAnimalPicture();
            //SetCanSelectAnimalGUIObject();
        }

        SetPlayLevelSelectActive();
       // SetCanSelectPlayLevelSelectlGUIObject();

        if (_canSelectPlayLevelSelect)
        {
            HandlePlayLevelSelectReturnVerticalHorizontalSwith();
          //  Return();
            LoadScene();
        }
    }

    #region AnimalSelection
    private void SetHummingbirdActive()
    {
        AnimalselectionGUIObjects.HummingbirdBtn.SetActive(true);
        AnimalselectionGUIObjects.HummingbirdSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.HummingbirdSilhoutteBtn.SetActive(false);
        AnimalselectionGUIObjects.HummingbirdSilhoutteSelectedBtn.SetActive(false);
    }

    private void SetHummigbirdDeactive()
    {
        AnimalselectionGUIObjects.HummingbirdBtn.SetActive(false);
        AnimalselectionGUIObjects.HummingbirdSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.HummingbirdSilhoutteBtn.SetActive(true);
        AnimalselectionGUIObjects.HummingbirdSilhoutteSelectedBtn.SetActive(false);
    }

    private void SetSurgeonfishActive()
    {
        AnimalselectionGUIObjects.SurgeonfishBtn.SetActive(true);
        AnimalselectionGUIObjects.SurgeonfishSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.SurgeonfishSilhoutteBtn.SetActive(false);
        AnimalselectionGUIObjects.SurgeonfishSilhoutteSelectedBtn.SetActive(false);
    }

    private void SetSurgeonfishDeactive()
    {
        AnimalselectionGUIObjects.SurgeonfishBtn.SetActive(false);
        AnimalselectionGUIObjects.SurgeonfishSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.SurgeonfishSilhoutteBtn.SetActive(true);
        AnimalselectionGUIObjects.SurgeonfishSilhoutteSelectedBtn.SetActive(false);
    }

    private void SetChameleonActive()
    {
        AnimalselectionGUIObjects.ChameleonBtn.SetActive(true);
        AnimalselectionGUIObjects.ChameleonSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.ChameleonSilhoutteBtn.SetActive(false);
        AnimalselectionGUIObjects.ChameleonSilhoutteSelectedBtn.SetActive(false);
    }

    private void SetChameleonDeactive()
    {
        AnimalselectionGUIObjects.ChameleonBtn.SetActive(false);
        AnimalselectionGUIObjects.ChameleonSelectedBtn.SetActive(false);
        AnimalselectionGUIObjects.ChameleonSilhoutteBtn.SetActive(true);
        AnimalselectionGUIObjects.ChameleonSilhoutteSelectedBtn.SetActive(false);
    }

    private void FillAnimalBtnArrays()
    {
        _animalBtns = new[] { AnimalselectionGUIObjects.HummingbirdBtn,
                              AnimalselectionGUIObjects.SurgeonfishBtn,
                              AnimalselectionGUIObjects.ChameleonBtn
                            };
        _animalSelectedBtns = new[] { AnimalselectionGUIObjects.HummingbirdSelectedBtn,
                                      AnimalselectionGUIObjects.SurgeonfishSelectedBtn,
                                      AnimalselectionGUIObjects.ChameleonSelectedBtn
                                    };
        _animalSilhoutteBtns = new[] { AnimalselectionGUIObjects.HummingbirdSilhoutteBtn,
                                       AnimalselectionGUIObjects.SurgeonfishSilhoutteBtn,
                                       AnimalselectionGUIObjects.ChameleonSilhoutteBtn
                                      };
        _animalSilhoutteSelectedBtns = new[] { AnimalselectionGUIObjects.HummingbirdSilhoutteSelectedBtn,
                                               AnimalselectionGUIObjects.SurgeonfishSilhoutteSelectedBtn,
                                               AnimalselectionGUIObjects.ChameleonSilhoutteSelectedBtn
                                              };
    }

    private void OnAnimalSelectionHorizontalMove()
    {
        Vector2 horizontalMove = Inputs.HorizontalMove;

        if (horizontalMove.x > 0 && !_isAnimalSelectionHorizontalMovePressed)
        {
            _isAnimalSelectionHorizontalMovePressed = true;
            _currentAnimalButtonIndex = (_currentAnimalButtonIndex + 1) % _animalBtns.Length;
            AnimalselectionGUIObjects.RightArrowBtn.SetActive(false);
            AnimalselectionGUIObjects.RightArrowSelectedBtn.SetActive(true);
            AnimalselectionGUIObjects.LeftArrowBtn.SetActive(true);
            AnimalselectionGUIObjects.LeftArrowSelectedBtn.SetActive(false);
        }
        else if (horizontalMove.x < 0 && !_isAnimalSelectionHorizontalMovePressed)
        {
            _isAnimalSelectionHorizontalMovePressed = true;
            _currentAnimalButtonIndex = (_currentAnimalButtonIndex - 1 + _animalBtns.Length) % _animalBtns.Length;
            AnimalselectionGUIObjects.RightArrowBtn.SetActive(true);
            AnimalselectionGUIObjects.RightArrowSelectedBtn.SetActive(false);
            AnimalselectionGUIObjects.LeftArrowBtn.SetActive(false);
            AnimalselectionGUIObjects.LeftArrowSelectedBtn.SetActive(true);
        }
        else if (horizontalMove.x == 0)
        {
            _isAnimalSelectionHorizontalMovePressed = false;
            AnimalselectionGUIObjects.RightArrowBtn.SetActive(true);
            AnimalselectionGUIObjects.RightArrowSelectedBtn.SetActive(false);
            AnimalselectionGUIObjects.LeftArrowBtn.SetActive(true);
            AnimalselectionGUIObjects.LeftArrowSelectedBtn.SetActive(false);
        }

        SelectAnimalButton(_currentAnimalButtonIndex);
    }

    private void SelectAnimalButton(int index)
    {
        int hummingbirdIndex = 0;
        int surgeonfishIndex = 1;
        int chameleonIndex = 2;

        // Deselect all buttons
        for (int i = 0; i < _animalBtns.Length; i++)
        {
            if(i == hummingbirdIndex)
            {
                if (IsHummingbirdActive)
                {
                    _animalSelectedBtns[i].gameObject.SetActive(false);
                    SetHummingbirdActive();
                }
                else
                {
                    _animalSilhoutteSelectedBtns[i].gameObject.SetActive(false);
                    SetHummigbirdDeactive();
                }
            }
            else if (i == surgeonfishIndex)
            {
                if (IsSurgeonfishActive)
                {
                    _animalSelectedBtns[i].gameObject.SetActive(false);
                    SetSurgeonfishActive();
                }
                else
                {
                    _animalSilhoutteSelectedBtns[i].gameObject.SetActive(false);
                    SetSurgeonfishDeactive();
                }
            }
            else if (i == chameleonIndex)
            {
                if (IsChameleonActive)
                {
                    _animalSelectedBtns[i].gameObject.SetActive(false);
                    SetChameleonActive();
                }
                else
                {
                    _animalSilhoutteSelectedBtns[i].gameObject.SetActive(false);
                    SetChameleonDeactive();
                }
            }
        }

        // Select the current button
        if (index == hummingbirdIndex)
        {
            if (IsHummingbirdActive)
                _animalSelectedBtns[index].gameObject.SetActive(true);
            else
                _animalSilhoutteSelectedBtns[index].gameObject.SetActive(true);
        }
        else if (index == surgeonfishIndex)
        {
            if (IsSurgeonfishActive)
                _animalSelectedBtns[index].gameObject.SetActive(true);
            else
                _animalSilhoutteSelectedBtns[index].gameObject.SetActive(true);
        }
        else if (index == chameleonIndex)
        {
            if (IsChameleonActive)
                _animalSelectedBtns[index].gameObject.SetActive(true);
            else
                _animalSilhoutteSelectedBtns[index].gameObject.SetActive(true);
        }

        // Deselect the unselected Buttons
        if(_animalSelectedBtns[index].gameObject.activeSelf)
            _animalBtns[index].gameObject.SetActive(false);
        else if(_animalSilhoutteSelectedBtns[index].gameObject.activeSelf)
            _animalSilhoutteBtns[index].gameObject.SetActive(false);
    }

    private void SetAnimalPicture()
    {
        if (!IsHummingbirdActive && AnimalselectionGUIObjects.HummingbirdSilhoutteSelectedBtn.activeSelf)
        {
            AnimalpicturesGUIObjects.HummingbirdSilhoutteImg.SetActive(true);
            AnimalpicturesGUIObjects.SurgeonfishSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.HummingbirdImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonImg.SetActive(false);
        }
        else if (IsHummingbirdActive && AnimalselectionGUIObjects.HummingbirdSelectedBtn.activeSelf)
        {
            AnimalpicturesGUIObjects.HummingbirdSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.HummingbirdImg.SetActive(true);
            AnimalpicturesGUIObjects.SurgeonfishImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonImg.SetActive(false);
        }
        else if (!IsSurgeonfishActive && AnimalselectionGUIObjects.SurgeonfishSilhoutteSelectedBtn.activeSelf)
        {
            AnimalpicturesGUIObjects.HummingbirdSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishSilhoutteImg.SetActive(true);
            AnimalpicturesGUIObjects.ChameleonSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.HummingbirdImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonImg.SetActive(false);
        }
        else if (IsSurgeonfishActive && AnimalselectionGUIObjects.SurgeonfishSelectedBtn.activeSelf)
        {
            AnimalpicturesGUIObjects.HummingbirdSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.HummingbirdImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishImg.SetActive(true);
            AnimalpicturesGUIObjects.ChameleonImg.SetActive(false);
        }
        else if (!IsChameleonActive && AnimalselectionGUIObjects.ChameleonSilhoutteSelectedBtn.activeSelf)
        {
            AnimalpicturesGUIObjects.HummingbirdSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonSilhoutteImg.SetActive(true);
            AnimalpicturesGUIObjects.HummingbirdImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonImg.SetActive(false);
        }
        else if (IsChameleonActive && AnimalselectionGUIObjects.ChameleonSelectedBtn.activeSelf)
        {
            AnimalpicturesGUIObjects.HummingbirdSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonSilhoutteImg.SetActive(false);
            AnimalpicturesGUIObjects.HummingbirdImg.SetActive(false);
            AnimalpicturesGUIObjects.SurgeonfishImg.SetActive(false);
            AnimalpicturesGUIObjects.ChameleonImg.SetActive(true);
        }
    }

    private void SetCanSelectAnimalGUIObject()
    {
        if(_canSelectAnimal)
        {
            AnimalselectionGUIObjects.ADSpaceEnter.SetActive(true);
            AnimalselectionGUIObjects.AnimalselectionInactive.SetActive(false);
            _isActionComfirmButtonReleased = false;
        }
        else
        {
            AnimalselectionGUIObjects.ADSpaceEnter.SetActive(false);
            AnimalselectionGUIObjects.AnimalselectionInactive.SetActive(true);
            _isActionComfirmButtonReleased = true;
        }
    }

    #endregion

    #region PlayLevelSelect
    private void SetPlayLevelSelectActive()
    {
        int hummingbirdIndex = 0;
        int surgeonfishIndex = 1;
        int chameleonIndex = 2;

        if (
            (IsHummingbirdActive && _animalSelectedBtns[hummingbirdIndex].activeSelf)
            || (IsSurgeonfishActive && _animalSelectedBtns[surgeonfishIndex].activeSelf)
            || (IsChameleonActive && _animalSelectedBtns[chameleonIndex].activeSelf)
            )
        {
            if(Inputs.Action || Inputs.Confirm)
            {
                PlayLevelselectGUIObjects.PlayLevelselect.SetActive(true);
                PlayLevelselectGUIObjects.ReturnBtn.SetActive(true);
                PlayLevelselectGUIObjects.ReturnSelectedBtn.SetActive(false);
                PlayLevelselectGUIObjects.UpArrowBtn.SetActive(true);
                PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(false);
                PlayLevelselectGUIObjects.PlayBtn.SetActive(false);
                PlayLevelselectGUIObjects.PlaySelectedBtn.SetActive(true);
                PlayLevelselectGUIObjects.LevelselectBtn.SetActive(true);
                PlayLevelselectGUIObjects.LevelselectSelectedBtn.SetActive(false);
                PlayLevelselectGUIObjects.DownArrowBtn.SetActive(true);
                PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(false);
                PlayLevelselectGUIObjects.LeftRightBackspaceSpaceEnter.SetActive(true);
                PlayLevelselectGUIObjects.LevelselectInactive.SetActive(false);
                PlayLevelselectGUIObjects.PlayLevelselectInactive.SetActive(false);

                AnimalselectionGUIObjects.ADSpaceEnter.SetActive(false);
                AnimalselectionGUIObjects.AnimalselectionInactive.SetActive(true);

                _canSelectAnimal = false;
                _canSelectPlayLevelSelect = true;
            }
        }
        if (
            (!IsHummingbirdActive && _animalSilhoutteSelectedBtns[hummingbirdIndex].activeSelf)
            || (!IsSurgeonfishActive && _animalSilhoutteSelectedBtns[surgeonfishIndex].activeSelf)
            || (!IsChameleonActive && _animalSilhoutteSelectedBtns[chameleonIndex].activeSelf)
            )
        {
            if (Inputs.Action || Inputs.Confirm)
            {
                PlayLevelselectGUIObjects.PlayLevelselect.SetActive(true);
                PlayLevelselectGUIObjects.ReturnBtn.SetActive(true);
                PlayLevelselectGUIObjects.ReturnSelectedBtn.SetActive(false);
                PlayLevelselectGUIObjects.UpArrowBtn.SetActive(true);
                PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(false);
                PlayLevelselectGUIObjects.PlayBtn.SetActive(false);
                PlayLevelselectGUIObjects.PlaySelectedBtn.SetActive(true);
                PlayLevelselectGUIObjects.LevelselectBtn.SetActive(true);
                PlayLevelselectGUIObjects.LevelselectSelectedBtn.SetActive(false);
                PlayLevelselectGUIObjects.DownArrowBtn.SetActive(true);
                PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(false);
                PlayLevelselectGUIObjects.LeftRightBackspaceSpaceEnter.SetActive(true);
                PlayLevelselectGUIObjects.LevelselectInactive.SetActive(true);
                PlayLevelselectGUIObjects.PlayLevelselectInactive.SetActive(false);

                AnimalselectionGUIObjects.ADSpaceEnter.SetActive(false);
                AnimalselectionGUIObjects.AnimalselectionInactive.SetActive(true);

                _canSelectAnimal = false;
                _canSelectPlayLevelSelect = true;
            }
        }
    }

    private void FillPlayLevelSelectReturnBtnArrays()
    {
        _playLevelSelectBtns = new[] { PlayLevelselectGUIObjects.PlayBtn,
                                       PlayLevelselectGUIObjects.LevelselectBtn
                                      };
        _playLevelSelectSelectedBtns = new[] { PlayLevelselectGUIObjects.PlaySelectedBtn,
                                               PlayLevelselectGUIObjects.LevelselectSelectedBtn
                                              };
        _playReturnBtns = new[] { PlayLevelselectGUIObjects.PlayBtn,
                                  PlayLevelselectGUIObjects.ReturnBtn
                                };
        _playReturnSelectedBtns = new[] { PlayLevelselectGUIObjects.PlaySelectedBtn,
                                          PlayLevelselectGUIObjects.ReturnSelectedBtn
                                         };
    }

    private void OnPlayLevelSelectVerticalMove()
    {
        Vector2 verticalMove = Inputs.VerticalMove;
 
        if (verticalMove.y > 0 && !_isPlayLevelSelectVerticalMovePressed)
        {
            _isPlayLevelSelectVerticalMovePressed = true;
            _currentPlayLevelSelectIndex = (_currentPlayLevelSelectIndex + 1) % _playLevelSelectBtns.Length;
            PlayLevelselectGUIObjects.UpArrowBtn.SetActive(false);
            PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(true);
            PlayLevelselectGUIObjects.DownArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(false);
        }
        else if (verticalMove.y < 0 && !_isPlayLevelSelectVerticalMovePressed)
        {
            _isPlayLevelSelectVerticalMovePressed = true;
            _currentPlayLevelSelectIndex = (_currentPlayLevelSelectIndex - 1 + _playLevelSelectBtns.Length) % _playLevelSelectBtns.Length;
            PlayLevelselectGUIObjects.UpArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.DownArrowBtn.SetActive(false);
            PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(true);
        }
        else if (verticalMove.y == 0)
        {
            _isPlayLevelSelectVerticalMovePressed = false;
            PlayLevelselectGUIObjects.UpArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.DownArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(false);
        }

        SelectPlayLevelSelectButton(_currentPlayLevelSelectIndex);
    }

    private void SelectPlayLevelSelectButton(int index)
    {
        if (!PlayLevelselectGUIObjects.LevelselectInactive.activeSelf)
        {
            // Deselect all buttons except the selected one
            for (int i = 0; i < _playLevelSelectBtns.Length; i++)
            {
                if (i == index)
                {
                    _playLevelSelectSelectedBtns[i].SetActive(true);
                }
                else if (_playLevelSelectSelectedBtns[i].activeSelf)
                {
                    _playLevelSelectSelectedBtns[i].SetActive(false);
                    _playLevelSelectBtns[i].SetActive(true);
                }
            }

            // Deselect the unselected Button
            if (_playLevelSelectSelectedBtns[index].gameObject.activeSelf)
                _playLevelSelectBtns[index].gameObject.SetActive(false);
        }
    }

    private void OnPlayLevelReturnHorizontalMove()
    {
        Vector2 horizontalMove = Inputs.HorizontalMove;

        if (horizontalMove.x > 0 && !_isPlayReturnHorizontalMovePressed)
        {
            _isPlayReturnHorizontalMovePressed = true;
            _currentPlayReturnIndex = (_currentPlayReturnIndex + 1) % _playReturnBtns.Length;
            PlayLevelselectGUIObjects.UpArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.LevelselectBtn.SetActive(true);
            PlayLevelselectGUIObjects.LevelselectSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.DownArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(false);
        }
        else if (horizontalMove.x < 0 && !_isPlayReturnHorizontalMovePressed)
        {
            _isPlayReturnHorizontalMovePressed = true;
            _currentPlayReturnIndex = (_currentPlayReturnIndex - 1 + _playReturnBtns.Length) % _playReturnBtns.Length;
            PlayLevelselectGUIObjects.UpArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.LevelselectBtn.SetActive(true);
            PlayLevelselectGUIObjects.LevelselectSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.DownArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(false);
        }
        else if (horizontalMove.x == 0)
        {
            _isPlayReturnHorizontalMovePressed = false;
            PlayLevelselectGUIObjects.UpArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.UpArrowSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.LevelselectBtn.SetActive(true);
            PlayLevelselectGUIObjects.LevelselectSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.DownArrowBtn.SetActive(true);
            PlayLevelselectGUIObjects.DownArrowSelectedBtn.SetActive(false);
        }

        SelectPlayReturnButton(_currentPlayReturnIndex);
    }

    private void SelectPlayReturnButton(int index)
    {
        // Deselect all buttons except the selected one
        for (int i = 0; i < _playReturnBtns.Length; i++)
        {
            if (i == index)
            {
                _playReturnSelectedBtns[i].SetActive(true);
            }
            else if (_playReturnSelectedBtns[i].activeSelf)
            {
                _playReturnSelectedBtns[i].SetActive(false);
                _playReturnBtns[i].SetActive(true);
            }
        }
        
        // Deselect the unselected Button
        if (_playReturnSelectedBtns[index].gameObject.activeSelf)
            _playReturnBtns[index].gameObject.SetActive(false);
    }

    private void HandlePlayLevelSelectReturnVerticalHorizontalSwith()
    {
        // Check if the vertical or horizontal input is pressed, or if the "return" button is selected.
        bool isVerticalMovePressed = Inputs.VerticalMove.y != 0;
        bool isHorizontalMovePressed = Inputs.HorizontalMove.x != 0;
        bool isReturnSelectedBtnActive = PlayLevelselectGUIObjects.ReturnSelectedBtn.activeSelf;

        // If the vertical input is pressed, reset the current return index to the first position,
        // and reset the horizontal input flag.
        if (isVerticalMovePressed)
        {
            _currentPlayReturnIndex = 0;
            _isPlayReturnHorizontalMovePressed = false;
        }

        // If the horizontal input is pressed, set the current level index to the first position
        // or the last position, depending on whether the "return" button is selected,
        // and reset the vertical input flag.
        if (isHorizontalMovePressed)
        {
            _currentPlayLevelSelectIndex = isReturnSelectedBtnActive ? _playLevelSelectBtns.Length - 1 : 0;
            _isPlayLevelSelectVerticalMovePressed = false;
        }

        // If the player is not pressing the vertical input anymore, notify the listeners for horizontal movement.
        if (!_isPlayLevelSelectVerticalMovePressed)
        {
            OnPlayLevelReturnHorizontalMove();
        }

        // If the player is not pressing the horizontal input anymore, and the "return" button is not selected,
        // notify the listeners for vertical movement.
        if (!_isPlayReturnHorizontalMovePressed && !isReturnSelectedBtnActive)
        {
                OnPlayLevelSelectVerticalMove();
        }





        /*if (Inputs.VerticalMove.y != 0)
        {
            _currentPlayRetrunIndex = 0;
            _isPlayReturnHorizontalMovePressed = false;
        }

        if (Inputs.HorizontalMove.x != 0)
        {
            if (!PlayLevelselectGUIObjects.ReturnSelectedBtn.activeSelf)
            {
                _currentPlayLevelSelectIndex = 0;
            }
            else
            {
                _currentPlayLevelSelectIndex = _playLevelSelectBtns.Length - 1;
            }
            _isPlayLevelSelectVerticalMovePressed = false;
        }
        if (!_isPlayLevelSelectVerticalMovePressed)
        {
            OnPlayLevelReturnHorizontalMove();
        }
        if (!_isPlayReturnHorizontalMovePressed && !PlayLevelselectGUIObjects.ReturnSelectedBtn.activeSelf)
        {
            OnPlayLevelSelectVerticalMove();
        }*/
    }

    private void SetCanSelectPlayLevelSelectlGUIObject()
    {
        if(!_canSelectPlayLevelSelect)
        {
            PlayLevelselectGUIObjects.ReturnBtn.SetActive(true);
            PlayLevelselectGUIObjects.ReturnSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.PlayBtn.SetActive(true);
            PlayLevelselectGUIObjects.PlaySelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.LevelselectBtn.SetActive(true);
            PlayLevelselectGUIObjects.LevelselectSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.LeftRightBackspaceSpaceEnter.SetActive(false);
            PlayLevelselectGUIObjects.LevelselectInactive.SetActive(false);
            PlayLevelselectGUIObjects.PlayLevelselectInactive.SetActive(true);

            _isPlayLevelSelectVerticalMovePressed = false;
            _isPlayReturnHorizontalMovePressed = false;
        }
        else
        {
            _canSelectAnimal= false;
            _canSelectPlayLevelSelect = true;
        }

      /*  else
        {
            PlayLevelselectGUIObjects.ReturnBtn.SetActive(true);
            PlayLevelselectGUIObjects.ReturnSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.PlayBtn.SetActive(false);
            PlayLevelselectGUIObjects.PlaySelectedBtn.SetActive(true);
            PlayLevelselectGUIObjects.LevelselectBtn.SetActive(true);
            PlayLevelselectGUIObjects.LevelselectSelectedBtn.SetActive(false);
            PlayLevelselectGUIObjects.LeftRightBackspaceSpaceEnter.SetActive(true);
            PlayLevelselectGUIObjects.PlayLevelselectInactive.SetActive(false);







        }*/
    }

  //  private void Update()
   // {
        /*Debug.Log(_canSelectAnimal + " _canSelectAnimal");
        Debug.Log(_isAnimalSelectionHorizontalMovePressed + " _isAnimalSelectionHorizontalMovePressed");
        Debug.Log(_isPlayLevelSelectVerticalMovePressed + " _isPlayLevelSelectVerticalMovePressed");
        Debug.Log(_isPlayReturnHorizontalMovePressed + " _isPlayReturnHorizontalMovePressed");
        Debug.Log(_canSelectPlayLevelSelect + " _canSelectPlayLevelSelect");
        Debug.Log(_isActionComfirmButtonReleased + " _isActionComfirmButtonReleased");*/

      //  Debug.Log(Inputs.Action);

//}

    private void LoadScene()
    {
        if (Inputs.Action == false && Inputs.Confirm == false)
        {
            _isActionComfirmButtonReleased = true;
        }
        
        if ((Inputs.Action || Inputs.Confirm)
            && PlayLevelselectGUIObjects.PlaySelectedBtn.activeSelf
            && _isActionComfirmButtonReleased)
        {
            if (AnimalselectionGUIObjects.HummingbirdSelectedBtn.activeSelf
                || AnimalselectionGUIObjects.HummingbirdSilhoutteSelectedBtn.activeSelf)

            {
                FindObjectOfType<SceneSwitch>().ChangeScene("IntroBird");
                _isActionComfirmButtonReleased = false;
            }
            else if (AnimalselectionGUIObjects.SurgeonfishSelectedBtn.activeSelf
                     || AnimalselectionGUIObjects.SurgeonfishSilhoutteSelectedBtn.activeSelf)
            {
                FindObjectOfType<SceneSwitch>().ChangeScene("IntroFish");
                _isActionComfirmButtonReleased = false;
            }
            else if (AnimalselectionGUIObjects.ChameleonSelectedBtn.activeSelf
                     || AnimalselectionGUIObjects.ChameleonSilhoutteSelectedBtn.activeSelf)
            {
                FindObjectOfType<SceneSwitch>().ChangeScene("IntroChameleon");
                _isActionComfirmButtonReleased = false;
            }
        }
    }

    private void Return()
    {
       
            if(PlayLevelselectGUIObjects.ReturnSelectedBtn.activeSelf
               && (Inputs.Action || Inputs.Confirm))
            {
                _canSelectPlayLevelSelect = false;
                _canSelectAnimal = true;
            }
        
    }
    #endregion
}