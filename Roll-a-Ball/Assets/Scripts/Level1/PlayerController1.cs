using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController1 : MonoBehaviour
{
	public float speed;
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	public bool hasWon = false; //variabila pentru a urmari daca a castigat
	public float movementX;
	private float movementY;
	public AudioClip coinSound;
	public Rigidbody rb;
	public int count;
	public float timeLeft = 30f; // am adaugat variabila pentru timp si setam un timp initial de 10 de secunde

	public void RestartGame() {
		Time.timeScale = 1f;
		SceneManager.LoadScene("MiniGame");
		//Debug.Log("Game open");
		/*GameObject[] gameObjects = FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];

		foreach (GameObject g in gameObjects)
		{
			if (g.tag == "PickUp")
			{
				g.SetActive(true);
			}
		}

		count = 0;
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = new Vector3(0, 0.04f, -8.34f);
		hasWon = false;
		SetCountText();

		timeLeft = 30f;
		timeText.gameObject.SetActive(true);

		winTextObject.SetActive(false);
		gameObject.SetActive(true);*/
	}

	public void Die()
	{
		winTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over!";
		winTextObject.SetActive(true);
		gameObject.SetActive(false);
	}

	void FixedUpdate()
	{

		Vector3 movement = new Vector3(movementX, 0.0f, movementY); // se creeaza un vector de miscare in functie de valorile obtinute
		rb.AddForce(movement * speed); //se aplica o forta asupra jucatorului pentru a-l misca

		// decrementam timpul ramas
		timeLeft -= Time.deltaTime;
		// actualizam textul pentru timpul ramas
		timeText.GetComponent<TextMeshProUGUI>().text = "Time Left: " + Mathf.RoundToInt(timeLeft).ToString();
		// daca timpul a expirat, afisam mesajul de Game Over si dezactivam jucatorul
		if (!hasWon && timeLeft < 0f) // verificam daca jocul a fost pierdut
		{
			Die(); //apelam functia pentru Game Over
		}

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) //daca jucatorul apasa tasta space
		{
			rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse); //este adaugata o forta de tip impuls asupra obiectului in sus, pe axa Y, forta de impuls este de 5 unitati pe axa Y
		}
		if (transform.position.y < -1f) // verificam daca jucatorul a cazut sub nivelul planului
		{
			Die();
		}
	}

	public void Start()
	{
		if (!hasWon && timeLeft < 0f)
		{
			winTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over!";
			winTextObject.SetActive(true);
			gameObject.SetActive(false);
		}
		//componenta Rigibody este responsabila pentru simularea fizicii obiectelor unui joc		rb = GetComponent<Rigidbody>();
		count = 0;
		SetCountText(); // se afiseaza textul pentru numarul de obiecte selectate, care a fost setat la 0 intial
		winTextObject.SetActive(false);
	}


	void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.CompareTag("PickUp"))  //daca jucatorul intra in contat cu un obiect de colectat(PickUp)
		{
			other.gameObject.SetActive(false); // face ca obiectul sa dispara

			Debug.Log("DEZACTIVAT");

			count = count + 1; //numarul de obiecte colectate e incrementat
			AudioSource.PlayClipAtPoint(coinSound, transform.position);
			SetCountText(); //se actualizeaza textul
		}
		else if (other.gameObject.CompareTag("Kill"))
		{
			Die();
		}
	}

	void OnMove(InputValue value)
	{
		Vector2 v = value.Get<Vector2>(); //cand dispoztivul de control este miscat, afla coordonatele lui x si y
		movementX = v.x;//sunt stocate in movementX/Y pentru a actualiza miscarea jucatorul in FixedUpdate().
		movementY = v.y;
	}



	public void SetCountText()
	{

		countText.text = "Count: " + count.ToString(); //actualizeaza numarul de obiecte colecatte
		if (count >= 16)
		{
			hasWon = true;
			timeText.gameObject.SetActive(false); //dezactivãm textul timpului rãmas
			winTextObject.SetActive(true); // daca a colectat toate obiecte se actoveaza textul YouWin
		}
		if (hasWon)
		{
			float timeTaken = 30f - timeLeft; //calculeaza timpul petrecut
			winTextObject.GetComponent<TextMeshProUGUI>().text += "\nTime: " + Mathf.RoundToInt(timeTaken).ToString() + "s"; // afiseaza textul YouWin si timpul petrecut
		}
	}
}
