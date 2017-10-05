using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearPlayer : MonoBehaviour
{
    /*
         * Declaração de váriavies:
         * As váriavies que forem declaradas como PUBLIC será exibidas no unity, sendo possível a alteração delas através da engine.
         * Existem alguns tipos de váriavés que são referentes aos objetos e atributos no unity, como Rigidbody2D.
         * 
         * Definem o tipo da váriavel como um dos atributos do unity:
         * Rigidbody2D , Animator, LayerMask
         * 
         */

    private Rigidbody2D rb2d;
    private Animator animator;
    public LayerMask plataforma;
    public Vector2 pontoColisao = Vector2.zero;
    public float raio;
    public bool estaNoChao;
    private float horizontal;
    private float vertical;
    public float velocidade;
    private bool viradoParaDireita;
    public Color debugColisao = Color.red;
    public float forcaPulo;
    public GameObject player;
   // public Transform atacando;


    // Use this for initialization

    /*
     * A seção void Start é utilizada quando o scprit é ativado logo no inicio, antes dos métodos do Update
     * chamando todos os objetos que precisam ser inicializaos junto com o jogo.
     */
    void Start()
    {
        /*
         * GetComponent<Rigidbody2D>() chama o objeto do unity Rigidbody2d e armazena na váriavel rb2d.
         * GetComponent<Animator>() chama o objeto do unity Animator e armazena na váriavel animator.
         * transform.localScale.x chama o obejto transform, localScale indica qual o vetor que irá verificar, X indica qual o eixo que será verificado.
         */
        player = GetComponent<GameObject>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        viradoParaDireita = transform.localScale.x > 0;
        Time.timeScale = 1;
    }

    // Update is called once per frame

    /*
     * O Update é chamado em cada frame do jogo, deve ser utilizado quando for fazer qualquer alteração de comportamento.
     */
    void FixedUpdate()
    {
        /*
         *  horizontal = Input.GetAxis("Horizontal");
         *  A váriavel horizontal, recebe o valor enviado pela tecla definida pelo Unity.
         *  Input.GetAxis é área do unity onde são definidos quais teclas serão utilizadas.
         *  
         *  movimentar e mudaDirecao, são metodos que recebem o valor passado pela váriavel horizontal. 
         */
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movimentar(horizontal);
        mudaDirecao(horizontal);
        EstaNoChao();
        ControlarEntradas();
        ControlarLayers();
        Cair();
    }

    private void movimentar(float h)
    {
        rb2d.velocity = new Vector2(h * velocidade, rb2d.velocity.y);

        /*
         * animator.SetFloat alterá o valor do parametro definido no unity na seção animator.
         * "Run" é o nome do parametro definido no unity
         * Mathf.Abs é para definir um valor absoluto, sendo sempre positivo, independente se o personagem se mova para a direita(positivo) ou para a esquerda(negativo)
         * Observação: a váriavel animator recebeu o valor que foi carregado pelo objeto inserido nela com a função getcomponent.
         */
        animator.SetFloat("Run", Mathf.Abs(h));
    }

    private void mudaDirecao(float horizontal)
    {
        if (horizontal > 0 && !viradoParaDireita || horizontal < 0 && viradoParaDireita)
        {
            viradoParaDireita = !viradoParaDireita;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }
    /*
     * O método EstaNoChao cria um circulo que para verificar se existe um intercessão entre o circulo criado e o objeto defino na última posição em Physics2D.
     * A váriável local pontoPosicao recebe os valores do objeto do unity transform.
     */
    private void EstaNoChao()
    {
        var pontoPosicao = pontoColisao;
        pontoPosicao.x += transform.position.x;
        pontoPosicao.y += transform.position.y;
        estaNoChao = Physics2D.OverlapCircle(pontoPosicao, raio, plataforma);
    }

    /*
     * OnDrawGizmos é um metodo do unity utilizado para desenhar formas no jogo, não é necessário chamar esse metódo em luguar algum.
     */
    void OnDrawGizmos()
    {
        Gizmos.color = debugColisao;
        var pontoPosicao = pontoColisao;
        pontoPosicao.x += transform.position.x;
        pontoPosicao.y += transform.position.y;
        Gizmos.DrawWireSphere(pontoPosicao, raio);

    }

    /*
     * O metódo pular verificar se o personagem está no chão e se o eixo y do objeto Rigidbody2D é menor ou igual a 0.
     * Se verdadeiro, ele adiciona uma determinada força(AddForce) no Rigidbody2D, através da criação de um novo vetor com 2 valores(new Vecto2)
     * sendo o primeiro valor utilizado para alterar o eixo X e o segundo valor utilizado para alterar o eixo Y.
     */
    void pular()
    {
        if (estaNoChao && rb2d.velocity.y <= 0)
        {
            rb2d.AddForce(new Vector2(0, forcaPulo));
            animator.SetTrigger("Pular");
        }
    }

    /*
     * O metódo cair verifica se o personagem está ou não no chão, analisando o valor da eixo y do objeto Rigidbody2D
     * SetBool, altera o valor do parametro Cair definida no unity em animator.
     */
    private void Cair()
    {
        if (!estaNoChao && rb2d.velocity.y <= 0)
        {
            animator.SetBool("Cair", true);
        }
        if (estaNoChao)
        {
            animator.SetBool("Cair", false);
        }
    }

    private void ControlarEntradas()
    {
        if (vertical > 0)
        {
            pular();
        }

        if (Input.GetMouseButton(button: 0) && estaNoChao)
        {
            animator.SetBool("Atacando", true);
            animator.SetFloat("Run", 0);
            rb2d.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;
          //  atacando.gameObject.SetActive(true);

        }
        else
        {
            animator.SetBool("Atacando", false);
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
           // atacando.gameObject.SetActive(false);
        }
    }

    /*
     * O metódo ControlarLayers faz alterações no modo em que as layers do animator no unity são lidas.
     * SetLayerWeight define qual o peso que a layer irá receber, o primeiro número define qual layer será alterada, a layer é definida no unity começano com o valor 0 e as posteriores são 1,2...
     * o segundo númeor define o peso dela, sendo entre 0 e 1. 
     */
    private void ControlarLayers()
    {
        if (!estaNoChao)
        {
            animator.SetLayerWeight(1, 1);
        }
        else
        {
            animator.SetLayerWeight(1, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D transformCollision)
    {
        if (transformCollision.gameObject.tag == "Troll")
        {
            Destroy(transformCollision.gameObject);
        }
    }
}
