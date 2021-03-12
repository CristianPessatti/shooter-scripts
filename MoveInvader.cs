using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInvader : MonoBehaviour {
    public float speed = 20.0f;
    private float magnitude;
    private bool moveX = false;
    private bool moveY = false;
    private bool moveZ = false;

    //essas variáveis aqui vão armazenar a distância que ele pode percorrer em cada eixo
    //observação: é a distância que ele vai andar referente a posição inicial do objeto, e não do mundo
    private float minimoX;
    private float maximoX;
    private float minimoY;
    private float maximoY;
    private float minimoZ;
    private float maximoZ;

    private float x;
    private float y;
    private float z;
    void Start() {
        moveX = (Random.value > 0.5f);
        moveY = (Random.value > 0.6f);
        moveZ = (Random.value > 0.8f);

        //definindo esses minimos e máximos
        //no eixo x, ele vai se movimentar de -40 até 40
        minimoX = gameObject.transform.position.x - 40;
        maximoX = gameObject.transform.position.x + 40;
        
        //no eixo y ele vai se mover da sua posição inical até 10
        minimoY = gameObject.transform.position.y;
        maximoY = gameObject.transform.position.y + 10;
        
        //no eixo z ele vai se movimentar de -7 a 3
        minimoZ = gameObject.transform.position.z - 7;
        maximoZ = gameObject.transform.position.z + 3;
    }

    void Update() {
        //isso aqui é só pro código ficar visualmente mais limpo
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;

        magnitude = Time.time * speed;

        //na função seguinte eu vou usar uma condição ternária, não sei se algum professor chegou a explicar o que é isso
        //mas basicamente é um if simplicado, pra escrever em uma só linha:
        // (condição ? valor_se_verdadeiro : valor_se_falso)
        //então em todos eles eu pergunto se é pra mover (de acordo com a variável booleana que vai ser aleatória)
        //se for pra mover eu uso a função PingPong pra definir como ele vai se mover, se não ele vai continuar na sua posição atual
        gameObject.transform.position = new Vector3(
            (moveX ? Mathf.PingPong(magnitude, maximoX-minimoX) + minimoX : x),
            (moveY ? Mathf.PingPong(magnitude, maximoY-minimoY) + minimoY : y),
            (moveZ ? Mathf.PingPong(magnitude, maximoZ-minimoZ) + minimoZ : z)
        );
    }
}
