using System.Diagnostics;
using UnityEngine;

public class TongueBase : MonoBehaviour
{
    public LayerMask DefaultLayers;
    public LayerMask AnimalsLayer;
    public float IndicatorMoveSpeed = 30f;
    public float RetractionSpeed = 75f;

    private GameObject _chameleon;
    private Inputs _inputs;
    private ChameleonController2D _chameleonController2D;
    private GameObject _root;
    private GameObject _indicator;
    private LineRenderer _indicatorVisual;
    private float _currentIndicatorDegrees = 0f;
    private GameObject _body;
    private LineRenderer _bodyVisual;

    public GameObject Curl;

    private GameObject _curlMiddle;
    private bool _isExtended = false;
    private float _extensionDelay = 0.3f;
    private Stopwatch _extensionDelayTimer;
    private bool _isRetracting = false;
    private bool _canRegisterNewInput = true;
    private bool _canShoot = true;
    private bool _isShotWhenFacingRight;
    private RaycastHit _directHit;
    private bool _hasHitObject = false;
    private GameObject _currentIndirectHitObject;
    private Vector3 _previousHitObjectPos;
    private bool _isDirectHit = false;

    private AudioSource _shootAudio;

    void Awake()
    {
        _chameleon = this.transform.parent.gameObject;
        _inputs = _chameleon.GetComponent<Inputs>();
        _chameleonController2D = _chameleon.GetComponent<ChameleonController2D>();

        _root = this.transform.GetChild(0).gameObject;
        _indicator = this.transform.GetChild(1).gameObject;
        _body = this.transform.GetChild(2).gameObject;

        _indicatorVisual = _indicator.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
        _bodyVisual = _body.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
        _curlMiddle = Curl.transform.GetChild(0).gameObject;

        _extensionDelayTimer = new Stopwatch();

        _shootAudio = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        SetBodyVisualPositionAtRoot();
        SetCurlRotation();
        MoveIndicator();
        ShootAndRetract();
        DirectHit();
        CheckDirectHit();
        IndirectHit();
        CheckValidIndirectHit();
        HandleTriggerHitObjects();
    }

    private void SetBodyVisualPositionAtRoot()
    {
        _bodyVisual.SetPosition(0, _root.transform.position);

        if(_canShoot)
            _bodyVisual.SetPosition(1, _root.transform.position);
    }

    private void SetCurlRotation()
    {
        if (!_isExtended)
        {
            if (_chameleonController2D.IsFacingRight)
                Curl.transform.rotation = Quaternion.Euler(Curl.transform.rotation.x, 0, Curl.transform.rotation.z);
            else
                Curl.transform.rotation = Quaternion.Euler(Curl.transform.rotation.x, 180, Curl.transform.rotation.z);
            return;
        }
        
        Vector3 direction = _bodyVisual.GetPosition(1) - _bodyVisual.GetPosition(0);
        direction = new Vector3(direction.x, direction.y, 0);
        
        if (_chameleonController2D.IsFacingRight && _isShotWhenFacingRight)
        {
            Curl.transform.rotation = Quaternion.FromToRotation(Vector3.right.normalized, direction);
        }
        
        if (!_chameleonController2D.IsFacingRight && _isShotWhenFacingRight)
        {
            Quaternion rot = Quaternion.FromToRotation(Vector3.right.normalized,
                                                       new Vector3(direction.x, direction.y, direction.z));
            Curl.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, 0, rot.eulerAngles.z);
        }
        
