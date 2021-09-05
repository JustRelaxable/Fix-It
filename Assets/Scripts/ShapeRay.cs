using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeRay : MonoBehaviour
{
    [SerializeField]private int rayPrecision = 10;
    [SerializeField] private bool debugRays = false;
    private MeshFilter _meshFilter;
    private Vector3[] _rayVertices;
    private int _shapeHits = 0;
    private Bounds _bounds;
    private List<Ray> _raysThatHitShape = new List<Ray>();

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _bounds = _meshFilter.mesh.bounds;
    }

    private void Start()
    {
        _rayVertices = GetRayVertices(rayPrecision);
        CalculateShapeRayHits();
        EventManager.instance.OnShapeRayInitialized.Invoke(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            float mal = GetRayPercentage();
            print(mal.ToString("F7"));
        }
        //print(transform.TransformPoint(_meshFilter.mesh.bounds.size));

        DebugRays();
    }

    private void DebugRays()
    {
        if (debugRays)
        {
            for (int i = 0; i < _raysThatHitShape.Count; i++)
            {
                Debug.DrawRay(_raysThatHitShape[i].origin, _raysThatHitShape[i].direction, Color.blue);
            }
        }
    }

    public float GetRayPercentage()
    {
        float hittedRays = 0;

        foreach (var ray in _raysThatHitShape)
        {
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Shape"))
                {
                    hittedRays++;
                }
            }
        }

        return 1-(hittedRays / _shapeHits);
    }

    private void CalculateShapeRayHits()
    {
        int verticesCount = _rayVertices.Length;
        int hittedRays = 0;

        foreach (var vertex in _rayVertices)
        {
            Ray ray = new Ray(transform.TransformPoint(vertex), Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Shape"))
                {
                    hittedRays++;
                    _raysThatHitShape.Add(ray);
                }
            }
        }
        _shapeHits = hittedRays;
    }

    private Vector3[] GetRayVertices(int precision)
    {
        float lengthX = _bounds.extents.x * 2;
        float lengthZ = _bounds.extents.y * 2;

        float precisionX = lengthX / (precision-1);
        float precisionZ = lengthZ / (precision-1);

        var rayPoints = new Vector3[precision * precision];

        for (int i = 0; i < precision; i++)
        {
            for (int j = 0; j < precision; j++)
            {
                int index = i * precision + j;
                rayPoints[index] = _bounds.min + new Vector3(precisionX * j, precisionZ * i,0);
            }
        }

        return rayPoints;
    }
}
