using UnityEngine;

namespace MegaManBattleNetwork
{
    public class WaitForSecondsOrChipSelection : CustomYieldInstruction
    {
        private float _startTime;
        private float _totalTime;
        private float _initialTotalTime;

        public WaitForSecondsOrChipSelection(float seconds)
        {
            _startTime = Time.time;
            _totalTime = seconds;
            _initialTotalTime = _totalTime;
        }

        public override bool keepWaiting
        {
            get
            {
                return IsTimeUp();
            }
        }

        private bool IsTimeUp()
        {
            if (GameManager.Instance.IsSelectingChips)
            {
                _totalTime = Time.time - _startTime + _initialTotalTime;
            }

            return Time.time - _startTime < _totalTime;
        }
    }
}
