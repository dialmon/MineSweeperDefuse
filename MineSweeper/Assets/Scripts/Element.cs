using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    [SerializeField]
    public bool mine = false; //é uma mina ?

    [SerializeField]
    public bool flag; // tem bandeira?

    [SerializeField]
    public bool open = false; // tem bandeira?

    [SerializeField]
    public int nearbyMines = 0;

    [SerializeField]
    private Sprite[] emptyTextures; //imagem que irá usar caso não for mina, representa os espaços que não tem minas

    [SerializeField]
    private Sprite mineTexture; // aqui representa se houver uma mina utilizará essa sprite

    [SerializeField]
    private Sprite flagTexture; // aqui representa se houver uma mina utilizará essa sprite

    private Sprite originalTexture; // sprite 

    void Start()
    {
        originalTexture = GetComponent<SpriteRenderer>().sprite;

        //mine = Random.value < 0.15; //configurando uma probabilidade de spawnar uma mina

        flag = false; // Inicia desativada

        //registra o bloco na matriz de blocos
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        //GameController.elements[x, y] = this;
    }

    
    void Update()
    {
        
    }
    //qual imagem irá carregar após clicar no bloco

    public void SetUpMine()
    {
        mine = true;
    }

    public void AddNearbyMines()
    {
        nearbyMines++;
    }

    //adjacentCount ira definir qual valor vai usar de sprite na tela
    public void Loadtexture() {

        if (mine)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture; //pegar o compomente o responsavel por mostrar a imagem do objeto e troca-lo pela minetexture

        }
        else {
            open = true;
            GetComponent<SpriteRenderer>().sprite = emptyTextures[nearbyMines]; //se não, trocar pelo vetor empty
        }
    }

    private void LoadTextureFlag()
    {
        if (flag)
        {
            GetComponent<SpriteRenderer>().sprite = flagTexture; //pegar o compomente o responsavel por mostrar a imagem do objeto e troca-lo pela flagTexture
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = originalTexture;
        }
    }

    private void OnMouseUpAsButton() {
        if (!flag && !GameController.gameover)
        {

            if (mine)
            {
                Loadtexture();
                print("Game Over !!!");
                GameController.UncoverMines();
            }
            else
            {
                //lógica de proximidade de mina
                Loadtexture();
                Debug.Log("textura trocada");
                if (nearbyMines == 0)
                {
                    int x = (int)transform.position.x;
                    int y = (int)transform.position.y;
                    Debug.Log("Enviando scan around");

                    GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ScanAround(x, y);
                }
            }
        }
    }

    // Deu erro
    public void UncoverElement()
    {
        if (!open)
        {
            Loadtexture();
            if (nearbyMines == 0)
            {
                int x = (int)transform.position.x;
                int y = (int)transform.position.y;
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().ScanAround(x, y);
            }
        }
    }

    public void UpdateFlag()
    {
        if (!open)
        {
            flag = !flag;
            LoadTextureFlag();
        }
    }
}
