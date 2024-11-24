using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Spin
{
    public class WheelOfFortuneSpin : WheelOfFortuneGenerator
    {
        [HideInInspector] public string _targetSegment;  // The segment that the wheel is supposed to stop on.
        [SerializeField] private float _rotationSpeed = 500f;  // Speed at which the wheel spins.
        [SerializeField] private float _smoothTime = 2f;  // Time over which the wheel slows down.
        [SerializeField] private int _totalSpins = 5;  // Number of complete spins before decelerating.

        private List<WheelSegment> _wheelSegments;  // Lista svih segmenata
        private float _targetAngle;  // Angle at which the wheel should stop.
        private float _currentAngle = 0f;  // Current angle of the wheel.
        private float _remainingRotation;  // Remaining rotation needed to complete the spin.
        private float _initialRotationSpeed;  // Initial rotation speed before deceleration.
        private bool _isSpinning = false;  // Flag indicating whether the wheel is currently spinning.

        private void Start() 
        {
            _wheelSegments = _segments;
            _initialRotationSpeed = _rotationSpeed;
            base.DrawRawWheelOfFortune();
        }

        private void Update()
        {
            if (_isSpinning)
            {
                // Update the current angle and decrease the remaining rotation
                _currentAngle += _rotationSpeed * Time.deltaTime;
                _remainingRotation -= _rotationSpeed * Time.deltaTime;

                // Fetch the current rotation
                Vector3 currentRotation = transform.eulerAngles;

                // Set only the Z axis to the new value, keeping X and Y constant
                transform.eulerAngles = new Vector3(currentRotation.x, currentRotation.y, -_currentAngle);

                // Calculate the point at which to start decelerating (50% of total rotation)
                float decelerationThreshold = _totalSpins * 180f; // 50% of total spins (180 degrees * number of spins)

                // Start decelerating if remaining rotation is less than or equal to the threshold
                if (_remainingRotation <= decelerationThreshold)
                {
                    float remainingFraction = _remainingRotation / decelerationThreshold;
                    _rotationSpeed = Mathf.Lerp(0f, _initialRotationSpeed, remainingFraction);
                }

                // Stop spinning when the wheel is close to the target angle and the remaining rotation is minimal
                if (Mathf.Abs(Mathf.DeltaAngle(_currentAngle % 360, _targetAngle)) < 1.0f && _remainingRotation <= 1.0f)
                {
                    _currentAngle = _targetAngle % 360; // Ensure continuous spinning
                    _isSpinning = false;
                    DetectSegment();
                }
            }
        }

        public void SpinWheel()
        {
            if (Application.isPlaying == false)
                return;

            // Find the target segment's angles.
            var targetSegment = _wheelSegments.Find(s => s.Label == _targetSegment);
            if (targetSegment != null)
            {
                SegmentAngles targetSegmentAngle = targetSegment.Angles;

                // Incorporate randomness to where exactly should the wheel stop at the targeted segment
                float randomAngleWithinSegment = Random.Range(targetSegmentAngle.StartAngle, targetSegmentAngle.StartAngle + targetSegmentAngle.DeltaAngle);
                _targetAngle = (randomAngleWithinSegment - 90f) % 360f; // Adjusting to stop at 90 degrees from above.

                // Set the remaining rotation needed to complete the total spins and reach the target angle.
                _remainingRotation = 360f * _totalSpins + Mathf.DeltaAngle(_currentAngle, _targetAngle); // Incorporate how many spins we want and the remaining angle between the start and target angle

                _rotationSpeed = _initialRotationSpeed;
                _isSpinning = true;
            }
            else
            {
                Debug.LogError("Segment not found: " + _targetSegment);
            }
        }

        private void DetectSegment()
        {
            // Calculate the final angle adjusted for the top position (90 degrees offset)
            float finalAngle = (_currentAngle + 90f) % 360f;

            // Normalize the final angle to be within the range [0, 360]
            if (finalAngle < 0)
                finalAngle += 360f;

            // Find the segment that corresponds to the final angle
            foreach (var segment in _wheelSegments)
            {
                float segmentEndAngle = segment.Angles.StartAngle + segment.Angles.DeltaAngle;
                if (finalAngle >= segment.Angles.StartAngle && finalAngle < segmentEndAngle)
                {
                    Debug.Log($"Stopped on: {segment.Label}");
                    return;
                }
            }
        }
    }
}
