
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 velocity;

    public float _smoothTimeX = 0;

    public float offsetX = 0;

    public GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        float _posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + offsetX, ref velocity.x, _smoothTimeX);
        transform.position = new Vector3(_posX, transform.position.y, transform.position.z);
    }
}
