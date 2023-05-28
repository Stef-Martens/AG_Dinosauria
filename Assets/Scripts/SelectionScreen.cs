using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectionScreen : MonoBehaviour
{
    public int AnimalCount = 3;

    public GameObject Background;
    public GameObject AnimalSelection;
    public GameObject Play;
    public GameObject PlayLevelSelect;
    public GameObject LevelSelection;
    public GameObject Animals;

    private bool _isHummingbirdActive = false;
    private bool _isSurgeonfishActive = false;
    private bool _isChameleonActive = false;

    private List<GameObject> _backgrounds= new List<GameObject>();
    private List<Button> _animalBtns = new List<Button>();
    private List<Button> _playBtns = new List<Button>();
    private List<Button> _playLevelSelectBtns = new List<Button>();
    private List<Button> _levelSelectionBtns = new List<Button>();
    private List<GameObject> _animals = new List<GameObject>();

    private Selectable _firstSelectable;

    private GameObject _lastActiveAnimalBackground;
    private GameObject _lastActiveAnimalImg;
    private bool _lastPlayScreenState;
    private bool _lastPlaySelectScreenState;

    private bool _isHummingbirdSelected = false;
    private bool _isSurgeonfishSelected = false;
    private bool _isChameleonSelected = false;

    private Inputs _inputs;
    private bool _isFirstActionConfirmInput = false;
    private bool _isAnimalKeyReleased = false;
    private bool _isPlayLevelKeyReleased = false;
    private bool _isPlayLevelReturnKeyReleased = false;

    private InputAction keyboardAction;
    private bool _isAnyKeyPressed = false;

    private void OnEnable()
    {
        InitBackgroundsList();
        InitAnimalButtonsList();
        InitPlayButtonsList();
        InitPlayLevelSelectButtonsList();
        InitLevelSelectionButtonsList();
        InitAnimalsList();

        InitSelectionScreen();

        _inputs = FindObjectOfType<Inputs>();
        _inputs.ActionInputEvent += OnActionConfirmInput;
        _inputs.ConfirmInputEvent += OnActionConfirmInput;

        keyboardAction = new InputAction("keyboardAction", InputActionType.Button, "<Keyboard>/anyKey");
        keyboardAction.Enable();
        keyboardAction.performed += OnAnyKeyInputPerformed;
        keyboardAction.canceled += OnAnyKeyInputCanceled;
    }

    private void OnDisable()
    {
        _inputs.ActionInputEvent -= OnActionConfirmInput;
        _inputs.ConfirmInputEvent -= OnActionConfirmInput;

        keyboardAction.performed -= OnAnyKeyInputPerformed;
        keyboardAction.canceled -= OnAnyKeyInputCanceled;

        keyboardAction.Disable();
        keyboardAction.Dispose();
    }
    private void OnAnyKeyInputPerformed(InputAction.CallbackContext context)
        => _isAnyKeyPressed = true;

    private void OnAnyKeyInputCanceled(InputAction.CallbackContext context)
        => _isAnyKeyPressed = false;

    void Update()
        => CheckSelectedButton();

    private void InitBackgroundsList()
    {
        _backgrounds = new List<GameObject>();

        for (int index = 0; index < AnimalCount; index++)
            _backgrounds.Add(Background.transform.GetChild(index).gameObject);
    }

    private void InitAnimalButtonsList()
    {
        _animalBtns = new List<Button>();

        for (int index = 0; index < AnimalCount; index++)
            _animalBtns.Add(AnimalSelection.transform.GetChild(index + 2).GetChild(0).GetComponent<Button>());

        _animalBtns.Add(AnimalSelection.transform.GetChild(0).GetComponent<Button>());

        foreach(Button btn in _animalBtns)
            btn.onClick.AddListener(OnAnimalButtonsPressed);
    }

    private void InitPlayButtonsList()
    {
        _playBtns = new List<Button>
        {
            Play.transform.GetChild(1).GetComponent<Button>(),
            Play.transform.GetChild(0).GetComponent<Button>()
        };

        foreach (Button btn in _playBtns)
            btn.onClick.AddListener(OnPlayButtonsPressed);
    }

    private void InitPlayLevelSelectButtonsList()
    {
        _playLevelSelectBtns = new List<Button>
        {
            PlayLevelSelect.transform.GetChild(1).GetComponent<Button>(),
            PlayLevelSelect.transform.GetChild(2).GetComponent<Button>(),
            PlayLevelSelect.transform.GetChild(0).GetComponent<Button>()
        };

        foreach (Button btn in _playLevelSelectBtns)
            btn.onClick.AddListener(OnPlayLevelButtonsPressed);
    }

    private void InitLevelSelectionButtonsList()
    {
        _levelSelectionBtns = new List<Button>
        {
            LevelSelection.transform.GetChild(2).GetComponent<Button>(),
            LevelSelection.transform.GetChild(3).GetComponent<Button>(),
            LevelSelection.transform.GetChild(4).GetComponent<Button>(),
            LevelSelection.transform.GetChild(1).GetComponent<Button>()
        };

        foreach (Button btn in _levelSelectionBtns)
            btn.onClick.AddListener(OnLevelSelectionButtonsPressed);
    }

    private void InitAnimalsList()
    {
        _animals = new List<GameObject>();

        for (int index = 0; index < AnimalCount; index++)
            _animals.Add(Animals.transform.GetChild(index).gameObject);
    }

    private void InitSelectionScreen()
    {
        _firstSelectable = _animalBtns?.FirstOrDefault();
        _firstSelectable?.Select();

        _isHummingbirdActive = FindObjectOfType<FirebaseManager>().MadeUser.animals[0].finished;
        _isSurgeonfishActive = FindObjectOfType<FirebaseManager>().MadeUser.animals[1].finished;
        _isChameleonActive = FindObjectOfType<FirebaseManager>().MadeUser.animals[2].finished;

        AnimalSelection.transform.GetChild(7).gameObject.SetActive(false);
        Play.transform.parent.GetChild(4).gameObject.SetActive(true);
        LevelSelection.SetActive(false);

        List<(int childIndex, bool isActive)> animalStates = new List<(int, bool)>
        {
            (2, _isHummingbirdActive),
            (3, _isSurgeonfishActive),
            (4, _isChameleonActive)
        };

        foreach (var state in animalStates)
        {
            int childIndex = state.childIndex;
            bool isActive = state.isActive;

            Transform childTransform = AnimalSelection.transform.GetChild(childIndex);
            childTransform.GetChild(1).gameObject.SetActive(isActive);
            childTransform.GetChild(2).gameObject.SetActive(!isActive);
        }
    }

    private void CheckSelectedButton()
    {
        foreach (Button btn in _animalBtns)
            if (btn.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                SetAnimalAndBackgroundImages(btn);
                SetPlayLevelSelectScreen(btn);
            }
    }

    private void SetAnimalAndBackgroundImages(Button selectedButton)
    {
        for (int index = 0; index < _animals.Count; index++)
        {
            bool isButtonSelected = (selectedButton == _animalBtns[index]);
            
            _backgrounds[index].gameObject.SetActive(isButtonSelected);

            if (isButtonSelected)
                _lastActiveAnimalBackground = _backgrounds[index];

            if (_animals[index].transform.GetChild(0).gameObject.activeSelf)
                _lastActiveAnimalImg = _animals[index].transform.GetChild(0).gameObject;
            else if (_animals[index].transform.GetChild(1).gameObject.activeSelf)
                _lastActiveAnimalImg = _animals[index].transform.GetChild(1).gameObject;

            if (index == 0)
            {
                _animals[0].transform.GetChild(0).gameObject.SetActive(isButtonSelected && _isHummingbirdActive);
                _animals[0].transform.GetChild(1).gameObject.SetActive(isButtonSelected && !_isHummingbirdActive);
            }
            else if (index == 1)
            {
                _animals[1].transform.GetChild(0).gameObject.SetActive(isButtonSelected && _isSurgeonfishActive);
                _animals[1].transform.GetChild(1).gameObject.SetActive(isButtonSelected && !_isSurgeonfishActive);
            }
            else if (index == 2)
            {
                _animals[2].transform.GetChild(0).gameObject.SetActive(isButtonSelected && _isChameleonActive);
                _animals[2].transform.GetChild(1).gameObject.SetActive(isButtonSelected && !_isChameleonActive);
            }
        }

        if (selectedButton == _animalBtns[_animalBtns.Count - 1])
        {
            _lastActiveAnimalBackground.gameObject.SetActive(true);
            _lastActiveAnimalImg.gameObject.SetActive(true);
        }
    }

    private void SetPlayLevelSelectScreen(Button selectedButton)
    {
        bool isActive = false;

        if (selectedButton == _animalBtns[0])
            isActive = _isHummingbirdActive;
        else if (selectedButton == _animalBtns[1])
            isActive = _isSurgeonfishActive;
        else if (selectedButton == _animalBtns[2])
            isActive = _isChameleonActive;

        Play.SetActive(!isActive);
        PlayLevelSelect.SetActive(isActive);

        if (selectedButton == _animalBtns[0]
            || selectedButton == _animalBtns[1]
            || selectedButton == _animalBtns[2])
        {
            _lastPlayScreenState = Play.activeSelf;
            _lastPlaySelectScreenState = PlayLevelSelect.activeSelf;
        }

        if (selectedButton == _animalBtns[_animalBtns.Count - 1])
        {
            Play.SetActive(_lastPlayScreenState);
            PlayLevelSelect.SetActive(_lastPlaySelectScreenState);
        }
    }

    private void OnActionConfirmInput(bool isPressed)
    {
        AllowaAnimalButtonsActionConfirmInputs(isPressed);
        AllowPlaySelectionScreenActionConfirmInputs(isPressed);
        AllowLevelSelectionActionConfirmInputs(isPressed);
    }

    private void AllowaAnimalButtonsActionConfirmInputs(bool isPressed)
    {
        if(isPressed)
            _isFirstActionConfirmInput = true;

        if (!_isPlayLevelReturnKeyReleased && !isPressed)
        {
            _isPlayLevelReturnKeyReleased = true;
        }
    }

    private void AllowPlaySelectionScreenActionConfirmInputs(bool isPressed)
    {
        if (!_isAnimalKeyReleased && !isPressed)
        {
            _isAnimalKeyReleased = true;
        }
    }

    private void AllowLevelSelectionActionConfirmInputs(bool isPressed)
    {
        if (!_isPlayLevelKeyReleased && !isPressed)
        {
            _isPlayLevelKeyReleased = true;
        }
    }

    private void OnAnimalButtonsPressed()
    {
        Button pressedBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
        
        if (_isAnyKeyPressed)
        {
            if (_isFirstActionConfirmInput || _isPlayLevelReturnKeyReleased)
            {
                for (int index = 0; index < _animalBtns.Count; index++)
                {
                    if (pressedBtn == _animalBtns[index])
                    {
                        if (index != _animalBtns.Count - 1)
                        {
                            if (Play.activeSelf)
                                _firstSelectable = _playBtns?.FirstOrDefault();
                            else
                                _firstSelectable = _playLevelSelectBtns?.FirstOrDefault();

                            _firstSelectable?.Select();

                            AnimalSelection.transform.GetChild(7).gameObject.SetActive(true);
                            Play.transform.parent.GetChild(4).gameObject.SetActive(false);

                            _isHummingbirdSelected = pressedBtn == _animalBtns[0];
                            _isSurgeonfishSelected = pressedBtn == _animalBtns[1];
                            _isChameleonSelected = pressedBtn == _animalBtns[2];
                        }
                        else
                        {
                            GoToHome();
                        }

                        _isFirstActionConfirmInput = false;
                        _isPlayLevelReturnKeyReleased = false;
                    }
                }
            }
        } 
    }

    private void OnPlayButtonsPressed()
    {
        Button pressedBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        if (_isAnyKeyPressed)
        {
            if (pressedBtn == _playBtns[0] && _isAnimalKeyReleased)
                GoToIntroLevels();

            if (pressedBtn == _playBtns[1] && _isAnimalKeyReleased)
                GoToAnimalSelection();
        }
    }

    private void OnPlayLevelButtonsPressed()
    {
        Button pressedBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        if (_isAnyKeyPressed)
        {
            if (pressedBtn == _playLevelSelectBtns[0] && _isAnimalKeyReleased)
                GoToIntroLevels();

            if (pressedBtn == _playLevelSelectBtns[1] && _isAnimalKeyReleased)
            {
                PlayLevelSelect.transform.parent.gameObject.SetActive(false);
                LevelSelection.SetActive(true);

                _firstSelectable = _levelSelectionBtns?.FirstOrDefault();
                _firstSelectable?.Select();
            }

            if (pressedBtn == _playLevelSelectBtns[2] && _isAnimalKeyReleased)
                GoToAnimalSelection();
        }
    }

    private void OnLevelSelectionButtonsPressed()
    {
        Button pressedBtn = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        if (_isAnyKeyPressed)
        {
            if (_isPlayLevelKeyReleased)
            {
                List<(string introScene, string miniGameScene, string quizScene)> levelSelectionScenes = new List<(string, string, string)>();

                if (_isHummingbirdSelected)
                    levelSelectionScenes.Add(("IntroBird", "HummingBird_Rico", "QuizBird"));

                else if (_isSurgeonfishSelected)
                    levelSelectionScenes.Add(("IntroFish", "Fish", "QuizFish"));

                else if (_isChameleonSelected)
                    levelSelectionScenes.Add(("IntroChameleon", "Chameleon", "QuizChameleon"));

                foreach (var sceneTuple in levelSelectionScenes)
                {
                    if (pressedBtn == _levelSelectionBtns[0])
                        FindObjectOfType<SceneSwitch>().ChangeScene(sceneTuple.introScene);
                    else if (pressedBtn == _levelSelectionBtns[1])
                        FindObjectOfType<SceneSwitch>().ChangeScene(sceneTuple.miniGameScene);
                    else if (pressedBtn == _levelSelectionBtns[2])
                        FindObjectOfType<SceneSwitch>().ChangeScene(sceneTuple.quizScene);
                }

                if (pressedBtn == _levelSelectionBtns[3])
                {
                    LevelSelection.SetActive(false);
                    PlayLevelSelect.transform.parent.gameObject.SetActive(true);
                    PlayLevelSelect.SetActive(true);
                    Play.SetActive(false);

                    _firstSelectable = _playLevelSelectBtns?.FirstOrDefault();
                    _firstSelectable?.Select();
                }
            }
        }
    }

    private void GoToHome()
        => FindObjectOfType<SceneSwitch>().ChangeScene("Home");

    private void GoToAnimalSelection()
    {
        if (_isHummingbirdSelected)
            _firstSelectable = _animalBtns[0];
        else if (_isSurgeonfishSelected)
            _firstSelectable = _animalBtns[1];
        else if (_isChameleonSelected)
            _firstSelectable = _animalBtns[2];

        _firstSelectable.Select();

        _isHummingbirdSelected = false;
        _isSurgeonfishSelected = false;
        _isChameleonSelected = false;

        _isAnimalKeyReleased = false;

        AnimalSelection.transform.GetChild(7).gameObject.SetActive(false);
        Play.transform.parent.GetChild(4).gameObject.SetActive(true);
    }

    private void GoToIntroLevels()
    {
        if (_isHummingbirdSelected)
            FindObjectOfType<SceneSwitch>().ChangeScene("IntroBird");
        else if (_isSurgeonfishSelected)
            FindObjectOfType<SceneSwitch>().ChangeScene("IntroFish");
        else if (_isChameleonSelected)
            FindObjectOfType<SceneSwitch>().ChangeScene("IntroChameleon");

        _isAnimalKeyReleased = false;
    }
}