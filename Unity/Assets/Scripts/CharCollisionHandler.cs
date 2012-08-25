using UnityEngine;
using System.Collections;

public class CharCollisionHandler : MonoBehaviour {

	void OnControllerColliderHit(ControllerColliderHit hit) {
		Vector3 pos = hit.collider.transform.position;
		Blockmechanics.CharacterCollision(pos, hit.collider.gameObject.GetComponent<BlockData>().id);
	}
}
