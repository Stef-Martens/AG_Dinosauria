using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineDrawerConnectionQuiz : MonoBehaviour
{
    public List<Image> LineImgs = new List<Image>();

    private BtnNavigationConnectionQuiz _btnNavigation;
   
    private List<Button> _leftColBtns;
    private List<Button> _rightColBtns;
    private List<Button> _leftColBtnsOrigList;

    private List<RectTransform> _leftColLineRoots = new List<RectTransform>();
    private List<RectTransform> _rightColLineRoots = new List<RectTransform>();

    private RectTransform _startPnt;
    private RectTransform _endPnt;
    private Image _activeLineImg;

    private List<Image> _permaActiveLines = new List<Image>();
    private List<Image> _LineImgsOrigList = new List<Image>();

    private bool test = false;
    int bleg;
    private void Awake()
    {
        foreach(Image img in LineImgs)
            img.transform.gameObject.SetActive(false);

        _btnNavigation= GetComponent<BtnNavigationConnectionQuiz>();

        _leftColBtns = _btnNavigation.LeftColBtns;
        _rightColBtns= _btnNavigation.RightColBtns;
        _leftColBtnsOrigList = _leftColBtns.ToList();

        _LineImgsOrigList = LineImgs.ToList();

        SetLineRootsLists();
    }

    private void Update()
    {
        if (test && bleg >= 0 && bleg < LineImgs.Count)
        {
            LineImgs[bleg].gameObject.SetActive(true);

            for (int index = 0; index < LineImgs.Count; index++)
            {
                if (index != bleg)
                {
                    LineImgs[index].gameObject.SetActive(false);
                }
            }


        }

        HandleLineValues();
        if(_startPnt!= null && _endPnt != null && _activeLineImg != null && test)
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

    private void DrawLine(RectTransform startPoint, RectTransform endPoint, Image lineImage)
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
    private void ButtonClicked()
    {
        test = true;
 
    }

    private void buttonRightclick()
    {
        test = false;
    }


    private void bla(int meuh)
    {

        bleg = meuh;

    }

    private void HandleLineValues()
    {
      


        _permaActiveLines = _leftColBtnsOrigList
            .Where(btn => !btn.interactable)
            .Select(btn => _LineImgsOrigList[_leftColBtnsOrigList.IndexOf(btn)])
            .Distinct()
            .OrderBy(line => line.name)
            .ToList();

        LineImgs = LineImgs.Except(_permaActiveLines).ToList();



        for (int indexLftCol = 0; indexLftCol <_leftColBtns.Count; indexLftCol ++)
        {
            if (_leftColBtns[indexLftCol].transform.gameObject == EventSystem.current.currentSelectedGameObject)
            {
                _leftColBtns[indexLftCol].onClick.AddListener(ButtonClicked);

                _startPnt = _leftColBtns[indexLftCol].transform.parent.GetChild(2).GetComponent<RectTransform>();
                _endPnt = _rightColBtns[0].transform.parent.GetChild(2).GetComponent<RectTransform>();
                _activeLineImg = LineImgs[indexLftCol];

                bla(indexLftCol);

               // LineImgs[indexLftCol].gameObject.SetActive(true);

                for (int index = 0; index < LineImgs.Count; index++)
                 {
                     if (index != indexLftCol )
                     {
                         LineImgs[index].gameObject.SetActive(false);
                     }
                 }
            }

            for (int indexRghtCol = 0; indexRghtCol < _rightColBtns.Count; indexRghtCol++)
            {
                if (_rightColBtns[indexRghtCol].transform.gameObject == EventSystem.current.currentSelectedGameObject)
                {
                    _rightColBtns[indexRghtCol].onClick.AddListener(buttonRightclick);
                    _endPnt = _rightColBtns[indexRghtCol].transform.parent.GetChild(2).GetComponent<RectTransform>();
                }
            }
        }
    }
}