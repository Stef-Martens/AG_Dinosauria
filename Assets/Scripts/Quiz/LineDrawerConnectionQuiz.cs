using System.Collections.Generic;
using System.Linq;
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
    private List<Button> _rightColBtnsOrigList;

    private List<RectTransform> _leftColLineRoots = new List<RectTransform>();
    private List<RectTransform> _rightColLineRoots = new List<RectTransform>();

    private RectTransform _startPnt;
    private RectTransform _endPnt;
    private Image _activeLineImg;

    private void Awake()
    {
        foreach(Image img in LineImgs)
            img.transform.gameObject.SetActive(false);

        _btnNavigation= GetComponent<BtnNavigationConnectionQuiz>();

        _leftColBtns = _btnNavigation.LeftColBtns;
        _rightColBtns= _btnNavigation.RightColBtns;

        _leftColBtnsOrigList = _leftColBtns.ToList();
        _rightColBtnsOrigList = _rightColBtns.ToList();

        SetLineRootsList();
    }

    private void Update()
    {
        DetermineDrawnLine();

        if(_startPnt!=null && _endPnt && _activeLineImg)
            DrawLine(_startPnt, _endPnt, _activeLineImg);
    }

    private void SetLineRootsList()
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

    private Vector3 GetCenterPosition(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        Vector3 center = Vector3.zero;
        for (int i = 0; i < 4; i++)
        {
            center += corners[i];
        }
        center /= 4f;

        return center;
    }

    private void DrawLine(RectTransform startPoint, RectTransform endPoint, Image lineImage)
    {
        Vector3 startPos = GetCenterPosition(startPoint);
        Vector3 endPos = GetCenterPosition(endPoint);

        Vector3 difference = endPos - startPos;

        lineImage.rectTransform.sizeDelta = new Vector2(difference.magnitude, lineImage.rectTransform.sizeDelta.y);

        lineImage.rectTransform.pivot = new Vector2(0, 0.5f);
        lineImage.rectTransform.position = startPos;
        lineImage.rectTransform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg);
    }

    /*   private void DetermineDrawnLine()
       {
           for(int index = 0; index < _leftColBtns.Count; index ++)
           {
               if (EventSystem.current.currentSelectedGameObject == _leftColBtns[index].gameObject
                   || !_leftColBtns[index].interactable)
               {
                   _startPnt = _leftColBtns[index].transform.parent.GetChild(2).GetComponent<RectTransform>();
                   _activeLineImg = LineImgs[index];
                   _activeLineImg.gameObject.SetActive(true);
               }
               else
               {
                   LineImgs[index].gameObject.SetActive(false);
               }
           }

           for (int index = 0; index < _rightColBtns.Count; index++)
           {
               if (EventSystem.current.currentSelectedGameObject == _rightColBtns[index].gameObject)
               {
                   _endPnt = _rightColBtns[index].transform.parent.GetChild(2).GetComponent<RectTransform>();
               }
           }
       }*/
    /*  private void DetermineDrawnLine()
      {
          int selectedLeftButtonIndex = -1;
          int selectedRightButtonIndex = -1;

          // Find the index of the currently selected button in the left column
          for (int index = 0; index < _leftColBtns.Count; index++)
          {
              if (EventSystem.current.currentSelectedGameObject == _leftColBtns[index].gameObject)
              {
                  selectedLeftButtonIndex = index;
                  break;
              }
          }

          // Find the index of the first selectable button in the right column
          for (int index = 0; index < _rightColBtns.Count; index++)
          {
              if (_rightColBtns[index].interactable)
              {
                  selectedRightButtonIndex = index;
                  break;
              }
          }

          // Update the line positions and visibility based on the selected button indices
          for (int index = 0; index < _leftColBtns.Count; index++)
          {
              if (index == selectedLeftButtonIndex && selectedRightButtonIndex != -1)
              {
                  _startPnt = _leftColBtns[index].transform.parent.GetChild(2).GetComponent<RectTransform>();
                  _activeLineImg = LineImgs[index];
                  _activeLineImg.gameObject.SetActive(true);
              }
              else
              {
                  LineImgs[index].gameObject.SetActive(false);
              }
          }

          if (selectedLeftButtonIndex != -1 && selectedRightButtonIndex != -1)
          {
              _endPnt = _rightColBtns[selectedRightButtonIndex].transform.parent.GetChild(2).GetComponent<RectTransform>();
          }
      }*/


    //GOOD
     private void DetermineDrawnLine()
      {
          GameObject selectedButton = EventSystem.current.currentSelectedGameObject;
          int selectedIndex = -1;

          // Find the index of the selected button in the left column
          for (int i = 0; i < _leftColBtns.Count; i++)
          {
              if (selectedButton == _leftColBtns[i].gameObject)
              {
                  selectedIndex = i;
                  break;
              }
          }

          if (selectedIndex != -1)
          {
              // Draw a line from the selected button to the first selected button in the right column
              _startPnt = _leftColBtns[selectedIndex].transform.parent.GetChild(2).GetComponent<RectTransform>();

              bool lineDrawn = false; // Flag to track if a line has been drawn

              for (int i = 0; i < _rightColBtns.Count; i++)
              {
                  if (_rightColBtns[i].interactable)
                  {
                      _endPnt = _rightColBtns[i].transform.parent.GetChild(2).GetComponent<RectTransform>();
                      lineDrawn = true; // Set the flag to indicate that a line has been drawn
                      break; // Exit the loop after finding the first selected button
                  }
              }

              // Set the active line image
              _activeLineImg = LineImgs[selectedIndex];
              _activeLineImg.gameObject.SetActive(true);


              // Deactivate all other line images
              for (int i = 0; i < LineImgs.Count; i++)
              {
                  if (i != selectedIndex)
                  {
                      LineImgs[i].gameObject.SetActive(false);
                  }
              }
          }
      }






}