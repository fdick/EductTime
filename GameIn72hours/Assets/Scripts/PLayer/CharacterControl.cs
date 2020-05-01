using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public enum TransitionParameter
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
    }

        public float speed = 10;
        public Animator SkinnedMeshAnimator;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Attack;
        public GameObject ColliderEdgePrefab;
        public List<GameObject> BottomSpehers = new List<GameObject>();
        public List<GameObject> FrontSpehers = new List<GameObject>();
        public List<Collider2D> RagdollParts = new List<Collider2D>();
        public List<Collider2D> CollidingParts = new List<Collider2D>();

        public float GravityMultiplier;
        public float PullMultiplier;


        private Rigidbody2D rigidBody;
        public Rigidbody2D RigidBody
        {
            get
            {
                if (rigidBody == null)
                {
                    rigidBody = GetComponent<Rigidbody2D>();
                }
                return rigidBody;
            }
        }

        private void Awake()
        {
            bool SwitchBack = false;

            if (!isFacingForward())
            {
                SwitchBack = true;
            }

            SetColliderSpheres();
            FaceForward(true);

            if (SwitchBack)
            {
                FaceForward(false);
            }

        }

        //private void TurnOnRagdoll()
        //{
        //    RigidBody.useGravity = false;
        //    RigidBody.velocity = Vector3.zero;
        //    this.gameObject.GetComponent<BoxCollider>().enabled = false;
        //    SkinnedMeshAnimator.enabled = false;
        //    SkinnedMeshAnimator.avatar = null;

        //    foreach (Collider c in RagdollParts)
        //    {
        //        c.isTrigger = false;
        //        c.attachedRigidbody.velocity = Vector3.zero;
        //    }
        //}



      

        private void SetColliderSpheres()
        {
            BoxCollider2D box = GetComponent<BoxCollider2D>();

            float bottom = box.bounds.min.y;
            float top = box.bounds.max.y;
            float front = box.bounds.max.x;
            float back = box.bounds.min.x;

            GameObject bottomFront = CreateEdgeSphere(new Vector2(front, bottom)); // back is front
            GameObject bottomBack = CreateEdgeSphere(new Vector2(back, bottom)); // front is back
            GameObject topFront = CreateEdgeSphere(new Vector2(back, top));

            BottomSpehers.Add(bottomBack);
            BottomSpehers.Add(bottomFront);

            FrontSpehers.Add(bottomBack);
            FrontSpehers.Add(topFront);

            float par = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f;
            CreateMiddleSpheres(bottomBack, -this.transform.forward, par, 4, BottomSpehers);
            float par2 = (bottomFront.transform.position - topFront.transform.position).magnitude / 10f;
            CreateMiddleSpheres(bottomBack, this.transform.up, par2, 9, FrontSpehers);
        }

        private void FixedUpdate()
        {
            if (RigidBody.velocity.y < 0)
            {
                rigidBody.velocity += (-Vector2.up * GravityMultiplier);
            }
            if (RigidBody.velocity.y > 0 && !Jump)
            {
                rigidBody.velocity += (-Vector2.up * PullMultiplier);

            }
        }

        public void CreateMiddleSpheres(GameObject start, Vector3 direction, float par, int iteration, List<GameObject> spheres)
        {


            for (int i = 0; i < iteration; i++)
            {
                Vector3 pos = start.transform.position + (direction * (i + 1) * par);

                GameObject go = CreateEdgeSphere(pos);
                spheres.Add(go);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity, transform);
            return obj;
        }

        public void MoveForward(float speed, float SpeedGraph)
        {
            transform.Translate(-Vector3.forward * -speed * SpeedGraph * Time.deltaTime);
        }

        public void FaceForward(bool forward)
        {
            if (forward)
            {
                this.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else
            {
                this.transform.rotation = Quaternion.Euler(0, -90, 0);
            }
        }

        public bool isFacingForward()
        {
            if (transform.forward.x > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

