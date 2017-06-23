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
using PlayStore.Service.AppTools;

namespace PlayStore.Service.AppTools
{
    public class GenerateEntitiesFromDTO
    {
        private readonly User _user;
        private readonly App _app;
        public GenerateEntitiesFromDTO(User user, App app)
        {
            _user = user;
            _app = app;
        }

        public void CopyUploadDTOOnUser(User user, UploadDTO uploadDTO)
        {
              user = new User(){
                  Name = uploadDTO.DeveloperName,
                  Surname = uploadDTO.DeveloperSurname,
                  Email = uploadDTO.Email
              };
        }

        public void CopyUploadDTOOnApp(App app, UploadDTO uploadDTO)
        {
              app = new App(){
                            Name = uploadDTO.AppName,
                            Genre = uploadDTO.Genre,
                            LastUpdate = uploadDTO.LastUpdate,
                            AppBrand = uploadDTO.AppBrand                    
                        }; 
        }

        public void CopyUploadDTOOnCompatibility(Compatibility compatibility, UploadDTO uploadDTO)
        {
              compatibility = new Compatibility(){
                                    AppId = _app.Id,
                                    DeviceType = uploadDTO.DeviceType
                                };
        }

        public void CopyUploadDTOOnPrice(Price price, UploadDTO uploadDTO)
        {
            price = new Price(){
                            AppId = _app.Id, 
                            Currency = uploadDTO.Currency,
                            Value = uploadDTO.Value
                        };
        }    

        public void CopyUploadDTOOnUserApp(UserApp userApp, UploadDTO uploadDTO)
        {
            userApp = new UserApp(){
                                UsersId = _user.Id,
                                AppsId = _app.Id
                            };
        }   

        public void CopyUploadDTOOnUploads(Upload upload, UploadDTO uploadDTO)
        {
            upload =  new Upload(){
                UsersId = _user.Id,
                UserAppId = _app.Id,
                Accepted = uploadDTO.Accepted,
                Update = uploadDTO.Update
            };
        }     
                      
    }
}