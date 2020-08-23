namespace GudKoodi.PathAgent.Example
{
    using UnityEngine;
    using GudKoodi.PathAgent;

    public class PlayerScript : MonoBehaviour
    {
        public PathAgent agent;

        public Rigidbody rb;

        public bool falling = false;

        void Update()
        {
            this.agent.Speed = Input.GetAxis("Horizontal") * 7;
            if (Input.GetButton("Jump") && !falling)
            {
                this.rb.velocity = new Vector3(this.rb.velocity.x, 8, this.rb.velocity.z);
                this.falling = true;
            }
        }

        void OnCollisionStay(Collision collisionInfo)
        {
            this.falling = false;
        }
    }
}
