using UnityEngine;

namespace _Game.Enemies.Scripts
{
    public class EnemyLinearMove : EnemyMove
    {
        protected override void Move()
         {
             if (_direction == Vector3.zero)
             {
                 Vector3 guidePoint = SelectGuidePoint();
                 _direction = guidePoint - Position;
                 _direction.Normalize();
             }
             
             Vector3 tempPos = Position;
             tempPos += _direction * (_speed * Time.deltaTime);
             Position = tempPos;
             
             Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.back);
             transform.rotation = targetRotation;
         }
    }
}