using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

	public GameObject Arrow;

	public GameObject GameOverDisplay;

	public float Thrust;  // The higher the thrust, the faster the ball.

	public Vector2 MaxForce;

	private Vector2 draggedFrom, draggedTo;

	private const float Epsilon = .01f;



	void Start () {
	
	}
	
	void Update () {
	
	}

	public void OnTriggerStay2D(Collider2D other) {
		Debug.Log (GetComponent<Rigidbody2D> ().velocity.magnitude);
		Debug.Log (other.gameObject.tag);
		if (other.gameObject.tag == "Finish" && GetComponent<Rigidbody2D>().velocity.magnitude < Epsilon) {
			GameOver();
		}
	}

	private void GameOver() {
		GameOverDisplay.SetActive (true);
	}

	public void OnMouseDown() {
		Debug.Log ("You touched it mofo" + Camera.main.ScreenToWorldPoint(Input.mousePosition));
		draggedFrom = Camera.main.ScreenToWorldPoint (Input.mousePosition);
	}
	
	public void OnMouseDrag() {
		draggedTo = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		DrawArrow ();
	}

	public void DrawArrow() {
		var clampedDragVector = CalculateDragVector ();
		Arrow.SetActive (true);
		Arrow.transform.localScale = new Vector2 (clampedDragVector.x, 1);
	}
	
	public void OnMouseUp() {
		Debug.Log ("You untouched it mofo" + Camera.main.ScreenToWorldPoint(Input.mousePosition));

		DragBall ();
		Arrow.SetActive (false);
	}

	private Vector2 CalculateDragVector() {
		draggedTo = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		var dragVector = draggedTo - draggedFrom;
		return new Vector2 (Mathf.Clamp(dragVector.x, -MaxForce.x, MaxForce.x), 
		                    Mathf.Clamp (dragVector.y, 0, MaxForce.y));
	}


	private void DragBall() {
		var clampedDragVector = CalculateDragVector ();
		Debug.Log ("dragVector: " + clampedDragVector);
		GetComponent<Rigidbody2D> ().AddForce (clampedDragVector * Thrust);
	}
}
