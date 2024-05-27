using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
	private Transform player;
	private Animator anime;
	private Vector2 pos;
	private bool moving = false;
	public float maxDist = 10;
	public float minDist = 3;
	private float xg;
	private float yg;
	public float vel = 0.01f;
	public fire f;
	private bool first = true;

	void Start () {
		anime = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
		pos = transform.position;
	}

	void Update () {
		xg = pos.x - player.position.x;
		yg = pos.y - player.position.y;
		CheckInput();
		if(moving) {
			transform.position = pos;
			moving = false;
		}
	}

	void CheckInput(){
		if(Vector3.Distance(pos,player.position) >= minDist){
			if(Vector3.Distance(pos,player.position) <= maxDist)
			{
				if(yg > 0 && yg < 0.5f) yg = 0;
				if(xg > 0 && xg < 0.5f) xg = 0;

				if(xg < 0) {
					pos += Vector2.right*vel;
					moving = true;
					anime.SetBool ("esquerda", false);
					anime.SetBool ("direita", true);
					anime.SetBool ("cima", false);
					anime.SetBool ("baixo", false);
					anime.SetBool ("idle", false);
				}
				else if(xg > 0) {
					pos -= Vector2.right*vel;
					moving = true;
					anime.SetBool ("esquerda", true);
					anime.SetBool ("direita", false);
					anime.SetBool ("cima", false);
					anime.SetBool ("baixo", false);
					anime.SetBool ("idle", false);
				}
				if(yg < 0) {
					pos += Vector2.up*vel;
					moving = true;
					anime.SetBool ("esquerda", false);
					anime.SetBool ("direita", false);
					anime.SetBool ("cima", true);
					anime.SetBool ("baixo", false);
					anime.SetBool ("idle", false);
				}
				else if(yg > 0) {
					pos -= Vector2.up*vel;
					moving = true;
					anime.SetBool ("esquerda", false);
					anime.SetBool ("direita", false);
					anime.SetBool ("cima", false);
					anime.SetBool ("baixo", true);
					anime.SetBool ("idle", false);
				}
			}
			else{
				moving = false;
				anime.SetBool ("esquerda", false);
				anime.SetBool ("direita", false);
				anime.SetBool ("cima", false);
				anime.SetBool ("baixo", false);
				anime.SetBool ("idle", true);
			}
		}else{
			moving = false;
			anime.SetBool ("esquerda", false);
			anime.SetBool ("direita", false);
			anime.SetBool ("cima", false);
			anime.SetBool ("baixo", false);
			anime.SetBool ("idle", true);
			if(first) explode();
		}
	}

	public void explode(){
		first = false;
		f.kill();
		gameObject.GetComponent<Animator>().Play("explosion");
		Destroy(gameObject,0.6f);
	}

	void OnCollisionEnter2D (Collision2D colisor) {
		if(colisor.gameObject.tag == "Player"){
			Debug.Log("damagem");
		}
	}
}