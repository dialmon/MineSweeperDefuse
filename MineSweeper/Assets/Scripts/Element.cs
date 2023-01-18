using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{

    [SerializeField]
    public bool mine; //� uma mina ?

    [SerializeField]
    private Sprite[] emptyTextures; //imagem que ir� usar caso n�o for mina, representa os espa�os que n�o tem minas

    [SerializeField]
    private Sprite mineTexture; // aqui representa se houver uma mina utilizar� essa sprite


    void Start()
    {
        mine = Random.value < 0.15; //configurando uma probabilidade de spawnar uma mina

        //registra o bloco na matriz de blocos
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        GameController.elements[x, y] = this;
    }

    
    void Update()
    {
        
    }
    //qual imagem ir� carregar ap�s clicar no bloco

    //adjacentCount ira definir qual valor vai usar de sprite na tela
    public void Loadtexture(int adjacentCount ) {

        if (mine)
        {
            GetComponent<SpriteRenderer>().sprite = mineTexture; //pegar o compomente o responsavel por mostrar a imagem do objeto e troca-lo pela minetexture
            
        }
        else {
            GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount]; //se n�o, trocar pelo vetor empty
        }
    }

    private void OnMouseUpAsButton() {
        if (mine)
        {
            //tela game over
            Loadtexture(0);
            print("Game Over !!!");
            GameController.UncoverMines();
        }
        else { 
            //l�gica de proximidade de mina
            Loadtexture(0);
        }
    }
}
