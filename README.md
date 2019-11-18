# AugenBookStore

# Design / Patterns:
- In BookStore.Delivery, the Factory method design pattern is implemented to get the delivery information for the following services: Motobike, Train, Aircraft. Reason for using this pattern for delivery is it would help to expand more services futhermore without breaking/changing the current system ("O" in SOLID).

- In BookStore.WebApi, the SimpleInjector IOC framework is implemented to let web api can switch to various data sources: google book api, third party api,... Currenly this project is using goolge book api as default per requirement. BookStore.WebApi project contains DataSourceInjector is responsible for switch data sources and inject services base on app setting key: "AppSettings.DataSource". The reason for using SimpleInjector IoC framework is it allow to scan by assembly and get all implementations of abstraction services instead of using built-in .NET Core IoC. More clearly, I created BookStore.Service.Abstraction project like an abstraction common layer to let Web Api interact with. BookStore.Service.GoogleBookApi, BookStore.Service.TestingThirdParty project are implementing the base service interfaces, currently is one: IBookService and WebAPI just only interact with BookStore.Service.Abstraction. The benefit for this design is when the system need to add more data source service, just easily expand by creating new project, switching in DataSourceInjector of BookStore.WebApi project.

# Summary breakdown:
- Prepare and design the Factory method for BookStore.Delivery (3 hours)
  + Prepare per requirement what need to implement (1 hours)
  + Design Factory method for delivery service (1.5 hours)
  + Testing (0.5 hour)
- Prepare data source switching (4.5 hours)
  + Research another IoC support for .NET Core (SimpleInjector) (2 hours)
  + Implment SimpleInjector and test (0.5 hour)
  + Implement abstraction (2 hours)
  
# Note:
- Google book api is authenticated by client_secret.json in BookStore.WebApi project with credential: test04718@gmail.com / vbjxzc123 (my test account). Or we can use another gmail account for testing.
