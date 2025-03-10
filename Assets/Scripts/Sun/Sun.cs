using UnityEngine;

public class Sun : MonoBehaviour
{
    public float dropYPosition;
    public float speed;
    public LayerMask layerMask;

    private void Update()
    {
        if (transform.position.y > dropYPosition)
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
    }
}
