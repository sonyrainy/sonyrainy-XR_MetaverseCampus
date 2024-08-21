using UnityEngine;

namespace SafeSystem
{
    [System.Serializable]
    public class SafeAudioClips
    {
        [Header("Sounds")]
        public Sound interactSound;
        public Sound boltUnlockSound;
        public Sound handleSpinSound;
        public Sound doorOpenSound;
        public Sound lockRattleSound;
        public Sound safeClickSound;

        public void PlaySafeClickSound()
        {
            SafeAudioManager.instance.Play(safeClickSound);
        }

        public void PlayRattleSound()
        {
            SafeAudioManager.instance.Play(lockRattleSound);
        }

        public void PlayDoorOpenSound()
        {
            SafeAudioManager.instance.Play(doorOpenSound);
        }


        public void PlayHandleSpinSound()
        {
            SafeAudioManager.instance.Play(handleSpinSound);
        }

        public void PlayBoltUnlockSound()
        {
            SafeAudioManager.instance.Play(boltUnlockSound);
        }

        public void PlayInteractSound()
        {
            SafeAudioManager.instance.Play(interactSound);
        }
    }
}
