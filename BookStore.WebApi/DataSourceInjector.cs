using BookStore.WebApi.Models;
using System.Reflection;
using SimpleInjector;
using BookStore.Service.Abstraction;

namespace BookStore.WebApi
{
    public class DataSourceInjector
    {
        private readonly Container _container;

        public DataSourceInjector(Container container)
        {
            _container = container;
        }

        public void InjectServices(DataSourceType dataSource)
        {
            switch (dataSource)
            {
                case DataSourceType.GoogleBookApi:
                    InjectGoogleBookServices();
                    break;
                case DataSourceType.TestingApi:
                    InjectTestingThirdPartyServices();
                    break;
                default:
                    InjectGoogleBookServices();
                    break;
            }
        }

        private void InjectGoogleBookServices()
        {
            var assembly = Assembly.GetAssembly(typeof(BookStore.Service.GoogleBookApi.GoogleBookService));
            _container.Register(typeof(IBookService<>), assembly);
        }

        private void InjectTestingThirdPartyServices()
        {
            var assembly = Assembly.GetAssembly(typeof(BookStore.Service.TestingThirdParty.SampleBookService));
            _container.Register(typeof(IBookService<>), assembly);
        }
    }
}