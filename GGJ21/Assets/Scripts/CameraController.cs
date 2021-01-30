using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float upOffset = 0.5f;
    [SerializeField] private float marginOffset = 1f;
    [SerializeField] private float lookAheadOffset;
    private Vector3 followPosition;

    //private void Start()
    //{
    //    followPosition = transform.position;
    //}
    
    private float marginOffsetX;

    void Start()
    {
        marginOffsetX = marginOffset * (float)Screen.width / Screen.height;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Debug.Log(Mathf.Abs(Player.instance.transform.position.x - transform.position.x));
        if (Mathf.Abs(Player.instance.transform.position.y - transform.position.y - upOffset) > marginOffset ||
            Mathf.Abs(Player.instance.transform.position.x - transform.position.x) > marginOffsetX)
        {
            //Debug.Log("Should be following!");
            followPosition = Player.instance.transform.position - Vector3.forward * 10f + upOffset * Vector3.up + lookAheadOffset * Input.GetAxisRaw("Horizontal") * Vector3.right;
        }
        transform.position = Vector3.Lerp(transform.position, followPosition, followSpeed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(marginOffsetX, marginOffset, 1) * 2);
    }
}
