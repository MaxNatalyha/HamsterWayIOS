using Services;
using Services.Ads;
using Shop;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class ServiceInstaller : MonoInstaller
    {
        [Header("Configs")]
        public AdsConfig adsConfig;
        public IAPConfig iapConfig;
        public SkinsConfig skinsConfig;
        public AudioConfig audioConfig;
        public RewardConfig rewardConfig;
        public LocalizationData localizationData;
        
        [Space(10)]
        public ShopPage shopPage;
        public SceneParticles sceneParticles;
        public SceneAudioSources sceneAudioSources;
        
        public override void InstallBindings()
        {
            BindAudioService();
            BindParticleService();
            BindPageNavigationService();
            BindLocalizationService();
            BindIAPService();
            BindRewardService();
            BindSkinsService();
            BindPlayerProgressService();
            BindAdsService();
        }
        
        private void BindSkinsService()
        {
            SkinsService skinsService = new SkinsService(skinsConfig);
            
            Container
                .Bind<ISkinsService>()
                .FromInstance(skinsService)
                .AsSingle();
        }

        private void BindRewardService()
        {
            RewardService rewardService = new RewardService(rewardConfig);
            
            Container
                .Bind<IRewardService>()
                .FromInstance(rewardService)
                .AsSingle();
            
            Container.QueueForInject(rewardService);
        }

        private void BindIAPService()
        {
            IAPService iapService = new IAPService(iapConfig, shopPage.financeSection);
            
            Container
                .Bind<IIAPService>()
                .FromInstance(iapService)
                .AsSingle();
            
            Container.QueueForInject(iapService);
        }

        private void BindAudioService()
        {
            AudioService audioService = new AudioService(audioConfig, sceneAudioSources);
            
            Container
                .Bind<IAudioService>()
                .FromInstance(audioService)
                .AsSingle();
        }

        private void BindParticleService()
        {
            ParticleService particleService = new ParticleService(sceneParticles);

            Container
                .Bind<IParticleService>()
                .FromInstance(particleService)
                .AsSingle();
        }

        private void BindPageNavigationService()
        {
            PageNavigationService navigationService = new PageNavigationService();
            
            Container
                .Bind<IPageNavigationService>()
                .FromInstance(navigationService)
                .AsSingle();
            
            Container.QueueForInject(navigationService);
        }
        
        
        private void BindLocalizationService()
        {
            LocalizationService localizationService = new LocalizationService(localizationData);
            
            Container
                .Bind<ILocalizationService>()
                .FromInstance(localizationService)
                .AsSingle();
        }

        private void BindPlayerProgressService()
        {
            PlayerProgressService playerProgressService = new PlayerProgressService();

            Container
                .Bind<IPlayerProgressService>()
                .FromInstance(playerProgressService)
                .AsSingle();
        }
        
        private void BindAdsService()
        {
            AdsService adsService = new AdsService(adsConfig);
            
            Container
                .Bind<IAdsService>()
                .FromInstance(adsService)
                .AsSingle();
        }
    }
}