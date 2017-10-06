using Application.TorreHanoi.Implementation;
using Application.TorreHanoi.Interface;
using Domain.TorreHanoi.Interface.Repository;
using Domain.TorreHanoi.Interface.Service;
using Domain.TorreHanoi.Service;
using Infrastructure.TorreHanoi.Cache;
using Infrastructure.TorreHanoi.ImagemHelper;
using Infrastructure.TorreHanoi.Log;
using Infrastructure.TorreHanoi.Queue;
using Infrastructure.TorreHanoi.ServiceAgent;
using SimpleInjector;
using System.Configuration;

namespace Infrastructure.TorreHanoi.Ioc
{
    public class Injector
    {
        public static Container Container { get; set; }

        public static void Register(Container container)
        {
            Container = container;

            container.Register<ITorreHanoiApplicationService, TorreHanoiApplicationService>(Lifestyle.Singleton);
            container.Register<ITorreHanoiDomainService, TorreHanoiDomainService>(Lifestyle.Singleton);
            container.Register<ILogger>(() => new Logger(container.GetInstance<ISlackServiceAgent>(), ConfigurationManager.AppSettings.Get("ConfiguracaoLog").Split(',')), Lifestyle.Singleton);
            container.Register<ISlackServiceAgent>(() => new SlackServiceAgent(ConfigurationManager.AppSettings.Get("SlackAdress"), ConfigurationManager.AppSettings.Get("SlackRoute")), Lifestyle.Singleton);
            container.Register<IQueueRepository, QueueRepository>(Lifestyle.Singleton);
            container.Register<IDesignerService, DesignerService>(Lifestyle.Singleton);
            container.Register<ICacheManager, CacheManager>(Lifestyle.Singleton);
        }
    }
}
