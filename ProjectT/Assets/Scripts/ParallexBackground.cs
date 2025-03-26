using UnityEngine;

public class ParallexBackground : MonoBehaviour
{
    [SerializeField] private GameObject cam;

    [SerializeField] private float parallexEffect;

    private float xPosition;
    private float lenght;
    private void Start()
    {
        cam = GameObject.Find("Main Camera");
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;

        xPosition = transform.position.x;
    }

    private void Update()
    {
        float distanceMove = cam.transform.position.x * (1 - parallexEffect);
        float distanceToMove = cam.transform.position.x * parallexEffect;

        transform.position = new Vector2(xPosition + distanceToMove, transform.position.y);

        if(distanceMove > xPosition+lenght)
            xPosition = xPosition +lenght;
        else if(distanceMove < xPosition - lenght)
            xPosition = xPosition - lenght;
    }


}
