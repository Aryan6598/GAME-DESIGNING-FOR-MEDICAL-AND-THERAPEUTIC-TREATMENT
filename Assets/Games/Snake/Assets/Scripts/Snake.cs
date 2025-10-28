using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene switching

public class Snake : MonoBehaviour
{
    public GameObject snakeBodyPrefab;
    public float moveSpeed = 0.5f;
    private Vector2 direction = Vector2.right;
    private List<Transform> snakeBody = new List<Transform>();
    public FoodSpawner foodSpawner;
    public GameObject gameOverImage;
    private bool isGameOver = false;

    void Start()
    {
        snakeBody.Add(this.transform);
        GrowSnake();
        InvokeRepeating("Move", 0.1f, moveSpeed);
        gameOverImage.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down) direction = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up) direction = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right) direction = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left) direction = Vector2.right;
    }

    void Move()
    {
        if (isGameOver) return;

        Vector2 previousPosition = snakeBody[0].position;
        Vector2 nextPosition = previousPosition + direction;

        // Rotate the head to face the direction
        if (direction == Vector2.up)
            transform.eulerAngles = new Vector3(0, 0, 90);
        else if (direction == Vector2.down)
            transform.eulerAngles = new Vector3(0, 0, -90);
        else if (direction == Vector2.left)
            transform.eulerAngles = new Vector3(0, 0, 180);
        else if (direction == Vector2.right)
            transform.eulerAngles = new Vector3(0, 0, 0);

        // Move the head
        snakeBody[0].position = nextPosition;

        // Move the body parts
        for (int i = 1; i < snakeBody.Count; i++)
        {
            Vector2 tempPosition = snakeBody[i].position;
            snakeBody[i].position = previousPosition;
            previousPosition = tempPosition;

            if (direction == Vector2.up || direction == Vector2.down)
                snakeBody[i].eulerAngles = new Vector3(0, 0, 90);
            else
                snakeBody[i].eulerAngles = new Vector3(0, 0, 0);
        }

        if (CheckSelfCollision())
        {
            GameOver();
        }
    }

    void GrowSnake()
    {
        GameObject newBodyPart = Instantiate(snakeBodyPrefab);
        newBodyPart.transform.position = snakeBody[snakeBody.Count - 1].position;
        newBodyPart.transform.position += (Vector3)(-direction * 0.5f);
        snakeBody.Add(newBodyPart.transform);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            GrowSnake();
            foodSpawner.SpawnFood();
        }
        else if (other.CompareTag("Boundary"))
        {
            GameOver();
        }
    }

    bool CheckSelfCollision()
    {
        for (int i = 1; i < snakeBody.Count; i++)
        {
            if (snakeBody[0].position == snakeBody[i].position)
            {
                return true;
            }
        }
        return false;
    }

    void GameOver()
    {
        isGameOver = true;
        gameOverImage.SetActive(true);
        CancelInvoke("Move");
    }

    // Function to restart the questionnaire
    public void ReturnToQuestionnaire()
    {
        SceneManager.LoadScene("QuestionnaireScene");  // Change to your actual questionnaire scene name
    }
}
