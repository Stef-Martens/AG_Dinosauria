using UnityEngine;

public class Fly : MonoBehaviour
{
    public float OrbRadius = 0.9f;
    public float MoveSpeed = 1.5f;
    
    private Vector2 _orbCenter;
    private Vector2 _targetPos;
    private Vector2 _randomDirection;
    private float _moveSpeedFactor = 10.0f;
    private float _smoothFactor = 0.1f;

    void Start()
    {
        _orbCenter = transform.position;
        _randomDirection = GetRandomDirection();
        _targetPos = transform.position;
    }

    void Update()
    {
        Move();
        //SetOrbVisualization();
    }

    private void Move()
    {
        Vector2 newPosition = _targetPos + (_randomDirection * MoveSpeed * _moveSpeedFactor * Time.deltaTime);
        float distance = Vector2.Distance(newPosition, _orbCenter);

        if (distance > OrbRadius)
        {
            _randomDirection = GetRandomDirection();
        }

        // Smoothly update the object's position
        _targetPos = Vector2.Lerp(_targetPos, newPosition, _smoothFactor);
        transform.position = _targetPos;
    }

    private void SetOrbVisualization()
    {
        GameObject orbVisualization;

        orbVisualization = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        orbVisualization.transform.localScale = new Vector3(OrbRadius * 2, OrbRadius * 2, OrbRadius * 2);
        orbVisualization.GetComponent<Renderer>().material = new Material(Shader.Find("Sprites/Default"));
        orbVisualization.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        orbVisualization.transform.position = _orbCenter;
    }

    private Vector2 GetRandomDirection()
    {
        return Random.insideUnitCircle.normalized;
    }
}