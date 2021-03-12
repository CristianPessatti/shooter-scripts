using UnityEngine;
using System.Collections;

public class Tiro : MonoBehaviour {

	// Referência ao projétil para atirar
	public GameObject projetil;
	public float forca = 10.0f;
	
	// Referência ao efeito sonoro de tiro
	public AudioClip shootSFX;
	
	//Variável pra armazenar a posição da camera, eu precisei criar essa variável porque o tiro vai sair da arma
	//porém ele não pode ir em direção ao z da arma, se não o tiro não vai em direção da mira
	//pro tiro ir em direção da mira ele precisa sair da arma e ir em direção ao z da camera
	public Transform gunCamera;

	//tempo que vai ter q esperar pra poder dar o próximo tiro (meio segundo)
	public float delayAfterShot = 0.5f;
	//variável que armazena o tempo que precisa ter passado pra poder dar o próximo tiro
	private float nextShot = 0.0f;

	// Update é chamado uma vez por frame
	void Update () {
		// Detecta se o botão de tiro foi pressionado
		// e tambem se ja passou o tempo necessário para atirar de novo
		if (Input.GetButtonDown("Fire1") && Time.time > nextShot)
		{	
			//aumenta o tempo necessário para o próximo tiro de acordo com o tempo entre cada tiro q eu defini
			nextShot = Time.time + delayAfterShot;
			// se o projetil foi definido
			if (projetil)
			{
				// Instancia o projetil na posição da camera + 1 metro a frente com a rotação da camera
				GameObject newProjectile = Instantiate(projetil, transform.position + transform.forward, transform.rotation) as GameObject;

				// Se o projetil não tiver um Rigidbody, adicione um
				if (!newProjectile.GetComponent<Rigidbody>()) 
				{
					newProjectile.AddComponent<Rigidbody>();
				}

				// Aplique força ao Rigidbody do newProjectile
				//Aqui eu adicionei a posição z da camera ao invés da posição z da arma
				newProjectile.GetComponent<Rigidbody>().AddForce(gunCamera.transform.forward * forca, ForceMode.VelocityChange);
				
				// toque o efeito sonoro, se houver
				if (shootSFX)
				{
					if (newProjectile.GetComponent<AudioSource> ()) { 
						// se o projetil tiver um componente AudioSource
						// toca o efeito sonoro através deste componente AudioSource.
						// nota: O audio irá viajar com o gameobject.
						newProjectile.GetComponent<AudioSource> ().PlayOneShot (shootSFX);
					} else {
						// dinamicamente cria um novo gameObject com um AudioSource
						// que se auto-destroi quando o audio termina
						AudioSource.PlayClipAtPoint (shootSFX, newProjectile.transform.position);
					}
				}
			}
		}
	}
}
