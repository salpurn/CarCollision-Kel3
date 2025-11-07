using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SpringJoint))]
public class RopeVisual : MonoBehaviour
{
    private LineRenderer line;
    private SpringJoint joint;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        joint = GetComponent<SpringJoint>();

        // gaya tali
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;
        line.positionCount = 2;
    }

    void Update()
    {
        if (joint.connectedBody != null)
        {
            // posisi anchor & objek
            Vector3 start = joint.connectedBody.transform.position;
            Vector3 end = transform.position;

            // gambar tali
            line.SetPosition(0, start);
            line.SetPosition(1, end);
        }
    }
}
