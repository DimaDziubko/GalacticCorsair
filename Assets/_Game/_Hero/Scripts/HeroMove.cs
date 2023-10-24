using _Game.Core.Services.Input;
using UnityEngine;
namespace _Game._Hero.Scripts
{
    public class HeroMove : MonoBehaviour
    {
        private IInputService _inputService;
        
        private float _speed;
        private float _rollMax;
        private float _pitchMax;

        [SerializeField] private Transform modelTransform;
        public Vector3 Direction => _direction;

        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public Quaternion Rotation
        {
            get => transform.rotation;
            set => transform.rotation = value;
        }
        
        public void Construct(IInputService inputService, float speed, float rollMult, float pitchMult)
        {
            _inputService = inputService;
            _speed = speed;
            _rollMax = rollMult;
            _pitchMax = pitchMult;
        }
        
        public void GameUpdate()
        {
            Move();
        }
        
        private float _yawSpeed = 180f;
        private Vector3 _direction = Vector3.up;
        //private Vector3 _movement;

        private float _pitchSmoothFactor = 1f;
        private float _currentPitch;

        private float _rollSmoothFactor = 1f;
        private float _currentRoll;

        private float _currentSpeed;
        private float _acceleration = 2f;
        private float _deceleration = 3f;
        private float _speedInterpolationFactor;
        private float _pitchMaxDeviationAngle = 20f;
        private float _crossProductRollDeviation = 0.1f;

        private void Move()
        {
            Vector3 input = Vector3.zero;
            input = _inputService.Axis;
            input.Normalize();
            
            CalculateMoving(input);

            CalculateYaw();
            CalculatePitch(input);
            CalculateRoll(input);
            
            modelTransform.localRotation = Quaternion.Euler( _currentPitch , 0, _currentRoll);
        }

        private void CalculateMoving(Vector3 input)
        {
            if (IsInput(input))
            {
                _currentSpeed = SmoothAccelerateSpeed();
            }
            else
            {
                _currentSpeed = SmoothDecelerateSpeed();
            }

            _speedInterpolationFactor = 0.05f;
            _direction = Vector3.Slerp(_direction, input, Time.deltaTime * _speed * _speedInterpolationFactor);
            _direction = new Vector3(_direction.x, _direction.y, 0);
            _direction.Normalize();
            
            //Vector3 movementProjection = new Vector3(_movement.x, _movement.y, 0);

            // if (movementProjection != Vector3.zero)
            // {
            //     _direction = movementProjection;
            //     _direction.Normalize();
            // }

            Vector3 pos = Position;
            pos += _direction * (_currentSpeed * Time.deltaTime);
            Position = new Vector3(pos.x, pos.y, 0);
        }

        private float SmoothDecelerateSpeed()
        {
            return Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * _deceleration);
        }

        private float SmoothAccelerateSpeed()
        {
            return Mathf.Lerp(_currentSpeed, _speed, Time.deltaTime * _acceleration);
        }

        private bool IsInput(Vector3 input) => input != Vector3.zero;

        private void CalculateYaw()
        {
            Quaternion targetRotation = Quaternion.LookRotation(this._direction, Vector3.back);
            
            float maxRotationAngle = _yawSpeed * Time.deltaTime;
            float angle = Quaternion.Angle(Rotation, targetRotation);
            
            float step = Mathf.Min(maxRotationAngle, angle);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        }

        private void CalculatePitch(Vector3 input)
        {
            var dotProduct = Vector3.Dot(_direction, input);
            
            if (NeedPitch(dotProduct))
            {
                _currentPitch = Mathf.Lerp(_currentPitch, _pitchMax, _pitchSmoothFactor * Time.deltaTime);
                return;
            }
            _currentPitch = Mathf.Lerp(_currentPitch, 0, _pitchSmoothFactor * Time.deltaTime);
        }

        private bool NeedPitch(float dotProduct)
        {
            return dotProduct > Mathf.Cos(Mathf.Deg2Rad * _pitchMaxDeviationAngle);
        }

