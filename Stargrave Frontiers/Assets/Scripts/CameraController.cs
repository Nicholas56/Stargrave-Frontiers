using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10;

    public Vector2 panLimit;

    private void Start()
    {
        transform.position = new Vector3(-10, -10, -10);
        panLimit = new Vector2(PlayerProfile.mapSize.x / 2, PlayerProfile.mapSize.y / 2);
    }

    // Update is called once per frame
    void Update ()
    {
        Vector3 pos = transform.position;
        if (Input.GetKey("w")|| Input.GetKey(KeyCode.UpArrow)|| Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y, panLimit.y);

        transform.position = pos;
        transform.position = new Vector3(transform.position.x,transform.position.y,-10);
	}
}
