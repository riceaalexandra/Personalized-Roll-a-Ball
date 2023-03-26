using UnityEngine;

public class ObstacleRight : MonoBehaviour
{
    public float speed = 1.0f; //viteza de miscare a obiectului
    public float leftLimit = -3.0f; //limita stanga de miscare a obiectului
    public float rightLimit = 3.0f; //limita dreapta de miscare a obiectului
    private int direction = 1; // variabila de directie a miscarii (1 pentru la dreapta, -1 pentru la stanga)
    private Vector3 startPos; //pozitia initiala a obiectului

    void Start()
    {
        startPos = transform.position; //se stocheaza pozitia initiala a obiectului
    }

    void Update()
    {
        if (transform.position.x > startPos.x + rightLimit) //daca obiectul a ajuns la limita dreapta
        {
            direction = -1; //se schimba directia miscarii
        }
        else if (transform.position.x < startPos.x + leftLimit) //daca obiectul a ajuns la limita stanga
        {
            direction = 1; //se schimba directia miscarii
        }

        transform.Translate(speed * direction * Time.deltaTime, 0, 0); //se muta obiectul la stanga sau la dreapta, in functie de directia miscarii
    }

}
