using System;
using PlayStore.GenericRepository;
using PlayStore.Model;
using PlayStore.DTO;
using PlayStore.Service;
using System.Collections.Generic;

namespace PlayStore
{
    class Program
    {
        static void Main(string[] args)
        {
            

            try
            {
                PlayStoreDBContext playStoreDBContext = new PlayStoreDBContext();
                
                GenericRepository<User> usersRepository = new GenericRepository<User>(playStoreDBContext);
                GenericRepository<UserApp> userAppUploadRepository = 
                new GenericRepository<UserApp>(playStoreDBContext);
                GenericRepository<App> appsRepository = 
                new GenericRepository<App>(playStoreDBContext);
                PlayStore.DTO.AppDTO appDTO = new PlayStore.DTO.AppDTO(); 
                AppService appService = new AppService(playStoreDBContext);

                // appsRepository.AddEntity(new Users() {
                    // Name="Daniele",
                    // Surname="Sicoli",
                    // City="Genova",
                    // Email = "test2.test2@gmail.com"
                // });

                // appsRepository.AddEntity(
                //     new Apps(){
                //         Name = "Fallout",
                //         Genre = "Science fiction",
                //         LastUpdate = "March",
                //         AppBrand = "MyBrand"
                //     }
                // );

                // userAppUploadRepository.AddEntity(
                //     new UserAppUpload(){

                // });

                // foreach(Users user in appStoreDBContext.Users)
                // {
                //     Console.WriteLine(user.Name+" "+user.City+" "+user.Email);
                // }

                // usersRepository.Update(new Users(){    Name="Daniele",
                //     Surname="Sicoli",
                //     City="Genova",
                //     Email = "test2.test2@gmail.com"});

                // foreach(UserAppUpload row in appStoreDBContext.UserAppUpload)
                // {
                //     Console.WriteLine(row.AppsId+" "+row.UsersId+" "+row.IsUpload);
                // }

                
                // appsRepository.DeleteSingleEntity(new Apps() { Id = 1 });
                // appsRepository.Delete(e => e.Id % 2==0);


                appService.AddUpload(new UploadDTO(){
                    DeveloperName = "Antonio",
                    DeveloperSurname = "LR",
                    Accepted = true,
                    DeviceType = "Android",
                    AppBrand = "Comitiva",
                    AppName = "Mordi e fuggi",
                    Currency = "$",
                    Value = "10",
                    Email = "anto.test@test.it",
                    LastUpdate = "20//06//2017"
                });
                playStoreDBContext.SaveChanges();

                List<App> returnedApps = new List<App>(appsRepository.GetAll());

                
                
                foreach(App app in returnedApps)
                    Console.WriteLine("Name: "+app.Name+" "+"Brand: "+app.AppBrand);

            }
            catch(Exception e)
            {
                Console.WriteLine("Message: "+e.Message);
                Console.WriteLine("Data; "+e.Data);
                Console.WriteLine("Source: "+e.Source);
                Console.WriteLine("Inner exception: "+e.InnerException);
            }

        }
        
    }
}
