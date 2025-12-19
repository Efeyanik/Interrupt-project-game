using UnityEngine;

public class RockMover : MonoBehaviour
{
    public float speed = 9f;
    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*speed*Time.deltaTime,Space.World);



        if (transform.position.x <= -15)
            Destroy(gameObject);
        

    }
}
