namespace Services
{
    public interface IAudioService
    {
        void Initialize();
        void OnMusicChange(bool music);
        void PlaySound(AudioTypes audioType);
    }
}