using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    //velocidade que o player pode se mover (se aplica somente ao se mover para frente e tras ou para os lados, não para pulo)
    public float speed = 3.0f;
    //aceleração da gravidade
    //detalhe: essa variável não define a velocidade que ele vai cair, mas sim a velocidade que ele vai acelerar quando estiver caindo
    public float gravity = -9.8f;
    //altura que o pulo vai ter
    public float jump = 3f;

    private CharacterController myController;

    //aqui é onde eu vou colocar o objeto que fica na parte de baixo do personagem, isso eu faço la no unity arrastando pra ca.
    public Transform groundCheck;
    //o raio que ele vai checar procurando pelo chão, ele pode ser bem pequeno mesmo
    public float groundCheckRadius = 0.4f;
    //aqui eu vou definir qual tag que ele vai procurar, no caso la no unity eu defino como ground (chão)
    public LayerMask groundMask;
    //essa variável é de verdadeiro ou falso, vai ser verdadeiro quando estiver no chão, falso quando estiver no ar
    private bool isGrounded;

    //essa variável que vai conter a velocidade da queda
    private Vector3 fall;

    void Start() {
        myController = gameObject.GetComponent<CharacterController>();
    }

    void Update() {
        //checando se está no chão ou não
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);

        //resetando a velocidade de queda quando estiver no chão
        //eu preciso fazer isso porque a velocidade da queda fica sempre aumentando de acordo com a aceleração da gravidade
        //então se eu não resetar toda vez que encostar no chão, a cada vez que ele pular vai cair mais rápido
        if (isGrounded && fall.y < 0) {
            //eu reseto ela mandando pra -2 e não pra 0, pq as vezes ele identifica que está encostando no chão mas não está, está um pouquinho acima
            //por isso eu coloco ele pra -2, se for 0 ele vai ficar meio que flutuando
            fall.y = -2f;
        }

        //se não me engano essa parte aqui ta igual a do professor, se não tiver ta bem parecido
        Vector3 movimentoZ = Input.GetAxis("Vertical") * Vector3.forward * speed * Time.deltaTime;
        Vector3 movimentoX = Input.GetAxis("Horizontal") * Vector3.right * speed * Time.deltaTime;

        Vector3 movimento = transform.TransformDirection(movimentoZ + movimentoX);

        //aqui ele vai pular se o jogador apertar o espaço E se o personagem estiver no chão
        if (isGrounded && Input.GetButtonDown("Jump")) {
            //formula do pulo: raiz quadrada de força vezes aceleração da gravidade vezes -2
            fall.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        //formula da queda: gravidade vezes tempo ao quadrado
        fall.y += gravity * Time.deltaTime;

        myController.Move(movimento);
        //como eu disse é na formula, é gravidade vezes tempo AO QUADRADO, por isso eu tenho q multiplicar de novo por tempo aqui
        myController.Move(fall * Time.deltaTime);
    }
}
