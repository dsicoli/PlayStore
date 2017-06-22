using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using System.Linq.Expressions;
using System.Data;
using PlayStore.Model;
using PlayStore.GenericRepository;
using PlayStore.DTO;
using PlayStore.Service.AppServiceTools;


namespace PlayStore.Service
{
    public class AppService
    {
        //Properties
        
        private App app;
        private List<User> users = new List<User>();
        private List<UserApp> userApp = new List<UserApp>();
        private AppDTO appDTO;
        private List<App> _apps = new List<App>();
        private PlayStoreDBContext _playStoreDBContext;
        private GenericRepository<User> _userRepository; 
        private GenericRepository<UserApp> _userAppRepository;
        private GenericRepository<Compatibilities> _compatibilityRepository;
        private GenericRepository<Prices> _priceRepository;
        private GenericRepository<Uploads> _uploadRepository;
        private GenericRepository<Downloads> _downloadRepository;
        private GenericRepository<App> _appRepository;
        
        //Constructor
        public AppService(PlayStoreDBContext playStoreDBContext)
        {
            _apps = playStoreDBContext.Apps.ToList<App>();
            _playStoreDBContext = playStoreDBContext;  
            _appRepository = new GenericRepository<App>(playStoreDBContext);
            _downloadRepository = new GenericRepository<Downloads>(playStoreDBContext);
            _uploadRepository = new GenericRepository<Uploads>(playStoreDBContext);
            _priceRepository = new GenericRepository<Prices>(playStoreDBContext);
            _compatibilityRepository = new GenericRepository<Compatibilities>(playStoreDBContext);
            _userAppRepository = new GenericRepository<UserApp>(playStoreDBContext);
            _userRepository = new GenericRepository<User>(playStoreDBContext);
        }    

        
        public void AddUpload(UploadDTO uploadDTO)
        {   
            //check sul valore nullo ArgumentNullException
            
            
            

            //***************************************************
            
            var tempUser = new User(){
                            Name = uploadDTO.DeveloperName,
                            Surname = uploadDTO.DeveloperSurname,
                            Email = uploadDTO.Email
                        };

            User checkUser = _playStoreDBContext.Users.Where( x => x.Email == tempUser.Email ).FirstOrDefault();

            if(checkUser == null)
            {
                //Add to Users
                User tempTempUser = _userRepository.AddEntityReturned(tempUser);
                
                //tempUser = tempTempUser;
                
                _playStoreDBContext.SaveChanges();
                  
            }
            
            tempUser = _playStoreDBContext.Users.Where( x => x.Email == tempUser.Email).FirstOrDefault();
            
            //*****************************************************
            var tempApp = new App(){
                            Name = uploadDTO.AppName,
                            Genre = uploadDTO.Genre,
                            LastUpdate = uploadDTO.LastUpdate,
                            AppBrand = uploadDTO.AppBrand                    
                        };  

            App checkApp = _playStoreDBContext.Apps.Where( x => x.LastUpdate == tempApp.LastUpdate).FirstOrDefault();

            if(checkApp == null)
            {
                //Add to Apps
                App tempTempApp = _appRepository.AddEntityReturned(tempApp);  

                //tempApp = tempTempApp;

                _playStoreDBContext.SaveChanges();
            }

            tempApp = _playStoreDBContext.Apps.Where( x => x.LastUpdate == tempApp.LastUpdate ).FirstOrDefault();
            
            //*****************************************************
            var tempCompatibility = new Compatibilities(){
                                    DeviceType = uploadDTO.DeviceType
                                };

            //Add to Compatibilities
            Compatibilities tempTempCompatibility = _compatibilityRepository.AddEntityReturned(tempCompatibility);

            tempCompatibility = tempTempCompatibility;

            _playStoreDBContext.SaveChanges();
            //*****************************************************

            var tempPrice = new Prices(){
                            Currency = uploadDTO.Currency,
                            Value = uploadDTO.Value
                        };

            //Add to Prices
            Prices tempTempPrice = _priceRepository.AddEntityReturned(tempPrice);

            tempPrice = tempTempPrice;

            _playStoreDBContext.SaveChanges();
            //*****************************************************

            var tempUserApp = new UserApp(){
                                UsersId = tempUser.Id,
                                AppsId = tempApp.Id
                            };

            UserApp checkUserApp = _playStoreDBContext.UserApp.Where( x => x.UsersId == tempUserApp.UsersId && x.AppsId == tempUserApp.AppsId).FirstOrDefault();

            if(checkUserApp == null)
            {
                //Add to UserApp
                UserApp tempTempUserApp = _userAppRepository.AddEntityReturned(tempUserApp);

                //tempUserApp=tempTempUserApp;

                _playStoreDBContext.SaveChanges();
            }

            tempUserApp = _playStoreDBContext.UserApp.Where(x => x.UsersId == tempUserApp.UsersId && x.AppsId == tempUserApp.AppsId).FirstOrDefault();
            
            //*****************************************************

            var tempUploads = new Uploads(){
                UsersId = tempUser.Id,
                UserAppId = tempUserApp.Id,
                Accepted = uploadDTO.Accepted,
                Update = uploadDTO.Update
            };
            
            //Add to Uploads
            Uploads tempTempUploads = _uploadRepository.AddEntityReturned(
                tempUploads
            );

            tempUploads = tempTempUploads;

            _playStoreDBContext.SaveChanges();
            //*****************************************************

        }


