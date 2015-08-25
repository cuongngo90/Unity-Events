using UnityEngine;
using System.Collections;

public class Damaged : MonoBehaviour
{

    Health _heath;
    // Use this for initialization
    void Start()
    {
        _heath = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            _heath.HP -= 10;
        }
    }
}
