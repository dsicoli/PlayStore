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
    public class ProjectingOverModel<T>
    {
        private User _user;
        private GenericRepository<User> _gRUser;
        private GenerateEntitiesFromDTO _generateEntitiesFromDTO;
        private DBDuplicates _dBDuplicates;
        public ProjectingOverModel( DBDuplicates dBDuplicates, GenerateEntitiesFromDTO generateEntitiesFromDTO, GenericRepository<User> gRUser)
        {
            _generateEntitiesFromDTO = generateEntitiesFromDTO;
            _dBDuplicates = dBDuplicates;
            _gRUser = gRUser;
        }

        public void UploadDTOProjectedOverUser(UploadDTO uploadDTO)
        {
            _generateEntitiesFromDTO.CopyUploadDTOOnUser(_user,uploadDTO);
            
            if(!_dBDuplicates.UserWithSameEmail(_user))
            {
                _gRUser.AddEntity(_user);
                _gRUser.SaveRepository();
            }
        }
    }
}    