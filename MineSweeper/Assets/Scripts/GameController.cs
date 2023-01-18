using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject block;

    public static int w = 10; //largura
    public static int h = 13; //altura

    public static Element[,] elements = new Element[w, h]; //aqui cria uma matriz de elementos registra todo os blocos que possui

    void Start()
    {
        CreateCamp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //cria o campo minado
    private void CreateCamp() {

        for (int i = 0; i < w; i++) {

            for (int j = 0; j < h; j++) { 
                 
                Instantiate(block, new Vector3(i, j, 0f), Quaternion.identity);
            }

        }
    }

    public static void UncoverMines() { 

        foreach(Element item in elements) {

            if (item.mine) {
                item.LoadTexture(0);
            }
        }
    }
}
