using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//LOOTBOX!!!!

public class MovePlate : MonoBehaviour
{
    public GameObject controller;
    GameObject reference = null;
    GameObject enemey = null;

    // Board postions not world positions 
    int matrixX;
    int matrixY;

    //false: movement, true :attacking

    public bool attack = false;


    public void Start()
    {
        if (attack)
        {
            // change to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);

        }
    }

    public int GetRandomNumber()
    {
        int num = Random.Range(0, 8);
        return num;
    }


    public int enemeyMoveX(int martrixX)
    {
        if (martrixX == 7)
        {
            return Random.Range(-1, 1);
        }

        else if (martrixX == 0)
        {
            return Random.Range(0, 2);
        }

        else
        {
            return Random.Range(-1, 2);
        }

    }

    public int enemeyMoveY(int martrixY)
    {
        if (martrixY == 7)
        {
            return Random.Range(-1, 1);
        }

        else if (martrixY == 0)
        {
            return Random.Range(0, 2);
        }

        else
        {
            return Random.Range(-1, 2);
        }

    }




    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");
            Destroy(cp);
        }

        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        controller.GetComponent<Game>().SetPosition(reference);
        enemey = GameObject.FindGameObjectWithTag("black_king");

        controller.GetComponent<Game>().SetPositionEmpty(enemey.GetComponent<Chessman>().GetXBoard(),
              enemey.GetComponent<Chessman>().GetYBoard());

        int offsetX = 0;
        int offsetY = 0;
        int KingX = controller.GetComponent<Game>().getKingPosition()[0];
        int KingY = controller.GetComponent<Game>().getKingPosition()[1];
        int[] KingXY = new int[2];

        while (offsetX == 0 && offsetY == 0)
        {
            offsetX = enemeyMoveX(KingX);
            offsetY = enemeyMoveY(KingY); break;

        }
        KingXY[0] = KingX + offsetX;
        KingXY[1] = KingY + offsetY;

        enemey.GetComponent<Chessman>().SetXBoard(KingX + offsetX);
        enemey.GetComponent<Chessman>().SetYBoard(KingY + offsetY);
        enemey.GetComponent<Chessman>().SetCoords();
        controller.GetComponent<Game>().setKingPosition(KingXY);
        controller.GetComponent<Game>().SetPosition(enemey);
        controller.GetComponent<Game>().moves++;

        // jhfj
        //
        // controller.GetComponent<Game>().NextTurn();



        //controller.GetComponent<Game>().NextTurn();
        //hjj
        reference.GetComponent<Chessman>().DestroyMovePlates();

    }
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }


    /*//not working AI
    private Stack getKingSquares(int x, int y, String piece) {
        Square startingSquare = new Square(x, y, piece);
        Stack moves = new Stack();
        Move validM, validM2, validM3, validM4;
        int tmpx1 = x + 1;
        int tmpx2 = x - 1;
        int tmpy1 = y + 1;
        int tmpy2 = y - 1;

        //if the kings movement to the left is not greater than 7 allow the piece to be moved, this prevents it from falling off the board
        if (!((tmpx1 > 7))) {//movement to the right

            Square tmp = new Square(tmpx1, y, newPiece(tmpx1, y));
            Square tmp1 = new Square(tmpx1, tmpy1, newPiece(tmpx1, tmpy1));
            Square tmp2 = new Square(tmpx1, tmpy2, newPiece(tmpx1, tmpy2));
            if (checkSurroundingSquares(tmp)) {// check the surrounding squares
                validM = new Move(startingSquare, tmp);
                if (!piecePresent(((tmp.getXC() * 75) + 20), (((tmp.getYC() * 75) + 20)))) {
                    moves.push(validM);
                } else {
                    if (!checkWhiteOponent(((tmp.getXC() * 75) + 20), (((tmp.getYC() * 75) + 20))) && (piece.contains("Black"))) {
                        moves.push(validM);
                    } else if (checkWhiteOponent(((tmp.getXC() * 75) + 20), (((tmp.getYC() * 75) + 20))) && (piece.contains("White"))) {
                        moves.push(validM);
                    }
                }
            }
            if (!(tmpy1 > 7)) { //kings movement down and to the right
                if (checkSurroundingSquares(tmp1)) {
                    validM2 = new Move(startingSquare, tmp1);
                    if (!piecePresent(((tmp1.getXC() * 75) + 20), (((tmp1.getYC() * 75) + 20)))) {
                        moves.push(validM2);
                    } else {
                        if (!checkWhiteOponent(((tmp1.getXC() * 75) + 20), (((tmp1.getYC() * 75) + 20))) && (piece.contains("Black"))) {
                            moves.push(validM2);
                        } else if (checkWhiteOponent(((tmp1.getXC() * 75) + 20), (((tmp1.getYC() * 75) + 20))) && (piece.contains("White"))) {
                            moves.push(validM2);
                        }

                    }
                }
            }
            if (!(tmpy2 < 0)) {//king movement up and to the right
                if (checkSurroundingSquares(tmp2)) {
                    validM3 = new Move(startingSquare, tmp2);
                    if (!piecePresent(((tmp2.getXC() * 75) + 20), (((tmp2.getYC() * 75) + 20)))) {
                        moves.push(validM3);
                    } else {
                        System.out.println("The values that we are going to be looking at are : " + ((tmp2.getXC() * 75) + 20) + " and the y value is : " + ((tmp2.getYC() * 75) + 20));
                        if (!checkWhiteOponent(((tmp2.getXC() * 75) + 20), (((tmp2.getYC() * 75) + 20))) && (piece.contains("Black"))) {
                            moves.push(validM3);
                        } else if (checkWhiteOponent(((tmp2.getXC() * 75) + 20), (((tmp2.getYC() * 75) + 20))) && (piece.contains("White"))) {
                            moves.push(validM3);
                        }

                    }
                }
            }
        }
        if (!((tmpx2 < 0))) { //movement to the left
            Square tmp3 = new Square(tmpx2, y, newPiece(tmpx2, y));
            Square tmp4 = new Square(tmpx2, tmpy1, newPiece(tmpx2, tmpy1));
            Square tmp5 = new Square(tmpx2, tmpy2, newPiece(tmpx2, tmpy2));
            if (checkSurroundingSquares(tmp3)) {
                validM = new Move(startingSquare, tmp3);
                if (!piecePresent(((tmp3.getXC() * 75) + 20), (((tmp3.getYC() * 75) + 20)))) {
                    moves.push(validM);
                } else {
                    if (!checkWhiteOponent(((tmp3.getXC() * 75) + 20), (((tmp3.getYC() * 75) + 20))) && (piece.contains("Black"))) {
                        moves.push(validM);
                    } else if (checkWhiteOponent(((tmp3.getXC() * 75) + 20), (((tmp3.getYC() * 75) + 20))) && (piece.contains("White"))) {
                        moves.push(validM);
                    }

                }
            }
            if (!(tmpy1 > 7)) { //movement down and to the left
                if (checkSurroundingSquares(tmp4)) {
                    validM2 = new Move(startingSquare, tmp4);
                    if (!piecePresent(((tmp4.getXC() * 75) + 20), (((tmp4.getYC() * 75) + 20)))) {
                        moves.push(validM2);
                    } else {
                        if (!checkWhiteOponent(((tmp4.getXC() * 75) + 20), (((tmp4.getYC() * 75) + 20))) && (piece.contains("Black"))) {
                            moves.push(validM2);
                        } else if (checkWhiteOponent(((tmp4.getXC() * 75) + 20), (((tmp4.getYC() * 75) + 20))) && (piece.contains("White"))) {
                            moves.push(validM2);
                        }
                    }
                }
            }
            if (!(tmpy2 < 0)) {//movement up and to the right
                if (checkSurroundingSquares(tmp5)) {
                    validM3 = new Move(startingSquare, tmp5);
                    if (!piecePresent(((tmp5.getXC() * 75) + 20), (((tmp5.getYC() * 75) + 20)))) {
                        moves.push(validM3);
                    } else {
                        if (!checkWhiteOponent(((tmp5.getXC() * 75) + 20), (((tmp5.getYC() * 75) + 20))) && (piece.contains("Black"))) {
                            moves.push(validM3);
                        } else if (checkWhiteOponent(((tmp5.getXC() * 75) + 20), (((tmp5.getYC() * 75) + 20))) && (piece.contains("White"))) {
                            moves.push(validM3);
                        }
                    }
                }
            }
        }
        Square tmp7 = new Square(x, tmpy1, newPiece(x, tmpy1));
        Square tmp8 = new Square(x, tmpy2, newPiece(x, tmpy2));
        if (!(tmpy1 > 7)) {
            if (checkSurroundingSquares(tmp7)) { //movement down the board
                validM2 = new Move(startingSquare, tmp7);
                if (!piecePresent(((tmp7.getXC() * 75) + 20), (((tmp7.getYC() * 75) + 20)))) {
                    moves.push(validM2);
                } else {
                    if (!checkWhiteOponent(((tmp7.getXC() * 75) + 20), (((tmp7.getYC() * 75) + 20))) && (piece.contains("Black"))) {
                        moves.push(validM2);
                    } else if (checkWhiteOponent(((tmp7.getXC() * 75) + 20), (((tmp7.getYC() * 75) + 20))) && (piece.contains("White"))) {
                        moves.push(validM2);
                    }

                }
            }
        }
        if (!(tmpy2 < 0)) {
            if (checkSurroundingSquares(tmp8)) { //movement up the board
                validM3 = new Move(startingSquare, tmp8);
                if (!piecePresent(((tmp8.getXC() * 75) + 20), (((tmp8.getYC() * 75) + 20)))) {
                    moves.push(validM3);
                } else {
                    if (!checkWhiteOponent(((tmp8.getXC() * 75) + 20), (((tmp8.getYC() * 75) + 20))) && (piece.contains("Black"))) {
                        moves.push(validM3);
                    } else if (checkWhiteOponent(((tmp8.getXC() * 75) + 20), (((tmp8.getYC() * 75) + 20))) && (piece.contains("White"))) {
                        moves.push(validM3);
                    }

                }
            }
        }
        return moves;
    } // end of the method getKingSquares*/

}

//LOOTBOX!!!!
