using UnityEngine;

namespace Services.Battle
{
    public class Slot : MonoBehaviour
    {
        public bool Turned;
        public int SlotNumber;
        [Space(10)] [ Header("Gizmo Settings")]
        public Color gizmoColor = new Color(1f, 0f, 0f, 0.3f);
        public float gizmoRadius = 0.5f;
        public float arrowLength = 0.7f;
        
        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, gizmoRadius);
            
            Vector3 start = transform.position;
            Vector3 end = start + transform.forward * arrowLength;
            
            Gizmos.color = Color.blue; 
            Gizmos.DrawLine(start, end);
            
            Vector3 right = Quaternion.Euler(0, 30, 0) * -transform.forward * 0.2f;
            Vector3 left = Quaternion.Euler(0, -30, 0) * -transform.forward * 0.2f;
            Gizmos.DrawLine(end, end + right);
            Gizmos.DrawLine(end, end + left);
        }
    }
}