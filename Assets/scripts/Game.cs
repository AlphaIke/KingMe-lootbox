using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{

    //LOOTBOX!!!!

    public GameObject chesspiece;
    public GameObject crate;
    // Start is called before the first frame update

    static string tagUntagged = "Untagged";
    static string tagPlayer = "black_king";
    static string tagEnemy = "Enemy";


    // postions and team for each chesspiece
    private GameObject[,] positions = new GameObject[8, 8];

    public int[] positionsKing = new int[2];
    public int moves = 0;
    private GameObject[] playerBlack = new GameObject[16];
    public GameObject[] playerWhite = new GameObject[16];

    private string currentPlayer = "white";
    private bool gameOver = false;

    void Start()
    {
        playerWhite = new GameObject[]
        {

            Create("white_knight",7,7),
            Create("white_rook",0,0),
            Create("white_knight",1,0),
            Create("white_bishop",2,0),
            Create("white_queen",3,0)
           /* Create("white_rook",0,0), Create("white_knight",1,0),Create("white_bishop",2,0), Create("white_queen",3,0),
            Create("white_king",4,0), Create("white_bishop",5,0),Create("white_knight",6,0), Create("white_rook",7,0),
            Create("white_pawn",0,1), Create("white_pawn",1,1),Create("white_pawn",2,1), Create("white_pawn",3,1),
            Create("white_pawn",4,1), Create("white_pawn",5,1),Create("white_pawn",6,1),Create("white_pawn",7,1),*/


        };

        playerBlack = new GameObject[]
        {
            Create("black_king",0,3),
            /*Create("black_king",4,7),Create("black_rook",0,7), Create("black_knight",1,7),Create("black_bishop",2,7), Create("black_queen",3,7),
            Create("black_bishop",5,7),Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn",0,6), Create("black_pawn",1,6),Create("black_pawn",2,6), Create("black_pawn",3,6),
            Create("black_pawn",4,6), Create("black_pawn",5,6),Create("black_pawn",6,6), Create("black_pawn",7,6),*/

          
    };
        for (int i = 0; i < playerBlack.Length; i++)
        {
            playerBlack[i].tag = tagPlayer;
        }


        // set all pieces on the board 
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);

            SetPosition(playerWhite[i]);
        }
        playerWhite[1].SetActive(false);
        playerWhite[2].SetActive(false);
        playerWhite[3].SetActive(false);
        playerWhite[4].SetActive(false);
    }
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public GameObject CreateBox(float x, float y)
    {
        

            GameObject obj = Instantiate(crate, new Vector3(0, 0, -1), Quaternion.identity);
            obj.name = "crate";

            x *= 0.16f;
            y *= 0.16f;

            x += -2.3f;
            y += -2.3f;

            obj.transform.position = new Vector3(x, y, -1.0f);

            return obj;

            
        
    }


    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public int[] getKingPosition()
    {
        return positionsKing;
    }

    public void setKingPosition(int[] x)
    {
        positionsKing = x;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    } 

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game"); 
           
        }
       if (moves % 3 == 0)
       {
            CreateBox((float)Random.Range(1, 31), (float)Random.Range(1, 31));
            moves++;
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
        SoundManagerScript.PlaySound("Win");

        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>().text = playerWinner + " is the winner";

        GameObject.FindGameObjectWithTag("RestartText").GetComponent<Text>().enabled = true;
    }

    //LOOTBOX!!!!
}