        if (!_chameleonController2D.IsFacingRight && !_isShotWhenFacingRight)
        {
            Quaternion rot = Quaternion.FromToRotation(-Vector3.right.normalized, direction);
            
            Curl.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, 180, -rot.eulerAngles.z);
        }
        
        if (_chameleonController2D.IsFacingRight && !_isShotWhenFacingRight)
        {
            Quaternion rot = Quaternion.FromToRotation(-Vector3.right.normalized,
                                                       new Vector3(direction.x, direction.y, direction.z));
            Curl.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, 180, -rot.eulerAngles.z);
        }
    }

    private void SetCurlRotationWhenDirectHitObject()
    {
        // this method is called in ShootAndRetract

        if (_chameleonController2D.IsFacingRight)
            Curl.transform.localRotation = Quaternion.Euler(0, 0, _currentIndicatorDegrees);
        else
            Curl.transform.localRotation = Quaternion.Euler(0, -180, _currentIndicatorDegrees);
    }

    private void MoveIndicator()
    {
        if (_inputs.VerticalMove.y > 0)
        {
            if (_chameleonController2D.IsFacingRight)
            {
                _currentIndicatorDegrees = Mathf.MoveTowards(_currentIndicatorDegrees, 90, Time.deltaTime * IndicatorMoveSpeed);
                _indicator.transform.rotation = Quaternion.Euler(0, 0, _currentIndicatorDegrees);
            }

            if (!_chameleonController2D.IsFacingRight)
            {
                _currentIndicatorDegrees = Mathf.MoveTowards(_currentIndicatorDegrees, 90, Time.deltaTime * IndicatorMoveSpeed);

                _indicator.transform.rotation = Quaternion.Euler(0, -180, _currentIndicatorDegrees);
            }
        }

        if (_inputs.VerticalMove.y < 0)
        {
            if (_chameleonController2D.IsFacingRight)
            {
                _currentIndicatorDegrees = Mathf.MoveTowards(_currentIndicatorDegrees, 0, Time.deltaTime * IndicatorMoveSpeed);
                _indicator.transform.rotation = Quaternion.Euler(0, 0, _currentIndicatorDegrees);
            }

            if (!_chameleonController2D.IsFacingRight)
            {
                _currentIndicatorDegrees = Mathf.MoveTowards(_currentIndicatorDegrees, 0, Time.deltaTime * IndicatorMoveSpeed);
                _indicator.transform.rotation = Quaternion.Euler(0, -180, _currentIndicatorDegrees);
            }
        }
    }

    private void ShootAndRetract()
    {
        if (_inputs.Action)
        {
            if (_canRegisterNewInput && _canShoot)
            {
                _shootAudio.Play();
                _isExtended = true;

                if (_chameleonController2D.IsFacingRight) _isShotWhenFacingRight = true;
                else _isShotWhenFacingRight = false;
            }

            if (_isExtended)
            {
                _canRegisterNewInput = false;
            }
        }
        else
        {
            _canRegisterNewInput = true;
        }

        if (_canShoot)
        {
            if (_isExtended)
            {
                if (!_hasHitObject) _hasHitObject = true;

                SetCurlRotationWhenDirectHitObject();

                Vector3 indicatorVisualPos = _indicatorVisual.GetPosition(1);
                float offsetPosZ = 0.01f;
                Vector3 newBodyVisualPos = new Vector3(indicatorVisualPos.x,
                                                       indicatorVisualPos.y,
                                                       indicatorVisualPos.z - offsetPosZ);
                _bodyVisual.SetPosition(1, newBodyVisualPos);
                Curl.transform.position = indicatorVisualPos;

                _canShoot = false;
                _extensionDelayTimer.Start();
            }
        }
        else
        {
            if (_extensionDelayTimer.Elapsed.TotalSeconds >= _extensionDelay)
            {
                Vector3 moveTowards = Vector3.MoveTowards(_bodyVisual.GetPosition(1),
                                                          _root.transform.position,
                                                          Time.deltaTime * RetractionSpeed);
                _bodyVisual.SetPosition(1, moveTowards);
                Curl.transform.position = moveTowards;
                _isRetracting = true;
            }
        }

        if (_bodyVisual.GetPosition(1) == _root.transform.position)
        {
            Curl.transform.position = _root.transform.position;
         
            _isExtended = false;
            _extensionDelayTimer.Stop();
            _extensionDelayTimer.Reset();
            _isRetracting = false;
            _canShoot = true;
            _hasHitObject = false;
        }
    }

    private void DirectHit()
    {
        _indicatorVisual.SetPosition(0, _root.transform.position);

        LayerMask layerMask = DefaultLayers | AnimalsLayer;
        RaycastHit hit;
        Ray ray = new Ray(_indicator.transform.position, _indicator.transform.TransformDirection(Vector3.right));

        if (Physics.Raycast(ray, out hit, 9999, layerMask))
        {
            _indicatorVisual.SetPosition(1, hit.point);
            _directHit = hit;
        }
    }

    private void CheckDirectHit()
    {
        GameObject directHitObject = _directHit.transform.gameObject;

        if (_inputs.Action && !_isDirectHit)
        {
            _isDirectHit = true;
            _previousHitObjectPos = _curlMiddle.transform.position;

            if (directHitObject.GetComponent<Fly>())
                directHitObject.SetActive(false);

            if (directHitObject.GetComponent<RaptorBird>())
                UnityEngine.Debug.Log(directHitObject.name.ToString());
        }

        if (_bodyVisual.GetPosition(1) == _root.transform.position)
        {
            if (!_inputs.Action)
            {
                _isDirectHit = false;
            }
        }
    }

    private void IndirectHit()
    {
        if (_isRetracting)
        {
            RaycastHit hit;
            Vector3 curlVisualPos = Curl.transform.GetChild(0).transform.position;
            Vector3 direction = curlVisualPos - _root.transform.position;
            direction = new Vector3(direction.x, direction.y, 0);

            Ray ray = new Ray(_root.transform.position, direction);
            if (Physics.Raycast(ray, out hit, 9999, AnimalsLayer))
            {
                _currentIndirectHitObject = hit.transform.gameObject;
            }
        }
    }

    private void CheckValidIndirectHit()
    {
        float maxAllowedDistance = 2.0f;

        if(_currentIndirectHitObject != null)
        {
            if (_currentIndirectHitObject.transform.position.y <= _previousHitObjectPos.y)
            {
                float dist = Vector3.Distance(_currentIndirectHitObject.transform.position, _curlMiddle.transform.position);
                if (dist <= maxAllowedDistance)
                {
                    _previousHitObjectPos = _currentIndirectHitObject.transform.position;
                    
                    if(_currentIndirectHitObject.GetComponent<Fly>())
                        _currentIndirectHitObject.SetActive(false);

                    if(_currentIndirectHitObject.GetComponent<RaptorBird>())
                        UnityEngine.Debug.Log(_currentIndirectHitObject.name.ToString());
                }
            }
        }

        if(!_isDirectHit)
        {
            _currentIndirectHitObject = null;
            _previousHitObjectPos = Vector3.zero;
        }
    }

    private void HandleTriggerHitObjects()
    {
        GameObject triggerHitObject = Curl.GetComponent<TongueCurl>().TriggerHitObject;

        if (triggerHitObject != null)
        {
            if (triggerHitObject.GetComponent<Fly>())
                triggerHitObject.SetActive(false);

            if(triggerHitObject.GetComponent<RaptorBird>())
                UnityEngine.Debug.Log(triggerHitObject.name.ToString());
        }
    }
}