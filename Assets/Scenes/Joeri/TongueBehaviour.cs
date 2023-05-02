using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class TongueBehaviour : MonoBehaviour
{
    public InputV1 InputV1;
    public ChameleonMoveV2 ChameleonMove;

    public GameObject TongueIndicator;
    public LineRenderer IndicatorVisual;
    public LineRenderer FlatVisual;
    public GameObject TongueCurl;
    public GameObject CurlAtMouth;

    public float IndicatorMoveSpeed = 30f;
    public float RetractionSpeed = 150f;

    public LayerMask Default;

    private Vector3 _curlNewPos;
    private Quaternion _curlOrigRotation;

    private Quaternion _curlNewRotation;

    private bool _canShoot = true;
    private bool _hasHitObject = false;
    private bool _canRegisterNewInput = true;

    public bool _isExtended = false;
    private Stopwatch _extensionDelayTimer;
    private float _extensionDelay = 0.3f;

    private float _currentIndicatorDegrees = 0f;

    public bool _isShotWhenFacingRight;

    //public Vector3 Direction;
    private Vector3 curlVisualPos;
    public bool _test = false;

    private RaycastHit _hit;

    private Vector3 direction2;



    public GameObject objTest;

    public bool _canGetPreviousHitObjectPostion = true;

    public Vector3 _previousHitObjectPosition;
    public GameObject _currentHitObject;

    public bool _isDirecthit = false;

    private void Awake()
    {
        FlatVisual.SetPosition(0, TongueIndicator.transform.position);
        FlatVisual.SetPosition(1, TongueIndicator.transform.position);

        _curlOrigRotation = TongueCurl.transform.localRotation;

        _extensionDelayTimer = new Stopwatch();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (_isExtended)
        {
            curlVisualPos = TongueCurl.transform.GetChild(0).transform.position;
            var direction = curlVisualPos - CurlAtMouth.transform.position;
            direction = new Vector3(direction.x, direction.y, 0);

            UnityEngine.Debug.DrawRay(CurlAtMouth.transform.position, direction, Color.blue);

            Ray ray = new Ray(CurlAtMouth.transform.position, direction);

            //  UnityEngine.Debug.DrawRay(CurlAtMouth.transform.position, direction);
            // UnityEngine.Debug.Log(TongueCurl.transform.position);
            if (Physics.Raycast(ray, out hit, 9999, Default))
            //  if (Physics.Raycast(ray, out hit, Vector3.Distance(CurlAtMouth.transform.position, curlVisualPos), Default)
            //   && hit.transform.GetComponent<FlyBehaviourV2>())
            {
                //  UnityEngine.Debug.Log(Vector3.Distance(CurlAtMouth.transform.position, curlVisualPos) + " to curl");
                // UnityEngine.Debug.Log(Vector3.Distance(CurlAtMouth.transform.position, hit.transform.position) + " to hit");

                // UnityEngine.Debug.DrawRay(CurlAtMouth.transform.position, direction);


                /*  if (curlVisualPos.y <= hit.transform.gameObject.transform.position.y)
                  {
                      UnityEngine.Debug.Log("jaja");
                      hit.transform.gameObject.SetActive(false);
                  }*/


                // if(hit.transform.GetComponent<FlyV1>().CanDestroy) { hit.transform.gameObject.SetActive(false); }




                // UnityEngine.Debug.Log("opiqreioajripojpoijagijreoigjiorea");
                if (hit.transform.GetComponent<FlyBehaviourV2>())
                {
                    if (!_isDirecthit)
                    {


                        //  UnityEngine.Debug.Log("bleg");


                        _currentHitObject = hit.transform.gameObject;
                    }
                }
                else
                    _currentHitObject = null;



                /*
                                    direction2 = hit.transform.position - CurlAtMouth.transform.position;
                                direction2 = new Vector3(direction2.x, direction2.y, 0);
                                _hit = hit;


                                objTest = _hit.transform.gameObject;
                                _test = true;





                                */


                //   UnityEngine.Debug.DrawRay(CurlAtMouth.transform.position, direction2, Color.blue);
                //  UnityEngine.Debug.DrawRay(CurlAtMouth.transform.position,
                //   direction2.normalized * Vector3.Distance(CurlAtMouth.transform.position, curlVisualPos), Color.yellow);




                // float lowerBound = hit.transform.position.y - hit.transform.GetChild(0).transform.GetComponent<Renderer>().bounds.size.y / 2;
                //float upperBound = hit.transform.position.y + hit.transform.GetChild(0).transform.GetComponent<Renderer>().bounds.size.y / 2;

                //     UnityEngine.Debug.Log(Vector3.Distance(CurlAtMouth.transform.position, direction2.normalized * Vector3.Distance(CurlAtMouth.transform.position, curlVisualPos)) + " curlnorm");
                //   UnityEngine.Debug.Log(Vector3.Distance(CurlAtMouth.transform.position, direction2) + " hit");




                /*   if (Vector3.Distance(CurlAtMouth.transform.position, direction2.normalized * Vector3.Distance(CurlAtMouth.transform.position, curlVisualPos))

                           <=
                       Vector3.Distance(CurlAtMouth.transform.position, direction2) )
                       hit.transform.gameObject.SetActive(false);*/





                //_test = true;
                //if(FlatVisual.GetPosition(1).y <= (hit.transform.position.y - hit.transform.GetChild(0).transform.GetComponent<Renderer>().bounds.size.y /2))





            }


            /* else //{
                 _test = false; */








            // _hit = new RaycastHit(); }

            // if(_test) {


            //
            //   if (curlVisualPos.y == hit.transform.position.y)
            //     hit.transform.gameObject.SetActive(false);
            // }








            //  else _previousHitObjectPosition = Vector3.zero;
        }
    }

    void Update()
    {
        //UnityEngine.Debug.Log(_isDirecthit + " direct hit");

        // if (InputV1.ShootTongue && _isDirecthit)
        //  _isDirecthit = true;

        CheckHitWhenTongueRetracts();

        SetCurlRotation();

        IndicatorVisual.SetPosition(0, TongueIndicator.transform.position);
        FlatVisual.SetPosition(0, TongueIndicator.transform.position);

        if (_canShoot)
            FlatVisual.SetPosition(1, TongueIndicator.transform.position);

        RaycastHit hit;
        Ray ray = new Ray(TongueIndicator.transform.position, TongueIndicator.transform.TransformDirection(Vector3.right));

        if (Physics.Raycast(ray, out hit, 9999, Default))
        {
            IndicatorVisual.SetPosition(1, hit.point);
            //  UnityEngine.Debug.Log(hit.transform.gameObject.name);
            // if (hit.transform.GetComponent<FlyBehaviourV2>() && InputV1.ShootTongue)
            //  hit.transform.gameObject.SetActive(false);

            if (hit.transform.GetComponent<FlyBehaviourV2>() && InputV1.ShootTongue && !_isDirecthit)

            {
                _isDirecthit = true;
                _currentHitObject = hit.transform.gameObject;

                hit.transform.gameObject.SetActive(false);

                //UnityEngine.Debug.Log("Destroyed hier");


            }

        }

        ShootTongue(hit);
        MoveIndicator();
    }


    private void CheckHitWhenTongueRetracts()
    {
        if (_currentHitObject != null && _canGetPreviousHitObjectPostion)
        {

            if (_previousHitObjectPosition == Vector3.zero)
            {
                _previousHitObjectPosition = _currentHitObject.transform.position;
                _canGetPreviousHitObjectPostion = false;

            }


            else if (_currentHitObject.transform.position.y <= _previousHitObjectPosition.y)

            {
                _previousHitObjectPosition = _currentHitObject.transform.position;
                _canGetPreviousHitObjectPostion = false;
            }



            /*  if (curlVisualPos.y <= _currentHitObject.transform.position.y
              && _currentHitObject.GetComponent<FlyV1>())
              {

                  UnityEngine.Debug.Log("hier");
                  _currentHitObject.gameObject.SetActive(false);



                  /*  if (_previousHitObject != null)
                    {
                        if (_currentHitObject.transform.position.y <= _previousHitObject.transform.position.y)
                        {
                            _currentHitObject.gameObject.SetActive(false);
                            _previousHitObject = _currentHitObject;
                        }
                    }
              }*/
        }


        if (_previousHitObjectPosition != Vector3.zero && _currentHitObject != null)
        {
            var lowerBoundX = _currentHitObject.transform.position.x -
                              (_currentHitObject.transform.GetChild(0).transform.GetComponent<Renderer>().bounds.size.x / 2);
            var upperBoundX = _currentHitObject.transform.position.x +
                              (_currentHitObject.transform.GetChild(0).transform.GetComponent<Renderer>().bounds.size.x / 2);

            // curlVisualPos = TongueCurl.transform.GetChild(0).transform.position;
            // var direction = curlVisualPos - _currentHitObject.transform.position;
            // direction = new Vector3(direction.x, direction.y, 0);


            float dist = Vector3.Distance(_currentHitObject.transform.position, TongueCurl.transform.GetChild(0).transform.position);

            // UnityEngine.Debug.Log(dist + "distance");

            if (_currentHitObject.transform.position.y <= _previousHitObjectPosition.y
                && dist <= 2f)
            /// && curlVisualPos.x >= lowerBoundX*1.2f
            //&& curlVisualPos.x <= upperBoundX*1.2f)
            {
                //UnityEngine.Debug.Log("hier2");
                _currentHitObject.gameObject.SetActive(false);
            }
        }

        if (_currentHitObject == null)
        {
            if (_previousHitObjectPosition != Vector3.zero
            )
            {
                _canGetPreviousHitObjectPostion = true;
            }
        }

        if (TongueCurl.transform.position == CurlAtMouth.transform.position && !InputV1.ShootTongue)
        {
            _previousHitObjectPosition = Vector3.zero;
            _canGetPreviousHitObjectPostion = true;
            _currentHitObject = null;
            _isDirecthit = false;
        }


















        /*if(_test && _hit.transform.gameObject != null ) {
            //if (Vector3.Distance(CurlAtMouth.transform.position, direction2.normalized * Vector3.Distance(CurlAtMouth.transform.position, curlVisualPos))

            // <=
            // Vector3.Distance(CurlAtMouth.transform.position, direction2))


            if (curlVisualPos.y <= _hit.transform.gameObject.transform.position.y)
            {
                UnityEngine.Debug.Log("jaja");
                _hit.transform.gameObject.SetActive(false);
                _test = false;
            }
        }
*/





        // UnityEngine.Debug.Log(_test);


    }

    private void ShootTongue(RaycastHit hit)
    {
        if (InputV1.ShootTongue && _canShoot && _canRegisterNewInput)
        {
            //CheckHit();

            _isExtended = true;

            if (Vector3.Normalize(this.transform.localScale).x > 0)
                _isShotWhenFacingRight = true;
            else _isShotWhenFacingRight = false;
        }

        if (_canShoot && _isExtended)
        {
            if (!_hasHitObject)
            {
                _curlNewPos = new Vector3(hit.point.x, hit.point.y, hit.point.z - .01f);
                _curlNewRotation = TongueIndicator.gameObject.transform.localRotation;
                _hasHitObject = true;
            }

            FlatVisual.SetPosition(1, _curlNewPos);
            TongueCurl.transform.localRotation = _curlNewRotation;

            _canShoot = false;
            _extensionDelayTimer.Start();
        }

        if (!_canShoot)
        {

            TongueCurl.transform.position = FlatVisual.GetPosition(1);


            if (!_isShotWhenFacingRight)
            {
                Vector3 direction = FlatVisual.GetPosition(1) - FlatVisual.GetPosition(0);
                direction = new Vector3(direction.x, direction.y, 0);

                Quaternion rot = Quaternion.FromToRotation(-Vector3.right.normalized,
                                                  new Vector3(direction.x, direction.y, direction.z));

                TongueCurl.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, 180, -rot.eulerAngles.z);
            }
        }

        if (!_canShoot && _extensionDelayTimer.Elapsed.TotalSeconds >= _extensionDelay)
        {
            Vector3 moveTowards = Vector3.MoveTowards(FlatVisual.GetPosition(1),
                                                       CurlAtMouth.transform.position,
                                                       Time.deltaTime * RetractionSpeed);

            FlatVisual.SetPosition(1, moveTowards);
            TongueCurl.transform.position = moveTowards;
        }

        if (TongueCurl.transform.position == CurlAtMouth.transform.position)
        {
            TongueCurl.transform.localRotation = _curlOrigRotation;
            _canShoot = true;
            _hasHitObject = false;

            _isExtended = false;
            _extensionDelayTimer.Stop();
            _extensionDelayTimer.Reset();
        }

        if (InputV1.ShootTongue && _isExtended)
            _canRegisterNewInput = false;

        if (!InputV1.ShootTongue)
            _canRegisterNewInput = true;

        //  UnityEngine.Debug.Log(Vector3.Normalize(this.transform.localScale));

        if (!_isExtended)

        {
            TongueCurl.transform.position = CurlAtMouth.transform.position;
            //      TongueCurl.transform.localScale = ChameleonMove.transform.localScale.normalized;

            if (Vector3.Normalize(this.transform.localScale).x > 0)
            {

                TongueCurl.transform.rotation = Quaternion.Euler(TongueCurl.transform.rotation.x, 0, TongueCurl.transform.rotation.z);
            }

            if (Vector3.Normalize(this.transform.localScale).x < 0)
                TongueCurl.transform.rotation = Quaternion.Euler(TongueCurl.transform.rotation.x, -180, TongueCurl.transform.rotation.z);



            // if (InputV1.Move == 1)
            //   TongueCurl.transform.rotation = Quaternion.Euler(TongueCurl.transform.rotation.x, 0, TongueCurl.transform.rotation.z);

        };
    }

    private void MoveIndicator()
    {

        if (InputV1.MoveIndicatorUp)
        {

            if (Vector3.Normalize(this.transform.localScale).x > 0)
            {
                _currentIndicatorDegrees = Mathf.MoveTowards(_currentIndicatorDegrees, 90, Time.deltaTime * IndicatorMoveSpeed);
                TongueIndicator.transform.rotation = Quaternion.Euler(0, 0, _currentIndicatorDegrees);
            }

            if (Vector3.Normalize(this.transform.localScale).x < 0)
            {
                _currentIndicatorDegrees = Mathf.MoveTowards(_currentIndicatorDegrees, 90, Time.deltaTime * IndicatorMoveSpeed);

                TongueIndicator.transform.rotation = Quaternion.Euler(0, 0, -_currentIndicatorDegrees);
            }
        }


        if (InputV1.MoveIndicatorDown)
        {

            if (Vector3.Normalize(this.transform.localScale).x > 0)
            {
                _currentIndicatorDegrees = Mathf.MoveTowards(_currentIndicatorDegrees, 0, Time.deltaTime * IndicatorMoveSpeed);
                TongueIndicator.transform.rotation = Quaternion.Euler(0, 0, _currentIndicatorDegrees);
            }

            if (Vector3.Normalize(this.transform.localScale).x < 0)
            {
                _currentIndicatorDegrees = Mathf.MoveTowards(_currentIndicatorDegrees, 0, Time.deltaTime * IndicatorMoveSpeed);
                TongueIndicator.transform.rotation = Quaternion.Euler(0, 0, -_currentIndicatorDegrees);
            }


        }
    }

    private void SetCurlRotation()
    {
        Vector3 direction = FlatVisual.GetPosition(1) - FlatVisual.GetPosition(0);
        direction = new Vector3(direction.x, direction.y, 0);

        if (ChameleonMove.IsFacingRight && _isShotWhenFacingRight)
        {
            TongueCurl.transform.rotation = Quaternion.FromToRotation(Vector3.right.normalized, direction);
        }

        if (!ChameleonMove.IsFacingRight && _isShotWhenFacingRight)
        {
            Quaternion rot = Quaternion.FromToRotation(Vector3.right.normalized,
                                                       new Vector3(direction.x, direction.y, direction.z));

            TongueCurl.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, 180, -rot.eulerAngles.z);
        }

        if (!ChameleonMove.IsFacingRight && !_isShotWhenFacingRight)
        {
            TongueCurl.transform.rotation = Quaternion.FromToRotation(-Vector3.right.normalized, direction);
        }

        if (ChameleonMove.IsFacingRight && !_isShotWhenFacingRight)
        {
            Quaternion rot = Quaternion.FromToRotation(-Vector3.right.normalized,
                                                       new Vector3(direction.x, direction.y, direction.z));

            TongueCurl.transform.rotation = Quaternion.Euler(rot.eulerAngles.x, 180, -rot.eulerAngles.z);
        }
    }

    private void CheckHit()
    {
        RaycastHit hit;
        Ray ray = new Ray(TongueIndicator.transform.position, TongueIndicator.transform.TransformDirection(Vector3.right));

        if (Physics.Raycast(ray, out hit, 9999, Default) && hit.transform.GetComponent<FlyBehaviourV2>())
            hit.transform.gameObject.SetActive(false);



    }
}