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

    private int maxMines = 10;

    void Start()
    {
        CreateCamp();
        CreateMines();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                elements[(int)hit.collider.gameObject.transform.position.x, (int)hit.collider.gameObject.transform.position.y].UpdateFlag();
            }
        }
    }

    private void CreateMines()
    {
        for (int i = 0; i < maxMines; i++)
        {
            int x = Random.Range(0, w);
            int y = Random.Range(0, h);

             Debug.Log($"Mine: {x}, {y}");
            if (!elements[x, y].mine)
            {
                elements[x, y].SetUpMine();
                SetUpCounters(x, y);
            }
            else i--; // Se já tem uma mina na posição gerada, diminui contador pra gerar outra
        }
    }

    private void SetUpCounters(int x, int y)
    {
        int minX = MinX(x, y);
        int maxX = MaxX(x, y);
        int minY = MinY(x, y);
        int maxY = MaxY(x, y);

        //Debug.Log($"Min: {minX}, {minY} | Max: {maxX}, {maxY}");
        for (int i = minX; i <= maxX; i++)
        {
            for(int j = minY; j <= maxY; j++)
            {
                //Debug.Log($"{i}, {j}");
                if (i != x || j != y)
                   elements[i, j].AddNearbyMines();
            }
        }
    }

    int MinX(int x, int y) {
        return Mathf.Clamp(x - 1, 0, x);
    }

    int MaxX(int x, int y) {
        return Mathf.Clamp(x + 1, x, w - 1); 
    }

    int MinY(int x, int y) {
        return Mathf.Clamp(y - 1, 0, y);
    }

    int MaxY(int x, int y)
    {
        return Mathf.Clamp(y + 1, y, h - 1);
    }

    public void ScanAround(int x, int y)
    {
        int minX = MinX(x, y);
        int maxX = MaxX(x, y);
        int minY = MinY(x, y);
        int maxY = MaxY(x, y);
        Debug.Log($"Scan Around from {x}, {y}");

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                Debug.Log($"{i}, {j}");
                if (i != x || j != y)
                    elements[i, j].UncoverElement();
            }
        }
    }

    //cria o campo minado
    private void CreateCamp() {

        for (int i = 0; i < w; i++) {

            for (int j = 0; j < h; j++) { 
                 
                GameObject g = Instantiate(block, new Vector3(i, j, 0f), Quaternion.identity);

                elements[i, j] = g.GetComponent<Element>();
            }

        }
    }

    public static void UncoverMines() { 

        foreach(Element item in elements) {

            if (item.mine) {
                item.Loadtexture();
            }
        }
    }
}