        private void CalculateRoll(Vector3 input)
        {

            Vector3 crossProduct = Vector3.Cross(_direction, input);

            if (NeedRollLeft(crossProduct))
            {
                _currentRoll = Mathf.Lerp(_currentRoll, _rollMax, _rollSmoothFactor * Time.deltaTime);
                return;
            }
            if (NeedRollRight(crossProduct))
            {
                _currentRoll = Mathf.Lerp(_currentRoll, -_rollMax, _rollSmoothFactor * Time.deltaTime);
                return;
            }
            
            _currentRoll = Mathf.Lerp(_currentRoll, 0, _rollSmoothFactor);
            
            // float signedAngle = Mathf.Atan2(_direction.y, _direction.x) - Mathf.Atan2(input.y, input.x);
            //
            // signedAngle *= Mathf.Rad2Deg;
            
            // Debug.Log($"SignedAngle: {signedAngle}");
            //
            // if (signedAngle > 10)
            // {
            //     _currentRoll = Mathf.Lerp(_currentRoll, _rollMult, _rollInterpolationSpeed);
            // }
            // else if (signedAngle < -10)
            // {
            //     _currentRoll = Mathf.Lerp(_currentRoll, -_rollMult, _rollInterpolationSpeed);
            // }
            // else if(signedAngle <= 10 && signedAngle >= - 10)
            // {
            //     _currentRoll = Mathf.Lerp(_currentRoll, 0, _rollInterpolationSpeed);
            // }
        }

        private bool NeedRollRight(Vector3 crossProduct)
        {
            return crossProduct.z > _crossProductRollDeviation;
        }

        private bool NeedRollLeft(Vector3 crossProduct)
        {
            return crossProduct.z < - _crossProductRollDeviation;
        }

        // void OnTriggerEnter(Collider other)
        // {
        //     Transform rootT = other.gameObject.transform.root;
        //     GameObject go = rootT.gameObject;
        //
        //     if(go == lastTriggerGo)
        //     {
        //         return;
        //     }
        //     lastTriggerGo = go;
        //
        //     if (go.tag == "Enemy")
        //     {
        //         shieldLevel--;
        //
        //         Enemy enemy = go.GetComponent<Enemy>();
        //         if (!enemy.notifiedOfDestruction)
        //         {
        //             Main.S.ShipDestroyed(enemy);
        //         }
        //         enemy.notifiedOfDestruction = true;
        //
        //         Destroy(go);
        //     }
        //     else if (go.tag == "PowerUp")
        //     {
        //         AbsorbPowerUp(go);
        //     }
        //     else if (go.tag == "ProjectileEnemy")
        //     {
        //         shieldLevel--;
        //         Destroy(go);
        //     }
        //     else
        //     {
        //         print("Triggered: " + go.name);
        //
        //     }
        // }

        // public void AbsorbPowerUp(GameObject go)
        // {
        //     PowerUp pu = go.GetComponent<PowerUp>();
        //     switch (pu.type)
        //     {
        //         case WeaponType.shield:
        //             shieldLevel++;
        //             break;
        //         default:
        //             if(pu.type == weapons[0].type)
        //             {
        //                 Weapon w = GetEmptyWeaponSlot();
        //                 if(w != null)
        //                 {
        //                     w.SetType(pu.type);
        //                 }
        //             }
        //             else
        //             {
        //                 ClearWeapons();
        //                 weapons[0].SetType(pu.type);
        //             }
        //             break;
        //     }
        //     pu.AbsorbedBy(gameObject);
        // }

        // public float shieldLevel 
        // {
        //     get
        //     {
        //         return (_shieldLevel);
        //     }
        //     set
        //     {
        //         _shieldLevel = Mathf.Min(value, 4);
        //
        //         if(value < 0)
        //         {
        //             Destroy(gameObject); ;
        //         }
        //     }
        // }

        // Weapon GetEmptyWeaponSlot()
        // {
        //     for(int i =0; i < weapons.Length; i++)
        //     {
        //         if(weapons[i].type == WeaponType.none)
        //         {
        //             return (weapons[i]);
        //         }
        //     }
        //     return (null);
        // }

        // void ClearWeapons()
        // {
        //     foreach(Weapon w in weapons)
        //     {
        //         w.SetType(WeaponType.none);
        //     }
        // }
    }
}
