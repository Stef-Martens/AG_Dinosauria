using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineDrawerConnectionQuiz : MonoBehaviour
{
    public List<Image> LineImgs = new List<Image>();

    private Inputs _inputs;
    private BtnNavigationConnectionQuiz _btnNavigation;
   
    public List<Button> _leftColBtns;
    public List<Button> _rightColBtns;
    private List<Button> _leftColBtnsOrigList;

    private List<RectTransform> _leftColLineRoots = new List<RectTransform>();
    private List<RectTransform> _rightColLineRoots = new List<RectTransform>();

    private RectTransform _startPnt;
    private RectTransform _endPnt;
    private Image _activeLineImg;

    public List<Image> _permaActiveLines = new List<Image>();
    private List<Image> _lineImgsOrigList = new List<Image>();

    #region New Solution
    private bool _hasClickedLeftColBtn = false;
    private int _currentLeftColBtnIndex;
    private bool _canDrawNewLine = true;
    #endregion

    private void Awake()
    {
        foreach (Image line in LineImgs)
        {
            line.transform.gameObject.SetActive(false);
            line.color = this.GetComponent<ConnectionQuiz>().BaseLineColor;
        }

        _btnNavigation= GetComponent<BtnNavigationConnectionQuiz>();

        _leftColBtns = _btnNavigation.LeftColBtns;
        _rightColBtns= _btnNavigation.RightColBtns;
        _leftColBtnsOrigList = _leftColBtns.ToList();

        _lineImgsOrigList = LineImgs.ToList();

        SetLineRootsLists();
    }

    private void OnEnable()
    {
        _inputs = FindObjectOfType<Inputs>();
        _inputs.ActionInputEvent += OnActionConfirmInput;
        _inputs.ConfirmInputEvent += OnActionConfirmInput;

        if (_lineImgsOrigList != null && _lineImgsOrigList.Count > 0)
        {
            LineImgs = _lineImgsOrigList.ToList();

            foreach (Image line in LineImgs)
            {
                line.transform.gameObject.SetActive(false);
                line.color = this.GetComponent<ConnectionQuiz>().BaseLineColor;
            }
        }

        _permaActiveLines?.Clear();
        _permaActiveLines = new List<Image>();
    }

    private void OnDisable()
    {
        _inputs.ActionInputEvent -= OnActionConfirmInput;
        _inputs.ConfirmInputEvent -= OnActionConfirmInput;
    }

    private void OnActionConfirmInput(bool isPressed)
    {
        _leftColBtns = this.GetComponent<BtnLeftColNavigation>().Buttons;
        _rightColBtns = this.GetComponent<BtnRightColNavigation>().Buttons;
    }

    private void Update()
    {
        HandleLineValues();

        if(_hasClickedLeftColBtn)
        {
            DrawLine(_startPnt, _endPnt, _activeLineImg);
        }
    }

    private void SetLineRootsLists()
    {
        if (_leftColBtns.Count > 0)
        {
            foreach (Button btn in _leftColBtns)
            {
                _leftColLineRoots.Add(btn.transform.parent.GetChild(2).GetComponent<RectTransform>());
            }
        }

        if (_rightColBtns.Count > 0)
        {
            foreach (Button btn in _rightColBtns)
            {
                _rightColLineRoots.Add(btn.transform.parent.GetChild(2).GetComponent<RectTransform>());
            }
        }
    }

    public void DrawLine(RectTransform startPoint, RectTransform endPoint, Image lineImage)
    {
        RectTransform canvasRectTransform = lineImage.canvas.GetComponent<RectTransform>();

        Vector2 startPointLocalPos;
        Vector2 endPointLocalPos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (canvasRectTransform, startPoint.position, lineImage.canvas.worldCamera, out startPointLocalPos);
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (canvasRectTransform, endPoint.position, lineImage.canvas.worldCamera, out endPointLocalPos);

        lineImage.rectTransform.pivot = new Vector2(0, 0.5f);
        lineImage.rectTransform.anchorMin = new Vector2(0, 0.5f);
        lineImage.rectTransform.anchorMax = new Vector2(1, 0.5f);  // Adjust anchorMax to span the entire width

        float lineLength = Vector2.Distance(startPointLocalPos, endPointLocalPos);
        float lineThickness = lineImage.rectTransform.sizeDelta.y;

        // Extend the line length by a factor
        lineLength *= 1.95f;

        lineImage.rectTransform.sizeDelta = new Vector2(lineLength, lineThickness);

        lineImage.rectTransform.position = startPoint.position;
        lineImage.rectTransform.rotation =
            Quaternion.Euler(0, 0, Mathf.Atan2(endPointLocalPos.y - startPointLocalPos.y, endPointLocalPos.x - startPointLocalPos.x) * Mathf.Rad2Deg);
    }

    #region New Solution
    private void LeftColButtonClicked()
    {
        _hasClickedLeftColBtn = true;
    }

    private void RightColButtonClicked()
    {
        _hasClickedLeftColBtn = false;
        _canDrawNewLine= true;
    }

    private void GetCurrentLeftColButtonIndex(int index)
    {
        _currentLeftColBtnIndex = index;
    }
    #endregion

    private void HandleLineValues()
    {
        #region New Solution
        if (_hasClickedLeftColBtn && _canDrawNewLine)
        {
            LineImgs[_currentLeftColBtnIndex].gameObject.SetActive(true);
            _canDrawNewLine = false;
        }
        #endregion

        _permaActiveLines = _leftColBtnsOrigList
            .Where(btn => !btn.interactable)
            .Select(btn => _lineImgsOrigList[_leftColBtnsOrigList.IndexOf(btn)])
            .Distinct()
            .OrderBy(line => line.name)
            .ToList();

        LineImgs = LineImgs.Except(_permaActiveLines).ToList();

        if (_leftColBtns != null && _leftColBtns.Count > 0)
        {
            for (int indexLftCol = 0; indexLftCol < _leftColBtns.Count; indexLftCol++)
            {
                if (_leftColBtns[indexLftCol].transform.gameObject == EventSystem.current.currentSelectedGameObject)
                {
                    #region New Solution
                    _leftColBtns[indexLftCol].onClick.AddListener(LeftColButtonClicked);
                    #endregion

                    _startPnt = _leftColBtns[indexLftCol].transform.parent.GetChild(2).GetComponent<RectTransform>();
                    #region Old Solution
                    //_endPnt = _rightColBtns[0].transform.parent.GetChild(2).GetComponent<RectTransform>();
                    #endregion
                    _activeLineImg = LineImgs[indexLftCol];

                    #region New Solution
                    GetCurrentLeftColButtonIndex(indexLftCol);
                    #endregion

                    #region Old Solution
                    /*LineImgs[indexLftCol].gameObject.SetActive(true);

                    for (int index = 0; index < LineImgs.Count; index++)
                     {
                         if (index != indexLftCol )
                         {
                             LineImgs[index].gameObject.SetActive(false);
                         }
                     }*/
                    #endregion
                }
            }
        }

        if (_rightColBtns != null && _rightColBtns.Count > 0)
        {
            for (int indexRghtCol = 0; indexRghtCol < _rightColBtns.Count; indexRghtCol++)
            {
                if (_rightColBtns[indexRghtCol].transform.gameObject == EventSystem.current.currentSelectedGameObject)
                {
                    #region New Solution
                    _rightColBtns[indexRghtCol].onClick.AddListener(RightColButtonClicked);
                    #endregion

                    _endPnt = _rightColBtns[indexRghtCol].transform.parent.GetChild(2).GetComponent<RectTransform>();
                }
            }
        }
    }
}