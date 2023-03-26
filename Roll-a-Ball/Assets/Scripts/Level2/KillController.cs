using UnityEngine;

public class KillController : MonoBehaviour
{
    public float speed = 1.0f; //viteza de miscare a obiectului
    public float upLimit = 3.0f; //limitele de miscare a obiectului
    public float downLimit = 1.0f;
    private int direction = 1; // variabila de directie a miscarii (1 pentru in sus, -1 pentru in jos)
    private Vector3 startPos; //pozitia initiala a obiectului


    void Start()
    {
        startPos = transform.position; //se stocheaza pozitia initiala a obiectului
    }

    void Update()
    {
        if (transform.position.y > startPos.y + upLimit) //daca obiectul a ajuns la limita superioara
        {
            direction = -1; //se schimba directia miscarii
        }
        else if (transform.position.y < startPos.y - downLimit) //daca obiectul a ajuns la limita inferioara
        {
            direction = 1; //se schimba directia miscarii
        }

        transform.Translate(0, speed * direction * Time.deltaTime, 0); //se muta obiectul in sus sau in jos, in functie de directia miscarii
    }

}
