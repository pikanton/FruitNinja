using UnityEngine;

namespace App.Scripts.Game.Blocks
{
    public class BlockCollider : MonoBehaviour
    {
        private float _colliderRadius;
        private float _currentColliderRadius;
        
        public void Initialize(float colliderRadius)
        {
            _colliderRadius = colliderRadius;
        }
        
        private void Update()
        {
            _currentColliderRadius = _colliderRadius * transform.localScale.x;
        }
        
        public bool OnTrigger(Vector3 checkingPosition)
        {
            return Vector3.Distance(checkingPosition, transform.position) <= _currentColliderRadius;
        }
    }
}