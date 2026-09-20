using System.Collections.Generic;
using UnityEngine;

namespace KobGamesSDKSlim
{
	public class ExplodingObjectBase : MonoBehaviour
	{
		[Header("Models")]
		public GameObject Model;

		public GameObject ExplodedModel;

		[SerializeField]
		protected List<Rigidbody> ExplodedModelPieces;

		[SerializeField]
		protected List<Vector3> ExplodedModelPiecesPos;

		public Vector2 ExplosionForceRange;

		public Vector2 ExplosionUpModifierRange;

		public float HorizontalForce;

		public float Torque;

		public float ExplosionRadius;

		public AudioClip ExplosionSound;

		private Rigidbody m_DummyPiece;

		public virtual void Start()
		{
			return;
		}

		public virtual void SetRefs()
		{
			Model = transform.FindDeepChild<GameObject>("Model");
			ExplodedModel = transform.FindDeepChild<GameObject>("Exploded Model");
			ExplodedModelPieces.Clear();
			ExplodedModelPiecesPos.Clear();
			if (ExplodedModel == null) return;
			for (int i = 0; i < ExplodedModel.transform.childCount; i++)
			{
				GameObject piece = ExplodedModel.transform.GetChild(i).gameObject;
				Rigidbody body = piece.GetComponent<Rigidbody>() ?? piece.AddComponent<Rigidbody>();
				ExplodedModelPieces.Add(body);
				ExplodedModelPiecesPos.Add(piece.transform.localPosition);
			}
		}

		protected virtual void Awake()
		{
			if (ExplodedModelPieces == null) ExplodedModelPieces = new List<Rigidbody>();
			if (ExplodedModelPiecesPos == null) ExplodedModelPiecesPos = new List<Vector3>();
			if (Model == null || ExplodedModel == null) SetRefs();
			if (Model == null || ExplodedModel == null) return;
			Model.SetActive(true);
			ExplodedModel.SetActive(false);
			for (int i = 0; i < ExplodedModelPieces.Count; i++)
			{
				Rigidbody body = ExplodedModelPieces[i];
				body.isKinematic = true;
				body.transform.localPosition = ExplodedModelPiecesPos[i];
			}
		}

		protected virtual void OnEnable()
		{
			return;
		}

		protected virtual void OnDisable()
		{
			ResetObject();
		}

		public virtual void Explode(Vector3 ExplosionCenter)
		{
			if (Model == null || ExplodedModel == null) return;
			Model.SetActive(false);
			ExplodedModel.SetActive(true);
			for (int i = 0; i < ExplodedModelPieces.Count; i++)
			{
				Rigidbody body = ExplodedModelPieces[i];
				body.isKinematic = false;
				float force = Random.Range(ExplosionForceRange.x, ExplosionForceRange.y);
				float up = Random.Range(ExplosionUpModifierRange.x, ExplosionUpModifierRange.y);
				body.AddExplosionForce(force, ExplosionCenter, ExplosionRadius, up, ForceMode.Impulse);
				body.AddForce(Random.Range(-HorizontalForce, HorizontalForce), 0f, Random.Range(-HorizontalForce, HorizontalForce), ForceMode.Impulse);
				body.AddTorque(Random.insideUnitSphere * Random.Range(-Torque, Torque), ForceMode.Impulse);
			}
		}

		public virtual void ResetObject()
		{
			if (Model != null) Model.SetActive(true);
			if (ExplodedModel != null) ExplodedModel.SetActive(false);
		}

		public virtual void OnCoreLoop_Reset()
		{
			ResetObject();
		}

		public ExplodingObjectBase()
		{
			ExplodedModelPieces = new List<Rigidbody>();
			ExplodedModelPiecesPos = new List<Vector3>();
		}
	}
}