        //Adds a Downloads object
        public void AddDownload(DownloadDTO downloadDTO)
        {
            //*****************************************************

            var tempUser = new User(){
                            Name = downloadDTO.UserName,
                            Surname = downloadDTO.UserSurname,
                            Email = downloadDTO.Email
                        };
            
            //Add to Users
            User tempTempUser = _userRepository.AddEntityReturned(tempUser);

            tempUser = tempTempUser;
            //*****************************************************

            var tempApp = new App(){
                            Name = downloadDTO.AppName,
                            Genre = downloadDTO.Genre,
                            LastUpdate = downloadDTO.LastUpdate,
                            AppBrand = downloadDTO.AppBrand                 
                        };  

            //Add to Apps
            App tempTempApp = _appRepository.AddEntityReturned(tempApp);  

            //Updates the object of type Apps
            tempApp = tempTempApp;

            //*****************************************************

            var tempCompatibility = new Compatibilities(){
                                    DeviceType = downloadDTO.DeviceType
            };

            //Add to Compatibilities
            Compatibilities tempTempCompatibility = _compatibilityRepository.AddEntityReturned(tempCompatibility);

            tempCompatibility = tempTempCompatibility;

            //*****************************************************


            var tempPrice = new Prices(){
                    Currency = downloadDTO.Currency,
                    Value = downloadDTO.Value
            };

            //Add to Prices
            Prices tempTempPrice = _priceRepository.AddEntityReturned(tempPrice);

            tempPrice = tempTempPrice;

            //*****************************************************

            var tempUserApp = new UserApp(){
                                UsersId = tempUser.Id,
                                AppsId = tempApp.Id
                            };

            //Add to UserApp
            UserApp tempTempUserApp = _userAppRepository.AddEntityReturned(tempUserApp);

            tempUserApp=tempTempUserApp;

            //*****************************************************

            var tempDownloads = new Downloads(){
                Successful = downloadDTO.Successful
            };
            
            //Add to Uploads
            Downloads tempTempUploads = _downloadRepository.AddEntityReturned(tempDownloads);

            tempDownloads = tempTempUploads;

            //*****************************************************

        }

        public AppDTO FindApp(string appName)
                {
                    app = _appRepository
                    .GetByLambda( x => 
                    x.Name == appName).FirstOrDefault();

                    userApp = _userAppRepository
                    .GetByLambda( x => 
                    x.AppsId == app.Id ).ToList<UserApp>();

                    foreach(UserApp userAppRel in userApp)
                    {
                        users.Add( 
                            _userRepository
                            .GetByLambda( x => 
                            x.Id == userAppRel.UsersId ).FirstOrDefault() );
                    }


                    users = users.Distinct().ToList<User>();

                    appDTO =  new AppDTO()
                    {
                        Name = app.Name,
                        Genre = app.Genre,
                        LastUpdate = app.LastUpdate,
                        AppBrand = app.AppBrand                        
                    }; 

                    foreach(User user in users)
                    {
                        appDTO.developersSet.Add(user);
                    }

                    return appDTO;
                }  

                public void UpdateAppService(App app)
                {
                    _appRepository.Update(app);
                }

                public void SaveAppService()
                {
                    _playStoreDBContext.SaveChanges();
                }

    }
}