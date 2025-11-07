using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private LineRenderer _line;
    [SerializeField, Range(2, 50)] private int _segmentCount = 10;

    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [SerializeField] private HingeJoint _hingeJointPrefab;

    public Transform[] segments;

    void Start()
    {
        GenerateRope();
    }

    void Update()
    {
        SetEndOfRopePositions();
        SetLinePoints();
    }

    private void SetEndOfRopePositions()
    {
        if (segments.Length > 0)
        {
            segments[0].position = _pointA.position;
            segments[segments.Length - 1].position = _pointB.position;
        }
    }

    private void SetLinePoints()
    {
        if (_line == null || segments == null || segments.Length == 0)
            return;

        _line.positionCount = segments.Length;
        for (int i = 0; i < segments.Length; i++)
        {
            _line.SetPosition(i, segments[i].position);
        }
    }

    private Vector3 GetSegmentPosition(int segmentIndex)
    {
        Vector3 posA = _pointA.position;
        Vector3 posB = _pointB.position;
        float fraction = (float)segmentIndex / (_segmentCount - 1);
        return Vector3.Lerp(posA, posB, fraction);
    }

    private void GenerateRope()
    {
        segments = new Transform[_segmentCount];

        for (int i = 0; i < _segmentCount; i++)
        {
            var currJoint = Instantiate(_hingeJointPrefab, GetSegmentPosition(i), Quaternion.identity, this.transform);
            segments[i] = currJoint.transform;

            if (i > 0)
            {
                int prevIndex = i - 1;
                currJoint.connectedBody = segments[prevIndex].GetComponent<Rigidbody>();
            }
        }

        // Ujung pertama nyambung ke pointA
        var firstJoint = segments[0].GetComponent<HingeJoint>();
        firstJoint.connectedBody = _pointA.GetComponent<Rigidbody>();

        // Ujung terakhir nyambung ke pointB
        var lastJoint = segments[segments.Length - 1].GetComponent<HingeJoint>();
        lastJoint.connectedBody = _pointB.GetComponent<Rigidbody>();
    }
}
