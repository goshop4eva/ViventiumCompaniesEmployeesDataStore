using Microsoft.AspNetCore.Mvc;
using ViventiumAPI.Models;

namespace ViventiumAPI.Services
{
    public interface IDataStoreService
    {
        public bool PostDataStore(string filePath);

        //public bool Validate();
    }
}
