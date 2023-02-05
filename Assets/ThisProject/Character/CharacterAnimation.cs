using UnityEngine;

namespace ThisProject.Character
{
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _animator;

        void Start()
        {
            _animator = GetComponent<Animator>();
        
            SetDirectAnimation(new Vector3(0.0f, -0.1f));
        }

        public void SetDirectAnimation(Vector3 direct)
        {
            _animator.SetFloat("X", direct.x);
            _animator.SetFloat("Y", direct.y);
        }

        public void SetIdleAnimation(Vector3 direct)
        {
            SetDirectAnimation(direct / 10.0f);
        }
    }
}
