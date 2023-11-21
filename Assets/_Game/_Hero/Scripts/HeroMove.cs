using _Game.Core.Services.Input;
using UnityEngine;
namespace _Game._Hero.Scripts
{
    public class HeroMove : MonoBehaviour
    {
        [SerializeField] private JetStream _jetStream;
        
        private IInputService _inputService;
        
        private HeroMovementConfig _movementConfig;

        private Vector3 _direction = Vector3.up;
        private float _currentPitch;
        private float _currentRoll;
        private float _currentSpeed;


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

        public void Construct(
            IInputService inputService,
            HeroMovementConfig movementConfig)
        {
            _inputService = inputService;
            _movementConfig = movementConfig;
        }

        public void GameUpdate()
        {
            Move();
            if(_jetStream) 
                _jetStream.GameUpdate(_currentSpeed, _movementConfig.Speed);
        }

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
            
            _direction = Vector3.Slerp(_direction, input, 
                Time.deltaTime * _movementConfig.Speed * _movementConfig.SpeedInterpolationFactor);
            _direction = new Vector3(_direction.x, _direction.y, 0);
            _direction.Normalize();

            Vector3 pos = Position;
            pos += _direction * (_currentSpeed * Time.deltaTime);
            Position = new Vector3(pos.x, pos.y, 0);
        }

        private float SmoothDecelerateSpeed()
        {
            return Mathf.Lerp(_currentSpeed, 0, Time.deltaTime * _movementConfig.Deceleration);
        }

        private float SmoothAccelerateSpeed()
        {
            return Mathf.Lerp(_currentSpeed,  _movementConfig.Speed, Time.deltaTime * _movementConfig.Acceleration);
        }

        private bool IsInput(Vector3 input) => input != Vector3.zero;

        private void CalculateYaw()
        {
            Quaternion targetRotation = Quaternion.LookRotation(this._direction, Vector3.back);
            
            float maxRotationAngle = _movementConfig.YawSpeed * Time.deltaTime;
            float angle = Quaternion.Angle(Rotation, targetRotation);
            
            float step = Mathf.Min(maxRotationAngle, angle);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
        }

        private void CalculatePitch(Vector3 input)
        {
            var dotProduct = Vector3.Dot(_direction, input);
            
            if (NeedPitch(dotProduct))
            {
                _currentPitch = Mathf.Lerp(_currentPitch,  _movementConfig.PitchMax, 
                    _movementConfig.PitchSmoothFactor * Time.deltaTime);
                return;
            }
            _currentPitch = Mathf.Lerp(_currentPitch, 0, _movementConfig.PitchSmoothFactor * Time.deltaTime);
        }

        private bool NeedPitch(float dotProduct)
        {
            return dotProduct > Mathf.Cos(Mathf.Deg2Rad * _movementConfig.PitchMaxDeviationAngle);
        }

        private void CalculateRoll(Vector3 input)
        {

            Vector3 crossProduct = Vector3.Cross(_direction, input);

            if (NeedRollLeft(crossProduct))
            {
                _currentRoll = Mathf.Lerp(_currentRoll, _movementConfig.RollMax,  _movementConfig.RollSmoothFactor * Time.deltaTime);
                return;
            }
            if (NeedRollRight(crossProduct))
            {
                _currentRoll = Mathf.Lerp(_currentRoll, -_movementConfig.RollMax, _movementConfig.RollSmoothFactor * Time.deltaTime);
                return;
            }
            
            _currentRoll = Mathf.Lerp(_currentRoll, 0, _movementConfig.RollSmoothFactor);
            
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
            return crossProduct.z > _movementConfig.RollDeviation;
        }

        private bool NeedRollLeft(Vector3 crossProduct)
        {
            return crossProduct.z < -  _movementConfig.RollDeviation;
        }
        
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
