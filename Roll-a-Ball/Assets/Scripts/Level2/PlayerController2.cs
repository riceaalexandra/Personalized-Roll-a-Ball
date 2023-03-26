using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController2 : MonoBehaviour
{
	public float speed;
	public TextMeshProUGUI timeText;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;
	public bool hasWon = false; //variabila pentru a urmari daca a castigat
	public float movementX;
	private float movementY;
	private int heartsLeft;
	public Rigidbody rb;
	public int count;
	public float timeLeft = 200f; // am adaugat variabila pentru timp si setam un timp initial de 10 de secunde
	public AudioClip coinSound;

	public void RestartGame()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene("Level2");
		/*Debug.Log("Game open");
		GameObject[] gameObjects = FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
		foreach (GameObject g in gameObjects)
		{
			if (g.tag == "PickUp")
			{
				g.SetActive(true);
			}
			else if (g.tag == "Heart1" || g.tag == "Heart2" || g.tag == "Heart3")
			{
				g.SetActive(true);
			}
		}

		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		transform.position = new Vector3(0, 0.04f, -8.34f);

		heartsLeft = 4;
		count = 0;
		hasWon = false;
		SetCountText();

		timeLeft = 200f;
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
	//Modificari:
	void FixedUpdate()
	{
		Vector3 movement = new Vector3(movementX, 0.0f, movementY); // se creeaza un vector de miscare in functie de valorile obtinute
		rb.AddForce(movement * speed); //se aplica o forta asupra jucatorului pentru a-l misca
		// decrementam timpul ramas
		timeLeft -= Time.deltaTime;
		// actualizam textul pentru timpul ramas
		timeText.GetComponent<TextMeshProUGUI>().text = "Time Left: " + Mathf.RoundToInt(timeLeft).ToString();
		// daca timpul a expirat, afisam mesajul de Game Over si dezactivam jucatorul
		if (timeLeft < 0f)
		{
			Die();
		}

	}

	//Jump Ball.
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space)) //daca jucatorul apasa tasta space
		{
			rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse); //este adaugata o forta de tip impuls asupra obiectului in sus, pe axa Y, forta de impuls este de 5 unitati pe axa Y
		}
		if (transform.position.y < -1f) // verificam daca jucatorul a cazut sub nivelul planului
		{
			winTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over!";
			winTextObject.SetActive(true);
			gameObject.SetActive(false);
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.gameObject.CompareTag("Ground")) // verifica daca jucatorul a iesit din coliziune cu terenul
		{
			winTextObject.GetComponent<TextMeshProUGUI>().text = "Game Over!";
			winTextObject.SetActive(true);
			gameObject.SetActive(false);
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
		heartsLeft = 4;
		//componenta Rigibody este responsabila pentru simularea fizicii obiectelor unui joc
		rb = GetComponent<Rigidbody>();
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
			AudioSource.PlayClipAtPoint(coinSound, transform.position);
			count = count + 1; //numarul de obiecte colectate e incrementat
			SetCountText(); //se actualizeaza textul
		}
		else if (other.gameObject.CompareTag("Kill"))
		{
			if (heartsLeft == 4)
			{
				heartsLeft = heartsLeft - 1;
				GameObject heartObject = GameObject.FindWithTag("Heart" + heartsLeft);
				heartObject.SetActive(false);
				transform.position = new Vector3(0, 0.04f, -8.34f);
				Debug.Log("heart=3");
			}
			else if (heartsLeft == 3)
			{
				heartsLeft = heartsLeft - 1;
				GameObject heartObject = GameObject.FindWithTag("Heart"+ heartsLeft);
				heartObject.SetActive(false);
				transform.position = new Vector3(0, 0.04f, -8.34f);
				Debug.Log("heart=2");
			}
			else if (heartsLeft == 2)
			{
				heartsLeft = heartsLeft - 1;
				GameObject heartObject = GameObject.FindWithTag("Heart"+ heartsLeft);
				heartObject.SetActive(false);
				transform.position = new Vector3(0, 0.04f, -8.34f);
				Debug.Log("heart=1");
			}
			else if (heartsLeft == 1)
			{
				Die();
			}
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
		countText.text = "Count: " + count.ToString(); //actualozeaza numarul de obiecte colecatte
		if (count >= 16)
		{
			hasWon = true;
			timeText.gameObject.SetActive(false); //dezactivãm textul timpului rãmas
			winTextObject.SetActive(true); // daca a colectat toate obiecte se actoveaza textul YouWin
		}
		if (hasWon)
		{
			float timeTaken = 200f - timeLeft; //calculeaza timpul petrecut
			winTextObject.GetComponent<TextMeshProUGUI>().text += "\nTime: " + Mathf.RoundToInt(timeTaken).ToString() + "s"; // afiseaza textul YouWin si timpul petrecut
		}
	}
}
