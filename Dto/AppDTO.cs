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

namespace PlayStore.DTO
{

    public class AppDTO
            {
                public string Name { get; set; }

                public string Genre { get; set; }
                
                public string LastUpdate { get; set; }

                public string AppBrand { get; set; }

                public string developerName {get;set;}

                public string developerEmail {get;set;}

                public string developerAdditionalInfos {get;set;}

                public List<User> developersSet = new List<User>();

                private List<App> _apps = new List<App>();

                public AppDTO()
                {
                    
                }

                public AppDTO(PlayStoreDBContext appStoreDBContext)
                {
                    _apps=appStoreDBContext.Apps.ToList<App>();
                }
                
                // public AppDTO FindAppDTO(Apps app)
                // {
                //     return new AppDTO()
                //     {
                //         Name = _apps.Where(x => x.Id==app.Id).FirstOrDefault().Name,
                //         Genre = _apps.Where(x => x.Id==app.Id).FirstOrDefault().Genre,
                //         LastUpdate = _apps.Where(x => x.Id==app.Id).FirstOrDefault().LastUpdate,
                //         AppBrand = _apps.Where(x => x.Id==app.Id).FirstOrDefault().AppBrand

                //     };
                // }
    
            }
}
